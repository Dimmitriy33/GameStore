namespace WebApp.BLL.Models
{
    public enum ServiceResultType
    {
        Success,
        Bad_Request = 400,
        Invalid_Data = 422,
        Not_Found = 404,
        Internal_Server_Error = 500,
        Unauthorized = 401
    }
}
