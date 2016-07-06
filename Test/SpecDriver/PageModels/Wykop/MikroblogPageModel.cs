using OpenQA.Selenium;

namespace Pages
{
    public class MikroblogPageModel : BasePageModel
    {
        private static By MikroblogTypicalElement = By.XPath("//*[contains(text()[not(parent::script)],\"Mikroblog :\")]");

        public MikroblogPageModel(IWebDriver driver)
            : base(driver, MikroblogTypicalElement)
        {
        }

    }
}