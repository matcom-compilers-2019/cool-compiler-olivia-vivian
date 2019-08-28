using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cool.CoolAST;
namespace Cool.Semantic_Checking
{
   public class Context:IContext
    {
        public IContext contextParent { get; set; }
        public InfType typeOfContext { get; set; }

        Dictionary<string, InfType> vars= new Dictionary<string, InfType>();

        
        Dictionary<string, Tuple<InfType[] , InfType >> funcs = new Dictionary<string, Tuple<InfType[], InfType>>();
        static Dictionary<string, InfType> declTypes = new Dictionary<string, InfType>();

        public Context() { }
        public Context(IContext parent, InfType type)
        {
            contextParent = parent;
            typeOfContext = type;
        }
        static Context()
        {
            declTypes.Add("Object", InfType.OBJECT);
            declTypes["Object"]._class = new NodeClass(-1, -1, "Object", "NULL");
            declTypes["Object"]._class.Context = new Context(NULL, declTypes["Object"]);

            declTypes.Add("Bool", new InfType { Name = "Bool", Parent = declTypes["Object"], Level = 1, _class = new NodeClass(-1, -1, "Bool", "Object") });
            declTypes.Add("Int", new InfType { Name = "Int", Parent = declTypes["Object"], Level = 1, _class = new NodeClass(-1, -1, "Int", "Object") });
            declTypes.Add("String", new InfType { Name = "String", Parent = declTypes["Object"], Level = 1, _class = new NodeClass(-1, -1, "String", "Object") });
            declTypes.Add("IO", new InfType { Name = "IO", Parent = declTypes["Object"], Level = 1, _class = new NodeClass(-1, -1, "IO", "Object") });


            declTypes["Bool"]._class.Context = new Context(declTypes["Object"]._class.Context, declTypes["Bool"]);
            declTypes["Int"]._class.Context = new Context(declTypes["Object"]._class.Context, declTypes["Int"]);
            declTypes["String"]._class.Context = new Context(declTypes["Object"]._class.Context, declTypes["String"]);
            declTypes["IO"]._class.Context = new Context(declTypes["Object"]._class.Context, declTypes["IO"]);

            declTypes["Object"]._class.Context.Def("abort", new InfType[0], declTypes["Object"]);
            declTypes["Object"]._class.Context.Def("type_name", new InfType[0], declTypes["String"]);
            declTypes["Object"]._class.Context.Def("copy", new InfType[0], declTypes["Object"]);

            declTypes["String"]._class.Context.Def("length", new InfType[0], declTypes["Int"]);
            declTypes["String"]._class.Context.Def("concat", new InfType[1] { declTypes["String"] }, declTypes["String"]);
            declTypes["String"]._class.Context.Def("substr", new InfType[2] { declTypes["Int"], declTypes["Int"] }, declTypes["String"]);

            declTypes["IO"]._class.Context.Def("out_string", new InfType[1] { declTypes["String"] }, declTypes["IO"]);
            declTypes["IO"]._class.Context.Def("out_int", new InfType[1] { declTypes["Int"] }, declTypes["IO"]);
            declTypes["IO"]._class.Context.Def("in_string", new InfType[0], declTypes["String"]);
            declTypes["IO"]._class.Context.Def("in_int", new InfType[0], declTypes["Int"]);

           

            

           


        }


     

        public bool IsTypeDefined(string name, out InfType type)
        {
            if (vars.TryGetValue(name, out type) || contextParent.IsTypeDefined(name, out type))
                return true;
            type = InfType.OBJECT;
            return false;

        }

        public bool IsMethodDefine(string name, InfType[] args, out InfType type)
        {
            type = InfType.OBJECT;
            if (funcs.ContainsKey(name) && funcs[name].Item1.Length == args.Length)
            {
                bool b = true;
                for (int i = 0; i < args.Length; ++i)
                    if (!(args[i].Inherit(funcs[name].Item1[i])))
                        b = false;
                if (b)
                {
                    type = funcs[name].Item2;
                    return true;
                }
            }

            if (contextParent.IsMethodDefine(name, args, out type))
                return true;

            type = InfType.OBJECT;
            return false;
        }

        public bool IsDefinedType(string name, out InfType type)
        {
            if (declTypes.TryGetValue(name, out type))
                return true;
            type = InfType.OBJECT;
            return false;
        }

        public bool Def(string name, InfType type)
        {
            if (vars.ContainsKey(name))
                return false;
            vars.Add(name, type);
            return true;
        }

        public bool Def(string name, InfType[] args, InfType type)
        {
            if (funcs.ContainsKey(name))
                return false;
            var t = new Tuple<InfType[], InfType>(args,type);

            funcs[name] = t;
            return true;
        }

      

        public IContext CreateChild()
        {
            return new Context()
            {
                contextParent = this,
                typeOfContext = this.typeOfContext
            };
        }

        public bool AddType(string name, InfType type)
        {
            declTypes.Add(name, type);
            return true;
        }

        public InfType GetType(string name)
        {
            if (declTypes.TryGetValue(name, out InfType type))
                return type;
            return InfType.OBJECT;
        }

        public bool ChangeType(string name, InfType type)
        {
            if (!vars.ContainsKey(name))
                vars.Add(name, type);
            vars[name] = type;
            return true;
        }



        private static ContextNull contextnull = new ContextNull();

        public static ContextNull NULL => contextnull;

        public class ContextNull : IContext
        {

            public IContext contextParent { get; set; }
            public InfType typeOfContext { get; set; }

            public bool AddType(string name, InfType type)
            {
                return false;
            }

            public bool ChangeType(string name, InfType type)
            {
                return false;
            }

            public IContext CreateChild()
            {
                return new Context()
                {
                    contextParent = NULL,
                    typeOfContext = null
                };
            }

            public bool Def(string name, InfType type)
            {
                return false;
            }

            public bool Def(string name, InfType[] args, InfType type)
            {
                return false;
            }

            public InfType GetType(string name)
            {
                return InfType.OBJECT;
            }

            public bool IsTypeDefined(string name, out InfType type)
            {
                type = InfType.OBJECT;
                return false;
            }

            public bool IsMethodDefine(string name, InfType[] args, out InfType type)
            {
                type = InfType.OBJECT;
                return false;
            }

            public bool IsDefinedType(string name, out InfType type)
            {
                type = InfType.OBJECT;
                return false;
            }
        }

    }
}
