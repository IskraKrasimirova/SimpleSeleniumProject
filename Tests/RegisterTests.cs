using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTestProject.Models;
using SeleniumTestProject.Pages;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            var errors = registerPage.GetErrorMessages();
            Assert.IsEmpty(errors, "Registration returned unexpected validation errors.");

            var dashboardPage = new DashboardPage(_driver);
            Assert.IsTrue(dashboardPage.IsLoggedIn(), "Registration failed.");
        }

        [Test]
        [Category("Register Tests")]
        [TestCaseSource(nameof(NotValidRegistrationData))]
        public void RegisterUserWithNotValidCredentials_ShouldShowErrorMessages(string testedCase, RegisterModel registerModel, List<string> errorMessages)
        {
            ExecuteNegativeRegistrationTest(testedCase, registerModel, errorMessages);
        }

        private static IEnumerable<TestCaseData> NotValidRegistrationData()
        {
            yield return new TestCaseData(
                "Missing UserName",
                new RegisterModel("", "Password1!", "a@a.com"),
                new List<string> { "The UserName field is required." }
            );
            yield return new TestCaseData(
                "Missing Password",
                new RegisterModel("TestUser", "", "a@a.com"),
                new List<string> { "The Password field is required." }
            );
            yield return new TestCaseData(
                "Missing Email",
                new RegisterModel("TestUser", "Password1!", ""),
                new List<string> { "The Email field is required." }
            );
            yield return new TestCaseData(
                "Too short UserName",
                new RegisterModel("Us", "Password1!", "a@a.com"),
                new List<string> { "The UserName must be at least 6 charecters long." }
            );
            //yield return new TestCaseData(
            //    "UserName with digits", // assuming digits are not allowed -> EAApp inconsistent in validation!!!
            //    new RegisterModel("Use123", "Password1!", "a@a.com"),
            //    new List<string> { "The UserName must be at least 6 charecters long." }
            //);
            yield return new TestCaseData(
                "Too short Password",
                new RegisterModel("TestUser", "Pass", "a@a.com"),
                new List<string> { "The Password must be at least 6 characters long." }
            );
            yield return new TestCaseData(
                "Password without digits", // Not stable test!
                new RegisterModel("TestUser", "Password", "a@a.com"),
                new List<string> { "Passwords must have at least one non letter or digit character. Passwords must have at least one digit ('0'-'9')." }
            );
            yield return new TestCaseData(
                "Password without special character", // Not stable test!
                new RegisterModel("TestUser", "Password1", "a@a.com"),
                new List<string> { "Passwords must have at least one non letter or digit character." }
            );
            yield return new TestCaseData(
                "Not valid email",
                new RegisterModel("TestUser", "Password1!", "invalid-email"),
                new List<string> { "The Email field is not a valid e-mail address." }
            );
            yield return new TestCaseData(
                "Existing UserName", // Not stable test - there is such user in the system, but not always the error message is shown
                new RegisterModel("Iskra123", "Password1!", "a@a.com"),
                new List<string> { "Name Iskra123 is already taken." }
            );
            yield return new TestCaseData(
                "Existing Email",
                new RegisterModel("ValidUser", "Password1!", "a@a.com"),
                new List<string> { "Email 'a@a.com' is already taken." }
            );
            yield return new TestCaseData(
                "Empty UserName, Password and Email fields",
                new RegisterModel("", "", ""),
                new List<string> { "The UserName field is required.", "The Password field is required.", "The Email field is required." }
            );
            yield return new TestCaseData(
                "Missing UserName and short Password",
                new RegisterModel("", "Pass", "a@a.com"),
                new List<string> { "The UserName field is required.", "The Password must be at least 6 characters long." }
            );
            yield return new TestCaseData(
                "Missing UserName and not valid Email",
                new RegisterModel("", "Password1!", "a a@a.com"),
                new List<string> { "The UserName field is required.", "The Email field is not a valid e-mail address." }
            );
            yield return new TestCaseData(
                "Missing Password and short UserName",
                new RegisterModel("Use", "", "a@a.com"),
                new List<string> { "The UserName must be at least 6 charecters long.", "The Password field is required." }
            );
            yield return new TestCaseData(
                "Missing Password and not valid Email",
                new RegisterModel("ValidUser", "", "a a@a.com"),
                new List<string> { "The Password field is required.", "The Email field is not a valid e-mail address." }
            );
            yield return new TestCaseData(
                "Missing Email and short UserName",
                new RegisterModel("Use", "Password1!", ""),
                new List<string> { "The UserName must be at least 6 charecters long.", "The Email field is required." }
            );
            yield return new TestCaseData(
                "Missing Email and short Password",
                new RegisterModel("ValidUser", "Pass", ""),
                new List<string> { "The Password must be at least 6 characters long.", "The Email field is required." }
            );
            yield return new TestCaseData(
                "Too short UserName and Password",
                new RegisterModel("Us", "Pass", "a@a.com"),
                new List<string> { "The UserName must be at least 6 charecters long.", "The Password must be at least 6 characters long." }
            );
            yield return new TestCaseData(
                "Too short UserName and Password, missing Email",
                new RegisterModel("Us", "Pass", ""),
                new List<string> { "The UserName must be at least 6 charecters long.", "The Password must be at least 6 characters long.", "The Email field is required." }
            );
            yield return new TestCaseData(
                "Too short UserName and Password, not valid Email",
                new RegisterModel("Us", "Pass", "a@a."),
                new List<string> { "The UserName must be at least 6 charecters long.", "The Password must be at least 6 characters long.", "The Email field is not a valid e-mail address." }
            );
            yield return new TestCaseData(
                "Missing UserName and Password, not valid Email",
                new RegisterModel("", "", "a@a .com"),
                new List<string> { "The UserName field is required.", "The Password field is required.", "The Email field is not a valid e-mail address." }
            );
            yield return new TestCaseData(
                "Missing UserName and Email, short Password",
                new RegisterModel("", "Pass", ""),
                new List<string> { "The UserName field is required.", "The Password must be at least 6 characters long.", "The Email field is required." }
            );
            yield return new TestCaseData(
                "Missing Password and Email, short UserName",
                new RegisterModel("Use", "", ""),
                new List<string> { "The UserName must be at least 6 charecters long.", "The Password field is required.", "The Email field is required." }
            );
            //yield return new TestCaseData(
            //    "Missing UserName and Password, existing Email", // Error message for existing email is not shown
            //    new RegisterModel("", "", "a@a.com"),
            //    new List<string> { "The UserName field is required.", "The Password field is required.", "Email 'a@a.com' is already taken." }
            //);
            //yield return new TestCaseData(
            //    "Missing Password, existing UserName and Email", // Only error for missing password is shown
            //    new RegisterModel("Iskra123", "", "a@a.com"),
            //    new List<string> { "Name Iskra123 is already taken.", "The Password field is required.", "Email 'a@a.com' is already taken." }
            //);
        }

        [Test]
        [TestCaseSource(nameof(ConfirmPasswordData))]
        public void RegisterUser_ConfirmPasswordValidation(string testedCase, RegisterModel model, List<string> errorMessages)
        {
            ExecuteNegativeRegistrationTest(testedCase, model, errorMessages);
        }

        private static IEnumerable<TestCaseData> ConfirmPasswordData()
        {
            yield return new TestCaseData(
                "Password mismatch",
                new RegisterModel("ValidUser", "Password1!", "user@test.com") { ConfirmPassword = "Different1!" },
                new List<string> { "The password and confirmation password do not match." }
            );
            yield return new TestCaseData(
                "Empty ConfirmPassword",
                new RegisterModel("ValidUser", "Password1!", "user@test.com") { ConfirmPassword = "" },
                new List<string> { "The password and confirmation password do not match." }
            );
            yield return new TestCaseData(
                "Short ConfirmPassword",
                new RegisterModel("ValidUser", "Password1!", "user@test.com") { ConfirmPassword = "Pass" },
                new List<string> { "The password and confirmation password do not match." }
            );
            yield return new TestCaseData(
                "Valid Password and ConfirmPassword",
                new RegisterModel("Valid User", "Password1!", "user@test.com") { ConfirmPassword = "Password1!" },
                new List<string>() // no errors expected
            );
        }

        private void ExecuteNegativeRegistrationTest(string testedCase, RegisterModel registerModel, List<string> errorMessages)
        {
            RegisterPage registerPage = new RegisterPage(_driver);
            registerPage.NavigateToRegisterPage();
            registerPage.FillRegistrationForm(registerModel);

            var actualErrors = registerPage.GetErrorMessages();

            Console.WriteLine($"Running case: {testedCase}");
            Console.WriteLine("Actual errors:");
            foreach (var error in actualErrors)
            {
                Console.WriteLine($" - {error}");
            }

            Assert.Multiple(() =>
            {
                foreach (var expectedError in errorMessages)
                {
                    Assert.IsTrue(actualErrors.Contains(expectedError), $"Expected error message '{expectedError}' was not found.");
                }
            });
        }

        private static string RandomLetters(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }



        // The UserName field is required.
        // The Password field is required.
        // The Email field is required.
        // The UserName must be at least 6 charecters long.
        // The Password must be at least 6 characters long.
        // Passwords must have at least one non letter or digit character. Passwords must have at least one digit ('0'-'9').
        // Passwords must have at least one non letter or digit character.
        // The password and confirmation password do not match.
        // Name Iskra123 is already taken.
        // Email 'a@a.com' is already taken.
    }
}
