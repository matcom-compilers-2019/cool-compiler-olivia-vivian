using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
   public class NodeString:UnaryNode
    {
        public string Characters { get; set; }

        public NodeString(ParserRuleContext context, string t) : base(context)
        {
            Characters = "";

          
            for (int i = 1; i < t.Length - 1; ++i)
            {
                Characters += t[i];
            }

          
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
