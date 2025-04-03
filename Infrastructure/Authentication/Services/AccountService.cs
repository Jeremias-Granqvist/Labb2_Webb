using Labb2_Infrastructure.Authentication.DTOs;
using System.Net.Http.Json;
using static Labb2_Infrastructure.Authentication.Responses.CustomResponses;
using Labb2_Infrastructure.Authentication.States;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Labb2_Infrastructure.Authentication.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("Api");

        }
        public async Task<LoginResponse> LoginAsync(LoginDTO model)
        {

            var response = await _httpClient.PostAsJsonAsync("api/account/login", model);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response from login API: {responseContent}");

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!;

        }

        public async Task<LoginResponse> RefreshToken(UserSession userSession)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/refresh-token", userSession);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
      
            var response = await _httpClient.PostAsJsonAsync("api/account/register", model);
            var result = await response.Content.ReadFromJsonAsync<RegistrationResponse>();
            return result!;
        }

        public static bool CheckIfUnauthorized(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return true;
            else return false;
        }

        public void GetProtectedClient()
        {
            if (Constants.JWTToken == "") return;
            
            _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);
        }

        public async Task GetRefreshToken()
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/refresh-token", new UserSession() { JWTToken = Constants.JWTToken });
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            Constants.JWTToken = result!.JWTToken;
        }
    }
}
