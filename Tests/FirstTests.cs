using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestProject.Tests
{
    public class FirstTests
    {
        private IWebDriver? driver;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GoogleSearchTest()
        {
            // Configure ChromeOptions to avoid detection
            // Reduces the possibility of Google CAPTCHA
            var options = new ChromeOptions(); 
            options.AddArgument("--disable-blink-features=AutomationControlled"); 
            options.AddExcludedArgument("enable-automation"); 
            options.AddAdditionalOption("useAutomationExtension", false);

            // 1. Create a new instance of Selenium WebDriver (e.g., ChromeDriver)
            driver = new ChromeDriver(options);

            // 2. Navigate to the URL
            driver.Navigate().GoToUrl("https://www.google.com/");

            // 3. Maximize the browser window
            driver.Manage().Window.Maximize();

            // Close Google consent popup if it appears
            try
            {
                var agreeButton = driver.FindElement(By.Id("L2AGLb"));
                agreeButton.Click();
            }
            catch (NoSuchElementException)
            {
                // Popup not shown — continue normally
            }

            // 4. Find the search box using its name attribute
            IWebElement searchBox = driver.FindElement(By.Name("q"));

            // 5. Enter a search term
            searchBox.SendKeys("Selenium");

            // 6. Click on the element
            searchBox.SendKeys(Keys.Enter);

            Assert.IsTrue(driver.Title.Contains("Selenium"), "Search did not complete successfully.");
        }

        [Test]
        public void DuckDuckGoSearchTest()
        {
            // 1. Create a new instance of Selenium WebDriver
            driver = new ChromeDriver();

            // 2. Navigate to the URL
            driver.Navigate().GoToUrl("https://duckduckgo.com/");

            // 3. Maximize the browser window
            driver.Manage().Window.Maximize();

            // 4. Find the search box using its name attribute
            //IWebElement searchBox = driver.FindElement(By.Name("q"));

            // Or Using ID locator for DuckDuckGo search box
            IWebElement searchBox = driver.FindElement(By.Id("searchbox_input"));

            // 5. Enter a search term
            searchBox.SendKeys("Selenium");

            // 6. Click on the element
            searchBox.SendKeys(Keys.Enter);

            Assert.IsTrue(driver.Url.Contains("q=Selenium"), "Search query was not executed.");
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Dispose();
        }
    }
}