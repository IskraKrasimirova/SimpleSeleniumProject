using OpenQA.Selenium;
using SeleniumTestProject.Actions;
using SeleniumTestProject.Models;

namespace SeleniumTestProject.Pages
{
    public class RegisterPage
    {
        private IWebDriver _driver;

        public RegisterPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement RegisterLink => _driver.FindElement(By.Id("registerLink"));
        public IWebElement UserNameTextbox => _driver.FindElement(By.Name("UserName"));
        public IWebElement PasswordTextbox => _driver.FindElement(By.Id("Password"));
        public IWebElement ConfirmPasswordTextbox => _driver.FindElement(By.Id("ConfirmPassword"));
        public IWebElement EmailTextbox => _driver.FindElement(By.Id("Email"));
        public IWebElement RegisterButton => _driver.FindElement(By.CssSelector("input[type='submit'][value='Register']"));


        public void NavigateToRegisterPage()
        {
            RegisterLink.ClickElement();
        }

        public void FillRegistrationForm(RegisterModel model)
        {
            UserNameTextbox.EnterText(model.UserName);
            PasswordTextbox.EnterText(model.Password);
            ConfirmPasswordTextbox.EnterText(model.ConfirmPassword);
            EmailTextbox.EnterText(model.Email);
            RegisterButton.ClickElement();
        }

        public string GetErrorMessage()
        {
            try
            {
                return _driver.FindElement(By.CssSelector(".text-danger")).Text;
            }
            catch
            {
                return null;
            }
        }

    }
}
