namespace SadadMisr.BLL.Models.Users.Login
{
    public class LoginOutput
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}