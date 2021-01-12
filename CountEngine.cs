using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ScraperApp
{
    class CountEngine : ICountEngine
    {
        public int CountCharsInFile(string strFileName, HashSet<string> exclusions, bool isCountCapitalWrds, out int? countCapitalWrds)        
        {
            if (!File.Exists(strFileName))
            {
                throw new Exception($"The file:{strFileName} doesn't exist.");
            }
            string content = File.ReadAllText(strFileName);

            return CountCharsInString(content, exclusions, isCountCapitalWrds, out countCapitalWrds);

        }        

        public int CountCharsInString(string strData, HashSet<string> exclusions, bool isCountCapitalWrds, out int? countCapitalWrds)
        {
            string[] words = strData.Split(' ');
            int result = 0;
            int? capitalWordsCnt = 0;

            foreach(var word in words)
            {
                if (isCountCapitalWrds)
                {
                    var firstChar= word.ToCharArray()[0];

                    if(firstChar >= 'A' && firstChar <= 'Z')
                        capitalWordsCnt++;
                }

                if(exclusions.Contains(word))                
                    continue;                

                result++;
            }
            if (isCountCapitalWrds)
                countCapitalWrds = capitalWordsCnt;
            else
                countCapitalWrds = null;




            return result;
        }

        public int CountCharsFiles(IParseConsoleParams parseParam, out int? resultWordsCapital)
        {            
            int resultCharCount = 0;
            int? wordCount = 0;
            foreach (var file in parseParam.GetFileNames())
            {                
                var cnt = CountCharsInFile(file, parseParam.GetExclusions() ,parseParam.IsCapitalLetterCount, out int? countCapitalWords);

                Console.Write($"In file {file} chars: {cnt}");
                if (parseParam.IsCapitalLetterCount)
                    Console.WriteLine($", words with Capital letter: {countCapitalWords.Value}");

                if (parseParam.IsCapitalLetterCount && countCapitalWords.HasValue)
                {
                    wordCount += countCapitalWords.Value;
                }

                resultCharCount += cnt;
            }
            if (parseParam.IsCapitalLetterCount)
                resultWordsCapital = wordCount;
            else resultWordsCapital = null;

            return resultCharCount;
        }

        
    }
}
