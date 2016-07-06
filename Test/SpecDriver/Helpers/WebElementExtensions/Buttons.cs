using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

/// <summary>
/// Methods for buttons
/// Here new selenium methods can be written. First parameter in a method must be "this IWebElement element" or "this IWebDriver driver"
/// </summary>
public static class WebElementExtensionsButtons
{
    private static int Timeout = Convert.ToInt16(ConfigurationManager.AppSettings["Timeout"]);

    public static void EmergencyClickAction(this IWebDriver driver, By element)
    {
        Actions action = new Actions(driver);
        IWebElement elementToClick = driver.FindElement(element);
        action.MoveToElement(elementToClick, 3, 3).Click().Build().Perform();
    }

    /// <summary>
    /// Tries to click again if first click failed
    /// </summary>
    /// <param name="element">element</param>
    public static void ForceClick(this IWebElement element)
    {
        try
        {
            element.Click();
        }
        catch
        {
            System.Threading.Thread.Sleep(700);
            element.Click();
        }
    }

    /// <summary>
    /// Tries to click again if first click failed, but is called directly from WebDriver instead of WebElement
    /// </summary>
    /// <param name="driver">driver</param>
    /// <param name="element">element</param>
    public static void ForceClick(this IWebDriver driver, By element)
    {
        try
        {
            driver.FindElement(element).Click();
        }
        catch
        {
            System.Threading.Thread.Sleep(700);
            driver.FindElement(element).Click();
        }
    }

    /// <summary>
    /// Clicks element, but is called directly from WebDriver, instead of WebElement
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="element"></param>
    public static void Click(this IWebDriver driver, By element)
    {
        driver.FindElement(element).Click();
    }

    /// <summary>
    /// Wait untils element is clickable
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="elementToClick">Element to click</param>
    public static void WaitUntilElementClickable(this IWebDriver driver, By elementToClick)
    {
        driver.LoseFocus();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
        wait.Until<bool>((d) =>
        {
            try
            {
                if (!d.IsElementDisabled(elementToClick))
                    return true;
                else
                    return false;
            }
            catch
            {
                if (d.FindElement(elementToClick).Enabled)
                    return true;
                else
                    return false;
            }

        });
    }

    /// <summary>
    /// If element is displayed it is clicked, if it is not displayed - nothing happens
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="elementToClick">Element to click</param>
    public static void ClickIfElementIsDisplayed(this IWebDriver driver, By elementToClick)
    {
        if (driver.IsElementDisplayed(elementToClick))
        {
            driver.FindElement(elementToClick).Click();
        }
    }

    /// <summary>
    /// If one element is displayed another specified element is clicked, if it is not displayed, nothing happens
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="elementToClick">Element to click</param>
    /// <param name="elementDisplayed">Element that needs to be displayed</param>
    public static void ClickIfElementIsDisplayed(this IWebDriver driver, By elementToClick, By elementDisplayed)
    {
        if (driver.IsElementDisplayed(elementDisplayed))
        {
            driver.FindElement(elementToClick).Click();
        }
    }
        

    /// <summary>
    /// Clicks on element with given text
    /// </summary>
    /// <param name="driver">WebDriver</param>
    /// <param name="elementText">text of element</param>
    public static void ClickElementWithText(this IWebDriver driver, string elementText)
    {
        IList<IWebElement> elements = driver.FindElements(By.XPath("//*[contains(text()[not(parent::script)],\"" + elementText + "\")]"));

        //in case there are more elements with given text, we need to try to click all of the one by one until we find one that works
        if (elements.Count > 1)
        {
            foreach (var element in elements.Where(e => e.Displayed))
            {
                try
                {
                    element.ForceClick();
                    break;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    continue;
                }
            }

        }
        else
        {
            driver.WaitUntilElementClickable(By.XPath("//*[contains(text()[not(parent::script)],\"" + elementText + "\")]"));
            driver.FindElement(By.XPath("//*[contains(text()[not(parent::script)],\"" + elementText + "\")]")).Click();
        }
    }
}
