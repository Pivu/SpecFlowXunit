using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using TechTalk.SpecFlow;

namespace StepDefinitions
{
    //Hooks - we use them to specify additional actions to be repeated between steps/features/scenarios
    [Binding]
    public class InitializeWebDriver : Steps
    {
        private readonly IObjectContainer objectContainer;

        public InitializeWebDriver(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
            string TestBrowser = ConfigurationManager.AppSettings["Browser"];
            string RemoteTestRun = ConfigurationManager.AppSettings["RemoteTestRun"];

            if (RemoteTestRun == "true")
            {
                SetRemoteDriver();
            }
            else
            {
                switch (TestBrowser)
                {
                    case "ie":
                        SetLocalIEDriver();
                        break;
                    case "chrome":
                        SetLocalChromedriver();
                        break;
                }
            }

            Debug.WriteLine("Scenario name: " + this.ScenarioContext.ScenarioInfo.Title);
        }

        private string FindDriver(string subpath)
        {
            var path = Environment.CurrentDirectory;
            var root = Path.GetPathRoot(path);

            while (path != root)
            {
                var dest = Path.Combine(path, subpath);
                if (Directory.Exists(dest))
                {
                    return dest;
                }

                path = Path.GetDirectoryName(path);
            }

            throw new Exception(string.Format("Could not find '{0}'", subpath));
        }

        private void SetRemoteDriver()
        {
            string SeleniumGridAddress = ConfigurationManager.AppSettings["SeleniumGridAddress"];
            int Timeout = Convert.ToInt16(ConfigurationManager.AppSettings["Timeout"]);
            DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
            var driver = new RemoteWebDriver(new Uri(SeleniumGridAddress), capabilities, new TimeSpan(0, 0, 0, Timeout));
            objectContainer.RegisterInstanceAs<IWebDriver>(driver);
        }

        private void SetLocalChromedriver()
        {
            int Timeout = Convert.ToInt16(ConfigurationManager.AppSettings["Timeout"]);
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            var driver = new ChromeDriver(FindDriver(@"Libraries\ChromeDriver"), options);
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 0, Timeout));
            
            objectContainer.RegisterInstanceAs<IWebDriver>(driver);
        }

        private void SetLocalIEDriver()
        {
            int Timeout = Convert.ToInt16(ConfigurationManager.AppSettings["Timeout"]);
            var driver = new InternetExplorerDriver(FindDriver(@"Libraries\IEDriver"));
            Thread.Sleep(1000);
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 0, Timeout));
            objectContainer.RegisterInstanceAs<IWebDriver>(driver);
        }
    }
}
