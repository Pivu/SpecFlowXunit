using OpenQA.Selenium;
using System.Configuration;
using System.Linq;

namespace Pages
{
    public class WykopPageModel : BasePageModel
    {
        private static string Url = ConfigurationManager.AppSettings["BaseUrl"];
        
        private static By WykopTypicalElement = By.XPath("//*[contains(text()[not(parent::script)],\"STRONA GŁÓWNA :\")]");
        private static By CommentsOfAllPostsLink = By.CssSelector(".fa-comments-o");
        private static By CommentsSection = By.CssSelector(".comments-stream");

        public WykopPageModel(IWebDriver driver)
            : base(driver, WykopTypicalElement, Url)
        {
        }


        public void GoToCommentsOfFirstPost()
        {
            Driver.GetElements(CommentsOfAllPostsLink).First().Click();
        }

        public bool IsCommentsSectionDisplayed()
        {
            return Driver.IsElementDisplayed(CommentsSection);
        }
    }
}