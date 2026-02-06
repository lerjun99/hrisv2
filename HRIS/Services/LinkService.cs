using Microsoft.AspNetCore.DataProtection;

namespace HRIS_UI.Services
{
    public class LinkService
    {
        private readonly IDataProtector _protector;

        public LinkService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("HrisLinkProtector.v1");
        }

        public string Protect(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;
            var protectedValue = _protector.Protect(plainText);
            return Uri.EscapeDataString(protectedValue);
        }

        public string? Unprotect(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;
            try
            {
                var decoded = Uri.UnescapeDataString(token);
                return _protector.Unprotect(decoded);
            }
            catch
            {
                return null;
            }
        }
    }
}
