namespace WebApp.BLL.Models
{
    public enum ServiceResultType
    {
        Success = 200,
        BadRequest = 400,
        InvalidData = 422,
        NotFound = 404,
        InternalServerError = 500,
        Unauthorized = 401
    }
}
