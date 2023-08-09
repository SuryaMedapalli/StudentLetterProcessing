using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace StudentLetterProcessing
{
    public class LetterService : ILetterService
    {
        public static StringBuilder studentfileIds = new StringBuilder();
        int counter = 0;

        //Combine two letter files into one file and save new file to output folder
        public void CombinedTwoLetter(string inputFile1, string inputFile2, string rsultFile, out StringBuilder fileIds)
        {
            
            if (!string.IsNullOrEmpty(inputFile1) && !string.IsNullOrEmpty(inputFile2))
            {
                string outputFolderPath = rsultFile;
                FileInfo admfile = new FileInfo(inputFile1);
                FileInfo schfile = new FileInfo(inputFile2);
                //merge two files for the same student 

                //get administration file student id
                string admfilename = admfile.Name;
                string schfilename = schfile.Name;

                string admFileStudentId = admfilename.Substring(admfilename.LastIndexOf('-'), 5).Substring(1).ToString();

                //get scholarship file student id
                string schFileStudentId = schfilename.Substring(schfilename.LastIndexOf('-'), 5).Substring(1).ToString();

                if (admFileStudentId.Equals(schFileStudentId))
                {
                    //merge and saving to output folder
                    File.WriteAllText(rsultFile + admFileStudentId + ".txt", File.ReadAllText(inputFile1) + Environment.NewLine + Environment.NewLine + File.ReadAllText(inputFile2));

                    // Archieving files
                    File.Move(inputFile1, ConfigurationManager.AppSettings["archiveFilePath"].ToString() + admfilename);
                    File.Move(inputFile2, ConfigurationManager.AppSettings["archiveFilePath"].ToString() + schfilename);

                    studentfileIds.Append(inputFile1.Substring(inputFile1.LastIndexOf('-'), 9).Substring(1).ToString() + Environment.NewLine);
                    studentfileIds.Append(inputFile2.Substring(inputFile2.LastIndexOf('-'), 9).Substring(1).ToString() + Environment.NewLine);

                    Program.noOfFilesProcessed = Program.noOfFilesProcessed + 2;
                }
            }
            fileIds = studentfileIds;
        }
    }
}
