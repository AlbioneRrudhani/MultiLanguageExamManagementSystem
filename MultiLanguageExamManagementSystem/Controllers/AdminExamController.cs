using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiLanguageExamManagementSystem.Models.Dtos.Exam;
using MultiLanguageExamManagementSystem.Models.Dtos.Question;
using MultiLanguageExamManagementSystem.Services.IServices;

namespace MultiLanguageExamManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Professor")]
    public class AdminExamController : ControllerBase
    {
        private readonly IAdminExamService _adminExamService;

        public AdminExamController(IAdminExamService adminExamService)
        {
            _adminExamService = adminExamService;
        }


        [HttpPost("exam")]
        public async Task<IActionResult> CreateExamAsync([FromBody] ExamCreateDto examToCreate)
        {
            try
            {
                await _adminExamService.CreateExamAsync(examToCreate);
                return Ok(new { message = "Exam created successfully." });
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


        [HttpPost("questions")]
        public async Task<IActionResult> CreateQuestionsAsync([FromBody] List<QuestionCreateDto> questionsToCreate)
        {
            try
            {
                await _adminExamService.CreateQuestionsAsync(questionsToCreate);
                return Ok(new { message = "Questions created successfully." });
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


        [HttpPost("associate-questions")]
        public async Task<IActionResult> AssociateQuestionsWithExamAsync(int examId, [FromBody] List<int> questionIds)
        {
            await _adminExamService.AssociateQuestionsWithExamAsync(examId, questionIds);
            return Ok(new { message = "Questions associated with exam successfully." });
        }


        [HttpPost("approve-request")]
        public async Task<IActionResult> ApproveExamRequestAsync(int examRequestId)
        {
            try
            {
                await _adminExamService.ApproveExamRequestAsync(examRequestId);
                return Ok(new { message = "The exam request has been approved." });
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
    }
}