using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
namespace Cool.CoolAST
{
   public abstract class NodeFuncCall:NodeExpr

    {
        public NodeID FuncName { get; set; }

        public List<NodeExpr> Arguments { get; set; }

        public NodeFuncCall(ParserRuleContext context) : base(context)
        {

        }
    }
}
