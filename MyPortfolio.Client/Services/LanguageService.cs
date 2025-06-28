using System.Text.Json;

namespace MyPortfolio.Client.Services
{
    public class LanguageService
    {
        private readonly HttpClient _http;
        public event Action? OnLanguageChanged;

        public Dictionary<string, JsonElement>? Strings { get; private set; }

        public string CurrentLanguage { get; private set; } = "en";
        public bool IsReady { get; private set; } = false;

        public LanguageService(HttpClient http)
        {
            _http = http;
        }

        public async Task LoadAsync(string cultureCode)
        {
            var fallbackCulture = "en";
            var langCode = cultureCode.Split('-')[0].ToLower();

            async Task<Dictionary<string, JsonElement>?> LoadFromFileAsync(string code)
            {
                try
                {
                    var stream = await _http.GetStreamAsync($"/i18n/{code}.json");
                    return await JsonSerializer.DeserializeAsync<Dictionary<string, JsonElement>>(stream);
                }
                catch
                {
                    return null;
                }
            }

            Strings = await LoadFromFileAsync(langCode) ?? await LoadFromFileAsync(fallbackCulture);
            CurrentLanguage = Strings == null ? fallbackCulture : langCode;
            IsReady = Strings != null;
            OnLanguageChanged?.Invoke();
        }

        public string T(string key)
        {
            if (Strings != null && Strings.TryGetValue(key, out var value))
            {
                return value.GetString() ?? $"[{key}]";
            }

            return $"[{key}]";
        }

        public string T(string section, string key)
        {
            if (Strings != null &&
                Strings.TryGetValue(section, out var sectionElement) &&
                sectionElement.ValueKind == JsonValueKind.Object &&
                sectionElement.TryGetProperty(key, out var value))
            {
                return value.GetString() ?? $"[{section}.{key}]";
            }

            return $"[{section}.{key}]";
        }
    }
}
