using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CCPROJECT
{
    class LexicalAnalyze
    {
        #region Keywords
        string[,] keywords = new string[,]{
            
            {"MAIN","Main"},
            {"INTEGER","DT"},
            {"FLOAT","DT"},
            {"CHARAC","DT"},
            {"STRING","DT"},
            {"IS","Assignment"},
            {"RETURN","Return"},
            {"TERMINATE","Break"},
            {"NEXT","Continue"},
            {"NOT","Unary"},
            {"EQUALS","Relational"},
            {"LESS","Relational"},
            {"GREAT","Relational"},
            {"ELESS","Relational"},
            {"EGREAT","Relational"},
            {"AND","Conditional"},
            {"OR","Conditional"},
            {"FUNCTION","Function"},
            {"CLASS","Class"},
            {"CHECK","Check"},
            {"FROM","From"},
            {"IF","If"},
            {"THEN","Then"},
            {"ELSE","Else"},
            {"UNTIL","Until"},
            {"RUN","Run"},
            {"TERMINATE","Break"},
            {"NEXT","Continue"},
            {"VOID","Void"},
            {"ERROR","Error"},
            {"+","AddSubOp"},
            {"-","AddSubOp"},
            {"*","MulDivOp"},
            {"/","MulDivOp"},
            {"++","Inc_Dec"},
            {"--","Inc_Dec"},
            {".","LineBreak"},
            {"{", "OpenScope"},
            {"}","CloseScope"},
            {"(","OpenParenthesis"},
            {")","CloseParenthesis"},
            {">","Member"},
            {":","Colon"},
            
            
    };
        #endregion

        bool FlagCheck;
        char[] LineArr,TempArr,TempArray;
        int Counter,TempACount;
        

        List<string> WordsList = new List<string>();
        List<string> LinesList = new List<string>();
        bool FirstRead = true;
        string Text;
        int Current_Line_Number = 0;
        int Total_Lines = 1;

        public LexicalAnalyze(string In_Text)
        {
            StreamWriter sr = new StreamWriter(@"../../Token.txt");
            try
            {
              
                if (!String.IsNullOrEmpty(In_Text))
                {

                    sr.WriteLine("Tokens From File:");
                    sr.Close();
                    GetLinesArray();
                    Text = In_Text;
                    GetTotalLines();
                    GetLexicalAnalysis();
                }
                sr.Close();
            }
            catch (Exception ex)
            {

                Form1.MessageString = "Please reload the file. Exception: \n" + ex.Message.ToString();
                sr.Close();
            }
        }

        void GetTotalLines()
        {

            char[] TextArray = Text.ToCharArray();
            int i = 0;

            try
            {



                while (TextArray[i] != null)
                {
                    if (TextArray[i] == '\n')
                        Total_Lines++;
                    i++;


                }

            }
            catch (IndexOutOfRangeException ex)
           {

                System.Diagnostics.Debug.WriteLine(ex.ToString());

            }

        }

        void GetLinesArray()
        {

            StreamReader sr = new StreamReader(@"../../Source.txt");

            string str=sr.ReadLine();
            LinesList.Add(str);

            while (str != null)
            {


                str = sr.ReadLine();
               LinesList.Add(str);
            
            }

            sr.Close();
        }

    
        public void GetLexicalAnalysis()
        {
            int i = 0;

            while (i<LinesList.Count)
            {


                string temp = LinesList.ElementAt(i);
                if (String.IsNullOrEmpty(temp))
                {

                    i++;
                }
                else
                {
                    Current_Line_Number = i + 1;
                    CheckCurrentLine(temp);

                    i++;
                }

            }


        }


        void CheckCurrentLine(string Current_Line_Text)
        {
            int count=0;
            string[,] Token = new string[100, 3]; 
            
            string[] Words = GetCurrentWords(Current_Line_Text);
            FirstRead = true;
           
            
            foreach (string str in Words)
            {


                //First Check For Lexical Error
                if (str[0] == 'L' && str[1] == 'E' && str[2] == '_')
                {


                    Token[count, 0] = "Lexical Error";
                    Token[count, 1] =str;
                    Token[count, 2] = Convert.ToString(Current_Line_Number);
                    count++;
                    continue;

                }
                else
                {
                    // Check for string Constant

                    if(str[0]=='\\')
                    {


                      string   TempStr=str.Remove(0, 1);

                      Token[count, 0] = "STRING_CONST";
                      Token[count, 1] = TempStr;
                      Token[count, 2] = Convert.ToString(Current_Line_Number);
                      count++;
                      continue;
                    }
                    else if (str[0] == '/') // Check for Character Constant
                    {


                        string TempStr = str.Remove(0, 1);

                        Token[count, 0] = "CHAR_CONST";
                        Token[count, 1] = TempStr;
                        Token[count, 2] = Convert.ToString(Current_Line_Number);
                        count++;
                        continue;
                    }
                    else if ((str[0] >= 65 && str[0] <= 90) || str == "." || str == "{" || str == "}" || str == "(" || str == ")" || str == ":" || str == "," || str == "*" || str == "++" || str == "+" || str == "--" || str == "-" || str == ">")
                    {
                        //Check For Keywords
                        for (int j = 0; j <43; j++)
                        {
                            if (str == keywords[j, 0])
                            {

                                Token[count, 0] = keywords[j, 1];
                                Token[count, 1] = keywords[j, 0];
                                Token[count, 2] = Convert.ToString(Current_Line_Number);
                                count++;
                                break;

                            }



                        }
                        // Check for Identifiers
                        if (IdentifierCheck(str))
                        {
                            Token[count, 0] = "ID";
                            Token[count, 1] = str;
                            Token[count, 2] = Convert.ToString(Current_Line_Number);
                            count++;
                            continue;



                        }
                    }
                    else if (str[0] >= 48 && str[0] <= 57)
                    {


                        // Check For Integer Constant
                        if (IntegerConstant(str))
                        {
                            Token[count, 0] = "INTEGER_CONST";
                            Token[count, 1] = str;
                            Token[count, 2] = Convert.ToString(Current_Line_Number);
                            count++;
                            continue;



                        }
                        // Check For Float Constant
                        if (FloatConstant(str))
                        {
                            Token[count, 0] = "FLOAT_CONST";
                            Token[count, 1] = str;
                            Token[count, 2] = Convert.ToString(Current_Line_Number);
                            count++;

                            continue;


                        }


                    }
                    else
                    {


                        Token[count, 0] = "Lexical Error";
                        Token[count, 1] = str;
                        Token[count, 2] = Convert.ToString(Current_Line_Number);
                        count++;
                        continue;

                    }
                   
             }
            }





            FileStream fs = new FileStream(@"../../Token.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            int i=0;
            for (i = 0; i < count; i++ )
            {
                string temp = "(" + Token[i, 0] + "," + Token[i, 1] + "," + Token[i, 2] + ")";
                sw.WriteLine(temp);
              

            }

            sw.Close();
            fs.Close();


        }

        bool IdentifierCheck(string str)
        {
            char[] TempArray=str.ToCharArray();
            bool Check = false;

            if ((str[0] >= 65 && str[0] <= 90))
            {

                if ((str[1] >= 97 && str[1] <= 122) || (str[1] >= 47 && str[1] <= 58))
                {

                    int c = 2;
                    for (c = 2; c < str.Length; c++)
                    {
                        if ((str[c] >= 65 && str[c] <= 90))
                        {
                            return false;

                        }
                       
                    }
                    Check = true;

                }
                return Check;

            }
            else
            {

                return false;
            
            }

            
        }

        bool FloatConstant(string str)
        {
            int i = 0;
            int DotCount = 0;
            
            char[] inputString = str.ToCharArray();
            int k = str.Length;

            try
            {
                for (i = 0; i < k; i++)
                {


                    int j = i + 1;
                    if (!(inputString[i] >= 47 && inputString[i] <= 58))
                    {
                        if (inputString[i] == 46)
                        {
                            DotCount++;

                            if (DotCount > 1)
                            {
                                return false;
                            }

                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {


                return true;
            
            }



            return true;
        
        }


        bool IntegerConstant(string str)
        {



            int i = -1;
   
char[] varLook=str.ToCharArray();
		
		while(true)
		{
				i++;
                try
                {
                    if ((varLook[i] >= 47 || varLook[i] <= 58))
                    { 
                    
                        
                        if(varLook[i+1]=='.')
                        return false; 
                    
                    
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    return true;
                }
        }


           

     
        

	}


        string[] GetCurrentWords(string Line)
        {
            // initialize list
            WordsList = new List<string>();

            FlagCheck = false; // initialize flag variable

            WordsList.Clear(); // clear List<>

            LineArr = Line.ToCharArray(); // LineArr initialization


         
          
            TempACount=0;

            Counter = 0;       // Counter

            while (true)
            {
                TempArray = new char[LineArr.Length]; // Temporary Array
                if (Counter == -1)     // Lexical Error
                    return WordsList.ToArray();

                if (Counter >= LineArr.Length)  // If line ends
                    break;

                int k = Counter;

             TempACount = Counter;
                // Space encounter
                if (LineArr[k] == ' ')
                {
                    k++;
                    Counter++;

                }
                try 
                { 
                // If any of punctuator or operator encountered
                    if (LineArr[k] == '.' || LineArr[k] == 62 || LineArr[k] == '{' || LineArr[k] == '}' || LineArr[k] == '(' || LineArr[k] == ')' || LineArr[k] == ':' || LineArr[k] == ',' || LineArr[k] == '*' || LineArr[k] == '/' || LineArr[k] == '+' || LineArr[k] == '-')
                {
                    bool Plus = false; bool Minus = false; bool Divide = false; 
                    // If +  then check for ++
                    if (LineArr[k] == '+' )
                    {
                        Plus = true;
                        try
                        {
                            if (LineArr[k + 1] == '+')
                            {
                                WordsList.Add("++");
                                Counter = k + 2;
                                Plus = false;
                                continue;
                            }
                            else
                            {
                                Plus = true;
                            
                            }
                        }
                        catch (Exception ex)
                        { }

                    }   // If -  then check for --
                    else if (LineArr[k] == '-')
                    {
                        Minus = true;
                        try
                        {
                            if (LineArr[k + 1] == '-')
                            {
                                WordsList.Add("--");
                                Counter = k + 2;
                                Minus = false;
                                continue;
                            }
                            else
                            {
                                Minus = true;

                            }
                        }
                        catch (Exception ex)
                        { }
                    } // If comment
                    else if (LineArr[k] == '/' )
                    {
                        Divide = true;
                        try
                        {
                            if (LineArr[k + 1] == '/' )
                            {
                                return WordsList.ToArray();
                            }
                            
                        }
                        catch (Exception ex)
                        { }
                        
                    }                   
                    else
                    {
                        // if single punctuator or operator encountered
                        try
                        {
                            string Tmp = string.Join("", LineArr[k]);
                            Tmp = Tmp.Trim('\0');

                            WordsList.Add(Tmp);
                            Counter++;
                            continue;
                        }
                        catch (Exception ee)
                        {

                            Counter = k++;
                            continue;

                        }

                    }
                    if (Plus)
                    {
                        try
                        {
                            string Tmp = string.Join("", LineArr[k]);
                            Tmp = Tmp.Trim('\0');

                            WordsList.Add(Tmp);
                            Counter++;
                            continue;
                        }
                        catch (Exception ee)
                        {

                            Counter = k++;
                            continue;

                        }
                    }
                    if (Minus)
                    {
                        try
                        {
                            string Tmp = string.Join("", LineArr[k]);
                            Tmp = Tmp.Trim('\0');

                            WordsList.Add(Tmp);
                            Counter++;
                            continue;
                        }
                        catch (Exception ee)
                        {

                            Counter = k++;
                            continue;

                        }
                    }
                    if (Divide)
                    {
                        try
                        {
                            string Tmp = string.Join("", LineArr[k]);
                            Tmp = Tmp.Trim('\0');

                            WordsList.Add(Tmp);
                            Counter++;
                            continue;
                        }
                        catch (Exception ee)
                        {

                            Counter = k++;
                            continue;

                        }
                    }

                    
                }
                // if single quote ' encountered
                if (LineArr[k] == 39)
                {
                    Counter = CharCheck(k);

                }
                // if double quote " encountered
                if (LineArr[k] == 34)
                {
                    Counter = StringCheck(++k);

                }                    //if keywords and identifiers encountered
                else if ((LineArr[k] >= 65 && LineArr[k] <= 90) || (LineArr[k] >= 97 && LineArr[k] <= 122))
                {
                    Counter = IdentifiersKeywordsCheck(k);
                }
                else if (LineArr[k] >= 48 && LineArr[k] <= 57)    // if Digits encountered 
                {

                    Counter = DigitsCheck(k);



                }

            }
                catch(IndexOutOfRangeException ex)
                {
                    return WordsList.ToArray();
                }

            }






            return WordsList.ToArray();

        }

        int DigitsCheck(int k)
        {


            int TempCount = k;

            TempACount = TempCount;

            while (TempACount < LineArr.Length)  // Max check runs till end of line
            {
                FlagCheck = false;
                try
                {

                    // If space occurs
                    if (LineArr[TempCount] == ' ')
                        break;


                    if (LineArr[TempACount] == ' ' || LineArr[TempACount] == '>' || LineArr[TempACount] == '{' || LineArr[TempACount] == '}' || LineArr[TempACount] == '(' || LineArr[TempACount] == ')' || LineArr[TempACount] == ':' || LineArr[TempACount] == ',' || LineArr[TempACount] == '*' || LineArr[TempACount] == '/' || LineArr[TempACount] == '+' || LineArr[TempACount] == '-')
                    {
                        TempACount = TempCount;
                        FlagCheck = true;
                        break;

                    }

                    // if digit is encountered then store in temp array
                    if (LineArr[TempCount] >= 48 && LineArr[TempCount] <= 57)
                    {
                        TempArray[TempCount] = LineArr[TempCount];
                        TempACount++;
                    }
                    else if (LineArr[TempCount] == '.')  // if floating point encountered
                    {

                        if (LineArr[TempCount + 1] >= 48 && LineArr[TempCount + 1] <= 57) // if next to floating point is a number then continue to store in temp array
                        {

                            TempACount = TempCount;
                            TempArray[TempACount] = LineArr[TempACount];
                            TempACount++;
                            while (true)
                            {

                                // if any punctuator or floating point/dot encounters break the string loop of temp array
                                if (LineArr[TempACount] == ' ' || LineArr[TempACount] == '>' || LineArr[TempACount] == '.' || LineArr[TempACount] == '{' || LineArr[TempACount] == '}' || LineArr[TempACount] == '(' || LineArr[TempACount] == ')' || LineArr[TempACount] == ':' || LineArr[TempACount] == ',' || LineArr[TempACount] == '*' || LineArr[TempACount] == '/' || LineArr[TempACount] == '+' || LineArr[TempACount] == '-')
                                {

                                    FlagCheck = true;
                                    break;

                                }
                                // else continue storing in temp array
                                TempArray[TempACount] = LineArr[TempACount];
                                TempACount++;

                            }

                        }
                        else   // if ANYTHING other than digits or floating point encounters, end the digit string
                        {
                            TempACount = TempCount++;

                            break;

                        }

                    }




                }
                catch (Exception ec)
                {
                    // this exception will be caught on IndexOutOfRange exception so loop should end here

                    break;
                }
                // If a number check is complete
                if (FlagCheck)
                    break;

                // if digit is encountered condition above is true it will come here increase the index and check next character
                TempCount++;
            }


            // when digit storing in temp array is completed, save it to string and add to WordsList
            string str = string.Join("", TempArray);
            str = str.Trim('\0');                           // trim any extra null characters
            WordsList.Add(str);                            // Add
            k = TempACount;                               // k should returned which sets the 
            //index where checking will be continued

            return k;
        }

        int IdentifiersKeywordsCheck(int k)
        {

            // Character Starting Index
            int CharSIndex = k;

            // temprary index
            int TempIndex = 0;

            // TempArray
            TempArray[TempIndex] = LineArr[k];
            TempIndex++;
            k++;


            // Another temp array
            char[] TmpCharArray = new char[LineArr.Length];


            // start loop
            while (true)
            {
                 try
                 {
                if (k >= LineArr.Length)    // if line ends that is length is finished break loop
                    break;
                if (LineArr[k] == ' ' || LineArr[k] == '>' || LineArr[k] == '.' || LineArr[k] == '\"' || LineArr[k] == '\'' || LineArr[k] == '{' || LineArr[k] == '}' || LineArr[k] == '(' || LineArr[k] == ')' || LineArr[k] == ':' || LineArr[k] == ',' || LineArr[k] == '*' || LineArr[k] == '/' || LineArr[k] == '+' || LineArr[k] == '-')
                    break;                     // if any punctuator encounters break loop
                else if (LineArr[k] >= 48 && LineArr[k] <= 57)    // if any number encounters check 2 things
                {
                    string TmpString = null;

                    int Tempk = k;
                    // first check preceding word is keword or not like \\ INT IS23. // IS23 = IS- keyword and 23- number two separate terms
                    Tempk = CharSIndex;
                    int i = 0;
                   while( i < TempIndex)
                    {
                        if (LineArr[Tempk] == ' ')
                            break;

                        TmpCharArray[i] = LineArr[Tempk];
                        Tempk++;
                        i++;
                    }
                    // save temp string from char array saved uptil now
                    TmpString = string.Join("", TmpCharArray);
                    TmpString = TmpString.Trim('\0');
                    // initialize keywords
                    #region keywords
                    string[,] keywords = new string[,]{
            
            {"MAIN","Main"},
            {"INTEGER","DataType"},
            {"FLOAT","DataType"},
            {"CHARAC","DataType"},
            {"STRING","DataType"},
            {"IS","Assignment"},
            {"RETURN","Return"},
            {"TERMINATE","Break"},
            {"NEXT","Continue"},
            {"NOT","Unary"},
            {"EQUALS","Relational"},
            {"LESS","Relational"},
            {"GREAT","Relational"},
            {"ELESS","Relational"},
            {"EGREAT","Relational"},
            {"AND","Conditional"},
            {"OR","Conditional"},
            {"FUNCTION","Function"},
            {"CLASS","Class"},
            {"CHECK","Control Start"},
            {"FROM","Control End"},
            {"IF","If"},
            {"THEN","Then"},
            {"ELSE","Else"},
            {"UNTIL","Until"},
            {"RUN","Run"},
            {"UNTIL","Run-Until"},
            {"VOID","Void"},
            {"+","Arithmetic"},
            {"-","Arithmetic"},
            {"*","Arithmetic"},
            {"/","Arithmetic"},
            {"++","Increment"},
            {"--","Decrement"},
            {".","Line Break"},
            {"{", "Open Bracket"},
            {"}","Close Bracket"},
            {"(","Open Parenthesis"},
            {")","Close Parenthesis"},
            {"}","Close Bracket"},
            {":","Colon"},
            
            
    };
                    #endregion

                    int c;
                    for (c = 0; c <= 40; c++)
                    {
                        if (TmpString == keywords[c, 0])
                        {
                            FlagCheck = true;  // keyword found
                            break;              // stop
                        }

                    }


                    if (FlagCheck) // keyword found
                    {

                        // preceding string was keyword so send that keyword to List<> and further parsing will be normal as for digits
                        TmpString = TmpString.Trim('\0');
                        WordsList.Add(TmpString);
                        k = k + 1 - 1;
                        return k;


                    }
                    else                // update starting index of character after digit 
                    {
                        // if number was encountered and no keyword save number as identifier in Temp Array of char for making string later
                        TempArray[TempIndex] = LineArr[k];
                        TempIndex++;
                        k=k+1;
                        CharSIndex = k + 1;
                    }




                }
                else
                {
                    // if not number then continue to register characters in temp array
                    TempArray[TempIndex] = LineArr[k];
                    TempIndex++;
                    k++;
                }
            }            
            catch(Exception ex)
            {
            
            
            
            }
            }

            // if no number was encountered or number was encountered but preceding string was not keyword so normal identifier
            // then register string
            string TempString = string.Join("", TempArray);
            TempString = TempString.Trim('\0');
            WordsList.Add(TempString);
            Counter = k;


            return k;
        }

        int StringCheck(int k)
        {

            // Save starting index of string
            int StartingIndex = k;
            // temp Array
            char[] TempC = new char[LineArr.Length - k];
            // counter
            int c = 0;
            // Exception Handler
            try
            {
		 TempC[c]='\\';
                c++;
                while (true)    // While double quote " is not found keep loop
                {
                    if (LineArr[k] == 34)
                        break;      // Double quote found
                    else
                    {                    // While double quote " is not found continue storing in temp array
                        TempC[c] = LineArr[k];
                        k++;
                        c++;
                    }


                }

                // if ending double quote " is found save the string
                string Tmp = string.Join("", TempC);
                Tmp = Tmp.Trim('\0');
                WordsList.Add(Tmp);
                k = k + 1;



            }
            catch (IndexOutOfRangeException ec)
            {
                // if line ends that is index is out of range - no quotes found lexical error
                string Tmp = "LE_" + StartingIndex;              // register lexical error as LE and where it started
                WordsList.Add(Tmp);
                k = -1;
                return k;


            }




            return k;
        }

        int CharCheck(int k)
        {





            k++;
            // temp array
            char[] TempC = new char[LineArr.Length - k];


            if (LineArr[k] == 92)         // if backward slash "\" is encountered
            {

                if ((LineArr[k + 1] >= 97 && LineArr[k + 1] <= 122) && LineArr[k + 2] == 39) // check for char in next and closing quotes in later index
                {

                    // if closing quote is found register the entry

                    string Tmp = @"/" + @"\" + Convert.ToString(LineArr[k + 1]);

                    WordsList.Add(Tmp);
                    k = k + 3;  // now start after the index where single quote ' was encountered

                }


            }
            else if (LineArr[k + 1] == 39)  // no backcslash just one character and closing quote
            {

                //if closing quote found register entry
           

                 string Tmp = @"/" + Convert.ToString(LineArr[k]);

                WordsList.Add(Tmp);
                k = k + 2;


            }
            else
            {
                // else no closing quote found
                string Tmp = "LE_" + Convert.ToString(LineArr[k]);  // register lexical error as LE
                WordsList.Add(Tmp);

                k = k + 1;

            }



            return k;
        }

        
        

    }

}
