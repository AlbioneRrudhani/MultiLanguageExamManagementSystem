using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiLanguageExamManagementSystem.Data.UnitOfWork;
using MultiLanguageExamManagementSystem.Models.Dtos.Exam;
using MultiLanguageExamManagementSystem.Models.Dtos.ExamSubmission;
using MultiLanguageExamManagementSystem.Models.Dtos.Question;
using MultiLanguageExamManagementSystem.Models.Entities;
using MultiLanguageExamManagementSystem.Models.Enum;
using MultiLanguageExamManagementSystem.Services.IServices;
using System.Security.Claims;

namespace MultiLanguageExamManagementSystem.Services
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IEmailService _emailService;

        public ExamService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsPrincipalAccessor claimsPrincipalAccessor, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _emailService = emailService;
        }



     
        public async Task<IEnumerable<ExamDto>> GetExams()
        {
            var examsToReturn = await _unitOfWork.Repository<Exam>().GetAll().Where(e => !string.IsNullOrEmpty(e.ProfessorId)).ToListAsync();

            var examDtos = _mapper.Map<IEnumerable<ExamDto>>(examsToReturn);
            return examDtos;
        }


        public async Task RequestToTakeExamAsync(int examId)
        {
            var userId = _claimsPrincipalAccessor.ClaimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var existingRequests = _unitOfWork.Repository<ExamRequest>()
                .GetByCondition(er => er.ExamId == examId && er.UserId == userId)
                .Count();

            if (existingRequests >= 3)
            {
                throw new Exception("No more attempts left for this exam.");
            }

            var examRequest = new ExamRequest
            {
                ExamId = examId,
                UserId = userId,
                RequestTime = DateTime.Now,
                Status = RequestStatus.Pending,

                AttemptCount = existingRequests + 1
            };

            _unitOfWork.Repository<ExamRequest>().Create(examRequest);
            _unitOfWork.Complete();
        }


        public async Task<List<ExamDto>> GetApprovedExamsForUserAsync()
        {
            var userId = _claimsPrincipalAccessor.ClaimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var approvedExams = await _unitOfWork.Repository<ExamRequest>()
                .GetByCondition(er => er.UserId == userId && er.Status == RequestStatus.Approved)
                .Include(er => er.Exam)
                .Select(er => er.Exam)
                .ToListAsync();

            var examDtos = _mapper.Map<List<ExamDto>>(approvedExams);
            return examDtos;
        }


        public async Task<List<QuestionDto>> GetExamQuestionsAsync(int examId)
        {
            var userId = _claimsPrincipalAccessor.ClaimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var isApproved = await _unitOfWork.Repository<ExamRequest>()
                .GetByCondition(er => er.ExamId == examId && er.UserId == userId && er.Status == RequestStatus.Approved)
                .AnyAsync();

            if (!isApproved)
            {
                throw new UnauthorizedAccessException("You do not have access to this exam.");
            }

            var questions = await _unitOfWork.Repository<Exam_Question>()
                .GetByCondition(eq => eq.ExamId == examId)
                .Include(eq => eq.Question)
                .Select(eq => eq.Question)
                .ToListAsync();

            var questionDtos = _mapper.Map<List<QuestionDto>>(questions);
            return questionDtos;
        }



        public async Task<string> SubmitExamAsync(SubmitExamDto submitExamDto)
        {
            var userId = _claimsPrincipalAccessor.ClaimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User ID not found in claims.");
            }

            var user = await _unitOfWork.Repository<User>()
                .GetByCondition(u => u.Auth0Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var userEmail = user.Email; 

            var examRequestId = await _unitOfWork.Repository<ExamRequest>()
                .GetByCondition(er => er.ExamId == submitExamDto.ExamId && er.UserId == userId && er.Status == RequestStatus.Approved)
                .Select(er => er.ExamRequestId)
                .FirstOrDefaultAsync();

            if (examRequestId == 0)
            {
                throw new UnauthorizedAccessException("You do not have access to submit this exam.");
            }

            var hasTakenExam = await _unitOfWork.Repository<TakenExam>()
                .GetByCondition(te => te.ExamId == submitExamDto.ExamId && te.UserId == userId)
                .AnyAsync();

            if (hasTakenExam)
            {
                throw new InvalidOperationException("You have already taken this exam.");
            }

            // Calculate exam score and collect answers
            var (score, totalQuestions, correctAnswers) = CalculateExamResults(submitExamDto);

            // Record TakenExam
            await RecordTakenExam(submitExamDto.ExamId, userId);

            // Send email
            await _emailService.SendExamResultsEmailAsync(userEmail, score, correctAnswers, totalQuestions);

            return "Your answers will be sent to your email.";
        }


        private (int, int, int) CalculateExamResults(SubmitExamDto submitExamDto)
        {
            var questions = _unitOfWork.Repository<Exam_Question>()
                .GetByCondition(eq => eq.ExamId == submitExamDto.ExamId)
                .Include(eq => eq.Question)
                .ToList();

            int correctAnswers = 0;

            foreach (var question in questions)
            {
                if (submitExamDto.Answers.TryGetValue(question.QuestionId, out var givenAnswer))
                {
                    if (question.Question.CorrectAnswer == givenAnswer)
                    {
                        correctAnswers++;
                    }
                }
            }

            var totalQuestions = questions.Count;
            var score = (correctAnswers * 100) / totalQuestions;

            return (score, totalQuestions, correctAnswers);
        }


        private async Task RecordTakenExam(int examId, string userId)
        {
            var takenExam = new TakenExam
            {
                ExamId = examId,
                UserId = userId,
                IsCompleted = true
            };

            _unitOfWork.Repository<TakenExam>().Create(takenExam);
             _unitOfWork.Complete(); 
        }

    }
}
