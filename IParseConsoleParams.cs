using System.Collections.Generic;
//using System.Data.



namespace ScraperApp
{
    public interface IParseConsoleParams
    {
        void LoadParams(string[] args);
        public List<string> GetFileNames();
        HashSet<string> GetExclusions();

        bool IsCapitalLetterCount { get; }
    }
    
}
