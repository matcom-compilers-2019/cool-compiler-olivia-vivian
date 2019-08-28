using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
   public class NodeAssign:NodeExpr
    {
        public IdentNode ID { get; set; }
        public NodeExpr ExpressionRight { get; set; }

        public NodeAssign(ParserRuleContext context) : base(context)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
