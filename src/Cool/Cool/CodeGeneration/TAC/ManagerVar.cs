using System;
using System.Collections.Generic;
using System.Text;

namespace Cool.CodeGeneration.TAC
{
   public class ManagerVar
    {
        public string c { set; get; }

        Dictionary<string, Stack<(int, string)>> l;
        Stack<int> StacVc;
        public int vc { set; get; }

        

        public ManagerVar()
        {
            vc = 0;
            l = new Dictionary<string, Stack<(int, string)>>();
            StacVc = new Stack<int>();
        }


        public void VCPush()
        {
            StacVc.Push(vc);
        }

        public int VCPeek()
        {
            return StacVc.Peek();
        }

        public void VCPop()
        {
            vc = StacVc.Pop();
        }


        public void VarPush(string n, string t)
        {
            if (!l.ContainsKey(n))
                l[n] = new Stack<(int, string)>();

            l[n].Push((vc, t));
        }

        public void VarPop(string n)
        {
            if (l.ContainsKey(n) && l[n].Count > 0)
                l[n].Pop();
        }

        public (int, string) GetVariable(string n)
        {
            if (l.ContainsKey(n) && l[n].Count > 0)
                return l[n].Peek();
            else
                return (-1, "");
        }

     

        public int VCInc()
        {
            ++vc;
            return vc;
        }
    }
}
