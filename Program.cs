using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

class Program
{
    static void Main(string[] args)
    {
        var extent = new ExtentReports();
        var htmlReporter = new ExtentHtmlReporter(Directory.GetCurrentDirectory() + "\\Reportes\\ExtentReport.html");
        extent.AttachReporter(htmlReporter);

        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        IWebDriver driver = new ChromeDriver(options);

        ExtentTest test = extent.CreateTest("Prueba Automatizada", "Descripción de la prueba automatizada");

        try
        {
            driver.Navigate().GoToUrl("https://www.oreilly.com/library-access/?language=eyJ...");

            test.Pass("Accediendo a la página");

            Thread.Sleep(3000);

            IWebElement link = driver.FindElement(By.CssSelector("a.orm-Link-root"));
            link.Click();
            Thread.Sleep(2000);
            TakeScreenshot(driver, "Click_Link");
            test.Pass("Haciendo clic en el enlace");

            IWebElement emailInput = driver.FindElement(By.CssSelector("input#email"));
            emailInput.SendKeys("20220910@itla.edu.do");

            test.Pass("Ingresando correo electrónico");

            Thread.Sleep(2000);

            IWebElement letsGoButton = driver.FindElement(By.CssSelector("button.orm-Button-root"));
            letsGoButton.Click();
            Thread.Sleep(10000);
            TakeScreenshot(driver, "Click_LetsGo");
            test.Pass("Haciendo clic en Let's Go");

            IWebElement gotItButton = driver.FindElement(By.CssSelector("a.orm-Button-root._successBtn_dojg6_90.orm-Button-link"));
            gotItButton.Click();
            Thread.Sleep(2000);
            TakeScreenshot(driver, "Click_GotIt");
            test.Pass("Haciendo clic en Got It");

            IWebElement searchBar = driver.FindElement(By.CssSelector("input#searchBar"));
            searchBar.SendKeys("C#");
            Thread.Sleep(3000);
            TakeScreenshot(driver, "Search_CSharp");
            test.Pass("Realizando búsqueda y escribiendo 'C#'");

            IWebElement resultLink = driver.FindElement(By.CssSelector("a.css-1xhcqly"));
            resultLink.Click();
            Thread.Sleep(5000);
            TakeScreenshot(driver, "Click_ResultLink");
            test.Pass("Haciendo clic en el resultado de la búsqueda");

            IWebElement continueButton = driver.FindElement(By.CssSelector("a[data-testid='startContentLink']"));
            continueButton.Click();
            Thread.Sleep(3000);
            TakeScreenshot(driver, "Click_Continue");
            test.Pass("Haciendo clic en Continue");

            IWebElement myOReillyButton = driver.FindElement(By.XPath("//button[contains(@aria-label, \"My O'Reilly\")]"));
            myOReillyButton.Click();
            Thread.Sleep(3000);
            TakeScreenshot(driver, "Click_MyOReilly");
            test.Pass("Haciendo clic en My O'Reilly");

            IWebElement signOutButton = driver.FindElement(By.CssSelector("a[data-testid='Desktop-Sign out']"));
            signOutButton.Click();
            Thread.Sleep(2000);
            TakeScreenshot(driver, "Click_SignOut");
            test.Pass("Haciendo clic en Sign Out");

            test.Pass("Prueba completada con éxito");
        }
        catch (Exception ex)
        {
            test.Fail("Error durante la ejecución de la prueba: " + ex.Message);

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots", "error_screenshot.png");
            Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath));
            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
            test.AddScreenCaptureFromPath(screenshotPath, "Captura de Pantalla de Error");
        }
        finally
        {
            extent.Flush();
            driver.Quit();
        }
    }

    static void TakeScreenshot(IWebDriver driver, string screenshotName)
    {
        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        var screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots", $"{screenshotName}.png");
        Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath));
        screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
    }
}
