using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCPROJECT
{
   partial class SyntaxCheck
    {
        static int CurrentIndex= 0;
       static bool IsSender = false;
        
        public static bool 
            Starting()
        {
            if (FunctionDec() || Declaration() || Initialization() || MAIN_Function() || ClassDec() || Enum())
                Starting();

            if (TokensArr[CurrentIndex] == "$")
                return true;

            return false;
        
        }


        
        public static bool MAIN_Function()
        {
            int reset = CurrentIndex;
            if (TokensArr[CurrentIndex] == "Function")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "Colon")
                {
                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "Main")
                    {
                        CurrentIndex++;
                        if (TokensArr[CurrentIndex] == "OpenParenthesis")
                        {
                            CurrentIndex++;
                            if (TokensArr[CurrentIndex] == "CloseParenthesis")
                            {


                                CurrentIndex++;
                                if (TokensArr[CurrentIndex] == "OpenScope")
                                {


                                    CurrentIndex++;
                                    if (FunctionBody())
                                    {

                                        return true;
                                    }
                                }
                            }


                        }


                    }


                }
            
            }

            CurrentIndex = reset;
            return false;

        }
       
       
       
       
       
        public static bool FunctionDec()
        {
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "Function")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "Colon")
                {
                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "ID")
                    {
                        CurrentIndex++;
                        if (TokensArr[CurrentIndex] == "OpenParenthesis")
                        {

                            CurrentIndex++;
                           if (TokensArr[CurrentIndex] == "DT" || TokensArr[CurrentIndex] == "Void")
                            {
                                CurrentIndex++;

                                if (Parameters())
                                {
                                    

                                    if (TokensArr[CurrentIndex] == "OpenScope")
                                    {
                                        CurrentIndex++;
                                        if (FunctionBody())
                                        {
                                            
                                            return true;
                                        }
                                        
                                    }
                                }
                            }
                        }

                    }

                }           
            
            }

            CurrentIndex = reset;
            return false;

        }

       public static bool FunctionBody()
       {
       int reset=CurrentIndex;
       
       

           if ( Declaration() || Initialization() || Inc_Dec() || FunctionCall() || Return_St() || ClassMemberCalls() || Until_Loop() || Run_Until() || If_Then_Else() || Check_From())
                                        {
                                            
               FunctionBody();
               return true;
                                           
                                        }

            if (TokensArr[CurrentIndex] == "CloseScope")
                                            {
                                                CurrentIndex++;
                                                return true;                        

                                            }

       CurrentIndex=reset;
       return false;
       
       }
        public static bool Parameters()
        {

            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                CurrentIndex++;
                return true;


            }


            if (TokensArr[CurrentIndex] == "Comma")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "DT")
                {
                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "ID")
                    {
                        CurrentIndex++;
                        if (Parameters())
                        {

                            return true;

                        }

                    }
                }
            }
            
            CurrentIndex = reset;
            return false;
        }



        public static bool Return_St()
        {
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "Return")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "LineBreak")
                {



                    CurrentIndex++;
                    return true;
                
                
                }

                else if (Ret_())
                {
                    if (TokensArr[CurrentIndex] == "LineBreak")
                    {

                        CurrentIndex++;
                        return true;

                    }



                } 
            
            
            }

            CurrentIndex = reset;
            return false;

        }
        public static bool Ret_()
        {
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "ID")
            {
                CurrentIndex++;
                return true;
            }


            IsSender = true;
            if (CONST()  || FunctionCall() || EXP())
            {
                
                return true;
            }

            IsSender = false;



            CurrentIndex = reset;
            return false;
        }
        
       

        public static bool CONST()
        {
            int reset = CurrentIndex;
            if (TokensArr[CurrentIndex] == "INTEGER_CONST" || TokensArr[CurrentIndex] == "FLOAT_CONST" || TokensArr[CurrentIndex] == "STRING_CONST" || TokensArr[CurrentIndex] == "CHAR_CONST")
            { CurrentIndex++; return true; }


            CurrentIndex = reset;
            return false;

        }
        public static bool Declaration()
        {
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "DT")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "ID")
                {

                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "LineBreak")
                    {
                        CurrentIndex++;
                        return true;
                    
                    
                    }
                
                }
            
            
            
            
            }

            
            if (ClassInstance())
                return true;

            CurrentIndex = reset;
            return false;

        }
        public static bool Initialization()
        {
            int reset = CurrentIndex;

            if (ClassMemberCalls() || TokensArr[CurrentIndex] == "ID" )
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "Assignment")
                {
                    CurrentIndex++;
                    if (I2())
                    {
                        if (TokensArr[CurrentIndex] == "LineBreak")
                        {
                            CurrentIndex++;
                            return true;



                        }




                    }



                }
            
            
            
            }
            CurrentIndex = reset;
           if(TokensArr[CurrentIndex]=="DT")
           {
               CurrentIndex++;
            if (TokensArr[CurrentIndex] == "ID")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "Assignment")
                {
                    CurrentIndex++;
                    if (I2())
                    {
                        if (TokensArr[CurrentIndex] == "LineBreak")
                        {
                            CurrentIndex++;
                            return true;



                        }




                    }



                }



            }
           }


           CurrentIndex = reset;
            return false;

        }
        public static bool I2()
        {
            int reset = CurrentIndex;


            IsSender = true;
            if (CONST() || FunctionCall() || ClassMemberCalls()  || EXP() || Enum_Calling())
                return true;
            IsSender = false;

            if (TokensArr[CurrentIndex] == "ID")
            {
                CurrentIndex++;
                return true;


            }

            CurrentIndex = reset;
            return false;

        }
        public static bool Inc_Dec()
        {
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "ID")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "Inc_Dec")
                {
                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "LineBreak")
                    {
                        CurrentIndex++;
                        return true;
                    }
                }

            }

            CurrentIndex = reset;
            return false;
        }
        
        public static bool Run_Until()
        {
            int reset = CurrentIndex;
            
            if (TokensArr[CurrentIndex] == "Run")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "OpenScope")
                {
                    CurrentIndex++;
                    if (Declaration() || Initialization() || Inc_Dec() || FunctionCall() || If_Then_Else() || Run_Until() || Until_Loop() || Check_From())
                    {
                        if (TokensArr[CurrentIndex] == "CloseScope")
                        {
                            CurrentIndex++;
                            if (Until_Run())
                            {
                               
                                return true;



                            }



                        }
                    
                    
                    }


                }
            
            
            
            }
            CurrentIndex = reset;
            return false;

        }
        public static bool Until_Run()
        {

            int reset = CurrentIndex;
            if (TokensArr[CurrentIndex] == "Until")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "OpenParenthesis")
                {
                    CurrentIndex++;
                    if (EXP())
                    {

                        if (TokensArr[CurrentIndex] == "CloseParenthesis")
                        {

                            CurrentIndex++;
                            if (TokensArr[CurrentIndex] == "LineBreak")
                            {
                                CurrentIndex++;
                                return true;



                            }



                        } 


                    }



                }



            }

            CurrentIndex = reset;
            return false;

        }
        public static bool RelationalExp()
        {
            int reset
                = CurrentIndex;


           
            
            
            if (ID_CONST())
            {
                if (TokensArr[CurrentIndex] == "Relational")
                {
                    CurrentIndex++;
                    if (ID_CONST())
                    {

                        if (RList())
                        {

                            return true;
                        
                        }



                    }



                }




            }

            CurrentIndex = reset;
            return false;

        }
        public static bool ID_CONST()
        {

            int reset = CurrentIndex;
            if (TokensArr[CurrentIndex] == "ID" || TokensArr[CurrentIndex] == "INTEGER_CONST" || TokensArr[CurrentIndex] == "FLOAT_CONST" || TokensArr[CurrentIndex] == "STRING_CONST" || TokensArr[CurrentIndex] == "CHAR_CONST")
            { CurrentIndex++; return true; }

            IsSender = true;
            if (FunctionCall())
                return true;

            IsSender = false;

            CurrentIndex = reset;
            return false;

            

        }
        public static bool RList()
        {
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                CurrentIndex++;
                return true;
            }
            
            if (TokensArr[CurrentIndex] == "Conditional")
            {
                CurrentIndex++;
                if (EXP())
                {

                    return true;
                }


            }


            CurrentIndex = reset;
            return false;

        }
        public static bool Until_Loop()
        {

            int reset = CurrentIndex;


            if (TokensArr[CurrentIndex] == "Until")
            {
                CurrentIndex++;
                if(TokensArr[CurrentIndex]=="OpenParenthesis")
                { 
                    CurrentIndex++;
                    if (EXP())
                    {
                        if (TokensArr[CurrentIndex] == "CloseParenthesis")
                        {
                            CurrentIndex++;
                            if (TokensArr[CurrentIndex] == "OpenScope")
                            {
                                CurrentIndex++;
                                if (Declaration() || Initialization() || Inc_Dec() || TerminateSt() || NextSt() || If_Then_Else() || FunctionCall() || Until_Loop() || Run_Until() )
                                {

                                    if (TokensArr[CurrentIndex] == "CloseScope")
                                    {
                                        CurrentIndex++;
                                        return true;

                                    }




                                }



                            }
                        }
                    }

                }



            }

            CurrentIndex = reset;
            return false;
        }
        public static bool Check_From()
        {

            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "Check")
            {
                   CurrentIndex++;
                   if (TokensArr[CurrentIndex] == "OpenParenthesis")
                   {
                       CurrentIndex++;
                       if (TokensArr[CurrentIndex] == "ID")
                       {
                           CurrentIndex++;
                           if (TokensArr[CurrentIndex] == "CloseParenthesis")
                           {
                               CurrentIndex++;

                               if (TokensArr[CurrentIndex] == "OpenScope")
                               {
                                   CurrentIndex++;

                                   if (TokensArr[CurrentIndex] == "From")
                                   {
                                       CurrentIndex++;
                                       if (FromList())
                                       {

                                           if (TokensArr[CurrentIndex] == "CloseScope")
                                           {
                                               CurrentIndex++;

                                               return true;
                                           }

                                       }

                                   }
                               }
                           }

                       }

                   }
            
            }

            CurrentIndex = reset;
            return false;

        }
        public static bool INT_CHAR()
        {
            int reset = CurrentIndex;
            if ( TokensArr[CurrentIndex] == "INTEGER_CONST"   || TokensArr[CurrentIndex] == "CHAR_CONST")
            { 
                CurrentIndex++;
                return true; 
            
            }


            CurrentIndex = reset;
            return false;

        

        }
        public static bool ErrorSt()
        {
            
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "Error")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "Colon")
                {
                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "OpenScope")
                    {
                        CurrentIndex++;
                        if (Declaration() || Initialization() || Inc_Dec() || FunctionCall() || TerminateSt() || NextSt() || Return_St() || Until_Loop() || Run_Until() || If_Then_Else())
                        {

                            if (TokensArr[CurrentIndex] == "CloseScope")
                            {
                                CurrentIndex++;
                                return true;

                            }
                        
                        }
                    }

                }

            }

            CurrentIndex = reset;
            return false;

        }
        public static bool FromList()
        {
            int reset = CurrentIndex;

            if (INT_CHAR())
            {
                if (TokensArr[CurrentIndex] == "Colon")
                {
                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "OpenScope")
                    {
                        CurrentIndex++;
                        if (Declaration() || Initialization() || Inc_Dec() || FunctionCall() || TerminateSt() || NextSt() || Return_St() || Until_Loop() || Run_Until() || If_Then_Else() )
                        {

                            if (TokensArr[CurrentIndex] == "CloseScope")
                            {
                                CurrentIndex++;
                                if (RepeatFrom())
                                {

                                    return true;
                                
                                }
                            }
                        
                        
                        }

                    }

                }


            }


            CurrentIndex = reset;
            return false;
        }
       
        public static bool RepeatFrom()
        {
            int reset = CurrentIndex;

            if (ErrorSt())
            {
                return true;


            }

            if (TokensArr[CurrentIndex] == "From")
            {
                CurrentIndex++;
                if (FromList())
                {

                    return true;
                
                }
            }




            CurrentIndex = reset;
            return false;

        }
        public static bool If_Then_Else()
        {
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "If")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "OpenParenthesis")
                {
                    CurrentIndex++;
                    if (EXP())
                    {
                        if (TokensArr[CurrentIndex] == "CloseParenthesis")
                        {
                            CurrentIndex++;
                            if (TokensArr[CurrentIndex] == "Then")
                            {
                                CurrentIndex++;
                                if (TokensArr[CurrentIndex] == "OpenScope")
                                {
                                    CurrentIndex++;
                                    if (Declaration() || Initialization() || Inc_Dec() || FunctionCall() ||  Until_Loop() || Run_Until() || If_Then_Else())
                                    {

                                        if (TokensArr[CurrentIndex] == "CloseScope")
                                        {
                                            CurrentIndex++;

                                            if (TokensArr[CurrentIndex] == "Else")
                                            {
                                                CurrentIndex++;
                                                if (TokensArr[CurrentIndex] == "OpenScope")
                                                {
                                                    CurrentIndex++;
                                                    if (Declaration() || Initialization() || Inc_Dec() || FunctionCall() || TerminateSt() || NextSt() || Return_St() || Until_Loop() || Run_Until() || If_Then_Else())
                                                    {
                                                        if (TokensArr[CurrentIndex] == "CloseScope")
                                                        {
                                                            CurrentIndex++;
                                                            return true;

                                                        }


                                                    }

                                                }

                                            }
                                            else
                                            {
                                                int i = CurrentIndex - 1;
                                                if (TokensArr[i] == "CloseScope")
                                                {
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

            }


            CurrentIndex = reset;
            return false;

        }
        public static bool Body()
        {
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "LineBreak")
            {
                CurrentIndex++;
                return true;


            }


            if (  Multiple_St())
                return true;

            if (SingleSt())
                return true;

          //  if (TokensArr[CurrentIndex] == "CloseScope")
               // return true;

            CurrentIndex = reset;
            return false;

        }
        public static bool SingleSt()
        {
            int reset = CurrentIndex;

            if (CurrentIndex == TokensArr.Length)
                return true;


            if (Declaration() || Initialization()  || Inc_Dec() || FunctionCall() || ClassMemberCalls() || TerminateSt() || NextSt() || Return_St() || FunctionDec() || Until_Loop() || Run_Until() || If_Then_Else() || Check_From())
                return true;



            CurrentIndex = reset;
                return false;

        }
        public static bool Multiple_St()
        {
            int reset = CurrentIndex;

            


            if (SingleSt())
            {
                if (Multiple_St())
                    return true;
                else
                    return false;
            
            }            
        //    else if (TokensArr[CurrentIndex] == "CloseScope")
         //  {

           //     return true;
         //   }
            else
            {
                CurrentIndex = reset;
                return false;
            }


        }
        public static bool TerminateSt() 
        {
            int reset = CurrentIndex;


            if (TokensArr[CurrentIndex] == "Break")
            {
                CurrentIndex++;

                if (TokensArr[CurrentIndex] == "LineBreak")
                {
                    CurrentIndex++;
                    return true;


                }


            }


            CurrentIndex = reset;
            return false; 

      
        }
        public static bool NextSt() 
        {
            int reset = CurrentIndex;
            
            
            if (TokensArr[CurrentIndex] == "Continue")
            {
                CurrentIndex++;

                if (TokensArr[CurrentIndex] == "LineBreak")
                {
                    CurrentIndex++;
                    return true;


                }

            
            }


            CurrentIndex = reset;
            return false; 
        }
     
        public static bool FunctionCall()
        {
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "ID")
            {
                CurrentIndex++;
                if (FCalls())
                {
                  
                    if (TokensArr[CurrentIndex] == "LineBreak")
                    {
                        if (IsSender)
                        {

                            IsSender = false;    
                            return true; 
                        }
                        else
                        {
                            CurrentIndex++;
                            IsSender = false;
                            return true;
                        }

                    }


                }

            }

            CurrentIndex = reset;
            return false;

        }
        public static bool FCalls()
        {
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "Assignment")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "ID")
                {
                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "OpenParenthesis")
                    {
                        CurrentIndex++;
                        if (CallParameters())
                        {
                            return true;

                        }

                    }


                }
            
            
            }
            
            if (TokensArr[CurrentIndex] == "OpenParenthesis")
            {
                CurrentIndex++;
                if (CallParameters())
                {


                    return true;
                
                }

            }

            CurrentIndex = reset;
            return false;

        }
        public static bool CallParameters()
        {
            int reset = CurrentIndex;
            if (TokensArr[CurrentIndex] == "ID")
            {
                CurrentIndex++;
                if (MultipleParameters())
                {
                    return true;


                }

            }
            if (TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                CurrentIndex++;
                return true;

            }


            CurrentIndex = reset;
            return false;

        }
        public static bool MultipleParameters()
        {

            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                CurrentIndex++;
                return true;
            }
           
                if (TokensArr[CurrentIndex] == "ID")
                {
                    CurrentIndex++;
                    if (MultipleParameters())
                    {
                        return true;


                    }

                }






                CurrentIndex = reset;
            return false;

        }


 
        public static bool ClassDec()
        {
            int reset = CurrentIndex;
            
            if (TokensArr[CurrentIndex] == "Class")
            {

                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "ID")
                {

                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "OpenScope")
                    {

                        CurrentIndex++;
                        if (ClassBody())
                        {


                            return true;
                        
                        
                        }



                    }



                }
            
            
            
            }


            CurrentIndex = reset;
            return false ;
        }

        public static bool ClassBody()
        {
            int reset = CurrentIndex;

            if (Declaration() || Initialization() || Inc_Dec())
            {

                if (ClassBody())
                {


                    return true;
                
                
                }



            }

            if (FunctionDec())
            {

                if (ClassBody())
                {


                    return true;


                }



            }

            if (TokensArr[CurrentIndex] == "CloseScope")
            {

                CurrentIndex++;
                return true;
            
            
            }

            CurrentIndex = reset;
            return false;
        }

        public static bool ClassInstance()
        {
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "ID")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "ID")
                {
                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "LineBreak")
                    {


                        CurrentIndex++;
                        return true;


                    }


                }

            }

            CurrentIndex = reset;
            return false;
        }

        public static bool ClassMemberCalls()
        {

            int reset = CurrentIndex;
           
            //here
           CurrentIndex = reset;
           if (TokensArr[CurrentIndex] == "ID")
           {
               CurrentIndex++;
               if (TokensArr[CurrentIndex] == "Member")
               {
                   CurrentIndex++;
                  
                       if (FunctionCall())
                       {


                           return true;


                       }

                       
                   }



              }

           CurrentIndex = reset;
           if (TokensArr[CurrentIndex] == "ID")
           {
               CurrentIndex++;
               if (TokensArr[CurrentIndex] == "Member")
               {
                   CurrentIndex++;

                   if (EXP() )
                   {


                       return true;


                   }


               }



           }
           CurrentIndex = reset;
           if (TokensArr[CurrentIndex] == "ID")
           {
               CurrentIndex++;
               if (TokensArr[CurrentIndex] == "Member")
               {
                   CurrentIndex++;

                   if ( Inc_Dec() )
                   {


                       return true;


                   }


               }



           }
           CurrentIndex = reset;
           if (TokensArr[CurrentIndex] == "ID")
           {
               CurrentIndex++;
               if (TokensArr[CurrentIndex] == "Member")
               {
                   CurrentIndex++;

                   if ( Enum_Calling())
                   {


                       return true;


                   }


               }



           }

           CurrentIndex = reset;
           if (TokensArr[CurrentIndex] == "ID")
           {
               CurrentIndex++;
               if (TokensArr[CurrentIndex] == "Member")
               {
                   CurrentIndex++;
                   if (TokensArr[CurrentIndex] == "ID")
                   {
                       CurrentIndex++;
                       return true;




                   }

               }




           }
           




            CurrentIndex = reset;
            return false;
        }
        public static bool EXP()
        {
            bool Flag = false;
           

            
                if (OR())
                {


                    Flag = true;
                }
            


            return Flag;
        }
        static bool OR()
        {
            bool Flag = false;

          

             if (AND())
                {

                    if (OR1())
                    {

                        Flag = true;
                    }
                }
            

            return Flag;
        }
        static bool OR1()
       {
           bool Flag = false;
           if (TokensArr[CurrentIndex] == "Conditional")
           {
               CurrentIndex++;
               if (AND())
               {
                 
                   if (OR1())
                   {
                       Flag = true;
                   }
               }
           }
           else if (TokensArr[CurrentIndex] == "Assignment" || TokensArr[CurrentIndex] == "LineBreak" || TokensArr[CurrentIndex] == "CloseParenthesis" || TokensArr[CurrentIndex] == "Then")
           {
               Flag = true;
           }


           return Flag;
       }
        static bool AND()
        {
            bool Flag = false;
                if (RE())


                {

                    if (AND1())
                    {

                        Flag = true;
                    }
                }
          
            return Flag;


        }

        static bool AND1()
        {
            bool Flag = false;
            if (TokensArr[CurrentIndex] == "Conditional")
            {
                CurrentIndex++;
                if (RE())
                {

                    if (AND1())
                    {
                        Flag = true;
                    }
                }
            }
            else if (TokensArr[CurrentIndex] == "Assignment" || TokensArr[CurrentIndex] == "LineBreak" || TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                Flag = true;
            }
            return Flag;
        }
        static bool RE()
        {
            bool Flag = false;
           

           
                if (Ex())
                {

                    if (RE1())
                    {

                        Flag = true;
                    }
                }
           
            return Flag;


        }
        public static bool RE1()
        {
            bool Flag = false;
            if (TokensArr[CurrentIndex] == "Relational")
            {
                CurrentIndex++;
                if (Ex())
                {

                    if (RE1())
                    {

                        Flag = true;
                    }
                }
            }
            else if (TokensArr[CurrentIndex] == "Conditional" || TokensArr[CurrentIndex] == "Assignment" || TokensArr[CurrentIndex] == "OR" || TokensArr[CurrentIndex] == "LineBreak" || TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                Flag = true;
            }
            return Flag;
        }


        static bool Ex()
        {
            bool Flag = false;
           

           
                if (T())
                {

                    if (Ex1())
                    {

                        Flag = true;
                    }
                }
            
            return Flag;

        }

        static bool Ex1()
        {
            bool Flag = false;
            if (TokensArr[CurrentIndex]== "AddSubOp")
            {
                CurrentIndex++;
                if (T())
                {

                    if (Ex1())
                    {
                        Flag = true;
                    }
                }
            }
            else if (TokensArr[CurrentIndex] == "Relational" || TokensArr[CurrentIndex] == "Conditional" || TokensArr[CurrentIndex] == "Assignment" || TokensArr[CurrentIndex] == "OR" || TokensArr[CurrentIndex] == "LineBreak" || TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                Flag = true;
            }


            return Flag;
        }
        static bool T()
        {
            bool Flag = false;
           

            
                if (FS())
                {

                    if (T1())
                    {

                        Flag = true;
                    }
                }
           
            return Flag;
        }

        static bool T1()
        {
            bool Flag = false;
            if (TokensArr[CurrentIndex]=="MulDivOp")
            {
                CurrentIndex++;
                if (FS())
                {

                    if (T1())
                    {

                        Flag = true;
                    }
                }
            }
            else if (TokensArr[CurrentIndex] == "AddSubOp" || TokensArr[CurrentIndex] == "Relational" || TokensArr[CurrentIndex] == "Conditional" || TokensArr[CurrentIndex] == "Assignment" || TokensArr[CurrentIndex] == "LineBreak" || TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                Flag = true;
            }
            return Flag;
        }
        static bool FS()
        {
            IsSender = true;
            int reset = CurrentIndex;
            if (ID_CONST())
            {

                return true;

            }
           
           
            IsSender = false;


            CurrentIndex = reset;
            return false;
        }


        public static bool Enum()
        {
            int reset = CurrentIndex;
            if (TokensArr[CurrentIndex] == "Enum")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "ID")
                {
                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "OpenScope")
                    {
                        CurrentIndex++;
                        if (EnumBody())
                        {
                            return true;
                        }

                    }

                }
            }
            CurrentIndex = reset;
            return false;

        }

        public static bool EnumBody()
        {
            int reset = CurrentIndex;
            
            if (TokensArr[CurrentIndex] == "ID")
            {
                CurrentIndex++;
                if (EnumBody2())
                {
                    return true;
                }
            }
            CurrentIndex = reset;
            return false;

        }

        public static bool EnumBody2()
        {
            int reset = CurrentIndex;
            if (TokensArr[CurrentIndex] == "Comma")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "ID")
                {
                    CurrentIndex++;
                    if (EnumBody2())
                    {
                        return true;
                    }
                }
            }
            else if(TokensArr[CurrentIndex]=="CloseScope")
            {
                CurrentIndex++;
                return true;
            }
            CurrentIndex = reset;
            return false;
        }

        public static bool Enum_Calling()
        {
            int reset = CurrentIndex;
            if (TokensArr[CurrentIndex] == "ID")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "Member")
                {
                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "ID")
                    {
                        CurrentIndex++;
                        if (TokensArr[CurrentIndex] == "LineBreak")
                        {
                            CurrentIndex++;
                            return true;
                        }
                    }
                }
            
            }





            reset = CurrentIndex;
            return false;

        }

    }
}
