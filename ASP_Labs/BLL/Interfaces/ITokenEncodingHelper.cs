namespace WebApp.BLL.Interfaces
{
    public interface ITokenEncodingHelper
    {
        string Decode(string token);
        string Encode(string token);
    }
}
