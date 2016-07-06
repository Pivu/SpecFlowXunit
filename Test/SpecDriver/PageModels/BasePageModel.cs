using OpenQA.Selenium;
using System;

namespace Pages
{
    //Base page model - all other page models must inherit from it.
    public class BasePageModel
    {
        protected IWebDriver Driver;
        public CommonElementsModel CommonElements;

        //Constructor of page object. It checks if the page is opened in the right place.
        protected BasePageModel(IWebDriver driver, By knownElementOnPage = null, String loadUrl = "")
        {
            Driver = driver;
            this.CommonElements = new CommonElementsModel(driver);

            if (loadUrl != String.Empty)
            {
                Driver.Navigate().GoToUrl(loadUrl);
            }

            if (knownElementOnPage != null)
            {
                this.FindKnownElementOnPage(knownElementOnPage);
            }
        }

        protected BasePageModel(IWebDriver driver) 
        {
            Driver = driver;
        }

        public string Title
        {
            get { return Driver.Title; }
        }

        private void FindKnownElementOnPage(By knownElementOnPage)
        {
            try
            {
                Driver.GetElement(knownElementOnPage);
            }
            catch
            {
                throw new ApplicationException("element \"" + knownElementOnPage.ToString() + "\" was not found on the page, is page not working?");
            }
        }
    }

}