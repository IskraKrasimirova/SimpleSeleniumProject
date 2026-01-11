using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTestProject.Models;
using SeleniumTestProject.Pages;

namespace SeleniumTestProject.Tests
{
    [TestFixture(Category = "UI Tests")]
    public class LoginTests
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
        [Category("Login Tests")]
        [TestCaseSource(nameof(ValidLoginData))]
        public void SuccessfulLoginWithValidUserCredentials(LoginModel loginModel)
        {
            var loginPage = new LoginPage(_driver);

            loginPage.NavigateToLoginPage();

            loginPage.UserLogin(loginModel.UserName, loginModel.Password);

            var dashboardPage = new DashboardPage(_driver);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(dashboardPage.IsLoggedIn(), "User is not logged in.");
                Assert.IsTrue(dashboardPage.IsLoggedInAs(loginModel.UserName), "Logged-in username does not match.");

                Assert.IsTrue(dashboardPage.IsWelcomeMessageDisplayed(), "Greeting not visible.");
                Assert.IsTrue(dashboardPage.IsLogOffLinkDisplayed(), "Log off link not visible.");
                Assert.IsTrue(dashboardPage.IsEmployeeDetailsLinkDisplayed(), "Employee Details link not visible.");
                Assert.IsTrue(dashboardPage.IsManageUsersLinkDisplayed(), "Manage Users link not visible.");
            });
        }

        private static IEnumerable<TestCaseData> ValidLoginData()
        {
            yield return new TestCaseData(
                new LoginModel("admin", "password")
            );
        }

        [Test]
        [Category("Login Tests")]
        [TestCaseSource(nameof(NotValidLoginData))]
        public void UnsuccessfulLoginWithNotValidUserCredentials(string testedCase, LoginModel loginModel)
        {
            var loginPage = new LoginPage(_driver);

            loginPage.NavigateToLoginPage();

            loginPage.UserLogin(loginModel.UserName, loginModel.Password);

            Assert.AreEqual("Invalid login attempt.", loginPage.ErrorMessage.Text, "Error message did not match expected.");
        }

        private static IEnumerable<TestCaseData> NotValidLoginData()
        {
            yield return new TestCaseData(
                "Invalid username",
                new LoginModel("admin1", "password")
            );
            yield return new TestCaseData(
                "Invalid password",
                new LoginModel("admin", "password1")
            );
            yield return new TestCaseData(
                "Invalid username and password",
                new LoginModel("admin2", "password2")
            );
        }
    }
}
