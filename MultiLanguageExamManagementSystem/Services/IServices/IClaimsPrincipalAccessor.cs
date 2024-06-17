using System.Security.Claims;

namespace MultiLanguageExamManagementSystem.Services.IServices
{
    public interface IClaimsPrincipalAccessor
    {
        ClaimsPrincipal ClaimsPrincipal { get; }
    }
}
