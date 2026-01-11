using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTestProject.Actions;
using SeleniumTestProject.Pages;

namespace SeleniumTestProject.Tests
{
    public class EAWebsiteTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        }

        [TearDown]
        public void TearDown() 
        {
            _driver.Dispose();
        }

        [Test]
        public void SuccsessfulLoginTest()
        {
            // Find the Login link and click it
            var loginLink = _driver.FindElement(By.Id("loginLink"));
            loginLink.Click();

            // Find the UserName textbox and enter valid username
            var userNameTextbox = _driver.FindElement(By.Name("UserName"));
            userNameTextbox.SendKeys("admin");

            // Find the Password textbox and enter valid password
            var passwordTextbox = _driver.FindElement(By.Id("Password"));
            passwordTextbox.SendKeys("password");

            // Find the Login button and click it
            //var loginButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            //var loginButton = _driver.FindElement(By.ClassName("btn"));
            var loginButton = _driver.FindElement(By.Id("loginIn"));
            loginButton.Click();

            // Verify that the user is logged in by checking for the presence of the greeting message
            Assert.IsTrue(_driver.PageSource.Contains("Hello admin!"), "Login failed or user not recognized.");

            // Verify that the user is logged in by checking for the presence of the Logout link
            var logoutLink = _driver.FindElement(By.LinkText("Log off"));

            Assert.IsTrue(logoutLink.Displayed, "Logout link is not displayed. Login might have failed.");

            // Additional verification: Check greeting message
            var greetingMessage = _driver.FindElement(By.LinkText("Hello admin!"));

            Assert.IsTrue(greetingMessage.Displayed, "Greeting message is not visible — user may not be logged in.");

            var adminText = _driver.FindElement(By.LinkText("Hello admin!")).Text;
            Assert.That(adminText, Is.EqualTo("Hello admin!"));

            // Additional verification: Check Employee Details link is present
            var employeeDetailsLink = _driver.FindElement(By.LinkText("Employee Details"));

            Assert.IsTrue(employeeDetailsLink.Displayed, "Employee Details link is not displayed.");

            // Additional verification: Check Manage Users link is present
            var manageUsersLink = _driver.FindElement(By.LinkText("Manage Users"));

            Assert.IsTrue(manageUsersLink.Displayed, "Manage Users link is not displayed.");
        }

        [Test]
        public void SuccsessfulLoginTestReducedCode()
        {
            // Find the Login link and click it
            _driver.FindElement(By.Id("loginLink")).Click();

            // Find the UserName textbox and enter valid username
            _driver.FindElement(By.Name("UserName")).SendKeys("admin"); 

            // Find the Password textbox and enter valid password
            _driver.FindElement(By.Id("Password")).SendKeys("password");

            // Find the Login button and click it
            _driver.FindElement(By.Id("loginIn")).Click();

            // Verify that the user is logged in by checking for the presence of the greeting message
            Assert.IsTrue(_driver.PageSource.Contains("Hello admin!"), "Login failed or user not recognized.");
        }

        [Test]
        public void SuccsessfulLoginTestWithCustomMethods()
        {
            // Find the Login link and click it
            CustomMethods.Click(_driver, By.Id("loginLink"));

            // Find the UserName textbox and enter valid username
            CustomMethods.EnterText(_driver, By.Name("UserName"), "admin");

            // Find the Password textbox and enter valid password
            CustomMethods.EnterText(_driver, By.Id("Password"), "password");

            // Find the Login button and click it
            CustomMethods.Click(_driver, By.Id("loginIn"));

            // Verify that the user is logged in by checking for the presence of the greeting message
            Assert.IsTrue(_driver.PageSource.Contains("Hello admin!"), "Login failed or user not recognized.");
        }

        [Test]
        public void SuccsessfulLoginTestWithPOM()
        {
            // Instantiate the LoginPage object
            var loginPage = new LoginPage(_driver);

            // Navigate to the login page
            loginPage.NavigateToLoginPage();

            // Perform login using the UserLogin method
            loginPage.UserLogin("admin", "password");

            // Verify that the user is logged in by checking for the presence of the greeting message
            Assert.IsTrue(_driver.PageSource.Contains("Hello admin!"), "Login failed or user not recognized.");
        }
    }
}
