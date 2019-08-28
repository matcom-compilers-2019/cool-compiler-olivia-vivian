using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Cool.Semantic_Checking;
namespace Cool.CoolAST
{
  public  class NodeClass:Node,IVisit
    {

        public List<NodeFeatures> classFeatures { get; set; }
        public IContext Context { get; set; }
        public NodeType ParentType { get; set; }
        public NodeType CType { get; set; }

     

        public NodeClass(ParserRuleContext context) : base(context)
        {
        }

        public NodeClass(int l, int c, string classN, string typeinh) : base(l, c)
        {
            CType= new NodeType(l, c, classN);
            ParentType = new NodeType(l, c, typeinh);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
