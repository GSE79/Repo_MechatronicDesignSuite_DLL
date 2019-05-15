using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechatronicDesignSuite_DLL.BaseTypes
{
    public class XPlatAutoGEN
    {
        int NumberColumns;
        int NumberRows;
        List<List<string>> InputTokens;
        List<string> OutputLines;

        public XPlatAutoGEN(int numCols)
        {
            NumberColumns = numCols;
            NumberRows = 0;
            InputTokens = new List<List<string>>();
            OutputLines = new List<string>();
        }
        public void AddLineTokens(List<string> LineTokens)
        {
            if(LineTokens.Count == NumberColumns)
            {
                InputTokens.Add(LineTokens);
                NumberRows++;
            }
        }
        public void AlignColumnsInputTokens()
        {
            List<int> ColumnWidths = new List<int>(NumberColumns);
            while (ColumnWidths.Count < NumberColumns)
                ColumnWidths.Add(0);
            foreach (List<string> LineTokenList in InputTokens)
            {
                for (int i = 0; i < LineTokenList.Count; i++)
                    if (LineTokenList[i].Length > ColumnWidths[i])
                    {
                        ColumnWidths[i] = getTokenLength(LineTokenList[i]);
                    }
            }
            foreach (List<string> LineTokenList in InputTokens)
            {
                OutputLines.Add(buildOutputLine(ColumnWidths, LineTokenList));
            }
        }
        private int getTokenLength(string TokenIn)
        {
            string tempString = TokenIn.TrimStart('\t');
            tempString = tempString.TrimEnd('\t');
            if (tempString.Contains("\n"))
                return (tempString.Substring(tempString.LastIndexOf("\n")+1)).Length-4;
            else
                return tempString.Length;

        }
        private string buildOutputLine(List<int> ColumnWidthsIn, List<string> LineTokenListIn)
        {
            string outstring = "";
            for(int tokenIndex=0; tokenIndex<NumberColumns; tokenIndex++)
            {
                LineTokenListIn[tokenIndex] = LineTokenListIn[tokenIndex].TrimEnd('\t');
                LineTokenListIn[tokenIndex] = LineTokenListIn[tokenIndex].TrimStart('\t');
                LineTokenListIn[tokenIndex] = LineTokenListIn[tokenIndex].Replace('\t', ' ');
                
                if(tokenIndex<(NumberColumns-1))
                    while (getTokenLength(LineTokenListIn[tokenIndex]) < (ColumnWidthsIn[tokenIndex]))
                        LineTokenListIn[tokenIndex] += ' ';

                outstring += "\t" + LineTokenListIn[tokenIndex];
            }
            return outstring;
        }
        public string ReturnOutputLines()
        {
            string outstring = "";
            foreach (string lineString in OutputLines)
                outstring += lineString + "\n";
            return outstring;
        }
    }
}
