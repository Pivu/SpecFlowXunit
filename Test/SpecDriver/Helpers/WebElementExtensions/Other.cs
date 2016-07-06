using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

//Here new selenium methods can be written.
public static class WebElementExtensionsOther
{
    private static int Timeout = Convert.ToInt16(ConfigurationManager.AppSettings["Timeout"]);

    /// <summary>
    /// Gets the element that is above given element in HTML code
    /// for example if you have "span" inside "div", the "div" will be returned
    /// </summary>
    /// <param name="element">element selector</param>
    /// <returns>parent of given element</returns>
    public static IWebElement GetParent(this IWebElement element)
    {
        return element.FindElement(By.XPath(".."));
    }

    /// <summary>
    /// Loses focus of current element, by clicking on 'body'.
    /// </summary>
    /// <param name="driver">WebDriver</param>
    public static void LoseFocus(this IWebDriver driver)
    {
        try
        {
            driver.GetElement(By.CssSelector("body")).SendKeys(Keys.Escape);
        }
        catch
        {
            System.Threading.Thread.Sleep(500);
            driver.GetElement(By.CssSelector("body")).SendKeys(Keys.Escape);
        }
    }

    /// <summary>
    /// This is mandatory if new tab is opened. It closes old tab and changes to the focus to the new tab.
    /// </summary>
    /// <param name="driver">WebDriver</param>
    public static void SwitchTab(this IWebDriver driver)
    {
        System.Threading.Thread.Sleep(4000);
        if (driver.WindowHandles.Count > 1)
        {
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }
    }

    /// <summary>
    /// Executes given JavaScript/jQuery
    /// Usage: Driver.Scripts().ExecuteScript(jQueryToRun);
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <returns>returs result of the script</returns>
    public static IJavaScriptExecutor Scripts(this IWebDriver driver)
    {
        return (IJavaScriptExecutor)driver;
    }

    /// <summary>
    /// Use this instead of FindElement. FindElement sucks.
    /// </summary>
    /// <param name="driver">driver</param>
    /// <param name="element">element</param>
    /// <param name="secondsToWait">seconds to wait before exception is thrown, default 2s</param>
    /// <returns></returns>
    public static IWebElement GetElement(this IWebDriver driver, By element, int secondsToWait = 3)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
        return wait.Until(drv => drv.FindElement(element));
    }

    /// <summary>
    /// Use this instead of FindElements. FindElements sucks.
    /// </summary>
    /// <param name="driver">driver</param>
    /// <param name="element">element</param>
    /// <param name="secondsToWait">seconds to wait before exception is thrown, default 2s</param>
    public static IList<IWebElement> GetElements(this IWebDriver driver, By element, int secondsToWait = 3)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
        try
        {
            wait.Until(drv => drv.FindElement(element));
        }
        catch { }
        return wait.Until(drv => drv.FindElements(element));
    }

    /// <summary>
    /// Checks if element is displayed
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="element">element locator</param>
    /// <returns>result - true or false</returns>
    public static bool IsElementDisplayed(this IWebDriver driver, By element, int maxSecondsToWait = 3)
    {
        IList<IWebElement> elements = driver.GetElements(element, maxSecondsToWait);

        if (elements.Count > 0)
        {
            if (elements.First().Displayed)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if element is enabled
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="element">element locator</param>
    /// <returns>result - true or false</returns>
    public static bool IsElementClickable(this IWebDriver driver, By element, int maxSecondsToWait = 3)
    {
        driver.GetElement(element, maxSecondsToWait);

        if (driver.FindElements(element).Count > 0)
        {
            if (driver.GetElement(element).Enabled)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
            
        return false;
    }

    /// <summary>
    /// Waits up to 1 minute for element to disappear
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="element">element selector</param>
    public static void WaitForElementToDisappear(this IWebDriver driver, By element)
    {
        int i;
        for (i = 1; i < 100; i++)
        {
            if (driver.IsElementDisplayed(element) == false)
            {
                break;
            }
            System.Threading.Thread.Sleep(200);
        }

    }

    /// <summary>
    /// waits up to 10 seconds for element to appear
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="element">element selector</param>
    /// <param name="waitTimeinS">optional wait time</param>
    public static void WaitForElementToAppear(this IWebDriver driver, By element, int secondsToWait = 10)
    {
        driver.GetElement(element, secondsToWait);
    }

    //TODO: Function to check if element is disabled. Selenium ".Enabled" method doesn't work that well.
    public static bool IsElementDisabled(this IWebDriver driver, By element)
    {
        throw new NotImplementedException();
    }
}
