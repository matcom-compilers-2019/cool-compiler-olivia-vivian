using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
   public class IdentNode:UnaryNode
    {
        public string Ident { get; set; }

        public IdentNode(ParserRuleContext context, string t) : base(context)
        {
            Ident = t;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
