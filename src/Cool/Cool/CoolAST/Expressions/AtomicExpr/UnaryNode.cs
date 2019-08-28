using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
   public abstract class UnaryNode:NodeExpr
    {
        public UnaryNode(ParserRuleContext context) : base(context) { }

        public UnaryNode(int l, int c) : base(l, c) { }
    }
}
