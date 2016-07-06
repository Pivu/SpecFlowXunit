using OpenQA.Selenium;

namespace Pages
{
    public class CommonElementsModel : BasePageModel
    {
        public TopMenuModel TopMenu;

        public CommonElementsModel(IWebDriver driver)
            : base(driver)
        {
            this.TopMenu = new TopMenuModel(driver);
        }

    }
}
