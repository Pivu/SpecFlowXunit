using OpenQA.Selenium;

namespace Pages
{
    public class TopMenuModel
    {
        private readonly IWebDriver Driver;

        private static By MainWykopButton = By.CssSelector(".wykopnew");
        private static By WykopaliskoButton = By.XPath("//a[contains(text()[not(parent::script)],\"Wykopalisko\")]");
        private static By MikroblogButton = By.CssSelector("li [title=\"Dyskusje użytkowników\"]");
        private static By HityButton = By.XPath("//a[contains(text()[not(parent::script)],\"Hity\")]");

        public TopMenuModel(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public MikroblogPageModel GoToMikroblog()
        {
            Driver.Click(MikroblogButton);
            return new MikroblogPageModel(Driver);
        }

        public WykopaliskoPageModel GoToWykopalisko()
        {
            Driver.Click(WykopaliskoButton);
            return new WykopaliskoPageModel(Driver);
        }

        public HityPageModel GoToHity()
        {
            Driver.Click(HityButton);
            return new HityPageModel(Driver);
        }

        public WykopPageModel GoToWykop()
        {
            Driver.Click(MainWykopButton);
            return new WykopPageModel(Driver);
        }
    }
}
