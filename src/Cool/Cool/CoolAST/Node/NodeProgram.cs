using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
   public class NodeProgram:Node,IVisit
    {
        public List<NodeClass> programClasses { get; set; }

        public NodeProgram(ParserRuleContext context) : base(context)
        {
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
