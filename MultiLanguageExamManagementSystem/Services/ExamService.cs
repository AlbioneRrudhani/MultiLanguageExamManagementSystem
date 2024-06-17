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

        public ExamService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
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



      

    }
}
