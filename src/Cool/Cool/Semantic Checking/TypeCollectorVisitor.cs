using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cool.CoolAST;
namespace Cool.Semantic_Checking
{
    public class TypeCollectorVisitor:IVisitor,ISemantic
    {
        ICollection<string> errors;
        IContext context;
        
        public NodeProgram SemanticChecker(NodeProgram node, IContext context, ICollection<string> errors)
        {
            this.context = context;
            this.errors = errors;
            node.Accept(this);
           
            return node;
            
        }
        public void Visit(NodeProgram node)
        {
            bool b = false;
            int m = -1;
            List<string>  types = new List<string> { "Bool", "Int", "String" };
            

            foreach (var item in node.programClasses)
            {
                context.AddType(item.CType.type, new InfType(item.CType.type, context.GetType(item.ParentType.type), item));
            }


            
            for (int i = 0; i < node.programClasses.Count; ++i)
                if (node.programClasses[i].CType.type == "Main")//Busca la clase Main
                    m = i;

            if (m == -1)
            {
                errors.Add($"The class 'Main' can't be found.");
                return;
            }

            
            foreach (var item in node.programClasses[m].classFeatures)
            {
                if (item is NodeMethod)
                {
                    var method = item as NodeMethod;
                    if (method.mNAme.Name == "main" && method.args.Count == 0)
                        b = true;
                }
            }

            if (b==false)
                
                errors.Add($"(Line: {  node.programClasses[m].Ln}, Column: {node.programClasses[m].Clmn})" + $" The class '{node.programClasses[m].CType.type}' doesn't have a  'main' method  without parameters.");


            foreach (var _class in node.programClasses)
            {
                if (!context.IsDefinedType(_class.ParentType.type, out InfType type))
                {
                    errors.Add($"(Line: {_class.ParentType.Ln}, Column: {_class.ParentType.Clmn})" + $" The following type '{_class.ParentType.type}' can't be found.")
                   ;//si la clase hereda de un tipo no declarado
                    return;
                }
                if (types.Contains(type.Name))
                {
                   
                    errors.Add($"(Line: {_class.Ln}, Column: {_class.Clmn})" + $" Can't inherit from '{type.Name}'");
                    //No se puede heredar de Int,ni bool,ni string
                    return;
                }
                _class.Accept(this);
            }

           



        }


        public void Visit(NodeClass node)
        {
            TypeCollectorVisitor collector = new TypeCollectorVisitor();
            collector.context = new Context(context.GetType(node.ParentType.type)._class.Context, context.GetType(node.CType.type));
            node.Context = collector.context;
            collector.errors = errors;
            

            foreach (var item in node.classFeatures)
            {
                item.Accept(collector);
            }
            
           
        }

        public void Visit(NodeAttr node)
        {
            if (!context.IsDefinedType(node.Field.Type.type, out InfType type))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" The type '{node.Field.Type.type}' can't be found.");


            if (context.IsTypeDefined(node.Field.Id.Name, out InfType t))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" The variable  with name :'{node.Field.Id.Name}' was already defined in this context.");


            context.Def(node.Field.Id.Name, type);//si el tipo ya esta definido y no hay vars con ese nombre y de ese tipo entonces guardo el nombre de la var
        }
        public void Visit(NodeMethod node)
        {
            if (!context.IsDefinedType(node.retType.type, out InfType returnType))
                errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" The type '{node.retType.type}' could not be found.");


            node.retType = new NodeType(node.retType.Ln, node.retType.Clmn, returnType.Name);

            InfType[] ArgsTypes = new InfType[node.args.Count];
            for (int i = 0; i < node.args.Count; ++i)
                if (!context.IsDefinedType(node.args[i].Type.type, out ArgsTypes[i]))
                    errors.Add($"(Line: {node.Ln}, Column: {node.Clmn})" + $" The type '{node.args[i].Type.type}' could not be found.");

            context.Def(node.mNAme.Name, ArgsTypes, returnType);
        }
        public void Visit(NodeAssign node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeAritmethicOp node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeBlock node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeBool node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeCase node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeFuncCallExplicit node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeFuncCall_Implicit node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeEqual node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeCompare node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeFields formalNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeIf node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeInt node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeIsVoid node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeLet node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeNegative node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeNew node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeNot node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeString node)
        {
            throw new NotImplementedException();
        }

        public void Visit(IdentNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeWhile node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeVoid node)
        {
            throw new NotImplementedException();
        }

        public void Visit(NodeSelf node)
        {
            throw new NotImplementedException();
        }


    }
}
