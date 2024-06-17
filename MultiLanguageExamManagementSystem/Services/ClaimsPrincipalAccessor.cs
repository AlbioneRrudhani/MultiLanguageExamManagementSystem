using MultiLanguageExamManagementSystem.Services.IServices;
using System.Security.Claims;

namespace MultiLanguageExamManagementSystem.Services
{
    public class ClaimsPrincipalAccessor : IClaimsPrincipalAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal ClaimsPrincipal => _httpContextAccessor.HttpContext?.User;
    }
}
