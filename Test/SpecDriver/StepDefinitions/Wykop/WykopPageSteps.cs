using OpenQA.Selenium;
using Pages;
using TechTalk.SpecFlow;
using Xunit;

namespace StepDefinitions
{
    [Binding]
    public class WykopStepDefs : BaseStepDefinitions
    {
        public WykopStepDefs(IWebDriver driver)
            :base(driver)
        {
        }

        [Given(@"I'm on Wykop main page")]
        public void GivenIMOnWykopMainPage()
        {
            WykopPage = new WykopPageModel(driver);
        }

        [When(@"I go to ""(.*)"" page")]
        public void WhenIGoToPage(string page)
        {
            switch (page)
            {
                case "Wykop":
                    WykopPage = BasePage.CommonElements.TopMenu.GoToWykop();
                    break;
                case "Wykopalisko":
                    WykopaliskoPage = BasePage.CommonElements.TopMenu.GoToWykopalisko();
                    break;
                case "Hity":
                    HityPage = BasePage.CommonElements.TopMenu.GoToHity();
                    break;
                case "Mikroblog":
                    MikroblogPage = BasePage.CommonElements.TopMenu.GoToMikroblog();
                    break;
            }
        }

        [When(@"I go to comments of the first post")]
        public void WhenIGoToCommentsOfTheFirstPost()
        {
            WykopPage.GoToCommentsOfFirstPost();
        }

        [Then(@"I should see some comments")]
        public void ThenIShouldSeeSomeComments()
        {
            Assert.True(WykopPage.IsCommentsSectionDisplayed(), "Comments were not found on the page");
        }


        [Then(@"The page title is ""(.*)""")]
        public void ThenThePageTitleIs(string title)
        {
            Assert.True(BasePage.Title == title, "Title was not as expected");
        }

    }
}
