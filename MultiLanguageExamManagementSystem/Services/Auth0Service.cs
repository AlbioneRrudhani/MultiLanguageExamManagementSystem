using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MultiLanguageExamManagementSystem.Data.UnitOfWork;
using MultiLanguageExamManagementSystem.Helpers.Models;
using MultiLanguageExamManagementSystem.Models.Entities;
using MultiLanguageExamManagementSystem.Services.IServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace MultiLanguageExamManagementSystem.Services
{
    public class Auth0Service : IAuth0Service
    {
        private readonly HttpClient _httpClient;
        private readonly Auth0Settings _auth0Settings;
        private readonly IUnitOfWork _unitOfWork;

        public Auth0Service(HttpClient httpClient, IOptions<Auth0Settings> auth0Settings, IUnitOfWork unitOfWork)
        {
            _httpClient = httpClient;
            _auth0Settings = auth0Settings.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> SignupUserAsync(Auth0SignupRequest request)
        {
            var signupRequest = new
            {
                client_id = _auth0Settings.ClientId,
                email = request.Email,
                password = request.Password,
                connection = _auth0Settings.Connection,
                username = request.Username,
            };

            var response = await _httpClient.PostAsJsonAsync(_auth0Settings.SignupEndpoint, signupRequest);
            return await HandleResponseAsync(response, "Failed to signup user");
        }



        public async Task<string> GetTokenAsync(Auth0TokenRequest request)
        {
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, $"{_auth0Settings.Domain}/oauth/token")
            {
                Content = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", request.Email), // use email as username
            new KeyValuePair<string, string>("password", request.Password),
            new KeyValuePair<string, string>("audience", _auth0Settings.Audience),
            new KeyValuePair<string, string>("client_id", _auth0Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", _auth0Settings.ClientSecret)
        })
            };
            tokenRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(tokenRequest);
            var tokenResponse = await HandleResponseAsync(response, "Failed to get token");

            // Extract auth0id from the token response
            var jwtToken = JsonConvert.DeserializeObject<JObject>(tokenResponse)["access_token"].ToString();
            var userEmail = request.Email;

            await SaveUserEmailAndAuth0IdAsync(userEmail, jwtToken);

            return tokenResponse;
        }



        private async Task SaveUserEmailAndAuth0IdAsync(string email, string jwtToken)
        {
            var auth0Id = ExtractAuth0IdFromToken(jwtToken);

            var user = await _unitOfWork.Repository<User>()
                .GetByCondition(u => u.Email == email)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                user = new User { Email = email };
                _unitOfWork.Repository<User>().Create(user);
            }

            user.Auth0Id = auth0Id;
            _unitOfWork.Complete(); 
        }



        private string ExtractAuth0IdFromToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var auth0IdClaim = token.Claims.FirstOrDefault(claim => claim.Type == "sub");

            if (auth0IdClaim != null)
            {
                return auth0IdClaim.Value;
            }
            else
            {
                throw new InvalidOperationException("No 'sub' claim found in the JWT token.");
            }
        }

        private static async Task<string> HandleResponseAsync(HttpResponseMessage response, string errorMessage)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"{errorMessage}. Status code: {response.StatusCode}, Error: {errorContent}");
            }
        }
    }
}