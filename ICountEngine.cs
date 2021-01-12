using System;
using System.Collections.Generic;
using System.Text;

namespace ScraperApp
{
    interface ICountEngine
    {       
        int CountCharsFiles(IParseConsoleParams parseParam, out int? resultWordsCapital);        
        int CountCharsInFile(string strFileName, HashSet<string> exclusions, bool isCountCapitalWrds, out int? countCapitalWrds);
        int CountCharsInString(string strData, HashSet<string> exclusions, bool isCountCapitalWrds, out int? countCapitalWrds);
    }
}
