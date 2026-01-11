using OpenQA.Selenium;

namespace SeleniumTestProject.Pages
{
    public class DashboardPage
    {
        private IWebDriver _driver;

        public DashboardPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement Greeting => _driver.FindElement(By.CssSelector("a[title='Manage']"));

        public IWebElement LogOffLink => _driver.FindElement(By.LinkText("Log off"));

        public IWebElement EmployeeDetailsLink => _driver.FindElement(By.LinkText("Employee Details"));

        public IWebElement ManageUsersLink => _driver.FindElement(By.LinkText("Manage Users"));

        public bool IsWelcomeMessageDisplayed() => Greeting.Displayed;

        public bool IsLogOffLinkDisplayed() => LogOffLink.Displayed;

        public bool IsEmployeeDetailsLinkDisplayed()
        {
            try
            {
                return EmployeeDetailsLink.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsManageUsersLinkDisplayed()
        {
            try
            {
                return ManageUsersLink.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsLoggedInAs(string username)
        {
            return Greeting.Text.Equals($"Hello {username}!");
        }

        public bool IsLoggedIn()
        {
            return IsWelcomeMessageDisplayed() && IsLogOffLinkDisplayed();
        }
    }
}
