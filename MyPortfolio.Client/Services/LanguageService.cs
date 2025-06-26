using System.Text.Json;

namespace MyPortfolio.Client.Services
{
    public class LanguageService
    {
        private readonly HttpClient _http;
        public event Action? OnLanguageChanged;

        public Dictionary<string, object>? Strings { get; private set; }

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

            try
            {
                var stream = await _http.GetStreamAsync($"/i18n/{langCode}.json");
                Strings = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(stream);
                CurrentLanguage = langCode;
            }
            catch
            {
                try
                {
                    var fallback = await _http.GetStreamAsync($"/i18n/{fallbackCulture}.json");
                    Strings = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(fallback);
                    CurrentLanguage = fallbackCulture;
                }
                catch
                {
                    Strings = null;
                    CurrentLanguage = fallbackCulture;
                }
            }

            IsReady = Strings != null;
            OnLanguageChanged?.Invoke();

        }

        public string T(string key)
        {
            if (Strings == null || !Strings.TryGetValue(key, out var value))
                return $"[{key}]";

            return value?.ToString() ?? $"[{key}]";
        }

        public string T(string section, string key)
        {
            if (Strings == null || !Strings.TryGetValue(section, out var sectionObj))
                return $"[{section}.{key}]";

            if (sectionObj is JsonElement sectionJson &&
                sectionJson.ValueKind == JsonValueKind.Object &&
                sectionJson.TryGetProperty(key, out var val))
            {
                return val.GetString() ?? $"[{section}.{key}]";
            }

            return $"[{section}.{key}]";
        }
    }
}
