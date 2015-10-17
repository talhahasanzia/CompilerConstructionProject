using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CCPROJECT
{
    class ReadTokensFromFile
    {

        public static List<int> LineNumber = new List<int>();
        public static List<string> CP = new List<string>();
        public static List<string> VP = new List<string>();
       public ReadTokensFromFile()
        {
            CP.Clear();
            VP.Clear();

          
           


            // File pointer
            FileStream fs = new FileStream(@"../../Token.txt", FileMode.Open);

            // FileReader Class
            StreamReader sr = new StreamReader(fs);

            // Temp string line
            string line = sr.ReadLine();

            // Read while end of file or null
            while (line != null)
            {
                // read a line
                line = sr.ReadLine();

                if (line == null)
                    break;
                // prints on screen

                Console.WriteLine(line);

                // Extract the class part from line save it to CPFromFile variable
                string CPFromFile = GetCPFromFile(line);   // GetCPFromFile function
                CP.Add(CPFromFile);        // Add CP to list

                // Extract the value part from line save it to VPFromFile variable
                string VPFromFile = GetVPFromFile(line);
                VP.Add(VPFromFile);


                // Extract the line number from line save it to CPFromFile variable
                int LineNumberFromFile = GetLineNumberFromFile(line);   // GetCPFromFile function
                LineNumber.Add(LineNumberFromFile);






            }
            sr.Close();
            fs.Close();

            VP.Add("$");
            CP.Add("$");
        
        }
        public static string GetCPFromFile(string str)
        {
            // temp string
            string CPFromFile = null;
            // string to character array conversion
            char[] CharArray = str.ToCharArray();
            int i = 1;


            // Read until first comma "," occurs
            while (CharArray[i] != ',')
            {
                // Whatever character comes add it to current string
                CPFromFile += Convert.ToString(CharArray[i]);
                i++;

            }

          

            // When finished , means comma occured , while loop ended
            // Return the string we build by character by character reading
            return CPFromFile;
        }


        public static string GetVPFromFile(string str)
        {

            // temp string
            string VPFromFile = null;
            bool FirstComma = false;
            // string to character array conversion
            char[] CharArray = str.ToCharArray();
            int i = 1;


            // Read until first comma "," occurs
            while (CharArray[i] != ',')
            {
                // Whatever character comes add it to current string
                i++;

            }

            if (CharArray[i] == ',')
            {
                i++;
                FirstComma = true;

            }
            int StartIndex = i;

            if (FirstComma)
            {

                while (CharArray[i] != ',')
                {
                    // Whatever character comes add it to current string
                    VPFromFile += Convert.ToString(CharArray[i]);
                    i++;

                }
            
            }
            // When finished , means comma occured , while loop ended
            // Return the string we build by character by character reading
            return VPFromFile;
        
        
        }
        // We check same here but difference is , as line number is after second comma we firts check for second comma
        // then start saveing integers of line numbers
        // and finish when close bracket of the token is encountered

        public static int GetLineNumberFromFile(string str)
        {

            string Temp = null;
            int lineN = 0;
            int i = 1;
            char[] CharArray = str.ToCharArray();
            int CommaCount = 0;
            while (CommaCount != 2)
            {
                // Whatever character comes add it to current string
                if (CharArray[i] == ',')
                    CommaCount++;
                i++;

            }
            while (CharArray[i] != ')')
            {
                // Whatever character comes add it to current string
                Temp += Convert.ToString(CharArray[i]);
                i++;

            }
            lineN = int.Parse(Temp);
            return lineN;

        }

    }
}
