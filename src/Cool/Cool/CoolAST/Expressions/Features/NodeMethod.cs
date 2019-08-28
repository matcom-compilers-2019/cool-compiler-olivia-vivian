using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
   public class NodeMethod:NodeFeatures
    {
        public NodeID mNAme { get; set; }
        public List<NodeFields> args { get; set; }
        public NodeType retType { get; set; }
        public NodeExpr body { get; set; }

        public NodeMethod(ParserRuleContext context) : base(context)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
