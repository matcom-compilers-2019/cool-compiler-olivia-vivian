using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cool.Semantic_Checking
{
  public  interface IContext
    {
        InfType GetType(string typetoget);
        bool AddType(string typetoadd, InfType type);
        
        bool IsTypeDefined(string var, out InfType vartype);
        bool IsMethodDefine(string funcName, InfType[] args, out InfType typeret);
        bool IsDefinedType(string typename, out InfType type);
        bool Def(string varname, InfType type);
        bool Def(string funcName, InfType[] args, InfType typeret);
        bool ChangeType(string name, InfType type);
        IContext contextParent { get; set; }
        InfType typeOfContext { get; set; }

        IContext CreateChild();
    }
}
