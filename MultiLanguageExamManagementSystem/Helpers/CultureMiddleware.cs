using System.Globalization;

namespace MultiLanguageExamManagementSystem.Helpers
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string acceptLanguageHeader = context.Request.Headers["Accept-Language"];
            string primaryLanguageCode = GetPrimaryLanguageCode(acceptLanguageHeader);

            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo(primaryLanguageCode);
            await _next(context);
        }

        private string GetPrimaryLanguageCode(string acceptLanguageHeader)
        {
            if (string.IsNullOrWhiteSpace(acceptLanguageHeader))
            {
                return "en-US"; 
            }

            string[] languagePreferences = acceptLanguageHeader.Split(',');
            string primaryLanguage = languagePreferences.FirstOrDefault()?.Split(';')[0].Trim();

            return primaryLanguage ?? "en-US"; 
        }

    }

}
