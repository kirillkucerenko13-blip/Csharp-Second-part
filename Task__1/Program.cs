using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task__1
{
    internal class Program :TextOperation
    {
        static void Main(string[] args)
        {
            string InputFile = ("C:\\Users\\kiril\\OneDrive\\Рабочий стол\\textPD21.txt");
            string OutputFile = ("C:\\Users\\kiril\\OneDrive\\Рабочий стол\\resultPD21.txt");

            ProcessFile(InputFile, OutputFile, new TextOperation().ToUpperCase);
            ProcessFile(InputFile, OutputFile, CountSimbols);
            ProcessFile(InputFile, OutputFile, CountWords);


        }



    }
}
