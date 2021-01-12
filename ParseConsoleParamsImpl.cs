using System;
using System.Collections.Generic;
using System.Text;


namespace ScraperApp
{
    public class ParseConsoleParamsImpl : IParseConsoleParams
    {
        List<string> _fileNames;
        HashSet<string> _exclusionSet;
        string _exclusions;
        bool _isLoaded = false;
        public bool IsCapitalLetterCount { get; private set; } = false;

        readonly string _fileNameCommamd;
        readonly string _countNameCommand;
        readonly string _exclusionNameCommand;
        readonly string _capitalNameCommand;
        const string _equalStr = "=";

        public ParseConsoleParamsImpl(string fileStr, string countStr, string exclusionStr, string capitalStr)
        {
            _fileNameCommamd = fileStr;
            _countNameCommand = countStr;
            _exclusionNameCommand = exclusionStr;
            _capitalNameCommand = capitalStr?? "-L";
        }

        public HashSet<string> GetExclusions()
        { 
            if (!_isLoaded)
                throw new Exception("Parameters not been loaded. Please, firstable execute method LoadParams");

            return _exclusionSet;
        }

        public List<string> GetFileNames()
        {
            if (!_isLoaded)
                throw new Exception("Parameters not been loaded. Please, firstable execute method LoadParams");

            return _fileNames;
        }       

        public void LoadParams(string[] args)
        {
            bool isFNParamExist = false;
            bool isCountParamExist = false;
            bool isExclusionExist = false;

            foreach(var a in args)
            {
                var lwCase = a.ToLower();

                if (lwCase.StartsWith(_fileNameCommamd.ToLower() + _equalStr))
                {
                    string fn = lwCase.Substring(_fileNameCommamd.Length + _equalStr.Length);
                    _fileNames = new List<string>(fn.Split(','));
                    isFNParamExist = true;
                }
                else if (lwCase.StartsWith(_countNameCommand.ToLower()))
                {
                    isCountParamExist = true;
                }
                else if (lwCase.StartsWith(_exclusionNameCommand.ToLower() + _equalStr))
                {
                    string excls = lwCase.Substring(_exclusionNameCommand.Length + _equalStr.Length);
                    _exclusionSet = new HashSet<string>(excls.Split(','));
                    isExclusionExist = true;
                }
                else if (lwCase.StartsWith(_capitalNameCommand.ToLower()))
                {
                    IsCapitalLetterCount = true;
                }
                else 
                {
                    throw new Exception($"This parameter: {a} doesn't support by app.");
                }
            }


            if (isCountParamExist && isExclusionExist && isFNParamExist)
                _isLoaded = true;
            else
            {
                throw new Exception($"Some of necessary parameters (-F,-C or -E) weren't been pass or not correctly passed."+
                    "\r\n See example: Scraper.exe -F=sample.txt,sample2.txt -C -E=a,the,on,at,any");
            }
        }
    }
}
