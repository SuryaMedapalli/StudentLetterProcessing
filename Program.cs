using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLetterProcessing
{
    class Program
    {
        public static int noOfFilesProcessed=0;
        static void Main(string[] args)
        {

            try
            {
                //get student administration & scholarship files from input folder
                string[] folders = System.IO.Directory.GetDirectories(ConfigurationManager.AppSettings["inputFilePath"].ToString(), "*", System.IO.SearchOption.AllDirectories);
                string[] adminstrationFiles = Directory.GetFiles(folders[2]);

                string inputFile1 = "";
                string inputFile2 = "";

                StringBuilder studentfileIds = new StringBuilder();

                foreach (string adminFile in adminstrationFiles)
                {
                    string[] scholarshipFiles = new string[] { };
                    FileInfo admfile = new FileInfo(adminFile);
                    inputFile1 = admfile.FullName.ToString();

                    scholarshipFiles = Directory.GetFiles(folders[3]);
                    if (scholarshipFiles.Count() > 0)
                    {
                        foreach (var scholarFile in scholarshipFiles)
                        {
                            FileInfo schfile = new FileInfo(scholarFile);
                            inputFile2 = schfile.FullName.ToString();
                            break;
                        }
                    }
                    else
                    {
                        inputFile2 = "";
                    }
                    LetterService service = new LetterService();
                    service.CombinedTwoLetter(inputFile1, inputFile2, ConfigurationManager.AppSettings["outputFilePath"].ToString(), out studentfileIds);
                }
                //text report
                File.WriteAllText(ConfigurationManager.AppSettings["outputFilePath"].ToString() + "text report.txt", DateTime.Now + " Report" + Environment.NewLine + "---------------------" + Environment.NewLine + Environment.NewLine + "No Of Combined Letters: " + noOfFilesProcessed + Environment.NewLine + studentfileIds);

            }
            catch (Exception ex)
            { }
        }
    }
}
