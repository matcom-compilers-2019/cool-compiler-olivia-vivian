using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Cool.CoolAST;   
namespace Cool.CoolParsing
{
    class CoolTreeAdapter: CoolBaseVisitor<Node>
    {
        public override Node VisitProgram([NotNull] CoolParser.ProgramContext c)
        {
            return new NodeProgram(c)
            {
                programClasses = c.defClass().Select(x => Visit(x) as NodeClass).ToList()
            };
        }
             public override Node VisitDefClass([NotNull] CoolParser.DefClassContext c)
        {
            var n = new NodeClass(c);
            var CType = new NodeType(c.TYPE(0).Symbol.Line, c.TYPE(0).Symbol.Column, c.TYPE(0).GetText());
            var FatherType = c.TYPE(1) == null ? NodeType.OBJECT : new NodeType(c.TYPE(1).Symbol.Line,
                                                        c.TYPE(1).Symbol.Column, c.TYPE(1).GetText());

            n.CType = CType;
            n.ParentType = FatherType;
            n.classFeatures = (from x in c.feature() select Visit(x) as NodeFeatures).ToList();

            return n;
        }
        public override Node VisitMethod([NotNull] CoolParser.MethodContext c)
        {
            var _n = new NodeID(c, c.ID().GetText());
            var n = new NodeMethod(c);
            n.mNAme = _n;
            n.args = (from x in c.field() select Visit(x) as NodeFields).ToList();
            var ret = new NodeType(c.TYPE().Symbol.Line, c.TYPE().Symbol.Column, c.TYPE().GetText());
            n.retType = ret;
            n.body = Visit(c.expr()) as NodeExpr;
            return n;
        }

        public override Node VisitAttr([NotNull] CoolParser.AttrContext c)
        {
            var n = new NodeAttr(c)
            {
                Field = Visit(c.field()) as NodeFields
            };

            if (c.expr() != null)
                n.Exp = Visit(c.expr()) as NodeExpr;
            else if (n.Field.Type.type == "Int")
                n.Exp = new NodeInt(c, "0");
            else if (n.Field.Type.type == "Bool")
                n.Exp = new NodeBool(c, "false");
            else if (n.Field.Type.type == "String")
                n.Exp = new NodeString(c, "");
            else
                n.Exp = new NodeVoid(n.Field.Type.type);

            return n;
        }
        public override Node VisitField([NotNull] CoolParser.FieldContext c)
        {
            return new NodeFields(c)
            {
                Id = new NodeID(c, c.ID().GetText()),
                Type = new NodeType(c.TYPE().Symbol.Line, c.TYPE().Symbol.Column, c.TYPE().GetText())
            };
        }
   
        public override Node VisitFuncCallExpl([NotNull] CoolParser.FuncCallExplContext c)
        {
            var n = new NodeFuncCallExplicit(c)
            {
                Expression = Visit(c.expr(0)) as NodeExpr
            };

            var scType = c.TYPE() == null ? new NodeType(n.Expression.Ln, n.Expression.Clmn, n.Expression.typeStat.Name) : new NodeType(c.TYPE().Symbol.Line, c.TYPE().Symbol.Column, c.TYPE().GetText());
            n.IdType = scType;
            var id = new NodeID(c.ID().Symbol.Line, c.ID().Symbol.Column, c.ID().GetText());
            n.FuncName = id;
            n.Arguments = (from x in c.expr().Skip(1) select Visit(x) as NodeExpr).ToList();
            return n;
        }

        public override Node VisitOpComp([NotNull] CoolParser.OpCompContext c)
        {
            NodeCompare n;
            switch (c.op.Text)
            {
                case "<=":
                    n = new NodeLessEqualThan(c);
                    break;
                case "<":
                    n = new NodeLessThan(c);
                    break;
                case "=":
                    n = new NodeEqual(c);
                    break;
                default:
                    throw new NotSupportedException();
            }
            n.LeftOperand = Visit(c.expr(0)) as NodeExpr;
            n.RightOperand = Visit(c.expr(1)) as NodeExpr;
            return n;
        }
        public override Node VisitInt([NotNull] CoolParser.IntContext c)
        {
            return new NodeInt(c, c.INT().GetText());
        }

        public override Node VisitIsvoid([NotNull] CoolParser.IsvoidContext c)
        {
            return new NodeIsVoid(c)
            {
                Operand = Visit(c.expr()) as NodeExpr
            };
        }
       

        public override Node VisitFuncCallImpl([NotNull] CoolParser.FuncCallImplContext c)
        {
            return new NodeFuncCall_Implicit(c)
            {
                FuncName = new NodeID(c, c.ID().GetText()),
                Arguments = (from x in c.expr() select Visit(x) as NodeExpr).ToList()

            };
        }

        public override Node VisitWhile([NotNull] CoolParser.WhileContext c)
        {
            return new NodeWhile(c)
            {
                Condition = Visit(c.expr(0)) as NodeExpr,     
                Body = Visit(c.expr(1)) as NodeExpr           
            };
        }

        public override Node VisitId([NotNull] CoolParser.IdContext c)
        {
            if (c.ID().GetText() == "self")
                return new NodeSelf(c);
            return new IdentNode(c, c.ID().GetText());
        }


        public override Node VisitNew([NotNull] CoolParser.NewContext c)
        {
            return new NodeNew(c)
            {
                TypeId = new NodeType(c.TYPE().Symbol.Line, c.TYPE().Symbol.Column, c.TYPE().GetText())
            };
        }

        public override Node VisitBoolNot([NotNull] CoolParser.BoolNotContext c)
        {
            return new NodeNot(c)
            {
                Operand = Visit(c.expr()) as NodeExpr
            };
        }


        public override Node VisitBlock([NotNull] CoolParser.BlockContext c)
        {
            return new NodeBlock(c)
            {
                Block = c.expr().Select(x => Visit(x) as NodeExpr).ToList()
            };
        }
        public override Node VisitOpArit([NotNull] CoolParser.OpAritContext c)
        {
            NodeAritmethicOp n;
            switch (c.op.Text)
            {
                case "*":
                    n = new NodeMult(c);
                    break;
                case "/":
                    n = new NodeDiv(c);
                    break;
                case "+":
                    n = new NodeAdd(c);
                    break;
                case "-":
                    n = new NodeMinus(c);
                    break;
                default:
                    throw new NotSupportedException();
            }
            n.LeftOperand = Visit(c.expr(0)) as NodeExpr;
            n.RightOperand = Visit(c.expr(1)) as NodeExpr;
            return n;

        }

        public override Node VisitAssignment([NotNull] CoolParser.AssignmentContext c)
        {
            return new NodeAssign(c)
            {
                ID = new IdentNode(c, c.ID().GetText()),
                ExpressionRight = Visit(c.expr()) as NodeExpr
            };
        }

        

        public override Node VisitLetIn([NotNull] CoolParser.LetInContext c)
        {
            var n = new NodeLet(c)
            {
                Initialization = (from x in c.attr() select Visit(x) as NodeAttr).ToList(),
                ExpressionBody = Visit(c.expr()) as NodeExpr
            };
            return n;
        }

        public override Node VisitIf([NotNull] CoolParser.IfContext c)
        {
            return new NodeIf(c)
            {
                Condition = Visit(c.expr(0)) as NodeExpr,   
                Body = Visit(c.expr(1)) as NodeExpr,   
                ElseBody = Visit(c.expr(2)) as NodeExpr    
            };
        }

       public override Node VisitCase([NotNull] CoolParser.CaseContext c)
        {
            var n = new NodeCase(c)
            {
                ExpressionCase = Visit(c.expr(0)) as NodeExpr
            };

            var _f = c.field().Select(x => Visit(x)).ToList();
            var _e = c.expr().Skip(1).Select(x => Visit(x)).ToList();
            for (int i = 0; i < _f.Count; ++i)
            { var f = _f[i] as NodeFields;
                var e = _e[i] as NodeExpr;

                var t = new Tuple<NodeFields, NodeExpr>(f, e);
                 n.Branches.Add(t); }

            return n;
        }


        public override Node VisitString([NotNull] CoolParser.StringContext c)
        {
            return new NodeString(c, c.STRING().GetText());
        }
        public override Node VisitParentheses([NotNull] CoolParser.ParenthesesContext c)
        {
            return Visit(c.expr());
        }

        public override Node VisitBoolean([NotNull] CoolParser.BooleanContext c)
        {
            return new NodeBool(c, c.value.Text);
        }
        public override Node VisitNegative([NotNull] CoolParser.NegativeContext c)
        {
            return new NodeNegative(c)
            {
                Operand = Visit(c.expr()) as NodeExpr
            };
        }
       
    }
}

