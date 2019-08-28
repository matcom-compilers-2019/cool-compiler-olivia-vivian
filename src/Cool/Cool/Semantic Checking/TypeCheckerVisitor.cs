using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cool.CoolAST;
namespace Cool.Semantic_Checking
{
    public class TypeCheckerVisitor : IVisitor, ISemantic
    {

        public IContext context;
        ICollection<string> errors;
        public TypeCheckerVisitor()
        {

        }
        public TypeCheckerVisitor(IContext context, ICollection<string> errors)
        {
            this.context = context;
            this.errors = errors;
        }
        public NodeProgram SemanticChecker(NodeProgram node, IContext context, ICollection<string> errors)
        {
            this.context = context;
            this.errors = errors;
            node.Accept(this);
            return node;
        }

        public void Visit(NodeProgram node)
        {
            foreach (var item in node.programClasses)
            {
                item.Accept(new TypeCheckerVisitor(item.Context, errors));
            }

       }


        public void Visit(NodeClass node)
        {
            foreach (var item in node.classFeatures)
            {
                item.Accept(this);
            }
            
        }
        public void Visit(NodeAttr node)
        {
            node.Exp.Accept(this);
            var aType = node.Exp.typeStat;

            if (!context.IsDefinedType(node.Field.Type.type, out InfType typeDeclared))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It´s not possible to find the type :'{node.Field.Type.type}' ");

            if (!(aType.Inherit(typeDeclared)))//si el tipo statico  no hereeda del que se declaro entonces esta mal
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It´s not possible to convert from '{aType.Name}' to '{typeDeclared.Name}'.");


            context.Def(node.Field.Id.Name, typeDeclared);
        }


        public void Visit(NodeMethod node)
        {
            var contextM = context.CreateChild();
            foreach (var arg in node.args)
            {
                if (!context.IsDefinedType(arg.Type.type, out InfType typeArg))
                    errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It´s not possible to find the type :{arg.Type.type}' ");
                contextM.Def(arg.Id.Name, typeArg);
            }

            if (!context.IsDefinedType(node.retType.type, out InfType typeReturn))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It's not possible to find the type '{node.retType.type}'");

            context.Def(node.mNAme.Name, node.args.Select(x => context.GetType(x.Type.type)).ToArray(), typeReturn);

            node.body.Accept(new TypeCheckerVisitor(contextM, errors));

            if (!(node.body.typeStat.Inherit(typeReturn)))
                errors.Add($"(Line: {node.body.Ln}, Column: {node.body.Clmn})" + $" It's not possible to  convert from '{node.body.typeStat.Name}' to '{typeReturn.Name}'.");

            node.retType = new NodeType(node.body.Ln, node.body.Clmn, typeReturn.Name);
        }

        public void Visit(NodeNot node)
        {
            node.Operand.Accept(this);

            if (node.Operand.typeStat.Name != "Bool")
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" The operator '{node.Symbol}' cannot be applied to operands of type '{node.Operand.typeStat.Name}'.");

            if (!context.IsDefinedType("Bool", out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It's not possible to find the type Bool");
        }

        public void Visit(NodeNegative node)
        {
            node.Operand.Accept(this);

            if (node.Operand.typeStat.Name != "Int")
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" +$" Operator '{node.Symbol}' cannot be applied to operands of type '{node.Operand.typeStat.Name}'.");

            if (!context.IsDefinedType("Int", out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It's not possible to find the type Int");
        }

        public void Visit(NodeAritmethicOp node)
        {
            node.LeftOperand.Accept(this);
            node.RightOperand.Accept(this);

            if (node.LeftOperand.typeStat.Name != node.RightOperand.typeStat.Name)
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" Operator '{node.Symbol}' cannot be applied to operands of type '{node.LeftOperand.typeStat.Name}' and '{node.RightOperand.typeStat.Name}'.");

            else if (node.LeftOperand.typeStat.Name != "Int" || node.RightOperand.typeStat.Name != "Int")
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" The operator '{node.Symbol}' must be applied to types 'Int'.");


            else if (!context.IsDefinedType("Int", out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" The type Int could not be found.");
        }

        public void Visit(NodeCompare node)
        {
            node.LeftOperand.Accept(this);
            node.RightOperand.Accept(this);

            if (node.LeftOperand.typeStat.Name != "Int" || node.RightOperand.typeStat.Name != "Int")
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" +$" Operator '{node.Symbol}' cannot be applied to operands of type '{node.LeftOperand.typeStat.Name}' and '{node.RightOperand.typeStat.Name}'."); ;

            if (!context.IsDefinedType("Bool", out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It's not possible to find the type Bool");
        }


        public void Visit(NodeEqual node)
        {
            node.LeftOperand.Accept(this);
            node.RightOperand.Accept(this);

            if (node.LeftOperand.typeStat.Name != node.RightOperand.typeStat.Name || !(new string[3] { "Bool", "Int", "String" }.Contains(node.LeftOperand.typeStat.Name)))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" Operator '{node.Symbol}' cannot be applied to operands of type '{node.LeftOperand.typeStat.Name}' and '{node.RightOperand.typeStat.Name}'.");

            if (!context.IsDefinedType("Bool", out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It's not possible to find the type Bool");
        }

        public void Visit(NodeBlock node)
        {
              foreach (var item in node.Block)
            {
                item.Accept(this);
            }

            var last = node.Block[node.Block.Count - 1];

            if (!context.IsDefinedType(last.typeStat.Name, out node.typeStat))
                errors.Add($"(Line: {last.Ln}, Column: {last.Clmn})" +$" It's not possible to find the type :'{last.typeStat.Name}'");
        }

        public void Visit(NodeAssign node)
        {
            node.ExpressionRight.Accept(this);

            if (!context.IsTypeDefined(node.ID.Ident, out InfType type))
                errors.Add($"(Line: {node.ID.Ln}, Column: {node.ID.Clmn}) The name " + $"'{node.ID.Ident}' it's not available in the current context.");

            if (!(node.ExpressionRight.typeStat.Inherit(type)))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" Cannot convert from '{node.ExpressionRight.typeStat.Name}' to '{type.Name}'.");

            node.typeStat = node.ExpressionRight.typeStat;
        }

        public void Visit(NodeVoid node)
        {
            node.typeStat = context.GetType(node.StatType);
        }


        public void Visit(NodeFuncCallExplicit node)
        {
            node.Expression.Accept(this);
            if (node.IdType.type == "Object")
                node.IdType = new NodeType(node.Expression.Ln, node.Expression.Clmn, node.Expression.typeStat.Name);

            if (!context.IsDefinedType(node.IdType.type, out InfType Tanc))
                errors.Add($"(Line: {node.IdType.Ln}, Column: {node.IdType.Clmn})" + $" It's not possible to find the type : '{node.IdType.type}' ");

            if (!(node.Expression.typeStat.Inherit(Tanc)))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" Cannot convert from '{node.Expression.typeStat.Name}' to '{Tanc.Name}'.");

            foreach (var item in node.Arguments)
            {
                item.Accept(this);
            }
          

            var cAncester = Tanc._class.Context;
            if (!(cAncester.IsMethodDefine(node.FuncName.Name, node.Arguments.Select(x => x.typeStat).ToArray(), out node.typeStat)))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" +  $" The name '{node.FuncName.Name}' it's not available in the current context.");
        }

        public void Visit(NodeFuncCall_Implicit node)
        {
            node.Arguments.ForEach(expArg => expArg.Accept(this));

            if (!context.IsMethodDefine(node.FuncName.Name, node.Arguments.Select(x => x.typeStat).ToArray(), out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" The name '{node.FuncName.Name}'it's not available in the current context.");
        }

        public void Visit(NodeInt node)
        {
            if (!context.IsDefinedType("Int", out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It's not possible to find the type Int");
        }

        public void Visit(NodeBool node)
        {
            if (!context.IsDefinedType("Bool", out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It's not possible to find the type Bool");
        }
        public void Visit(NodeString node)
        {
            if (!context.IsDefinedType("String", out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It's not possible to find the type String");
        }
        public void Visit(IdentNode node)
        {
            if (!context.IsTypeDefined(node.Ident, out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn}) The name " + $"'{node.Ident}' it's not available in the current context.");
        }

        public void Visit(NodeFields node)
        {
            if (!context.IsDefinedType(node.Type.type, out node.typeStat))
                errors.Add($"(Line: {node.Type.Ln}, Column: {node.Type.Clmn})" +$"It's not possible to find the type: '{node.Type.type}' ");
        }

        public void Visit(NodeSelf node)
        {
            node.typeStat = context.typeOfContext;
        }
        public void Visit(NodeCase node)
        {
            node.ExpressionCase.Accept(this);

            int selectB = -1;
            var exp0 = node.ExpressionCase.typeStat;
            var expk = context.GetType(node.Branches[0].Item1.Type.type);

            for (int i = 0; i < node.Branches.Count; ++i)
            {
                if (!context.IsDefinedType(node.Branches[i].Item1.Type.type, out InfType type))
                    errors.Add($"(Line: {node.Branches[i].Item1.Type.Ln}, Column: {node.Branches[i].Item1.Type.Clmn})" + $" It's not possible to find the type :'{node.Branches[i].Item1.Type.type}'");

                var typeK = context.GetType(node.Branches[i].Item1.Type.type);

                var CBranch = context.CreateChild();
                CBranch.Def(node.Branches[i].Item1.Id.Name, typeK);

                node.Branches[i].Item2.Accept(new TypeCheckerVisitor(CBranch, errors));

                expk = node.Branches[i].Item2.typeStat;

                if (selectB == -1 && exp0.Inherit(typeK))
                    selectB = i;

                if (i == 0)
                    node.typeStat = node.Branches[0].Item2.typeStat;
                node.typeStat = LCA(node.typeStat, expk);
            }
            node.BranchSelected = selectB;

            if (node.BranchSelected == -1)
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" +$" It's necessary that at least one branch is matched");
        }
        public void Visit(NodeIf node)
        {
            node.Condition.Accept(this);
            node.Body.Accept(this);
            node.ElseBody.Accept(this);

            if (node.Condition.typeStat.Name != "Bool")
                errors.Add($"(Line: {node.Condition.Ln}, Column: {node.Condition.Clmn})" +$" Cannot convert from '{node.Condition.typeStat.Name}' to '{context.GetType("Bool").Name}'." );

            node.typeStat = LCA(node.Body.typeStat, node.ElseBody.typeStat);
        }

        public void Visit(NodeLet node)
        {
            var contextLet = context.CreateChild();

            foreach (var expInit in node.Initialization)
            {
                expInit.Exp.Accept(new TypeCheckerVisitor(contextLet, errors));
                var typeAssignExp = expInit.Exp.typeStat;

                if (!contextLet.IsDefinedType(expInit.Field.Type.type, out InfType typeDeclared))
                    errors.Add($"(Line: {expInit.Field.Type.Ln}, Column: {expInit.Field.Type.Clmn})" +$" It's not possible to find the type '{expInit.Field.Type.type}' ");

                if (!(typeAssignExp.Inherit(typeDeclared)))
                    errors.Add($"(Line: {expInit.Field.Type.Ln}, Column: {expInit.Field.Type.Clmn})" + $" Cannot convert from '{typeAssignExp.Name}' to '{typeDeclared.Name}'.");

                if (contextLet.IsTypeDefined(expInit.Field.Id.Name, out InfType typeOld))
                    contextLet.ChangeType(expInit.Field.Id.Name, typeDeclared);
                else
                    contextLet.Def(expInit.Field.Id.Name, typeDeclared);
            }

            node.ExpressionBody.Accept(new TypeCheckerVisitor(contextLet, errors));
            node.typeStat = node.ExpressionBody.typeStat;
        }

        public void Visit(NodeNew node)
        {
            if (!context.IsDefinedType(node.TypeId.type, out node.typeStat))
                errors.Add($"(Line: {node.TypeId.Ln}, Column: {node.TypeId.Clmn})" + $" The type '{node.TypeId.type}' could not be found.");
        }

        public void Visit(NodeWhile node)
        {
            node.Condition.Accept(this);
            node.Body.Accept(this);

            if (node.Condition.typeStat.Name != "Bool")
                errors.Add($"(Line: {node.Condition.Ln}, Column: {node.Condition.Clmn})" + $" Cannot convert from '{node.Condition.typeStat.Name}' to '{context.GetType("Bool")}'.");

            if (!context.IsDefinedType("Object", out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" +$" It's not possible to find the type Object");
        }


        public void Visit(NodeIsVoid node)
        {
            node.Operand.Accept(this);

            if (!context.IsDefinedType("Bool", out node.typeStat))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" It's not possible to find the type Object");
        }

        public static InfType LCA(InfType a, InfType b)
        {
            while (a.Level < b.Level) b = b.Parent;
            while (b.Level < a.Level) a = a.Parent;
            while (a != b) { a = a.Parent; b = b.Parent; }
            return a;
        }

    }
}