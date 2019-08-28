using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
namespace Cool.CoolAST
{
   public abstract class NodeBinaryOp:NodeExpr
    {
        public NodeExpr LeftOperand { get; set; }
        public NodeExpr RightOperand { get; set; }

        public NodeBinaryOp(ParserRuleContext context) : base(context)
        {
        }

        public abstract string Symbol { get; }
    }
}
