using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;

namespace WebApp.BLL.Helpers
{
    public class TokenEncodingHelper : ITokenEncodingHelper
    {
        #region Constants

        private const string InvalidTokenMessage = "Invalid token";

        #endregion

        public string Encode(string token)
        {
            if (token is null)
            {
                throw new CustomExceptions(ServiceResultType.BadRequest, InvalidTokenMessage);
            }

            var tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

            return codeEncoded;
        }

        public string Decode(string token)
        {
            if (token is null)
            {
                throw new CustomExceptions(ServiceResultType.BadRequest, InvalidTokenMessage);
            }

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            return codeDecoded;
        }
    }
}
