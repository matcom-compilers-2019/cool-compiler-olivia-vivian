using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Cool.CoolAST
{
  public abstract  class NodeAritmethicOp:NodeBinaryOp
    {
        public NodeAritmethicOp(ParserRuleContext context) : base(context)
        {
        }
    }
}
