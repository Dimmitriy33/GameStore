namespace WebApp.BLL.DTO
{
    public class ResetPasswordUserDTO
    {
        public string Id { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
