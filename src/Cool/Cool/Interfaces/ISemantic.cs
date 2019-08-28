using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cool.CoolAST;
namespace Cool.Semantic_Checking
{
    interface ISemantic
    {
        NodeProgram SemanticChecker(NodeProgram node, IContext c, ICollection<string> _err);
    }
}
