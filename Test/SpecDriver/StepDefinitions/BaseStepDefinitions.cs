using OpenQA.Selenium;
using Pages;
using TechTalk.SpecFlow;

namespace StepDefinitions
{
    //If we want to use any page model in our step definitions, this page model needs to be defined here first. 
    //It is the only connection between Page Models and Steps.
    

    [Binding]
    public class BaseStepDefinitions : Steps
    {
        private const string CurrentPageKey = "Current.Page";
        protected readonly IWebDriver driver;
        
        public BaseStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
        }

        //This page is used for all common elements, like top or sidebar menu
        protected BasePageModel BasePage
        {
            get { return this.ScenarioContext.Get<BasePageModel>(CurrentPageKey); }
            set { this.ScenarioContext.Set(value, CurrentPageKey); }
        }

        protected WykopPageModel WykopPage
        {
            get { return this.ScenarioContext.Get<WykopPageModel>(CurrentPageKey); }
            set { this.ScenarioContext.Set(value, CurrentPageKey); }
        }

        protected HityPageModel HityPage
        {
            get { return this.ScenarioContext.Get<HityPageModel>(CurrentPageKey); }
            set { this.ScenarioContext.Set(value, CurrentPageKey); }
        }

        protected MikroblogPageModel MikroblogPage
        {
            get { return this.ScenarioContext.Get<MikroblogPageModel>(CurrentPageKey); }
            set { this.ScenarioContext.Set(value, CurrentPageKey); }
        }

        protected WykopaliskoPageModel WykopaliskoPage
        {
            get { return this.ScenarioContext.Get<WykopaliskoPageModel>(CurrentPageKey); }
            set { this.ScenarioContext.Set(value, CurrentPageKey); }
        }
    }
}
