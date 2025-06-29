using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ROE.API.Utility
{
    public static class JwtConfigurationHelper
    {
        public static string GetRequiredSetting(this IConfiguration config, string key)
        {
            var value = config[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException($"Missing required JWT configuration setting: '{key}'");
            }
            return value;
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey(this IConfiguration config, string key)
        {
            var secret = config.GetRequiredSetting(key);
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        }
    }

}
