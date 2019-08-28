using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
   public class NodeIf:NodeKW
    {
        public NodeExpr Condition { get; set; }
        public NodeExpr Body { get; set; }
        public NodeExpr ElseBody { get; set; }

        public NodeIf(ParserRuleContext context) : base(context)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
