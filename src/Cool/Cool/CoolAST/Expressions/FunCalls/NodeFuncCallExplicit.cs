using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
    public class NodeFuncCallExplicit:NodeFuncCall
    {
        public NodeExpr Expression { get; set; }
        public NodeType IdType { get; set; }

        public NodeFuncCallExplicit(ParserRuleContext context) : base(context)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
