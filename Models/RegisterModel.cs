namespace SeleniumTestProject.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }

        public RegisterModel(string username, string password, string email)
        {
            UserName = username;
            Password = password;
            ConfirmPassword = password;
            Email = email;
        }
    }
}
