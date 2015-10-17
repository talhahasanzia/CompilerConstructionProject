using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCPROJECT
{
    partial class Semantic
    {
        static bool IsSender = false;
        static string FunctionParams;
        static string FunctionDataExpression;
        static string  FunctionName;
        static string FunctionR;

        public static bool Starting()
        {
            if (FunctionDec() || Declaration() || Initialization() || MAIN_Function() || ClassDec())
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

                                    StartScope();

                                    CurrentIndex++;
                                    if (Body())
                                    {
                                        if (TokensArr[CurrentIndex] == "CloseScope")
                                        {

                                            CloseScope();

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





        public static bool FunctionDec()
        {
            string FunctionName = null;
            string FunctionRet = null;

            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "Function")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "Colon")
                {
                    CurrentIndex++;
                    if (TokensArr[CurrentIndex] == "ID")
                    {
                        if (Scopes.Count > 0)
                        {
                            if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                                FunctionName = VPArr[CurrentIndex];
                        }

                        else if (LookUp(VPArr[CurrentIndex], 0) == false)
                            FunctionName = VPArr[CurrentIndex];

                        else
                            return false; // REDECLARATION ERROR

                        CurrentIndex++;
                        if (TokensArr[CurrentIndex] == "OpenParenthesis")
                        {

                            CurrentIndex++;
                            if (TokensArr[CurrentIndex] == "DT" || TokensArr[CurrentIndex] == "Void")
                            {
                                if (TokensArr[CurrentIndex] == "DT")
                                {
                                    
                                    FunctionRet = VPArr[CurrentIndex];


                                }
                                else
                                {

                                    FunctionRet = "VOID";
                                
                                }
                                CurrentIndex++;

                                if (Parameters())
                                {

                                    FunctionDataExpression = FunctionRet + "." + FunctionParams;
                                    InsertFunction(FunctionName, FunctionDataExpression);


                                    if (TokensArr[CurrentIndex] == "OpenScope")
                                    {
                                        StartScope();
                                        CurrentIndex++;

                                        if (Body())
                                        {


                                            if (TokensArr[CurrentIndex] == "CloseScope")
                                            {
                                                CloseScope();
                                                InsertFunction(FunctionName, FunctionDataExpression);
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

            }

            CurrentIndex = reset;
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

            if (TokensArr[CurrentIndex] == "DT")
            {

                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "ID")
                {
                    if (Scopes.Count > 0)
                    {
                        if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                            FunctionParams += VPArr[CurrentIndex]+".";
                    }

                    else if (LookUp(VPArr[CurrentIndex], 0) == false)
                        FunctionParams += VPArr[CurrentIndex] + ".";

                    else
                        return false; // REDECLARATION ERROR;

                    CurrentIndex++;
                    if (Parameters())
                    {

                        return true;

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
            if (CONST() || FunctionCall() || EXP())
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
                        if (Scopes.Count > 0)
                        {
                            if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                                Insert(VPArr[CurrentIndex], VPArr[CurrentIndex - 1], Scopes.Peek());
                        }

                        else if (LookUp(VPArr[CurrentIndex], 0) == false)
                            Insert(VPArr[CurrentIndex], VPArr[CurrentIndex - 1], 0);

                        else
                            return false; // REDECLARATION ERROR

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

            if (TokensArr[CurrentIndex] == "ID")
            {
                if (Scopes.Count > 0)
                {
                    if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                        return false;
                }
                else if (LookUp(VPArr[CurrentIndex], 0) == false)
                    return false;
               

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
            if (TokensArr[CurrentIndex] == "DT")
            {
                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "ID")
                {
                    if (Scopes.Count > 0)
                    {
                        if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                            Insert(VPArr[CurrentIndex], VPArr[CurrentIndex - 1], Scopes.Peek());
                    }
                    else if (LookUp(VPArr[CurrentIndex], 0) == false)
                        Insert(VPArr[CurrentIndex], VPArr[CurrentIndex - 1], 0);
                    else
                        return false; // REDECLARATION ERROR

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
            if (CONST() || FunctionCall() || ClassMemberCalls() || EXP())
                return true;
            IsSender = false;

            if (TokensArr[CurrentIndex] == "ID")
            {
                if (Scopes.Count > 0)
                {
                    if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                        return false;
                }
                else if (LookUp(VPArr[CurrentIndex], 0) == false)
                    return false;
               

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
                if (Scopes.Count > 0)
                {
                    if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                        return false;
                }
                else if (LookUp(VPArr[CurrentIndex], 0) == false)
                    return false;

                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "Inc_Dec")
                {

                    if (String.IsNullOrEmpty(GetDataype(VPArr[CurrentIndex - 1], Scopes.Peek())))
                        return false;

                    string DT = GetDataype(VPArr[CurrentIndex - 1], Scopes.Peek());

                    if (IncrementDecCompatibility(DT) == false)
                        return false;

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
                    StartScope();

                    CurrentIndex++;
                    if (Body())
                    {
                        if (TokensArr[CurrentIndex] == "CloseScope")
                        {
                            CloseScope();
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
            int reset = CurrentIndex;





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
            if ( TokensArr[CurrentIndex] == "INTEGER_CONST" || TokensArr[CurrentIndex] == "FLOAT_CONST" || TokensArr[CurrentIndex] == "STRING_CONST" || TokensArr[CurrentIndex] == "CHAR_CONST")
            { CurrentIndex++; return true; }

            if (TokensArr[CurrentIndex] == "ID")
            {
                if (Scopes.Count > 0)
                {
                    if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                        return false;
                }
                else if (LookUp(VPArr[CurrentIndex], 0) == false)
                    return false;
                
                CurrentIndex++; 
                return true; 
            
            
            }

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
                if (TokensArr[CurrentIndex] == "OpenParenthesis")
                {
                    CurrentIndex++;
                    if (EXP())
                    {
                        if (TokensArr[CurrentIndex] == "CloseParenthesis")
                        {
                            CurrentIndex++;
                            if (TokensArr[CurrentIndex] == "OpenScope")
                            {
                                StartScope();

                                CurrentIndex++;
                                if (Body())
                                {

                                    if (TokensArr[CurrentIndex] == "CloseScope")
                                    {
                                        CloseScope();

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
                        if (Scopes.Count > 0)
                        {
                            if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                                return false;
                        }
                        else if (LookUp(VPArr[CurrentIndex], 0) == false)
                            return false;

                        CurrentIndex++;
                        if (TokensArr[CurrentIndex] == "CloseParenthesis")
                        {
                            CurrentIndex++;

                            if (TokensArr[CurrentIndex] == "OpenScope")
                            {
                                StartScope();

                                CurrentIndex++;

                                if (TokensArr[CurrentIndex] == "From")
                                {
                                    CurrentIndex++;
                                    if (FromList())
                                    {

                                        if (TokensArr[CurrentIndex] == "CloseScope")
                                        {

                                            CloseScope();
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
            if (TokensArr[CurrentIndex] == "INTEGER_CONST" || TokensArr[CurrentIndex] == "CHAR_CONST")
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
                        StartScope();

                        CurrentIndex++;
                        if (Body())
                        {

                            if (TokensArr[CurrentIndex] == "CloseScope")
                            {
                                CloseScope();
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
                        StartScope();
                        CurrentIndex++;
                        if (Body())
                        {

                            if (TokensArr[CurrentIndex] == "CloseScope")
                            {
                                CloseScope();
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
                                    StartScope();

                                    CurrentIndex++;
                                    if (Body())
                                    {

                                        if (TokensArr[CurrentIndex] == "CloseScope")
                                        {

                                            CloseScope();
                                            CurrentIndex++;

                                            if (TokensArr[CurrentIndex] == "Else")
                                            {
                                                CurrentIndex++;
                                                if (TokensArr[CurrentIndex] == "OpenScope")
                                                {
                                                    StartScope();
                                                    CurrentIndex++;
                                                    if (Body())
                                                    {
                                                        if (TokensArr[CurrentIndex] == "CloseScope")
                                                        {
                                                            CloseScope();
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


            if (Multiple_St())
                return true;

            if (SingleSt())
                return true;

            if (TokensArr[CurrentIndex] == "CloseScope")
                return true;

            CurrentIndex = reset;
            return false;

        }
        public static bool SingleSt()
        {
            int reset = CurrentIndex;

            if (Declaration() || Initialization() || Inc_Dec() || FunctionCall() || TerminateSt() || NextSt() || Return_St() || Until_Loop() || Run_Until() || If_Then_Else() || Check_From())
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

            else if (TokensArr[CurrentIndex] == "CloseScope")
            {

                return true;
            }
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
            FunctionDataExpression = null;
           
            int reset = CurrentIndex;

            if (TokensArr[CurrentIndex] == "ID")
            {
                if (Scopes.Count > 0)
                {
                    if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                        return false;
                }
                else if (LookUp(VPArr[CurrentIndex], 0) == false)
                    return false;

                FunctionName = VPArr[CurrentIndex];
                
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
                FunctionR = VPArr[CurrentIndex - 1];

                CurrentIndex++;
                if (TokensArr[CurrentIndex] == "ID")
                {
                    if (Scopes.Count > 0)
                    {
                        if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                            return false;
                    }
                    else if (LookUp(VPArr[CurrentIndex], 0) == false)
                        return false;

                    FunctionName = VPArr[CurrentIndex];
                

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
                FunctionR = "VOID";
                CurrentIndex++;
                if (CallParameters())
                {

                    FunctionDataExpression = FunctionR + "." + FunctionParams;
                    
                    if (LookUpFunction(FunctionName, FunctionDataExpression) == false)
                        return false;

                    return true;

                }

            }

            CurrentIndex = reset;
            return false;

        }
        public static bool CallParameters()
        {
            FunctionParams = null;
            int reset = CurrentIndex;
            if (TokensArr[CurrentIndex] == "ID")
            {
                if (Scopes.Count > 0)
                {
                    if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                        return false;
                }
                else if (LookUp(VPArr[CurrentIndex], 0) == false)
                    return false;

                FunctionParams += VPArr[CurrentIndex] + ".";
                
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
                if (Scopes.Count > 0)
                {
                    if (LookUp(VPArr[CurrentIndex], Scopes.Peek()) == false)
                        return false;
                }
                else if (LookUp(VPArr[CurrentIndex], 0) == false)
                    return false;

                FunctionParams += VPArr[CurrentIndex] + ".";

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
                        StartScope();
                        CurrentIndex++;
                        if (ClassBody())
                        {


                            return true;


                        }



                    }



                }



            }


            CurrentIndex = reset;
            return false;
        }

        public static bool ClassBody()
        {
            int reset = CurrentIndex;

            if (Body())
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
                CloseScope();

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
                    else
                    {
                        IsSender = true;
                        if (FunctionCall())
                        {


                            return true;


                        }

                        IsSender = false;
                        if (EXP())
                        {


                            return true;


                        }
                    }



                }




            }





            CurrentIndex = reset;
            return false;
        }
        public static bool EXP()
        {
            bool sn = false;



            if (OR())
            {


                sn = true;
            }



            return sn;
        }
        static bool OR()
        {
            bool sn = false;



            if (AND())
            {

                if (OR1())
                {

                    sn = true;
                }
            }


            return sn;
        }
        static bool OR1()
        {
            bool sn = false;
            if (TokensArr[CurrentIndex] == "Conditional")
            {

                string str = Compatible(VPArr[CurrentIndex - 1], VPArr[CurrentIndex + 1], VPArr[CurrentIndex]);

                if (String.IsNullOrEmpty(str))
                    return false;

              

               
                CurrentIndex++;
                if (AND())
                {

                    if (OR1())
                    {
                        sn = true;
                    }
                }
            }
            else if (TokensArr[CurrentIndex] == "Assignment" || TokensArr[CurrentIndex] == "LineBreak" || TokensArr[CurrentIndex] == "CloseParenthesis" || TokensArr[CurrentIndex] == "Then")
            {
                sn = true;
            }


            return sn;
        }
        static bool AND()
        {
            bool sn = false;



            if (RE())
            {

                if (AND1())
                {

                    sn = true;
                }
            }

            return sn;


        }

        static bool AND1()
        {
            bool sn = false;
            if (TokensArr[CurrentIndex] == "Conditional")
            {

                string str = Compatible(VPArr[CurrentIndex - 1], VPArr[CurrentIndex + 1], VPArr[CurrentIndex]);

                if (String.IsNullOrEmpty(str))
                    return false;

                CurrentIndex++;
                if (RE())
                {

                    if (AND1())
                    {
                        sn = true;
                    }
                }
            }
            else if (TokensArr[CurrentIndex] == "Assignment" || TokensArr[CurrentIndex] == "LineBreak" || TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                sn = true;
            }
            return sn;
        }
        static bool RE()
        {
            bool sn = false;



            if (Ex())
            {

                if (RE1())
                {

                    sn = true;
                }
            }

            return sn;


        }
        public static bool RE1()
        {
            bool sn = false;
            if (TokensArr[CurrentIndex] == "Relational")
            {
                string str = Compatible(VPArr[CurrentIndex - 1], VPArr[CurrentIndex + 1], VPArr[CurrentIndex]);

                if (String.IsNullOrEmpty(str))
                    return false;

                CurrentIndex++;
                if (Ex())
                {

                    if (RE1())
                    {

                        sn = true;
                    }
                }
            }
            else if (TokensArr[CurrentIndex] == "Conditional" || TokensArr[CurrentIndex] == "Assignment" || TokensArr[CurrentIndex] == "OR" || TokensArr[CurrentIndex] == "LineBreak" || TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                sn = true;
            }
            return sn;
        }


        static bool Ex()
        {
            bool sn = false;



            if (T())
            {

                if (Ex1())
                {

                    sn = true;
                }
            }

            return sn;

        }

        static bool Ex1()
        {
            bool sn = false;
            if (TokensArr[CurrentIndex] == "AddSubOp")
            {

                string str = Compatible(VPArr[CurrentIndex - 1], VPArr[CurrentIndex + 1], VPArr[CurrentIndex]);
                
                if (String.IsNullOrEmpty(str))
                    return false;
                
                CurrentIndex++;
                if (T())
                {

                    if (Ex1())
                    {
                        sn = true;
                    }
                }
            }
            else if (TokensArr[CurrentIndex] == "Relational" || TokensArr[CurrentIndex] == "Conditional" || TokensArr[CurrentIndex] == "Assignment" || TokensArr[CurrentIndex] == "OR" || TokensArr[CurrentIndex] == "LineBreak" || TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                sn = true;
            }


            return sn;
        }
        static bool T()
        {
            bool sn = false;



            if (FS())
            {

                if (T1())
                {

                    sn = true;
                }
            }

            return sn;
        }

        static bool T1()
        {
            bool sn = false;
            if (TokensArr[CurrentIndex] == "MulDivOp")
            {
                string str = Compatible(VPArr[CurrentIndex - 1], VPArr[CurrentIndex + 1], VPArr[CurrentIndex]);

                if (String.IsNullOrEmpty(str))
                    return false;

                CurrentIndex++;
                if (FS())
                {

                    if (T1())
                    {

                        sn = true;
                    }
                }
            }
            else if (TokensArr[CurrentIndex] == "AddSubOp" || TokensArr[CurrentIndex] == "Relational" || TokensArr[CurrentIndex] == "Conditional" || TokensArr[CurrentIndex] == "Assignment" || TokensArr[CurrentIndex] == "LineBreak" || TokensArr[CurrentIndex] == "CloseParenthesis")
            {
                sn = true;
            }
            return sn;
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









    }
}
