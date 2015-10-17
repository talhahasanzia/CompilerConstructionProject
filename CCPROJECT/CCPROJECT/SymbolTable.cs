using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCPROJECT
{
    class SymbolTable
    {


        public string Name { get; set; }
        public string DataType { get; set; }
        public int Scope { get; set; }
        public List<string> DataTypes;
    }
}
