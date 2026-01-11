namespace SeleniumTestProject.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginModel(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
