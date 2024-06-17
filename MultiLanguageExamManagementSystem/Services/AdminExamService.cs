using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiLanguageExamManagementSystem.Data.UnitOfWork;
using MultiLanguageExamManagementSystem.Models.Dtos.Exam;
using MultiLanguageExamManagementSystem.Models.Dtos.Question;
using MultiLanguageExamManagementSystem.Models.Entities;
using MultiLanguageExamManagementSystem.Models.Enum;
using MultiLanguageExamManagementSystem.Services.IServices;
using System.Security.Claims;

namespace MultiLanguageExamManagementSystem.Services
{
    public class AdminExamService : IAdminExamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

        public AdminExamService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsPrincipalAccessor claimsPrincipalAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
        }


        #region exam
        public async Task CreateExamAsync(ExamCreateDto examToCreate)
        {
            var professorId = _claimsPrincipalAccessor.ClaimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var exam = _mapper.Map<Exam>(examToCreate);
            exam.ProfessorId = professorId;

            _unitOfWork.Repository<Exam>().Create(exam);
            _unitOfWork.Complete();
        }


        #endregion


        #region questions
        public async Task CreateQuestionsAsync(List<QuestionCreateDto> questionsToCreate)
        {
            var professorId = _claimsPrincipalAccessor.ClaimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(professorId))
            {
                throw new UnauthorizedAccessException("Professor ID not found in claims");
            }

            var questions = _mapper.Map<List<Question>>(questionsToCreate);

            foreach (var question in questions)
            {
                question.ProfessorId = professorId;
            }

            _unitOfWork.Repository<Question>().CreateRange(questions);
            _unitOfWork.Complete();
        }
        #endregion


        public async Task AssociateQuestionsWithExamAsync(int examId, List<int> questionIds)
        {
            var exam = await _unitOfWork.Repository<Exam>().GetByCondition(e => e.ExamId == examId).FirstOrDefaultAsync();
            if (exam == null)
            {
                throw new Exception($"Exam with ID {examId} not found.");
            }

            exam.Exam_Questions ??= new List<Exam_Question>();

            var questions = _unitOfWork.Repository<Question>().GetByCondition(q => questionIds.Contains(q.QuestionId)).ToList();

            if (questions.Count != questionIds.Count)
            {
                throw new Exception("One or more questions were not found.");
            }

            foreach (var question in questions)
            {
                exam.Exam_Questions.Add(new Exam_Question { ExamId = examId, QuestionId = question.QuestionId });
            }
            _unitOfWork.Complete();
        }

    }
}
