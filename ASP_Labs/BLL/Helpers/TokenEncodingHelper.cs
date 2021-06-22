using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace WebApp.BLL.Helpers
{
    public static class TokenEncodingHelper
    {
        public static string Encode(string token)
        {
            if (token == null)
            {
                return null;
            }
            var tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            return codeEncoded;
        }

        public static string Decode(string token)
        {
            if (token == null)
            {
                return null;
            }
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            return codeDecoded;
        }
    }
}
