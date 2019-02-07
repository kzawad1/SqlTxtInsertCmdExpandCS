using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.IO;

namespace SqlTxtInsertCmdExpandCS
{
    class Program
    {

        static void WriteLineToFile(ref string sOutputFile, ref string sLine)
        {
            TextWriter tw = new StreamWriter(sOutputFile, true);
            tw.Write(sLine);
            tw.Close();
        }

        static void ExpandInsertIntoNewFile(ref string sqlFileAsText, ref string sOutputFile, ref int iTotalInserts)
        {
            string[] sqlCmdTextArray;
            uint uiIndex = 0;
            bool bIsParseLine = false;
            string sNewLine = "";

            sqlCmdTextArray = sqlFileAsText.Split(';');
            foreach (string s in sqlCmdTextArray)
            {
                bIsParseLine = false;

                if (s.Contains("CREATE TABLE"))
                {
                    TableRecords.AddTableFromDefintion(s);
                }
                else if (s.Contains("INSERT INTO"))
                {
                    sNewLine = TableRecords.ParseInsertStatement(s);
                    bIsParseLine = true;
                    iTotalInserts++;
                }
                else
                {
                    //Console.WriteLine("COMMAND: UNKNOWN/Not of Interest");
                }

                uiIndex++;

                if (false == bIsParseLine)
                {
                    sNewLine = s + ";";
                }

                WriteLineToFile(ref sOutputFile, ref sNewLine);
            }
        }

        static void ReadSqlFileAsText(ref string filePathNameIn, ref string fileAsTextOut)
        {
            string textLine = "";

            if (true == System.IO.File.Exists(filePathNameIn))
            {
                System.IO.StreamReader objReader;
                objReader = new System.IO.StreamReader(filePathNameIn);

                do
                {
                    textLine = objReader.ReadLine() + "\r\n";
                    fileAsTextOut += textLine;
                } while (objReader.Peek() != -1);

                objReader.Close();
            }
            else
            {
                Console.WriteLine("Cound not open file: " + filePathNameIn);
            }
        }

        static string CreateOutFileName(string sFileInput)
        {
            string sFileOutput = "";
            string sFileExt = "";
            string sTimeStamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");

            sFileExt = Path.GetExtension(sFileInput);
            if (".db" == sFileExt.ToLower())
            {
                sFileOutput = sFileInput.Replace(sFileExt, "." + sTimeStamp + sFileExt);
            }
            else if (".sql" == sFileExt.ToLower())
            {
                sFileOutput = sFileInput.Replace(sFileExt, "." + sTimeStamp + sFileExt);
            }
            else
            {
                Console.WriteLine("sFileExt: " + sFileExt);
                sFileOutput = sFileInput + "." + sTimeStamp;
            }

            return sFileOutput;
        }

        static void Main(string[] args)
        {
            string sqlFileAsText = "";
            string sFileInput = "";
            string sFileOutput = "";
            int iTotalInserts = 0;

            Console.WriteLine("SQL Text File (*.sql) 'INSERT' Statement Expander");
            Console.WriteLine(" PURPOSE: Make DIFF's easier.");
            Console.WriteLine(" ");

            // A parameter must be passed with the file name, otherwise exit
            if (args.Length > 0)
            {
                sFileInput = args[0];
                sFileOutput = CreateOutFileName(sFileInput);

                Console.WriteLine("Input File:  " + sFileInput);
                Console.WriteLine("Output File: " + sFileOutput);
                Console.WriteLine("Working....");

                // Read the *.sql file into one big string
                ReadSqlFileAsText(ref sFileInput, ref sqlFileAsText);

                // Write to the console
                ExpandInsertIntoNewFile(ref sqlFileAsText, ref sFileOutput, ref iTotalInserts);

                Console.WriteLine("Finished with Parsing, expanded {0} INSERT statements.", iTotalInserts);
            }
        }
    }
}
