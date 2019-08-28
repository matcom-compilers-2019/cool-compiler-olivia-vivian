using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
namespace Cool.CoolAST
{
   public abstract class NodeUnaryOperation:NodeExpr
    {
        public NodeExpr Operand { get; set; }

        public abstract string Symbol { get; }

        public NodeUnaryOperation(ParserRuleContext context) : base(context)
        {
        }
    }
}
