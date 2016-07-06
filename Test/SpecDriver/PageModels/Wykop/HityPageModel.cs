using OpenQA.Selenium;

namespace Pages
{
    public class HityPageModel : BasePageModel
    {
        private static By HityTypicalElement = By.XPath("//*[contains(text()[not(parent::script)],\"HITY :\")]");

        public HityPageModel(IWebDriver driver)
            : base(driver, HityTypicalElement)
        {
        }

    }
}