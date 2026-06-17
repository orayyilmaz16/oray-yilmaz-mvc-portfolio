using OtpNet;


namespace OrayPortfolio.Web.Services
{

        public static class TwoFactorService
    {
        public static bool ValidateCode(string secretKey, string code)
        {
            var bytes = Base32Encoding.ToBytes(secretKey);
            var totp = new Totp(bytes);
            return totp.VerifyTotp(code, out _);
        }
    }
}
