using System;
using System.Collections.Generic;
using System.Text;
using Cool.CodeGeneration.TAC;
namespace Cool.Interfaces
{
  public  interface IGenCodeVisitor
    {

        void Visit(Locate line);

        void Visit(VarToMemory line);

        void Visit(VarToVar line);

        void Visit(ConstantToMemory line);

        void Visit(MemoryToVar line);

        void Visit(ConstantToVar line);

        void Visit(StringToVar line);

        void Visit(StringToMemory line);

        void Visit(LabelToVar line);

        void Visit(LabelToMemory line);

        void Visit(LabelCall line);

        void Visit(CallAddr line);

        void Visit(Comments line);

        void Visit(Jump line);

        void Visit(CondJump line);

        void Visit(MyLabel line);

        void Visit(NullToVar line);

        void Visit(BinOp line);

        void Visit(UnaryOp line);

        void Visit(Param line);

        void Visit(OutParam line);

        void Visit(InParam line);

        void Visit(Return line);
        void Visit(Inherits line);
    }
    
}
