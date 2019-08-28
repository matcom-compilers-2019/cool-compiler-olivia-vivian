using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
namespace Cool.CoolAST
{
   public class NodeID:TempNode
    {
        public string Name { get; set; }

        public NodeID(ParserRuleContext context, string t) : base(context)
        {
            Name = t;
        }

        public NodeID(int l, int c, string t) : base(l, c)
        {
            Name = t;
        }

        public override string ToString()
        {
            return Name;
        }

      
        static readonly NameNull nullId = new NameNull();

        public static NameNull NULL => nullId;

        public class NameNull : NodeID
        {
            public NameNull(int l = 0, int c = 0, string n = "Null-Id-Object") : base(l, c, n) { }
        }
      
    }
}
