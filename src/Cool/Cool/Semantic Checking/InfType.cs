using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cool.CoolAST;
namespace Cool.Semantic_Checking
{
    public class InfType
    {
        public InfType Parent { get; set; }
        public NodeClass _class { get; set; }
        public string Name { get; set; }
       
        public int Level { get; set; }

        public InfType()
        {
            Parent = null;
            _class = null;
            Name = "Object";
            Level = 0;
        }

        public InfType(string n, InfType p, NodeClass c)
        {
            Name = n;
            Parent = p;
            _class = c;
            Level = p.Level + 1;
        }

        
       public virtual bool Inherit(InfType other)
        {
            if (Name == other.Name) return true;
            return Parent.Inherit(other);
        }

       
        
        private static InfTypeObj objectType = new InfTypeObj();

        public static InfTypeObj OBJECT => objectType;

        public class InfTypeObj : InfType
        {

            public override bool Inherit(InfType other)
            {
                return Name == other.Name;
            }
        }
    

        public override string ToString()
        {
            return Name + " :inherits from" + Parent.Name;
        }
    }
}
