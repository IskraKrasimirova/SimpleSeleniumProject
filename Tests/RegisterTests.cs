using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTestProject.Models;
using SeleniumTestProject.Pages;

namespace SeleniumTestProject.Tests
{
    public class RegisterTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            _driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }

        [Test]
        [Category("Register Tests")]
        public void RegisterUserWithValidCredentials()
        {
            var uniqueUser = "User" + RandomLetters(6);
            var uniqueEmail = $"user{Guid.NewGuid().ToString("N").Substring(0, 8)}@test.com";
            var registerModel = new RegisterModel(uniqueUser, "Password1!", uniqueEmail);

            RegisterPage registerPage = new RegisterPage(_driver);
            registerPage.NavigateToRegisterPage();
            registerPage.FillRegistrationForm(registerModel);

            var error = registerPage.GetErrorMessage(); 
            Console.WriteLine("VALIDATION ERROR: " + error);
            Assert.IsNull(error, "Registration form returned an error: " + error);

            var dashboardPage = new DashboardPage(_driver);
            Assert.IsTrue(dashboardPage.IsLoggedIn(), "Registration failed.");
        }

        private static string RandomLetters(int length) 
        { 
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; 
            var random = new Random(); 
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray()); 
        }

        private static IEnumerable<TestCaseData> ValidRegistrationData()
        {
            yield return new TestCaseData(
                new RegisterModel("TestUser", "Password1!", "a@a.com")
            );
        }

        // The UserName field is required.
        // The Password field is required.
        // The Email field is required.
        // The UserName must be at least 6 charecters long.
        // The Password must be at least 6 characters long.
        // Passwords must have at least one non letter or digit character. Passwords must have at least one digit ('0'-'9').
        // The password and confirmation password do not match.
        // Name Iskra123 is already taken.
        // Email 'a@a.com' is already taken.
    }
}
