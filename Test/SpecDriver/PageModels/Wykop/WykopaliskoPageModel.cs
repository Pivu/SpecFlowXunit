using OpenQA.Selenium;

namespace Pages
{
    public class WykopaliskoPageModel : BasePageModel
    {
        private static By WykopaliskoTypicalElement = By.XPath("//*[contains(text()[not(parent::script)],\"WYKOPALISKO :\")]");

        public WykopaliskoPageModel(IWebDriver driver)
            : base(driver, WykopaliskoTypicalElement)
        {
        }

    }
}