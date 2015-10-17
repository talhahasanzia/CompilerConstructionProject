using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCPROJECT
{
    partial class SyntaxCheck
    {
       public static string MessageString=null;
        static string[] TokensArr=null;

        public SyntaxCheck()
        {

            ReadTokensFromFile rt = new ReadTokensFromFile();

            TokensArr = ReadTokensFromFile.CP.ToArray();
            CurrentIndex = 0;
           // Starting();

            bool temp =Starting(); ;

            if (temp)
                MessageString = "Successful Syntax Parsing.";
            else
                MessageString = "Syntax Error.";
        
        }


       
      
       
        
        
        }

    }

