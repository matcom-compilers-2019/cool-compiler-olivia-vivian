using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
   public  class NodeAttr:NodeFeatures
    {
        public NodeFields Field { get; set; }
        public NodeExpr Exp { get; set; }

        public NodeAttr(ParserRuleContext context) : base(context)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
