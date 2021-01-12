using ScraperApp;
using System;
using System.Collections.Generic;


namespace ConsoleApp
{
    partial class Program
    {
         static void Main(string[] args)
        {
            try
            {
                int resultCharCount = 0;                

                IParseConsoleParams parseParam = new ParseConsoleParamsImpl("-F", "-C", "-E", "-L");
                parseParam.LoadParams(args);

                ICountEngine engine = new CountEngine();
                resultCharCount = engine.CountCharsFiles(parseParam, out int? resultWordsCapital);

                Console.Write($"Total count of characters in files is: {resultCharCount} ");
                if (resultWordsCapital.HasValue)
                    Console.WriteLine($", total count of words starting with Capiral letter is {resultWordsCapital}.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
    }
}
