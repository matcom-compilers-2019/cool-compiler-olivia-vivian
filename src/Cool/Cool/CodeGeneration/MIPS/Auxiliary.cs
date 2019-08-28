using System;
using System.Collections.Generic;
using System.Text;
using Cool.CodeGeneration.TAC;
using Cool.Interfaces;

namespace Cool.CodeGeneration.MIPS
{
   public class Auxiliary:IGenCodeVisitor
    {
        int actualLine;
        string actualFunction;
        int sc;

        public Dictionary<string, int> sizeFunVar;
        public Dictionary<string, (int, int)> limitsFun;
        public Dictionary<string, int> paramsCountFun;
        public Dictionary<string, int> strCount;
        public Dictionary<string, string> Inherit;

       
        public Auxiliary(List<Instructions> lin)
        {
            sc = 0;
            limitsFun = new Dictionary<string, (int, int)>();
            paramsCountFun = new Dictionary<string, int>();
            strCount = new Dictionary<string, int>();
            sizeFunVar = new Dictionary<string, int>();
             Inherit = new Dictionary<string, string>();
           

            for (actualLine = 0; actualLine < lin.Count; ++actualLine)
            {
                lin[actualLine].Accept(this);
            }
        }

        public void Visit(MyLabel line)
        {
            if (line.H[0] != '_')
            {
                actualFunction = line.Label;
                sizeFunVar[actualFunction] = 0;
                limitsFun[actualFunction] = (actualLine, -1);
                paramsCountFun[actualFunction] = 0;
            }
        }

        public void Visit(Locate line)
        {
            sizeFunVar[actualFunction] = Math.Max(sizeFunVar[actualFunction], line.var + 1);
        }


        public void Visit(BinOp line)
        {
            sizeFunVar[actualFunction] = Math.Max(sizeFunVar[actualFunction], line.varAss + 1);
        }

        public void Visit(UnaryOp line)
        {
            sizeFunVar[actualFunction] = Math.Max(sizeFunVar[actualFunction], line.varassign + 1);
        }

        public void Visit(StringToVar line)
        {
            sizeFunVar[actualFunction] = Math.Max(sizeFunVar[actualFunction], line.Leftie + 1);
            if (!strCount.ContainsKey(line.Rightie)) { strCount[line.Rightie] = sc++; }
             }

        public void Visit(VarToVar line)
        {
            sizeFunVar[actualFunction] = Math.Max(sizeFunVar[actualFunction], line.Leftie + 1);
        }

        public void Visit(MemoryToVar line)
        {
            sizeFunVar[actualFunction] = Math.Max(sizeFunVar[actualFunction], line.Leftie + 1);
        }

        public void Visit(ConstantToVar line)
        {
            sizeFunVar[actualFunction] = Math.Max(sizeFunVar[actualFunction], line.Leftie + 1);
        }

        

        public void Visit(LabelToVar line)
        {
            sizeFunVar[actualFunction] = Math.Max(sizeFunVar[actualFunction], line.Leftie + 1);
        }

       

        public void Visit(Param line)
        {
            sizeFunVar[actualFunction] = Math.Max(sizeFunVar[actualFunction], line.cv + 1);
            ++paramsCountFun[actualFunction];
        }

        public void Visit(Return line)
        {
            limitsFun[actualFunction] = (limitsFun[actualFunction].Item1, actualLine);
        }

        public void Visit(StringToMemory line)
        {
            if (!strCount.ContainsKey(line.Rightie)) { strCount[line.Rightie] = sc++; }
            
                
            
        }

        public void Visit(Inherits line)
        {
            Inherit[line.chld] = line.prnt;

            if (!strCount.ContainsKey(line.chld))
                strCount[line.chld] = sc++;
            if (!strCount.ContainsKey(line.prnt))
                strCount[line.prnt] = sc++;
        }

        public void Visit(VarToMemory line)
        {
            return;
            throw new NotImplementedException();
        }

        public void Visit(ConstantToMemory line)
        {
            return;
            throw new NotImplementedException();
        }


        public void Visit(LabelToMemory line)
        {
            return;
            throw new NotImplementedException();
        }

        public void Visit(LabelCall line)
        {
            return;
            throw new NotImplementedException();
        }

        public void Visit(CallAddr line)
        {
            return;
            throw new NotImplementedException();
        }

        public void Visit(Comments line)
        {
            return;
            throw new NotImplementedException();
        }

        public void Visit(Jump line)
        {
            return;
            throw new NotImplementedException();
        }

        public void Visit(CondJump line)
        {
            return;
            throw new NotImplementedException();
        }


        public void Visit(NullToVar line)
        {
            return;
            throw new NotImplementedException();
        }

        public void Visit(OutParam line)
        {
            return;
            throw new NotImplementedException();
        }

        public void Visit(InParam line)
        {
            return;
            throw new NotImplementedException();
        }

    }
}
