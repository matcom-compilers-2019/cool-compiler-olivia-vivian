using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
    public class NodeCase:NodeKW
    {
        public NodeExpr ExpressionCase { get; set; }
        public List<Tuple<NodeFields , NodeExpr >> Branches { get; set; }
    public int BranchSelected { get; set; }

    public NodeCase(ParserRuleContext context) : base(context)
        {
        Branches = new List<Tuple<NodeFields, NodeExpr>> ();
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
}
