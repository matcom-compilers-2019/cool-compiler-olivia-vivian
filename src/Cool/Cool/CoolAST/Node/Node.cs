using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
namespace Cool.CoolAST
{
   public abstract class Node
    {
        public Dictionary<string, dynamic> Attrs { get; }
        public int Ln { get; }

        public int Clmn { get; }
        
         public Node(ParserRuleContext context)
        {
            Attrs = new Dictionary<string, dynamic>();
            Ln = context.Start.Line;
            Clmn = context.Start.Column + 1;
            
        }

        public Node(int l, int c)
        {
            Ln = l;
            Clmn = c;
        }
    }
}
