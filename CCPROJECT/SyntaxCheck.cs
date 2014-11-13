using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCPROJECT
{
    partial class SyntaxCheck
    {
        static int CurrentLine=-1;
        static string[] TokensArr=null;

        public SyntaxCheck()
        {

            ReadTokensFromFile rt = new ReadTokensFromFile();

            TokensArr = ReadTokensFromFile.CP.ToArray();

            Starting();
        
        
        }


       
      
       
        
        
        }

    }

