using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebApp.BLL.Models;

namespace WebApp.BLL.Helpers
{
    public static class TokenEncodingHelper
    {
        //constants
        private const string invalidTokenMessage = "Invalid token";
        public static string Encode(string token)
        {
            if (token is null)
            {
                throw new CustomExceptions(ServiceResultType.Bad_Request, invalidTokenMessage);
            }
            var tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            return codeEncoded;
        }

        public static string Decode(string token)
        {
            if (token is null)
            {
                throw new CustomExceptions(ServiceResultType.Bad_Request, invalidTokenMessage);
            }
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            return codeDecoded;
        }
    }
}
