using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace CCPROJECT
{
    class TokenReader
    {

        List<string> Tokens = new List<string>();

        public TokenReader()
        {
            FileStream fs = new FileStream(@"../../Token.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            try
            {
               

                string str = sr.ReadLine();
                Tokens.Add(str);

                while (str != null)
                {

                    str = sr.ReadLine();
                    Tokens.Add(str);


                }
                sr.Close();
                fs.Close();
                
            }
            catch (Exception ex)
            {
                sr.Close();
                fs.Close();
            }
        
        
        
        }


        public string[] GetTokensArray()
        {

            string[] Tokens_Array = Tokens.ToArray();
            return Tokens_Array;
        
        
        
        }




    }
}
