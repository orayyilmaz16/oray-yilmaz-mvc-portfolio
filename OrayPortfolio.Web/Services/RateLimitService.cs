namespace OrayPortfolio.Web.Services
{
    public static class RateLimitService
    {
        private static Dictionary<string, (int Count, DateTime Reset)> _attempts = new();

        public static bool IsBlocked(string ip)
        {
            if (!_attempts.ContainsKey(ip)) return false;

            var (count, reset) = _attempts[ip];
            return count >= 5 && reset > DateTime.Now;
        }

        public static void RegisterFail(string ip)
        {
            if (!_attempts.ContainsKey(ip))
                _attempts[ip] = (1, DateTime.Now.AddMinutes(5));
            else
            {
                var (count, reset) = _attempts[ip];
                _attempts[ip] = (count + 1, reset);
            }
        }

        public static void Reset(string ip)
        {
            if (_attempts.ContainsKey(ip))
                _attempts.Remove(ip);
        }
    }

}
