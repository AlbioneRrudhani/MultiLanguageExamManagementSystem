using MultiLanguageExamManagementSystem.Helpers.Models;

namespace MultiLanguageExamManagementSystem.Services.IServices
{
    public interface IAuth0Service
    {
       // Task<string> SignupUserAsync( string email, string password, string username);
        //Task<string> GetTokenAsync(string username, string password);

        Task<string> SignupUserAsync(Auth0SignupRequest signupRequest);
        Task<string> GetTokenAsync(Auth0TokenRequest tokenRequest);

    }
}
