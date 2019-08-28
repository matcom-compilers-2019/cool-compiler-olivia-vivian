using Cool.Semantic_Checking;
using System.Collections.Generic;

namespace Cool.CodeGeneration.TAC
{
    public class VT

    {
      
        Dictionary<(string, string), List<string>> mtds;
        Dictionary<(string, string), string> attrs;
        Dictionary<string, List<(string, string)>> vtables;
        IContext context;
       

        
        public static List<string> IO = new List<string> { "out_string", "out_int", "in_string", "in_int" };
     

        public VT(IContext c)
        {
            
           
            mtds = new Dictionary<(string, string), List<string>>();
            attrs = new Dictionary<(string, string), string>();
            vtables = new Dictionary<string, List<(string, string)>>();
            context = c;
            NewClass("Object");
            
                NewMethod("Object", "abort", new List<string>());
                NewMethod("Object", "type_name", new List<string>());
                NewMethod("Object", "copy", new List<string>());


            NewClass("IO");
            NewMethod("IO", "out_string", new List<string>() { "String" });
            NewMethod("IO", "out_int", new List<string>() { "Int" });
            NewMethod("IO", "in_string", new List<string>());
            NewMethod("IO", "in_int", new List<string>());

            NewClass("String");
            NewMethod("String", "length", new List<string>());
            NewMethod("String", "concat", new List<string>() { "String" });
            NewMethod("String", "substr", new List<string>() { "Int", "Int" });

            NewClass("Int");
            NewClass("Bool");
        }

    

        public void NewMethod(string c, string m, List<string> at)
        {
            mtds[(c, m)] = at;

            string label = c + "." + m;
            if (c != "Object")
            {
                string parent = context.GetType(c).Parent.Name;
                int i = vtables[parent].FindIndex((x) => x.Item2 == m);
               
                if (i != -1)
                {
                    vtables[c][i] = (c, m);
                    return;
                }
            }

            vtables[c].Add((c, m));
        }
        public void NewClass(string cclass)
        {
            vtables[cclass] = new List<(string, string)>();

            if (cclass != "Object")
            {
                string parent = context.GetType(cclass).Parent.Name;
                vtables[parent].ForEach(m => vtables[cclass].Add(m));
            }
        }
        public int ClassLarge(string cclass)
        {
            return (vtables[cclass].Count + 3);
        }
        public int Offst(string c, string i)
        {
            return vtables[c].FindIndex((x) => x.Item2 == i) + 3;
        }

     

        public List<string> ParamsTypes(string c, string m)
        {
            return mtds[Def(c, m)];
        }

        public void NewAttr(string c, string a, string t)
        {
            attrs[(c, a)] = t;

            if (c != "Object")
            {
                string parent = context.GetType(c).Parent.Name;
                int i = vtables[parent].FindIndex((x) => x.Item2 == a);
                
                if (i != -1)
                    return;
            }

            vtables[c].Add((c, a));
        }

        public string AttrType(string cclass, string attr)
        {
            return attrs[Def(cclass, attr)];
        }

        public (string, string) Def(string c, string i)
        {
            return vtables[c].Find((x) => x.Item2 == i);
        }

    }
}
