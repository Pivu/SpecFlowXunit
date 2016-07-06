using OpenQA.Selenium;
using System;
using System.Configuration;
using System.Globalization;

//Here new selenium methods can be written.
public static class WebElementExtensionsInputs
{
    private static int Timeout = Convert.ToInt16(ConfigurationManager.AppSettings["Timeout"]);

    /// <summary>
    /// Clears input contents and inserts new text
    /// </summary>
    /// <param name="element">Element to fill with text</param>
    /// <param name="text">String to be passed to input</param>
    public static void Set(this IWebElement element, string text)
    {
        if (text == null)
        {
            text = "";
        }
        try
        {
            element.Clear();
            element.SendKeys(text);
        }
        catch
        {
            System.Threading.Thread.Sleep(500);
            element.Clear();
            element.SendKeys(text);
        }
    }

    /// <summary>
    /// Writes some text in text box, but is called directly from WebDriver, not WebElement
    /// </summary>
    /// <param name="driver">driver</param>
    /// <param name="element">element</param>
    /// <param name="text">text</param>
    public static void Set(this IWebDriver driver, By element, string text)
    {
        if (text == null)
        {
            text = "";
        }
        try
        {
            driver.FindElement(element).Clear();
            driver.FindElement(element).SendKeys(text);
        }
        catch
        {
            System.Threading.Thread.Sleep(500);
            driver.FindElement(element).Clear();
            driver.FindElement(element).SendKeys(text);
        }
    }

    /// <summary>
    /// Checks if element with given text exists
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="text">element text</param>
    /// <returns></returns>
    public static bool CheckIfElementWithTextExists(this IWebDriver driver, string text)
    {
        return driver.IsElementDisplayed(By.XPath("//*[contains(text()[not(parent::script)],\"" + text + "\")]"));
    }

    /// <summary>
    /// Checks if element with given text is displayed
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="text">element text</param>
    /// <returns></returns>
    /// 
    public static bool IsElementWithTextDisplayed(this IWebDriver driver, string text)
    {
        return driver.IsElementDisplayed(By.XPath("//*[contains(text()[not(parent::script)],\"" + text + "\")]"));
    }


    /// <summary>
    /// Sets date in a datepicker. Format "dd-MM-yyy".
    /// </summary>
    /// <param name="datepicker">Datepicker field</param>
    /// <param name="year">Year</param>
    /// <param name="month">Month</param>
    /// <param name="day">Day</param>
    public static void SetDate(this IWebElement datepicker, int year, int month, int day)
    {
        DateTime theDate = new DateTime(year, month, day);
        string newDate = theDate.ToString("dd-MM-yyyy");
        datepicker.Clear();
        datepicker.Set(newDate);
    }

    /// <summary>
    /// Gets value of one datepicker and sets date in anohter datepicker basing on its date
    /// "datepicker date" = "source datepicker date" + "days to add". Format "dd-MM-yyy".
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="datepickerToSet">datepicker to set date on</param>
    /// <param name="datepickerToBaseOn">datepicker to get date from</param>
    /// <param name="daysToAdd">how many days should be added</param>
    public static void SetDateBasedOnDatepicker(this IWebDriver driver, By datepickerToSet, By datepickerToBaseOn, double daysToAdd)
    {
        DateTime datepickerDate = DateTime.ParseExact(driver.GetText(datepickerToBaseOn), "dd-MM-yyyy", new CultureInfo("da-DK"));
        DateTime addedDays = datepickerDate.AddDays(daysToAdd);
        string newDate = addedDays.ToString("dd-MM-yyyy");
        driver.FindElement(datepickerToSet).Clear();
        driver.FindElement(datepickerToSet).Set(newDate);
        driver.FindElement(datepickerToSet).SendKeys(Keys.Escape);
        driver.LoseFocus();
    }

    /// <summary>
    /// Adds given number of days to current date. Format "dd-MM-yyy".
    /// </summary>
    /// <param name="datepicker">Datepicker field</param>
    /// <param name="daysToAdd">Number of days to add</param>
    public static void SetDateFromNow(this IWebElement datepicker, double daysToAdd)
    {
        DateTime currentDate = DateTime.Now;
        DateTime addedDays = currentDate.AddDays(daysToAdd);
        string newDate = addedDays.ToString("dd-MM-yyyy");
        datepicker.Click();
        datepicker.Clear();
        datepicker.Set(newDate);
            
    }

    /// <summary>
    /// Uses jQuery to get value of given fields (for example input fields). Can be used only with CSS selectors.
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="element">element css locator</param>
    /// <returns></returns>
    private static String GetVal(this IWebDriver driver, By element)
    {
        IJavaScriptExecutor js = driver as IJavaScriptExecutor;
        string selector = element.ToString();
        string[] selectorArray = new string[1];
        selectorArray = selector.Split(new string[] { "r: " }, StringSplitOptions.None);
        String jQueryToRun = String.Format("return $(\'" + selectorArray[1] + "\').val()");
        String val = (String)js.ExecuteScript(jQueryToRun);
        return val;
    }

    /// <summary>
    /// Gets text of element, better than the usual Selenium ".Text" method. Preferred CSS selector, but might also work with XPath.
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="element">element selector</param>
    /// <returns>string</returns>
    public static String GetText(this IWebDriver driver, By element)
    {
        string savedString;
        savedString = driver.FindElement(element).Text;

        if (savedString == "")
        {
            savedString = driver.GetVal(element);
        }

        return savedString;
    }

    //TODO: Dropdown select first option
    public static void DropdownSelectFirst(this IWebDriver driver, By dropdown)
    {
        throw new NotImplementedException();
    }

    //TODO: Dropdown select option by text
    public static void DropdownSelectByText(this IWebDriver driver, By dropdown, string text)
    {
        throw new NotImplementedException();
    }
}