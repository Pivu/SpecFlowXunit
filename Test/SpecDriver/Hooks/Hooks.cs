using NLog;
using OpenQA.Selenium;
using Pages;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;

namespace Hooks
{
    [Binding]
    public class Hooks : Steps
    {
        string ScreenshotsPath = ConfigurationManager.AppSettings["ScreenshotsPath"];

        private readonly IWebDriver driver;

        public Hooks(IWebDriver driver)
        {
            this.driver = driver;
        }


        [AfterScenario]
        public void AfterScenario()
        {
            string DebugLogName = GetDebugLogName("DebugLog");
            string traceFileName = GetDebugLogName("SpecFlowLog");
            string[] DebugLogs = {traceFileName, DebugLogName};
            string screenshotName = this.ScenarioContext.ScenarioInfo.Title + "-" + Helper.GetTimestamp(DateTime.Now);
            
            if (File.Exists(DebugLogName) && File.Exists(traceFileName))
            {
                CombineDebugLogs(DebugLogs);
            }
            
            if (this.ScenarioContext.TestError != null)
            {
                if (File.Exists(traceFileName))
                {
                    System.IO.File.Move(traceFileName, ScreenshotsPath + screenshotName + ".txt");
                }
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();

                try
                {
                    ss.SaveAsFile(ScreenshotsPath + screenshotName + ".png", System.Drawing.Imaging.ImageFormat.Png);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("{0} Exception caught.", e);
                }
            }
            else
            {
                if (File.Exists(traceFileName))
                {
                    System.IO.File.Move(traceFileName, "passedLogs\\" + screenshotName + ".txt");
                }
            }

            driver.Quit();
        }

        public string GetDebugLogName(string targetName)
        {
            string rtnVal = string.Empty;

            if (LogManager.Configuration != null && LogManager.Configuration.ConfiguredNamedTargets.Count != 0)
            {
                NLog.Targets.Target t = LogManager.Configuration.FindTargetByName(targetName);
                if (t != null)
                {
                    NLog.Layouts.Layout layout = (t as NLog.Targets.FileTarget).FileName;
                    rtnVal = layout.Render(LogEventInfo.CreateNullEvent());
                }
            }

            return rtnVal;
        }

        public void CombineDebugLogs(string[] fileNames)
        {
            string[] Filename = fileNames;

            string text = "";
            int count = 1;
            while (count <= Filename.Length)
            {
                text += System.IO.File.ReadAllText(Filename[count++ - 1], Encoding.GetEncoding("iso-8859-1"));
                text += Environment.NewLine;;
            }
                

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter(Filename[0]);
            file.Write(text);
            file.Close();

            //Delete the file
            count = 1;
            while (count < Filename.Length)
                File.Delete(Filename[count++]);
        }
    }
}
