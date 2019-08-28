using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
    public class NodeVoid:UnaryNode
    {
        public string StatType { get; }
        public NodeVoid(string t, int l = 0, int c = 0) : base(l, c)
        {
            StatType = t;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
