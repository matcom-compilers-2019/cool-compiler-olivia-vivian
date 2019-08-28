using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;

namespace Cool.CoolAST

{
    public  abstract class NodeExpr:Node,IVisit
    {
        public InfType typeStat = InfType.OBJECT;

        public NodeExpr(ParserRuleContext context) : base(context) { }

        public NodeExpr(int l, int c) : base(l, c) { }

        public abstract void Accept(IVisitor visitor);
    }
}
