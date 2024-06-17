using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiLanguageExamManagementSystem.Models.Dtos.ExamSubmission;
using MultiLanguageExamManagementSystem.Services.IServices;

namespace MultiLanguageExamManagementSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamsController(IExamService examService)
        {
            _examService = examService;
        }


        [HttpGet("exams")]
        [Authorize]
        public async Task<IActionResult> GetExamsCreatedByProfessorsAsync()
        {
            try
            {
                var exams = await _examService.GetExams();
                return Ok(exams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpPost("request")]
        [Authorize]
        public async Task<IActionResult> RequestToTakeExamAsync(int examId)
        {
            try
            {
                await _examService.RequestToTakeExamAsync(examId);
                return Ok(new { message = "Your request is sent. You need to wait until it is approved by the professor" });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("approved-exams")]
        [Authorize]
        public async Task<IActionResult> GetApprovedExamsForUserAsync()
        {
            try
            {
                var exams = await _examService.GetApprovedExamsForUserAsync();
                return Ok(exams);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("exam-with-questions")]
        [Authorize]
        public async Task<IActionResult> GetExamQuestionsAsync(int examId)
        {
            try
            {
                var questions = await _examService.GetExamQuestionsAsync(examId);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("submit-exam")]
        [Authorize]
        public async Task<ActionResult<int>> SubmitExam(SubmitExamDto submitExamDto)
        {
            var score = await _examService.SubmitExamAsync(submitExamDto);
            return Ok(score);
        }
    }
}
