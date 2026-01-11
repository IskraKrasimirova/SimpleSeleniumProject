using OpenQA.Selenium;
using SeleniumTestProject.Actions;

namespace SeleniumTestProject.Pages
{
    public class LoginPage
    {
        private IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement LoginLink => _driver.FindElement(By.Id("loginLink"));
        public IWebElement UserNameTextbox => _driver.FindElement(By.Name("UserName"));
        public IWebElement PasswordTextbox => _driver.FindElement(By.Id("Password"));
        public IWebElement LoginButton => _driver.FindElement(By.Id("loginIn"));

        public void NavigateToLoginPage()
        {
            LoginLink.ClickElement();
        }

        public void UserLogin(string username, string password)
        {
            UserNameTextbox.EnterText(username);
            PasswordTextbox.EnterText(password);
            LoginButton.ClickElement();
        }
    }
}
