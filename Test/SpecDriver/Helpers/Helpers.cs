using NLipsum.Core;
using System;
using System.Diagnostics;
using System.Text;

//Methods that might be helpful, like generating random words, etc.
public static class Helper
{
    private static LipsumGenerator lipsum = new LipsumGenerator();

    public static string GenerateRandomWords(int numberOfWords)
    {
        string randomWords = ConvertStringArrayToString(lipsum.GenerateWords(numberOfWords));
        return randomWords.Trim();
    }

    public static string GenerateRandomSentences(int numberOfSentences)
    {
        string randomWords = ConvertStringArrayToString(lipsum.GenerateSentences(numberOfSentences));
        return randomWords.Trim();
    }

    public static string GenerateRandomNumber(int minimum, int maximum)
    {
        Random rnd = new Random();
        int randomNumber = rnd.Next(minimum, maximum + 1);
        return randomNumber.ToString();
    }

    static string ConvertStringArrayToString(string[] array)
    {
        StringBuilder builder = new StringBuilder();
        foreach (string value in array)
        {
            builder.Append(value);
            builder.Append(' ');
        }
        return builder.ToString();
    }

    public static String GetTimestamp(DateTime value)
    {
        return value.ToString("yyyy-MM-dd-HH.mm.ss.ffff");
    }

    public static void LogIntoOutput(string messageToLog)
    {
        Debug.WriteLine(messageToLog);
    }
}