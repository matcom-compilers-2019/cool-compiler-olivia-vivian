using System;
using System.Collections.Generic;
using System.Text;
using Cool.Interfaces;

namespace Cool.CodeGeneration.TAC
{
    public abstract class Instructions
    {
        public abstract void Accept(IGenCodeVisitor visitor);
    }
    public class Locate : Instructions
    {
        public int var { get; }
        public int t { get; }

        public Locate(int v, int s)
        {
            var = v;
            t = s;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"t{var} = Alloc {t};";
        }
    }

    public class VarToMemory : Assign<int>
    {
        public int Offset { get; }
        public VarToMemory(int l, int r, int o = 0)
        {
            Leftie = l;
            Rightie = r;
            Offset = o;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"*(t{Leftie} + {Offset}) = t{Rightie}";
        }
    }

    public class VarToVar : Assign<int>
    {
        public VarToVar(int l, int r)
        {
            Leftie = l;
            Rightie = r;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"t{Leftie} = t{Rightie}";
        }
    }

    public class ConstantToMemory : Assign<int>
    {
        public int offst { get; }
        public ConstantToMemory(int l, int r, int o = 0)
        {
            Leftie = l;
            Rightie = r;
            offst = o;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"*(t{Leftie} + {offst}) = {Rightie}";
        }
    }

    public class MemoryToVar : Assign<int>
    {
        public int offst { get; }

        public MemoryToVar(int l, int r, int o = 0)
        {
            Leftie = l;
            Rightie = r;
            offst = o;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"t{Leftie} = *(t{Rightie} + {offst})"; ;
        }
    }


    public class ConstantToVar : Assign<int>
    {

        public ConstantToVar(int l, int r)
        {
            Leftie = l;
            Rightie = r;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"t{Leftie} = {Rightie}";
        }
    }

    public class StringToVar : Assign<string>
    {

        public StringToVar(int l, string r)
        {
            Leftie = l;
            Rightie = r;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"t{Leftie} = \"{Rightie}\"";
        }
    }

    public class StringToMemory : Assign<string>
    {
        public int offst { get; }
        public StringToMemory(int l, string r, int offset = 0)
        {
            Leftie = l;
            Rightie = r;
            offst = offset;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"*(t{Leftie} + {offst}) = \"{Rightie}\"";
        }
    }


    public class LabelToVar : Assign<MyLabel>
    {
        public LabelToVar(int l, MyLabel r)
        {
            Leftie = l;
            Rightie = r;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"t{Leftie} = \"{Rightie.Label}\"";
        }
    }

    public class LabelToMemory : Assign<MyLabel>
    {
        public int offst { get; }
        public LabelToMemory(int l, MyLabel r, int o)
        {
            Leftie = l;
            Rightie = r;
            offst = o;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"*(t{Leftie} + {offst}) = Label \"{Rightie.Label}\"";
        }
    }

    public class NullToVar : Instructions
    {
        public int v { get; }

        public NullToVar(int var)
        {
            v = var;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }


        public override string ToString()
        {
            return $"t{v} = NULL;";
        }
    }
    public class LabelCall : Instructions
    {
        public MyLabel M { get; }
        public int Res { get; }
        public LabelCall(MyLabel method, int resv = -1)
        {
            M = method;
            Res = resv;
        }

        public override string ToString()
        {
            if (Res == -1)
                return $"Call {M.Label};";
            else
                return $"t{Res} = Call {M.Label};";
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class CallAddr : Instructions
    {
        public int Addr { get; }
        public int Res { get; }
        public CallAddr(int a, int r = -1)
        {
            Addr = a;
            Res = r;
        }

        public override string ToString()
        {
            if (Res == -1)
                return $"Call t{Addr};";
            else
                return $"t{Res} = Call t{Addr};";
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    public class Comments : Instructions
    {
        string Comment { get; }
        public Comments(string comment)
        {
            Comment = comment;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "// " + Comment;
        }
    }

    public class Inherits : Instructions
    {
        public string chld;
        public string prnt;


        public Inherits(string c, string p)
        {
            chld = c;
            prnt = p;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"_class.{chld}: _class.{prnt}";
        }
    }
    public class Jump : Instructions
    {
        public MyLabel Label;

        public Jump(MyLabel label)
        {
            Label = label;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }


        public override string ToString()
        {
            return $"Goto {Label.Label}";
        }
    }

    public class CondJump : Instructions
    {
        public MyLabel Label;
        public int VarCond;
        public CondJump(int cv, MyLabel l)
        {
            Label = l;
            VarCond = cv;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"IfZ t{VarCond} Goto {Label.Label}";
        }
    }

    public class Label
    {
        public string H { get; }
        public string Etiq { get; }

        public Label(string head, string tag = "")
        {
            H = head;
            Etiq = tag;
        }

       
    }

    public class MyLabel : Instructions
    {
        public string H { get; }
        public string Etiquet { get; }

        public string Label
        {
            get
            {
                if (Etiquet != "")
                    return H + "." + Etiquet;
                else
                    return H;
            }
        }

        public MyLabel(string h, string e = "")
        {
            H = h;
            Etiquet = e;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return Label + ":";
            
        }

        
    }

    public class BinOp : Instructions
    {
        public int varAss { get; }
        public int lov { get; }
        public int rov { get; }
        public string Symbol { get; }

        public BinOp(int av, int l, int r, string symbol)
        {
            varAss = av;
            lov = l;
            rov = r;
            Symbol = symbol;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"t{varAss} = t{lov} {Symbol} t{rov}";
        }
    }

    public class UnaryOp : Instructions
    {
        public int varassign { get; }
        public int varOp { get; }
        public string Symbol { get; }

        public UnaryOp(int varass, int o, string symbol)
        {
            varassign = varass;
            varOp = o;
            Symbol = symbol;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"t{varassign} = {Symbol} t{varOp}";
        }
    }
    public class Param : Instructions
    {
        public int cv;
        public Param(int vcc)
        {
            cv = vcc;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "PARAM t" + cv + ";";
        }
    }

    public class OutParam : Instructions
    {
        int c;
        public OutParam(int t)
        {
            c = t;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"PopParam {c};";
        }
    }
    public class InParam : Instructions
    {
        public int v;
        public InParam(int var)
        {
            v = var;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "PushParam t" + v + ";";
        }
    }

    public class Return : Instructions
    {
        public int v { get; }

        public Return(int var = -1)
        {
            v = var;
        }

        public override void Accept(IGenCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "Return " + (v == -1 ? "" : "t" + v) + ";\n";
        }
    }

    public abstract class Assign<T> : Instructions
    {
        public int Leftie { get; protected set; }
        public T Rightie { get; protected set; }

    }
}