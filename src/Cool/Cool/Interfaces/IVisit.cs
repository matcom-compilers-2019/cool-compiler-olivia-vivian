using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cool.Semantic_Checking
{
  public  interface IVisit
    {
        void Accept(IVisitor visitor);
    }
}
