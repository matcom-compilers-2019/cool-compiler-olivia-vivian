using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
   public class NodeType:TempNode
    {
        public string type { get; set; }

        public NodeType(ParserRuleContext context, string t) : base(context)
        {
            type = t;
        }

        public NodeType(int l, int c, string t) : base(l, c)
        {
            type = t;
        }

        public override string ToString()
        {
            return type;
        }

        #region OBJECT
        private static readonly TypeObj objectType = new TypeObj();

        public static TypeObj OBJECT => objectType;

        public class TypeObj : NodeType
        {
            public TypeObj(int l = 0, int c = 0, string n = "Object") : base(l, c, n) { }
        }
        #endregion

    }
}
