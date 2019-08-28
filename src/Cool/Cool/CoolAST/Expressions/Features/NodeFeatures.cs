using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
   public abstract class NodeFeatures:Node,IVisit
    {
        public NodeFeatures(ParserRuleContext context) : base(context) { }

        public abstract void Accept(IVisitor visitor);
    }
}
