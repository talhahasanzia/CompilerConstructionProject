using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCPROJECT
{
   partial class SyntaxCheck
    {
        static int CurrentIndex= 0;

        
        public static bool Starting()
        {
            if (FunctionDec() || Declaration() || Initialization() || MainClass() || MAIN_Function())
                return true;
            return false;
        
        }


        public static bool MainClass()
        {
          

                
            return false;
        
        }
        public static bool MAIN_Function()
        {
            int i=CurrentIndex;
            if (TokensArr[i] == "Function")
            { 
                i++;
                if (TokensArr[i] == "Colon")
                {
                    i++;
                    if (TokensArr[i] == "Main")
                    {
                        i++;
                        if (TokensArr[i] == "Open Paranthesis")
                        {
                            i++;
                            if (TokensArr[i] == "Close Paranthesis")
                            {

                                CurrentIndex = i + 1;
                                return true;

                            }


                        }


                    }


                }
            
            }

            return false;

        }
        public static bool ClassDec()
        {
           
                
            return false;

        }
        public static bool ClassBody()
        {
           
            return false;

        }
        public static bool ClassInstance()
        {
            
            return false;

        }
        public static bool ClassMemberCalls()
        {

        
            
            return false;

        }
        public static bool C1()
        {
           
            return false;

        }
        public static bool C2()
        {
        
            return false;

        }
        public static bool C3()
        {

            
            return false;

        }
        public static bool DotCalls()
        {
           


            return false;

        }
        public static bool DotCalls_()
        {
          
            return false;
            
        }
        public static bool FunctionDec()
        {
            int resetCurrentTo = CurrentIndex;
            int i = CurrentIndex;
            if (TokensArr[i] == "Function")
            {
                i++;
                if (TokensArr[i] == "Colon")
                {
                    i++;
                    if (TokensArr[i] == "ID")
                    {
                        i++;
                        if (TokensArr[i] == "Open Paranthesis")
                        {

                           i++;
                           if (TokensArr[i] == "DT" || TokensArr[i] == "Void")
                            {
                                CurrentIndex=i++;

                                if (Parameters())
                                {
                                    i=CurrentIndex++;

                                    if (TokensArr[i] == "Open Scope")
                                    {

                                        CurrentIndex = i++;

                                        if (Body())
                                        {
                                            i = CurrentIndex++;

                                            if (TokensArr[i] == "Close Scope")
                                            {
                                                CurrentIndex = i++;
                                                return true;                        

                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }

                }           
            
            }
            CurrentIndex = resetCurrentTo;
            return false;

        }
        public static bool Return_St()
        {
            int resetCurrentTo = CurrentIndex;
            int i = CurrentIndex;

            if (TokensArr[i] == "Return")
            {
               CurrentIndex i++;
                if (TokensArr[i] == "Return")
                {
                    i++;



                } 
            
            
            }
           
            return false;

        }
       
        
        public static bool Parameters()
        {
           
           
           
            return false;
        }

        public static bool CONST()
        {
            
            return false;

        }
        public static bool Declaration()
        {
           
            return false;

        }
        public static bool Initialization()
        {
            return false;

        }
        public static bool I2()
        {
            
            return false;

        }
        public static bool Inc_Dec(string[] str)
        {
           
            return false;

        }
        public static bool Run_Until()
        {
          
            return false;

        }
        public static bool Until_Run()
        {
           
           
            return false;

        }
        public static bool RelationalExp()
        {
            
           
            return false;

        }
        public static bool ID_CONST()
        {
            
            return false;

        }
        public static bool RList()
        {

            return false;

        }
        public static bool Until_Loop()
        {
           
            return false;
        }
        public static bool Check_From()
        {
          
            return false;

        }
        public static bool INT_CHAR()
        {
            
            return false;

        }
        public static bool ErrorSt()
        {

           
          

            return false;

        }
        public static bool FromList()
        {
          
            return false;
        }
        public static bool RepeatFrom()
        {

            
                
            return false;

        }
        public static bool If_Then_Else()
        {
           
            return false;

        }
        public static bool Body()
        {
            
            return false;

        }
        public static bool SingleSt()
        {
          
                return false;

        }
        public static bool Multiple_St()
        {
           
                return false;

        }
        public static bool TerminateSt() 
        {
          
            return false; 
        }
        public static bool NextSt() 
        {
            
            return false; 
        }
     
        public static bool FunctionCall()
        {
            
            return false;

        }
        public static bool FCalls()
        {

            return false;

        }
        public static bool CallParameters()
        {
            

            return false;

        }
        public static bool MultipleParameters()
        {

           
            return false;

        }
        public static bool EXP()
        {
            
            return false;

        }
    }
}
