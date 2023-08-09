using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLetterProcessing
{
    interface ILetterService
    {
         void CombinedTwoLetter(string inputFile1, string inputFile2, string rsultFile, out StringBuilder fileids);
    }
}
