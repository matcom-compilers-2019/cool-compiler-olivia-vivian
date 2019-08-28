using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
  public  class NodeBool:UnaryNode
    {
        public bool BoolValue { get; set; }

        public NodeBool(ParserRuleContext context, string t) : base(context)
        {
            BoolValue = bool.Parse(t);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
