using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cool.CoolAST;
namespace Cool.Semantic_Checking
{
  public  interface IVisitor
    {
        void Visit(NodeAritmethicOp node);
        void Visit(NodeAssign node);
        void Visit(NodeAttr node);
        void Visit(NodeBool node);
        void Visit(NodeCase node);
        void Visit(NodeClass node);
        void Visit(NodeCompare node);
        void Visit(NodeFuncCallExplicit node);
        void Visit(NodeFuncCall_Implicit node);
        void Visit(NodeEqual node);
        void Visit(NodeFields node);
        void Visit(IdentNode node);
        void Visit(NodeIf node);
        void Visit(NodeInt node);
        void Visit(NodeIsVoid node);
        void Visit(NodeLet node);
        void Visit(NodeMethod node);
        void Visit(NodeNegative node);
        void Visit(NodeNew node);
        void Visit(NodeNot node);
        void Visit(NodeProgram node);
        void Visit(NodeSelf node);
        void Visit(NodeBlock node);
        void Visit(NodeString node);
        void Visit(NodeVoid node);
        void Visit(NodeWhile node);
    }
}
