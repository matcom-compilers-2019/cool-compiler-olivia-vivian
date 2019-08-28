using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
namespace Cool.CoolAST
{
  public abstract class NodeKW:NodeExpr
    {
        public NodeKW(ParserRuleContext context) : base(context)
        {
        }
    }
}
