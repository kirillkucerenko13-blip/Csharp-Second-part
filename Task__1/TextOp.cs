using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task__1;
using System.IO;

namespace Task__1
{
    public delegate string TextOp(string text);
    public  class TextOperation
    {
        public string ToUpperCase(string text)
        {
            return text.ToUpper();
        }

        public static string CountSimbols(string text)
        {
          
            return $"CountSimbols{text.Length}";
        }

        public static string CountWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "CountWords0";

            int wordCount = text
                .Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Length;
            return $"CountWords{wordCount}";
        }
        public static void ProcessFile(string inputFile, string outputFile, TextOp operation)
        {

            string text = File.ReadAllText(inputFile);


            string result = operation(text);

            using (StreamWriter sw = new StreamWriter(outputFile, append: true))
            {
                sw.WriteLine(result);
            }

        }
       
        
       }


}

