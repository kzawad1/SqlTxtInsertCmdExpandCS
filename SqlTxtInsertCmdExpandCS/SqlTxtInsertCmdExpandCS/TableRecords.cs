using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SqlTxtInsertCmdExpandCS
{
    class TableRecords
    {
        static Dictionary<string, TableRecordEntry> _hash = new Dictionary<string, TableRecordEntry>();

        public static void AddTableFromDefintion(string strSqlTableDefEntry)
        {
            string strTableData = "";
            string strTableName = "";
            string strTableDefPrefix = "";

            string pattern = @"CREATE TABLE `([a-zA-Z].*)` ";
            Match match = Regex.Match(strSqlTableDefEntry, pattern);
            if (match.Success)
            {
                strTableName = match.Groups[1].Value;

                strTableDefPrefix = "\r\nCREATE TABLE `" + strTableName + "` (\r\n  ";
                strTableData = strSqlTableDefEntry.Replace(strTableDefPrefix, "");

                TableRecordEntry curTblEntry = new TableRecordEntry();
                strTableData = strTableData.Replace("\n  ", "");
                curTblEntry.StrItemArray = strTableData.Split('\r');
                curTblEntry.CalculateMaxStrLen();

                _hash.Add(strTableName, curTblEntry);
            }
            else
            {
                pattern = @"CREATE TABLE ([a-zA-Z].*)\(";
                match = Regex.Match(strSqlTableDefEntry, pattern);
                if (match.Success)
                {
                    strTableName = match.Groups[1].Value;

                    strTableDefPrefix = "\r\nCREATE TABLE " + strTableName + "(\r\n   ";
                    strTableData = strSqlTableDefEntry.Replace(strTableDefPrefix, "");

                    TableRecordEntry curTblEntry = new TableRecordEntry();
                    strTableData = strTableData.Replace("\n   ", "");
                    curTblEntry.StrItemArray = strTableData.Split('\r');
                    curTblEntry.CalculateMaxStrLen();

                    _hash.Add(strTableName, curTblEntry);
                }
            }
        }

        public static string ParseInsertStatement(string strSqlInsertEntry)
        {
            string strSqlInsertEntryNew = "";
            string strTblInsertData = "";
            string strTblInsertTableName = "";

            string pattern = @"INSERT INTO ""([a-zA-Z].*)"" ";
            Match match = Regex.Match(strSqlInsertEntry, pattern);
            if (match.Success)
            {
                // SYNTAX 1 ---------------------------------------------------
                strTblInsertTableName = match.Groups[1].Value;

                string strFind = "\r\nINSERT INTO \"" + strTblInsertTableName + "\" VALUES(";
                strTblInsertData = strSqlInsertEntry.Replace(strFind, "");

                bool bContainsTbl = _hash.ContainsKey(strTblInsertTableName);

                strSqlInsertEntryNew += strFind;
                strSqlInsertEntryNew += "\n"; // "\r\n"

                string[] strValues = strTblInsertData.Split(',');
                int iCount = 0;
                foreach (string s in strValues)
                {
                    string strNewEntry = "";
                    string strDesc = "";
                    if (true == bContainsTbl)
                    {
                        strDesc = _hash[strTblInsertTableName].StrItemArray[iCount];
                    }

                    if (0 != iCount)
                    {
                        strSqlInsertEntryNew += ",";
                        strSqlInsertEntryNew += "\n";  // "\r\n"
                    }

                    strNewEntry = "/* " + iCount.ToString().PadLeft(3, ' ') + ": " + strDesc + " */ " + s;
                    strSqlInsertEntryNew += strNewEntry;

                    iCount++;
                }

                strSqlInsertEntryNew += ";";
            }
            else
            {
                // SYNTAX 2 ---------------------------------------------------
                pattern = @"INSERT INTO ([a-zA-Z].*) VALUES";
                match = Regex.Match(strSqlInsertEntry, pattern);
                if (match.Success)
                {
                    strTblInsertTableName = match.Groups[1].Value;

                    string strFind = "\r\nINSERT INTO " + strTblInsertTableName + " VALUES(";
                    strTblInsertData = strSqlInsertEntry.Replace(strFind, "");

                    bool bContainsTbl = _hash.ContainsKey(strTblInsertTableName);

                    strSqlInsertEntryNew += strFind;
                    strSqlInsertEntryNew += "\n"; // "\r\n"

                    string[] strValues = strTblInsertData.Split(',');
                    int iCount = 0;
                    foreach (string s in strValues)
                    {
                        string strNewEntry = "";
                        string strDesc = "";
                        if (true == bContainsTbl)
                        {
                            strDesc = _hash[strTblInsertTableName].StrItemArray[iCount];
                        }

                        if (0 != iCount)
                        {
                            strSqlInsertEntryNew += ",";
                            strSqlInsertEntryNew += "\n";  // "\r\n"
                        }

                        strNewEntry = "/* " + iCount.ToString().PadLeft(3, ' ') + ": " + strDesc + " */ " + s;
                        strSqlInsertEntryNew += strNewEntry;

                        iCount++;
                    }

                    strSqlInsertEntryNew += ";";
                }
            }

            return strSqlInsertEntryNew;
        }

        public static void WriteHashMapToConsole()
        {
            foreach (KeyValuePair<string, TableRecordEntry> pair in _hash)
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Table Name: " + pair.Key);
                TableRecordEntry currTblEntry = pair.Value;
                int iCount = 0;
                foreach (string s in currTblEntry.StrItemArray)
                {
                    Console.WriteLine(iCount.ToString().PadLeft(3, ' ') + ": " + s);
                    iCount++;
                }
            }
        }

        public class TableRecordEntry
        {
            public int MaxColumnCharLen { get; set; }
            public string[] StrItemArray { get; set; }

            public void CalculateMaxStrLen()
            {
                MaxColumnCharLen = 0;
                foreach (string s in StrItemArray)
                {
                    if (s.Length > MaxColumnCharLen)
                    {
                        MaxColumnCharLen = s.Length;
                    }
                }
            }

            public void PadStringArray()
            {
                for (int iIndex = 0; iIndex < StrItemArray.Count(); iIndex++)
                {
                    string strLocal = StrItemArray[iIndex];
                    int iCurrStrSize = strLocal.Length;
                    if (iCurrStrSize < MaxColumnCharLen)
                    {
                        Console.WriteLine(strLocal);

                        int iPadLen = (MaxColumnCharLen - iCurrStrSize);

                        strLocal = strLocal.PadRight(iPadLen, '.');
                        StrItemArray[iIndex] = strLocal;
                    }
                }
            }
        };

    }
}
