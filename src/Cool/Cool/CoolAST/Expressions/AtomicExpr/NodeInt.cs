using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
   public class NodeInt:UnaryNode
    {
        public int Number { get; set; }

        public NodeInt(ParserRuleContext context, string t) : base(context)
        {
             Number= int.Parse(t);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
