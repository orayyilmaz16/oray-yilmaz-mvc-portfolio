using OtpNet;

namespace OrayPortfolio.Web.Services
{
    public static class TwoFactorKeyGenerator
    {
        public static string GenerateSecretKey()
        {
            var bytes = KeyGeneration.GenerateRandomKey(20);
            return Base32Encoding.ToString(bytes);
        }
    }
}
