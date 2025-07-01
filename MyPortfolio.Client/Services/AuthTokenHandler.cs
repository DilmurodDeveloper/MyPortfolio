using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace MyPortfolio.Client.Services
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly IJSRuntime jsRuntime;

        public AuthTokenHandler(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
