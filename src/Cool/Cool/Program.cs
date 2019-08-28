using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Cool.CoolAST;
using Cool.CoolParsing;
using Cool.Semantic_Checking;
using System.IO;
using Cool.CodeGeneration.TAC;
using Cool.CodeGeneration.MIPS;

namespace Cool
{
    class Program
    {

        public struct Category { public string Parsing; public string Semantic; public string CodeGeneration; public string None; }

        public struct Status { public string Success; public string Fail; }

        static void Main(string[] args)
        {
            Console.WriteLine("Put the file to compile");
            string _in = Console.ReadLine();

            string generatedCodeMips = "";
            string generatedCodeTAC = "";
            string casesofproof = "";

            if (_in == "")
            {
                var category = new Category() { Parsing = "Parsing", Semantic = "Semantic", CodeGeneration = "CodeGeneration", None = "None" };
                var status = new Status() { Success = "Success", Fail = "Fail" };

                //cambias el valor de myfile y pones el nombre del q quieres probar de los archivos .cl que estan en la carpeta Tester
                string fileName = "hello_world";
                string myfile = fileName + ".cl";
                string cool_program = myfile.Substring(0, myfile.Length - 3);
                string _category = category.Semantic;
                string _status = status.Success;

                if (_category == category.Parsing || _category == category.Semantic)
                {
                    generatedCodeMips = $"../../../Tester/{_category}/{_status}/{cool_program}.s";
                    generatedCodeTAC = $"../../../Tester/{_category}/{_status}/{cool_program}.tac";
                    casesofproof = $"../../../Tester/{_category}/{_status}/" + myfile;
                }
                else if (_category == category.CodeGeneration)
                {
                    generatedCodeMips = $"../../../Tester/{_category}/{cool_program}.s";
                    generatedCodeTAC = $"../../../Tester/{_category}/{cool_program}.tac";
                    casesofproof = $"../../../Tester/{_category}/" + myfile;
                }
                else
                {
                    generatedCodeMips = $"../../../Tester/{cool_program}.s";
                    generatedCodeTAC = $"../../../Tester/{cool_program}.tac";
                    casesofproof = "../../../Tester/" + myfile;
                }
            }
            else
            {
                casesofproof = _in;
                string temp = _in.Substring(0, _in.Length - 3);
                generatedCodeMips = temp+".s";
                generatedCodeTAC = temp+".tac";
            }

            if (!File.Exists(casesofproof))
            {
                Console.WriteLine(casesofproof);
                Console.WriteLine("Can't find the file");
                Environment.ExitCode = 1;
                return;
            }

            Node root = EntryParser(casesofproof);

            if (root == null)
            {
                Environment.ExitCode = 1;
                return;
            }

            if (!(root is NodeProgram))
            {
                Environment.ExitCode = 1;
                return;


            }

            var progContext = new Context();
            NodeProgram rootProgram = root as NodeProgram;
            if (!CheckSemantics(rootProgram, progContext))
            {
                Environment.ExitCode = 1;
                return;
            }
            CodeGeneration(rootProgram, generatedCodeMips, generatedCodeTAC, progContext);


        }

        #region Methods
        public static Node EntryParser(string entry)
        {

            {
                var e = new AntlrFileStream(entry);
                var l = new CoolLexer(e);

                var errors = new List<string>();
                l.RemoveErrorListeners();
                l.AddErrorListener(new LexerErrorListener(errors));

                var t = new CommonTokenStream(l);

                var p = new CoolParser(t);

                p.RemoveErrorListeners();
                p.AddErrorListener(new ParserErrorListener(errors));

                IParseTree tree = p.program();

                if (errors.Any())
                {
                    Console.WriteLine();
                    foreach (var item in errors)
                        Console.WriteLine(item);
                    return null;
                }

                var treeAdapter = new CoolTreeAdapter();
                Node syntaxAbsTree = treeAdapter.Visit(tree);
                return syntaxAbsTree;
            }

        }
        public static bool CheckSemantics(NodeProgram root, Context context)
        {
            var errors = new List<string>();

            var prog = new TypeCollectorVisitor().SemanticChecker(root, context, errors);
            if (errors.Count > 0)
            {
                foreach (var item in errors)
                {
                    Console.WriteLine(item);
                }
                return false;
            }

            else



            {
                prog = new TypeCheckerVisitor().SemanticChecker(prog, context, errors);
                if (errors.Count > 0)
                {
                    foreach (var item in errors)
                    {
                        Console.WriteLine(item);
                    }
                    return false;


                }

                else return true;
            }
        }
        private static void CodeGeneration(NodeProgram root, string outputPathMIPS, string outPathTAC, Context context)
        {

            List<Instructions> g = (new GenerateTAC()).GetIntermediateCode(root, context);
            Console.WriteLine(g.Count);
            string tac = "";
            foreach (var y in g)
            {

                tac += y;

                Console.WriteLine(y);
            }

            File.WriteAllText(outPathTAC, tac);
            Console.WriteLine();
            Console.WriteLine("CODE");

            Console.WriteLine();
            string mipsCode = (new GenerateMips()).GenerateCode(g);
            Console.WriteLine(mipsCode);

            File.WriteAllText(outputPathMIPS, mipsCode);
        }

        #endregion

    }
}
