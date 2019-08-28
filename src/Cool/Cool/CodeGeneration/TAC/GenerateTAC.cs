using System;
using System.Collections.Generic;
using System.Text;
using Cool.CodeGeneration;
using Cool.CoolAST;
using Cool.Semantic_Checking;

namespace Cool.CodeGeneration.TAC
{
    public class GenerateTAC : IVisitor
    {
        ManagerVar vm;
        VT vt;
        IContext context;
        bool objectRet = false;
        List<Instructions> Instr;
        public static List<string> objectMethods = new List<string> { "abort", "type_name", "copy" };


        #region Methods
        public List<Instructions> GetIntermediateCode(NodeProgram n, IContext c)
        {
            context = c;

            n = (new Helper()).Opt(n, c);

            Instr = new List<Instructions>();
            vm = new ManagerVar();
            vt = new VT(c);
            vm.VCPush();
            Begin();
           vm.VCPop();
            n.Accept(this);

            vm.VCPush();
            Start();
            vm.VCPop();

            return Instr;
        }
        void Begin()
        {
            int self = vm.VCPeek();
            (string, string) l;
            List<string> obj = new List<string> { "abort", "type_name", "copy" };

            Instr.Add(new LabelCall(new MyLabel("start")));

            Instr.Add(new MyLabel("Object", "constructor"));
            Instr.Add(new Param(self));
            foreach (var f in objectMethods)
            {
                l = vt.Def("Object", f);
                Instr.Add(new Comments("Method: " + l.Item1 + "." + l.Item2));
                Instr.Add(new LabelToMemory(self, new MyLabel(l.Item1, l.Item2), vt.Offst("Object", f)));
            }

            Instr.Add(new Comments("Class Name is Object"));
            Instr.Add(new StringToMemory(0, "Object", 0));
            Instr.Add(new Comments("Class Large: " + vt.ClassLarge("Object") ));
            Instr.Add(new ConstantToMemory(0, vt.ClassLarge("Object"), 1));
            Instr.Add(new Return());

            Instr.Add(new MyLabel("IO", "constructor"));
            Instr.Add(new Param(self));
            Instr.Add(new InParam(self));
            Instr.Add(new LabelCall(new MyLabel("Object", "constructor")));
            Instr.Add(new OutParam(1));



            foreach (var f in VT.IO)
            {
                l = vt.Def("IO", f);
                Instr.Add(new Comments("Method: " + l.Item1 + "." + l.Item2));
                Instr.Add(new LabelToMemory(self, new MyLabel(l.Item1, l.Item2), vt.Offst("IO", f)));
            }

            Instr.Add(new Comments("Class name is Object"));
            Instr.Add(new StringToMemory(0, "IO", 0));
            Instr.Add(new Comments("Class large: " + vt.ClassLarge("IO") ));
            Instr.Add(new ConstantToMemory(0, vt.ClassLarge("IO"), 1));
            Instr.Add(new Comments("Class Label"));
            Instr.Add(new LabelToMemory(0, new MyLabel("_class", "IO"), 2));
            Instr.Add(new Return());

            Instr.Add(new Inherits("IO", "Object"));
            Instr.Add(new Inherits("Int", "Object"));
            Instr.Add(new Inherits("Bool", "Object"));
            Instr.Add(new Inherits("String", "Object"));

             Instr.Add(new MyLabel("_wrapper", "Int"));
            Instr.Add(new Param(self));
            Instr.Add(new Locate(self + 1, vt.ClassLarge("Int") + 1));
            Instr.Add(new InParam(self + 1));
            Instr.Add(new LabelCall(new MyLabel("Object", "constructor")));
            Instr.Add(new OutParam(1));
            Instr.Add(new StringToMemory(self + 1, "Int", 0));
            Instr.Add(new VarToMemory(self + 1, self, vt.ClassLarge("Int")));
            Instr.Add(new LabelToMemory(self + 1, new MyLabel("_class", "Int"), 2));
            Instr.Add(new Return(self + 1));

            
            Instr.Add(new MyLabel("_wrapper", "Bool"));
            Instr.Add(new Param(self));
            Instr.Add(new Locate(self + 1, vt.ClassLarge("Bool") + 1));
            Instr.Add(new InParam(self + 1));
            Instr.Add(new LabelCall(new MyLabel("Object", "constructor")));
            Instr.Add(new OutParam(1));
            Instr.Add(new StringToMemory(self + 1, "Bool", 0));
            Instr.Add(new VarToMemory(self + 1, self, vt.ClassLarge("Bool")));
            Instr.Add(new LabelToMemory(self + 1, new MyLabel("_class", "Bool"), 2));
            Instr.Add(new Return(self + 1));
            
            Instr.Add(new MyLabel("_wrapper", "String"));
            Instr.Add(new Param(self));
            Instr.Add(new Locate(self + 1, vt.ClassLarge("String") + 1));
            Instr.Add(new InParam(self + 1));
            Instr.Add(new LabelCall(new MyLabel("Object", "constructor")));
            Instr.Add(new OutParam(1));
            Instr.Add(new StringToMemory(self + 1, "String", 0));
            Instr.Add(new VarToMemory(self + 1, self, vt.ClassLarge("String")));
            Instr.Add(new LabelToMemory(self + 1, new MyLabel("_class", "String"), 2));
            Instr.Add(new Return(self + 1));


      
            Instr.Add(new MyLabel("Object", "abort"));
            Instr.Add(new Jump(new MyLabel("_abort")));

            Instr.Add(new MyLabel("Object", "type_name"));
            Instr.Add(new Param(0));
            Instr.Add(new MemoryToVar(0, 0, 0));
            Instr.Add(new Return(0));


            Instr.Add(new MyLabel("Object", "copy"));
            Instr.Add(new Param(0));
            Instr.Add(new MemoryToVar(1, 0, 1));
            Instr.Add(new ConstantToVar(2, 4));
            Instr.Add(new BinOp(1, 1, 2, "*"));
            Instr.Add(new InParam(0));
            Instr.Add(new InParam(1));
            Instr.Add(new LabelCall(new MyLabel("_copy"), 0));
            Instr.Add(new OutParam(2));
            Instr.Add(new Return(0));

            Instr.Add(new MyLabel("IO", "out_string"));
            Instr.Add(new Param(0));
            Instr.Add(new Param(1));
            Instr.Add(new InParam(1));
            Instr.Add(new LabelCall(new MyLabel("_out_string"), 0));
            Instr.Add(new OutParam(1));
            Instr.Add(new Return(0));

            Instr.Add(new MyLabel("IO", "out_int"));
            Instr.Add(new Param(0));
            Instr.Add(new Param(1));
            Instr.Add(new InParam(1));
            Instr.Add(new LabelCall(new MyLabel("_out_int"), 0));
            Instr.Add(new OutParam(1));
            Instr.Add(new Return(0));

            Instr.Add(new MyLabel("IO", "in_string"));
            Instr.Add(new Param(0));
            Instr.Add(new LabelCall(new MyLabel("_in_string"), 0));
            Instr.Add(new Return(0));


            Instr.Add(new MyLabel("IO", "in_int"));
            Instr.Add(new Param(0));
            Instr.Add(new LabelCall(new MyLabel("_in_int"), 0));
            Instr.Add(new Return(0));

           
            Instr.Add(new MyLabel("String", "length"));
            Instr.Add(new Param(0));
            Instr.Add(new InParam(0));
            Instr.Add(new LabelCall(new MyLabel("_stringlength"), 0));
            Instr.Add(new OutParam(1));
            Instr.Add(new Return(0));


            Instr.Add(new MyLabel("String", "concat"));
            Instr.Add(new Param(0));
            Instr.Add(new Param(1));
            Instr.Add(new InParam(0));
            Instr.Add(new InParam(1));
            Instr.Add(new LabelCall(new MyLabel("_stringconcat"), 0));
            Instr.Add(new OutParam(2));
            Instr.Add(new Return(0));


            Instr.Add(new MyLabel("String", "substr"));
            Instr.Add(new Param(0));
            Instr.Add(new Param(1));
            Instr.Add(new Param(2));
            Instr.Add(new InParam(0));
            Instr.Add(new InParam(1));
            Instr.Add(new InParam(2));
            Instr.Add(new LabelCall(new MyLabel("_stringsubstr"), 0));
            Instr.Add(new OutParam(3));
            Instr.Add(new Return(0));
        }

        void Start()
        {
            Instr.Add(new MyLabel("start"));
            New("Main");
            Instr.Add(new InParam(vm.VCPeek()));
            Instr.Add(new LabelCall(new MyLabel("Main", "main")));
            Instr.Add(new OutParam(1));
            }
        void ObjCover()
        {
            int t;
            int r = vm.VCPeek();
            string etiq = Instr.Count.ToString();

            vm.VCPush();
            vm.VCInc();
            t = vm.vc;
            Instr.Add(new StringToVar(t, "Int"));
            Instr.Add(new BinOp(t, 1, t, "="));
            Instr.Add(new CondJump(t, new MyLabel("_attempt_bool", etiq)));
            Instr.Add(new InParam(r));
            Instr.Add(new LabelCall(new MyLabel("_wrapper", "Int"), r));
            Instr.Add(new OutParam(1));
            Instr.Add(new Jump(new MyLabel("_not_more_attempt", etiq)));
            vm.VCPop();

            Instr.Add(new MyLabel("_attempt_bool", etiq));
            vm.VCPush();
            vm.VCInc();
            t = vm.vc;
            Instr.Add(new StringToVar(t, "Bool"));
            Instr.Add(new BinOp(t, 1, t, "="));
            Instr.Add(new CondJump(t, new MyLabel("_attempt_string", etiq)));
            Instr.Add(new InParam(r));
            Instr.Add(new LabelCall(new MyLabel("_wrapper", "Bool"), r));
            Instr.Add(new OutParam(1));
            Instr.Add(new Jump(new MyLabel("_not_more_attempt", etiq)));
            vm.VCPop();

            Instr.Add(new MyLabel("_attempt_string", etiq));
            vm.VCPush();
            vm.VCInc();
            t = vm.vc;
            Instr.Add(new StringToVar(t, "String"));
            Instr.Add(new BinOp(t, 1, t, "="));
            Instr.Add(new CondJump(t, new MyLabel("_not_more_attempt", etiq)));
            Instr.Add(new InParam(r));
            Instr.Add(new LabelCall(new MyLabel("_wrapper", "String"), r));
            Instr.Add(new OutParam(1));
            vm.VCPop();

            Instr.Add(new MyLabel("_not_more_attempt", etiq));
        }

        void RetType(string rettype)
        {
            if (rettype == "Int" || rettype == "Bool" || rettype == "String")
            {
                Instr.Add(new Comments($" {rettype} is the return type"));
                Instr.Add(new StringToVar(1, rettype));
            }
            else
            {
                Instr.Add(new Comments($"Object is the return type"));
                Instr.Add(new StringToVar(1, "Object"));
            }
        }

        #endregion
        #region NodeVisitor
        public void Visit(NodeProgram n)
        {
            List<NodeClass> s = new List<NodeClass>();
            s.AddRange(n.programClasses);
            s.Sort((x, y) => (context.GetType(x.CType.type) .Inherit( context.GetType(y.CType.type) )? 1 : -1));

            foreach (var c in s)
            {
                vt.NewClass(c.CType.type);

                List<NodeAttr> attributes = new List<NodeAttr>();
                List<NodeMethod> methods = new List<NodeMethod>();

                foreach (var f in c.classFeatures)
                    if (f is NodeAttr)
                    {
                        attributes.Add((NodeAttr)f);
                    }
                    else
                    {
                        methods.Add((NodeMethod)f);
                    }

                foreach (var m in methods)
                {
                    List<string> tofP = new List<string>();
                    foreach (var x in m.args)
                        tofP.Add(x.Type.type);

                    vt.NewMethod(c.CType.type, m.mNAme.Name, tofP);
                }

                foreach (var attr in attributes)
                    vt.NewAttr(c.CType.type, attr.Field.Id.Name, attr.Field.Type.type);
            }

            foreach (var c in s)
                c.Accept(this);
        }


      

        public void Visit(NodeClass n)
        {
            string cclass;
            cclass = vm.c = n.CType.type;
            Instr.Add(new Inherits(n.CType.type, context.GetType(n.CType.type).Parent.Name));
            int self = vm.vc = 0;
            vm.VCInc();
            vm.VCPush();

            List<NodeAttr> attributes = new List<NodeAttr>();
            List<NodeMethod> methods = new List<NodeMethod>();

            foreach (var f in n.classFeatures)
                if (f is NodeAttr)
                    attributes.Add((NodeAttr)f);
                else
                    methods.Add((NodeMethod)f);


            foreach (var method in methods)
            {
                method.Accept(this);
            }
            Instr.Add(new MyLabel(vm.c, "constructor"));
            Instr.Add(new Param(self));

            if (vm.c != "Object")
            {
                Instr.Add(new InParam(self));
                MyLabel label = new MyLabel(n.ParentType.type, "constructor");
                Instr.Add(new LabelCall(label));
                Instr.Add(new OutParam(1));
            }


         foreach (var method in methods)
            {
                (string, string) label = vt.Def(n.CType.type, method.mNAme.Name);
                Instr.Add(new Comments("Method: " + label.Item1 + "." + label.Item2));
                Instr.Add(new LabelToMemory(self, new MyLabel(label.Item1, label.Item2), vt.Offst(n.CType.type, method.mNAme.Name)));
                
            }


            foreach (var attr in attributes)
            {
                vm.VCPush();
                attr.Accept(this);
                vm.VCPop();
                Instr.Add(new Comments("Attr: " + attr.Field.Id.Name));
                Instr.Add(new VarToMemory(self, vm.VCPeek(), vt.Offst(n.CType.type, attr.Field.Id.Name)));
            }


            Instr.Add(new Comments("Class Name: " + n.CType.type));
            Instr.Add(new StringToMemory(0, n.CType.type, 0));
            Instr.Add(new Comments("Class Large: " + vt.ClassLarge(n.CType.type) ));
            Instr.Add(new ConstantToMemory(0, vt.ClassLarge(n.CType.type), 1));
            Instr.Add(new Comments("Class Label"));
            Instr.Add(new LabelToMemory(0, new MyLabel("_class", n.CType.type), 2));
            Instr.Add(new Return(-1));
            vm.VCPop();



        }

        public void Visit(NodeAttr node)
        {
            node.Exp.Accept(this);

            if ((node.Exp.typeStat.Name == "Int" ||node.Exp.typeStat.Name == "Bool" ||node.Exp.typeStat.Name == "String") &&node.Exp.typeStat.Name == "Object")
            {
                Instr.Add(new InParam(vm.VCPeek()));
                Instr.Add(new LabelCall(new MyLabel("_wrapper", node.Exp.typeStat.Name), vm.VCPeek()));
                Instr.Add(new OutParam(1));
            }
        }

        public void Visit(NodeMethod node)
        {
            Instr.Add(new MyLabel(vm.c, node.mNAme.Name));

            objectRet = node.retType.type == "Object";

            int self = vm.vc = 0;
            Instr.Add(new Param(self));

            if (objectRet)
                vm.VCInc();

            vm.VCInc();

            foreach (var formal in node.args)
            {
                Instr.Add(new Param(vm.vc));
                vm.VarPush(formal.Id.Name, formal.Type.type);
                vm.VCInc();
            }

            vm.VCPush();
            node.body.Accept(this);

            if (objectRet)
                ObjCover();

            Instr.Add(new Return(vm.VCPeek()));


            vm.VCPop();

            foreach (var formal in node.args)
            {
                vm.VarPop(formal.Id.Name);
            }

            objectRet = false;
        }

        


        public void Visit(NodeCase n)
        {
            string st = n.ExpressionCase.typeStat.Name;

            int r = vm.VCPeek();
            int e = vm.VCInc();

            vm.VCPush();
            n.ExpressionCase.Accept(this);
            vm.VCPop();

           

            if (st == "String" || st == "Int" ||st == "Bool")
            {
                int i = n.Branches.FindIndex((x) => x.Item1.Type.type == st);
                string v = n.Branches[i].Item1.Id.Name;

                vm.VarPush(v, n.Branches[i].Item1.Type.type);

                int t = vm.VCInc();
                vm.VCPush();

                n.Branches[i].Item2.Accept(this);

                vm.VCPop();

                vm.VarPop(v);

                Instr.Add(new VarToVar(vm.VCPeek(), t));
            }
            else
            {
                string etiq = Instr.Count.ToString();

                List<Tuple<NodeFields, NodeExpr>> s = new List<Tuple<NodeFields, NodeExpr>>();
                s.AddRange(n.Branches);
                s.Sort((x, y) => (context.GetType(x.Item1.Type.type) .Inherit( context.GetType(y.Item2.typeStat.Name)) ? -1 : 1));

                for (int i = 0; i < s.Count; ++i)
                {
                 
                    vm.VarPush(s[i].Item1.Id.Name, s[i].Item1.Type.type);

                    string bT = s[i].Item1.Type.type;
                    vm.VCPush();
                    vm.VCInc();

                    Instr.Add(new MyLabel("_case", etiq + "." + i));
                    Instr.Add(new StringToVar(vm.vc, bT));
                    Instr.Add(new BinOp(vm.vc, e, vm.vc, "inherit"));
                    Instr.Add(new CondJump(vm.vc, new MyLabel("_case", etiq + "." + (i + 1))));


                    if ((bT == "Int" ||  bT == "Bool" || bT == "String"))
                    {
                        if (st == "Object")
                        {

                            Instr.Add(new MemoryToVar(e, e, vt.ClassLarge(bT)));

                            vm.VCPush();
                            s[i].Item2.Accept(this);
                            vm.VCPop();

                            Instr.Add(new VarToVar(r, vm.VCPeek()));
                            Instr.Add(new Jump(new MyLabel("_endcase", etiq)));
                        }
                    }
                    else
                    {
                        vm.VCPush();
                        s[i].Item2.Accept(this);
                        vm.VCPop();

                        Instr.Add(new VarToVar(r, vm.VCPeek()));
                        Instr.Add(new Jump(new MyLabel("_endcase", etiq)));
                    }
                     vm.VCPop();

                    vm.VarPop(s[i].Item1.Id.Name);
                }

                Instr.Add(new MyLabel("_case", etiq + "." + s.Count));
                Instr.Add(new Jump(new MyLabel("_caseselectionexception")));
                Instr.Add(new MyLabel("_endcase", etiq));


            }
        }

        public void Visit(NodeInt node)
        {
            Instr.Add(new ConstantToVar(vm.VCPeek(), node.Number));
            if (objectRet)
                RetType("Int");
        }

        public void Visit(NodeBool n)
        {
            Instr.Add(new ConstantToVar(vm.VCPeek(), n.BoolValue ? 1 : 0));
            if (objectRet)
                RetType("Bool");
        }

        public void Visit(NodeAritmethicOp n)
        {
            if (n.Attrs.ContainsKey("integer_constant_value"))
                Instr.Add(new ConstantToVar(vm.VCPeek(), n.Attrs["integer_constant_value"]));
            else
                OptBinOp(n);
            if (objectRet)
                RetType("Int");
        }

        public void Visit(NodeAssign n)
        {

            n.ExpressionRight.Accept(this);
            var (t, type) = vm.GetVariable(n.ID.Ident);

            if (type == "")
                type = vt.AttrType(vm.c, n.ID.Ident);


            if ((n.ExpressionRight.typeStat.Name == "Int" ||  n.ExpressionRight.typeStat.Name == "Bool" | n.ExpressionRight.typeStat.Name == "String") &&
                type == "Object")
            
            {
                Instr.Add(new InParam(vm.VCPeek()));
                Instr.Add(new LabelCall(new MyLabel("_wrapper", n.ExpressionRight.typeStat.Name), vm.VCPeek()));
                Instr.Add(new OutParam(1));
            }

            if (t != -1)
            {
                
                Instr.Add(new VarToVar(t, vm.VCPeek()));
            }
            else
            {
                int offset = vt.Offst(vm.c, n.ID.Ident);
                Instr.Add(new VarToMemory(0, vm.VCPeek(), offset));
            }


        }

        public void Visit(NodeBlock n)
        {
            foreach (var s in n.Block)
            {
                s.Accept(this);
            }
        }

        public void Visit(IdentNode n)
        {
            var (t, _t) = vm.GetVariable(n.Ident);
            if (t != -1)
            {
                Instr.Add(new Comments("Var: " + n.Ident));
                Instr.Add(new VarToVar(vm.VCPeek(), t));
            }
            else
            {
                Instr.Add(new Comments("Attr: " + vm.c + "." + n.Ident));
                Instr.Add(new MemoryToVar(vm.VCPeek(), 0, vt.Offst(vm.c, n.Ident)));
            }

            if (objectRet)
                RetType(_t);
        }


        public void Visit(NodeCompare n)
        {
            OptBinOp(n);
            if (objectRet)
            {
                Instr.Add(new Comments($"Return type is Bool"));
                Instr.Add(new StringToVar(1, "Bool"));
            }
        }

        public void Visit(NodeFuncCallExplicit n)
        {
            string c = n.IdType.type;
            n.Expression.Accept(this);
            DispatchVisit(n, c);
        }

        public void Visit(NodeFuncCall_Implicit n)
        {
            string c = vm.c;
            Instr.Add(new VarToVar(vm.VCPeek(), 0));
            DispatchVisit(n, c);
        }

        void DispatchVisit(NodeFuncCall n, string c)
        {
            string m = n.FuncName.Name;

            if (m == "abort" && (c == "Int" || c == "String" || c == "Bool"))
            {
                Instr.Add(new LabelCall(new MyLabel("Object", "abort")));
                return;
            }

            if (m == "type_name")
            {
                if (c == "Int" || c == "Bool" || c == "String")
                {
                    Instr.Add(new StringToVar(vm.VCPeek(), c));
                    return;
                }
            }

            
            if (m == "copy")
            {
                if (c == "Int" || c == "Bool" || c == "String")
                {
                    Instr.Add(new InParam(vm.VCPeek()));
                    Instr.Add(new LabelCall(new MyLabel("_wrapper", c), vm.VCPeek()));
                    Instr.Add(new OutParam(1));
                    return;
                }
            }

            vm.VCPush();
            int function_address = vm.VCInc();
            int offset = vt.Offst(c, m);

            List<int> par = new List<int>();
            List<string> pt = vt.ParamsTypes(c, m);
            for (int i = 0; i < n.Arguments.Count; ++i)
            {
                vm.VCInc();
                vm.VCPush();
                par.Add(vm.vc);
                n.Arguments[i].Accept(this);

                if (pt[i] == "Object" && ( n.Arguments[i].typeStat.Name == "Int" || n.Arguments[i].typeStat.Name == "Bool" ||
                    n.Arguments[i].typeStat.Name == "String"))
                {
                    Instr.Add(new InParam(vm.VCPeek()));
                    Instr.Add(new LabelCall(new MyLabel("_wrapper", n.Arguments[i].typeStat.Name), vm.VCPeek()));
                    Instr.Add(new OutParam(1));
                }

                vm.VCPop();
            }

            vm.VCPop();

            if (c != "String")
            {
                Instr.Add(new Comments("Method: " + c + "." + m));
                Instr.Add(new MemoryToVar(function_address, vm.VCPeek(), offset));
            }

            Instr.Add(new InParam(vm.VCPeek()));

            foreach (var p in par)
            {
                Instr.Add(new InParam(p));
            }

            if (c != "String")
            {
                Instr.Add(new CallAddr(function_address, vm.VCPeek()));
            }
            else
            {
                Instr.Add(new LabelCall(new MyLabel(c, m), vm.VCPeek()));
            }

            if (objectRet)
                RetType(n.typeStat.Name);

            Instr.Add(new OutParam(par.Count + 1));
        }

       


        public void Visit(NodeEqual n)
        {
            OptBinOp(n);
            if (objectRet)
                RetType("Bool");
        }

        void OptBinOp(NodeBinaryOp n)
        {
            vm.VCPush();

            int v1 = vm.VCInc();
            vm.VCPush();
            n.LeftOperand.Accept(this);
            vm.VCPop();

            int v = vm.VCInc();
            vm.VCPush();
            n.RightOperand.Accept(this);
            vm.VCPop();

            vm.VCPop();

            if (n.LeftOperand.typeStat.Name == "String" && n.Symbol == "=")
            {
                Instr.Add(new BinOp(vm.VCPeek(), v1, v, "=:="));
                return;
            }

            Instr.Add(new BinOp(vm.VCPeek(), v1, v, n.Symbol));
        }

        public void Visit(NodeString n)
        {
            Instr.Add(new StringToVar(vm.VCPeek(), n.Characters));
            if (objectRet)
                RetType("String");
        }

        public void Visit(NodeLet node)
        {
            vm.VCPush();

            foreach (var attr in node.Initialization)
            {
                vm.VCInc();
                vm.VarPush(attr.Field.Id.Name, attr.Field.Type.type);
                vm.VCPush();
                attr.Accept(this);
                
                vm.VCPop();
            }
            vm.VCInc();

            node.ExpressionBody.Accept(this);

            foreach (var attr in node.Initialization)
            {
                vm.VarPop(attr.Field.Id.Name);
            }
            vm.VCPop();

            if (objectRet)
                RetType(node.typeStat.Name);
        }

        public void Visit(NodeNew node)
        {
            if (node.TypeId.type == "Int" || node.TypeId.type == "Bool" || node.TypeId.type == "String")
            {
                if (node.TypeId.type == "Int" || node.TypeId.type == "Bool")
                    Instr.Add(new ConstantToVar(vm.VCPeek(), 0));
                else
                    Instr.Add(new StringToVar(vm.VCPeek(), ""));
            }
            else
            {
                New(node.TypeId.type);
            }

            if (objectRet)
                RetType(node.TypeId.type);
        }

        public void New(string c)
        {
            int size = vt.ClassLarge(c);
            Instr.Add(new Locate(vm.VCPeek(), size));
            Instr.Add(new InParam(vm.VCPeek()));
            Instr.Add(new LabelCall(new MyLabel(c, "constructor")));
            Instr.Add(new OutParam(1));
        }

        public void Visit(NodeIsVoid n)
        {
            
            if (n.Operand.typeStat.Name == "Int" ||n.Operand.typeStat.Name == "String" || n.Operand.typeStat.Name == "Bool")
                Instr.Add(new ConstantToVar(vm.VCPeek(), 0));
            else
                UnaryOperationVisit(n);

            if (objectRet)
                RetType("Bool");
        }

        public void Visit(NodeNegative n)
        {
            UnaryOperationVisit(n);
            if (objectRet)
                RetType("Int");
        }


        public void Visit(NodeNot n)
        {
            UnaryOperationVisit(n);
            if (objectRet)
                RetType("Bool");
        }

        void UnaryOperationVisit(NodeUnaryOperation n)
        {
            vm.VCPush();
            vm.VCInc();
            int v = vm.vc;
            vm.VCPush();
            n.Operand.Accept(this);
            vm.VCPop();
            Instr.Add(new UnaryOp(vm.VCPeek(), v, n.Symbol));
        }

        public void Visit(NodeIf n)
        {
            string etiq = Instr.Count.ToString();

            n.Condition.Accept(this);

            Instr.Add(new CondJump(vm.VCPeek(), new MyLabel("_else", etiq)));

            n.Body.Accept(this);
            Instr.Add(new Jump(new MyLabel("_endif", etiq)));

            Instr.Add(new MyLabel("_else", etiq));
            n.ElseBody.Accept(this);

            Instr.Add(new MyLabel("_endif", etiq));

        }


        public void Visit(NodeWhile n)
        {
            string etiq = Instr.Count.ToString();

            Instr.Add(new MyLabel("_whilecondition", etiq));

            n.Condition.Accept(this);

            Instr.Add(new CondJump(vm.VCPeek(), new MyLabel("_endwhile", etiq)));

            n.Body.Accept(this);

            Instr.Add(new Jump(new MyLabel("_whilecondition", etiq)));

            Instr.Add(new MyLabel("_endwhile", etiq));
        }


        public void Visit(NodeVoid node)
        {
            Instr.Add(new NullToVar(vm.VCPeek()));
        }

        public void Visit(NodeSelf node)
        {
            Instr.Add(new VarToVar(vm.VCPeek(), 0));
        }
        public void Visit(NodeFields node)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    class Helper : IVisitor
    {
        IContext cont;

        public NodeProgram Opt(NodeProgram node, IContext c)
        {
            cont = c;
            node.Accept(this);
            return node;
        }

        public void Visit(NodeAritmethicOp n)
        {
            n.LeftOperand.Accept(this);
            n.RightOperand.Accept(this);

            if (n.LeftOperand.Attrs.ContainsKey("integer_constant_value") &&n.RightOperand.Attrs.ContainsKey("integer_constant_value"))
            {
                int t1 = n.LeftOperand.Attrs["integer_constant_value"];
                int t2 = n.RightOperand.Attrs["integer_constant_value"];
                switch (n.Symbol)
                {
                    case "+":
                        n.Attrs["integer_constant_value"] = t1 + t2;
                        break;
                    case "-":
                        n.Attrs["integer_constant_value"] = t1 - t2;
                        break;
                    case "*":
                        n.Attrs["integer_constant_value"] = t1 * t2;
                        break;
                    case "/":
                        if (t2 != 0)
                            n.Attrs["integer_constant_value"] = t1 / t2;
                        break;
                    default:
                        break;
                }
            }

        }

        public void Visit(NodeAssign node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeAttr node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeBool node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeCase node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeClass node)
        {
            foreach (var f in node.classFeatures)
            {
                f.Accept(this);
            }


        }

        public void Visit(NodeCompare node)
        {
            // throw new NotImplementedException();
        }

        public void Visit(NodeFuncCallExplicit node)
        {
            // throw new NotImplementedException();
        }

        public void Visit(NodeFuncCall_Implicit node)
        {
            foreach (var a in node.Arguments)
                a.Accept(this);
        }

        public void Visit(NodeEqual node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeFields node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(IdentNode node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeIf node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeInt node)
        {
            node.Attrs["integer_constant_value"] = node.Number;
        }

        public void Visit(NodeIsVoid node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeLet node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeMethod node)
        {
            node.body.Accept(this);
        }

        public void Visit(NodeNegative node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeNew node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeNot node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeProgram node)
        {
            foreach (var c in node.programClasses)
                c.Accept(this);
        }

        public void Visit(NodeSelf node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeBlock node)
        {
            foreach (var s in node.Block)
                s.Accept(this);
        }

        public void Visit(NodeString node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeVoid node)
        {
            //throw new NotImplementedException();
        }

        public void Visit(NodeWhile node)
        {
            //throw new NotImplementedException();
        }
    }
}


