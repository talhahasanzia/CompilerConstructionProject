using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCPROJECT
{
     
   

    partial class Semantic
    {
        public static string MessageString = null;
        static string[] TokensArr = null;
        static string[] VPArr = null;
        static int CurrentIndex, CurrentScope;
        static Stack<int> Scopes = new Stack<int>();
      static   public List<SymbolTable> SymbolTableList = new List<SymbolTable>();

        public Semantic()
        {



            ReadTokensFromFile rt = new ReadTokensFromFile();

            TokensArr = ReadTokensFromFile.CP.ToArray();
            VPArr = ReadTokensFromFile.VP.ToArray();

            CurrentIndex = 0;
            CurrentScope = 0;

            bool temp = Starting(); ;

            if (temp)
                MessageString = "Successful Semantic Parsing.";
            else
                MessageString = "Semantic Error.";
        
        
        }


        public static void StartScope()
        {
            CurrentScope++;
            Scopes.Push(CurrentScope);
        }
        public static void CloseScope()
        {
            Scopes.Pop();
        }


        public static bool LookUp(string Name, int Scope)
        {



            if (SymbolTableList.Any(X => X.Name == Name && X.Scope == Scope)) // lookup in current scope
                return true;
            else if (SymbolTableList.Any(X => X.Name == Name && X.Scope == 0)) // lookup in global
                return true;
            else
            {

                if (FindInAllScopes(Name)) // lookup in parents
                {

                    return true;
                
                
                }
            
            }
            return false;
        }



        public static bool FindInAllScopes(string Name)
        {
            bool Found = false;

            foreach (int s in Scopes)
            {


                if (SymbolTableList.Any(X => X.Name == Name && X.Scope == s))
                {
                    Found = true;
                    break;
                }
            
            
            }



            return Found;
        }



        public static void Insert(string Name, string Datatype, int scope)
        {

            SymbolTable st = new SymbolTable();

            st.Name = Name;
            st.DataType = Datatype;
            st.Scope = scope;

            SymbolTableList.Add(st);


            ;
        }
        public static void InsertClass(string Name, List<string> Datatypes)
        {

            SymbolTable st = new SymbolTable();
            st.Name = Name;


            st.DataTypes = Datatypes;

            st.Scope = 0;

            SymbolTableList.Add(st);
        
        
        
        
        }
        public static void InsertFunction(string Name, string Params)
        {

            SymbolTable st = new SymbolTable();
            st.Name = Name;
            st.DataType = Params;
            st.Scope = 0;

            SymbolTableList.Add(st);
        
        
        }
        public static bool LookUpFunction(string Name, string DataExpression)
        {

             if (SymbolTableList.Any(X => X.Name == Name && X.DataType== DataExpression)) // lookup in global
                return true;


            return false;
        }
        public static string Compatible(string LT, string RT, string OP)
        {

            string resultType = null;

            switch (OP)
            {
                case "+":
                    if ((LT == "INTEGER" || LT == "FLOAT") && (RT == "INTEGER" || RT == "FLOAT")
                        || ((LT == "STRING" && RT == "STRING")))
                    {
                        if (LT == "STRING" && RT == "STRING")
                            resultType = "STRING";

                        else if (LT == "FLOAT" || RT == "FLOAT")
                            resultType = "FLOAT";

                        else if (LT == "INTEGER" && RT == "INTEGER")
                            resultType = "INTEGER";
                    }
                    break;

                case "-":
                case "*":
                case "/":

                    if ((LT == "INTEGER" || LT == "FLOAT") && (RT == "INTEGER" || RT == "FLOAT"))
                    {

                        if (LT == "FLOAT" || RT == "FLOAT")
                            resultType = "FLOAT";

                        else if (LT == "INTEGER" && RT == "INTEGER")
                            resultType = "INTEGER";
                    }
                    break;

                case "AND":
                case "OR":
                        resultType = "True";
                    
                    break;

                case "GREAT":
                case "LESS":
                case "EGREAT":
                case "ELESS":
                    if ((LT == "INTEGER" || LT == "FLOAT") && (RT == "INTEGER" || RT == "FLOAT"))
                    {
                        resultType = "True";
                    }
                    break;

                case "EQUALS":

                    if (((LT == "INTEGER" || LT == "FLOAT") && (RT == "INTEGER" || RT == "FLOAT"))
                        || ((LT == "CHARAC" && RT == "CHARAC"))
                        || ((LT == "STRING" && RT == "STRING")))
                    { resultType = "True"; }
                    break;
            }

            return resultType;
        
        }

        public static bool IncrementDecCompatibility(string DataType)
        {

            if (DataType == "FLOAT" || DataType == "INTEGER")
            {

                return true;
            
            
            
            }


            return false;
        }
        public static string GetDataype(string Name, int Scope)
        {

            string s = null;

            if (LookUp(Name, Scope))
            {
                foreach(SymbolTable st in SymbolTableList)
                {


                    if (st.Name == Name && st.Scope == Scope)
                    {


                        s = st.DataType;
                        break;
                    }
                    else if (st.Name == Name && st.Scope == 0)
                    {

                        {


                            s = st.DataType;
                            break;
                        }

                    }
                    else
                    {
                        bool Flag = false;

                        foreach (int sc in Scopes)
                        {

                            if (st.Name == Name && st.Scope == sc)
                            {


                                s = st.DataType;
                                Flag = true;
                                break;

                            }
                            


                        }

                        if (Flag)
                        {
                            break;
                        
                        }
                    
                    }
                
                }
            
            }

            return s;
        
        }
    }
}
