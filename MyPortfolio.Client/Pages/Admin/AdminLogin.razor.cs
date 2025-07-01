using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Shared.DTOs.Login;

namespace MyPortfolio.Client.Pages.Admin
{
    public partial class AdminLogin
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;
        [Inject] public IHttpClientFactory HttpClientFactory { get; set; } = default!;

        private LoginDto loginModel = new();
        private string? errorMessage;
        private bool isLoading = false;
        private string passwordType = "password";

        private async Task HandleLogin()
        {
            isLoading = true;
            errorMessage = null;

            try
            {
                var client = HttpClientFactory.CreateClient("AuthorizedClient");
                var response = await client.PostAsJsonAsync("api/auth/login", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                    if (result is not null)
                    {
                        await JS.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
                        Nav.NavigateTo("/admin/dashboard");
                    }
                }
                else
                {
                    errorMessage = "Invalid username or password";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error: {ex.Message}";
            }

            isLoading = false;
        }
    }
}
