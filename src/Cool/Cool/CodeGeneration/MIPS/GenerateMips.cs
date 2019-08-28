using System;
using System.Collections.Generic;
using System.Text;
using Cool.Interfaces;
using Cool.CodeGeneration.TAC;

namespace Cool.CodeGeneration.MIPS
{
    public class GenerateMips : IGenCodeVisitor
    {
        List<string> Code;
        List<string> Data;
        string current_function;
        int size;
        int param_count;
        Auxiliary aux;

        public string GenerateCode(List<Instructions> lines)
        {
            Code = new List<string>();
            Data = new List<string>();
            param_count = 0;
            aux = new Auxiliary(lines);

            

            foreach (var str in aux.strCount)
            {
                
                Data.Add($"str{str.Value}: .asciiz \"{str.Key}\"");
            }

            foreach (var x in aux.Inherit)
            {
                string s = $"_class.{ x.Key}: .word str{aux.strCount[x.Key]}, ";

                string p = x.Value;
                while (p != "Object")
                {
                    s += $"str{aux.strCount[p]}, ";
                    p = aux.Inherit[p];
                }

                s += $"str{aux.strCount["Object"]}, 0";
                Data.Add(s);
            }

            for (int i = 0; i < lines.Count; ++i)
            {
                Code.Add($"# {lines[i]}");
                lines[i].Accept(this);
            }

            string codeStatic = "";

            codeStatic += ".data\n";
            codeStatic += "buffer: .space 65536\n";
            codeStatic += "strsubstrexception: .asciiz \"Substring index exception\n\"\n";


            foreach (string s in Data)
                codeStatic += s + "\n";

            codeStatic += "\n.globl main\n";
            codeStatic += ".text\n";

            codeStatic += "_inherit:\n";
            codeStatic += "lw $a0, 8($a0)\n";
            codeStatic += "_inherit.loop:\n";
            codeStatic += "lw $a2, 0($a0)\n";
            codeStatic += "beq $a1, $a2, _inherit_true\n";
            codeStatic += "beq $a2, $zero, _inherit_false\n";
            codeStatic += "addiu $a0, $a0, 4\n";
            codeStatic += "j _inherit.loop\n";
            codeStatic += "_inherit_false:\n";
            codeStatic += "li $v0, 0\n";
            codeStatic += "jr $ra\n";
            codeStatic += "_inherit_true:\n";
            codeStatic += "li $v0, 1\n";
            codeStatic += "jr $ra\n";
            codeStatic += "\n";

            codeStatic += "_copy:\n";
            codeStatic += "lw $a1, 0($sp)\n";
            codeStatic += "lw $a0, -4($sp)\n";
            codeStatic += "li $v0, 9\n";
            codeStatic += "syscall\n";
            codeStatic += "lw $a1, 0($sp)\n";
            codeStatic += "lw $a0, 4($a1)\n";
            codeStatic += "move $a3, $v0\n";
            codeStatic += "_copy.loop:\n";
            codeStatic += "lw $a2, 0($a1)\n";
            codeStatic += "sw $a2, 0($a3)\n";
            codeStatic += "addiu $a0, $a0, -1\n";
            codeStatic += "addiu $a1, $a1, 4\n";
            codeStatic += "addiu $a3, $a3, 4\n";
            codeStatic += "beq $a0, $zero, _copy.end\n";
            codeStatic += "j _copy.loop\n";
            codeStatic += "_copy.end:\n";
            codeStatic += "jr $ra\n";
            codeStatic += "\n";

            codeStatic += "_abort:\n";
            codeStatic += "li $v0, 10\n";
            codeStatic += "syscall\n";
            codeStatic += "\n";

            codeStatic += "_out_string:\n";
            codeStatic += "li $v0, 4\n";
            codeStatic += "lw $a0, 0($sp)\n";
            codeStatic += "syscall\n";
            codeStatic += "jr $ra\n";
            codeStatic += "\n";

            codeStatic += "_out_int:\n";
            codeStatic += "li $v0, 1\n";
            codeStatic += "lw $a0, 0($sp)\n";
            codeStatic += "syscall\n";
            codeStatic += "jr $ra\n";
            codeStatic += "\n";

            codeStatic += "_in_string:\n";
            codeStatic += "move $a3, $ra\n";
            codeStatic += "la $a0, buffer\n";
            codeStatic += "li $a1, 65536\n";
            codeStatic += "li $v0, 8\n";
            codeStatic += "syscall\n";
            codeStatic += "addiu $sp, $sp, -4\n";
            codeStatic += "sw $a0, 0($sp)\n";
            codeStatic += "jal String.length\n";
            codeStatic += "addiu $sp, $sp, 4\n";
            codeStatic += "move $a2, $v0\n";
            codeStatic += "addiu $a2, $a2, -1\n";
            codeStatic += "move $a0, $v0\n";
            codeStatic += "li $v0, 9\n";
            codeStatic += "syscall\n";
            codeStatic += "move $v1, $v0\n";
            codeStatic += "la $a0, buffer\n";
            codeStatic += "_in_string.loop:\n";
            codeStatic += "beqz $a2, _in_string.end\n";
            codeStatic += "lb $a1, 0($a0)\n";
            codeStatic += "sb $a1, 0($v1)\n";
            codeStatic += "addiu $a0, $a0, 1\n";
            codeStatic += "addiu $v1, $v1, 1\n";
            codeStatic += "addiu $a2, $a2, -1\n";
            codeStatic += "j _in_string.loop\n";
            codeStatic += "_in_string.end:\n";
            codeStatic += "sb $zero, 0($v1)\n";
            codeStatic += "move $ra, $a3\n";
            codeStatic += "jr $ra\n";
            codeStatic += "\n";

            codeStatic += "_in_int:\n";
            codeStatic += "li $v0, 5\n";
            codeStatic += "syscall\n";
            codeStatic += "jr $ra\n";
            codeStatic += "\n";

            codeStatic += "_stringlength:\n";
            codeStatic += "lw $a0, 0($sp)\n";
            codeStatic += "_stringlength.loop:\n";
            codeStatic += "lb $a1, 0($a0)\n";
            codeStatic += "beqz $a1, _stringlength.end\n";
            codeStatic += "addiu $a0, $a0, 1\n";
            codeStatic += "j _stringlength.loop\n";
            codeStatic += "_stringlength.end:\n";
            codeStatic += "lw $a1, 0($sp)\n";
            codeStatic += "subu $v0, $a0, $a1\n";
            codeStatic += "jr $ra\n";
            codeStatic += "\n";

            codeStatic += "_stringconcat:\n";
            codeStatic += "move $a2, $ra\n";
            codeStatic += "jal _stringlength\n";
            codeStatic += "move $v1, $v0\n";
            codeStatic += "addiu $sp, $sp, -4\n";
            codeStatic += "jal _stringlength\n";
            codeStatic += "addiu $sp, $sp, 4\n";
            codeStatic += "add $v1, $v1, $v0\n";
            codeStatic += "addi $v1, $v1, 1\n";
            codeStatic += "li $v0, 9\n";
            codeStatic += "move $a0, $v1\n";
            codeStatic += "syscall\n";
            codeStatic += "move $v1, $v0\n";
            codeStatic += "lw $a0, 0($sp)\n";
            codeStatic += "_stringconcat.loop1:\n";
            codeStatic += "lb $a1, 0($a0)\n";
            codeStatic += "beqz $a1, _stringconcat.end1\n";
            codeStatic += "sb $a1, 0($v1)\n";
            codeStatic += "addiu $a0, $a0, 1\n";
            codeStatic += "addiu $v1, $v1, 1\n";
            codeStatic += "j _stringconcat.loop1\n";
            codeStatic += "_stringconcat.end1:\n";
            codeStatic += "lw $a0, -4($sp)\n";
            codeStatic += "_stringconcat.loop2:\n";
            codeStatic += "lb $a1, 0($a0)\n";
            codeStatic += "beqz $a1, _stringconcat.end2\n";
            codeStatic += "sb $a1, 0($v1)\n";
            codeStatic += "addiu $a0, $a0, 1\n";
            codeStatic += "addiu $v1, $v1, 1\n";
            codeStatic += "j _stringconcat.loop2\n";
            codeStatic += "_stringconcat.end2:\n";
            codeStatic += "sb $zero, 0($v1)\n";
            codeStatic += "move $ra, $a2\n";
            codeStatic += "jr $ra\n";
            codeStatic += "\n";

            codeStatic += "_stringsubstr:\n";
            codeStatic += "lw $a0, -8($sp)\n";
            codeStatic += "addiu $a0, $a0, 1\n";
            codeStatic += "li $v0, 9\n";
            codeStatic += "syscall\n";
            codeStatic += "move $v1, $v0\n";
            codeStatic += "lw $a0, 0($sp)\n";
            codeStatic += "lw $a1, -4($sp)\n";
            codeStatic += "add $a0, $a0, $a1\n";
            codeStatic += "lw $a2, -8($sp)\n";
            codeStatic += "_stringsubstr.loop:\n";
            codeStatic += "beqz $a2, _stringsubstr.end\n";
            codeStatic += "lb $a1, 0($a0)\n";
            codeStatic += "beqz $a1, _substrexception\n";
            codeStatic += "sb $a1, 0($v1)\n";
            codeStatic += "addiu $a0, $a0, 1\n";
            codeStatic += "addiu $v1, $v1, 1\n";
            codeStatic += "addiu $a2, $a2, -1\n";
            codeStatic += "j _stringsubstr.loop\n";
            codeStatic += "_stringsubstr.end:\n";
            codeStatic += "sb $zero, 0($v1)\n";
            codeStatic += "jr $ra\n";
            codeStatic += "\n";

            codeStatic += "_substrexception:\n";
            codeStatic += "la $a0, strsubstrexception\n";
            codeStatic += "li $v0, 4\n";
            codeStatic += "syscall\n";
            codeStatic += "li $v0, 10\n";
            codeStatic += "syscall\n";
            codeStatic += "\n";

            codeStatic += "_stringcmp:\n";
            codeStatic += "li $v0, 1\n";
            codeStatic += "_stringcmp.loop:\n";
            codeStatic += "lb $a2, 0($a0)\n";
            codeStatic += "lb $a3, 0($a1)\n";
            codeStatic += "beqz $a2, _stringcmp.end\n";
            codeStatic += "beq $a2, $zero, _stringcmp.end\n";
            codeStatic += "beq $a3, $zero, _stringcmp.end\n";
            codeStatic += "bne $a2, $a3, _stringcmp.differents\n";
            codeStatic += "addiu $a0, $a0, 1\n";
            codeStatic += "addiu $a1, $a1, 1\n";
            codeStatic += "j _stringcmp.loop\n";
            codeStatic += "_stringcmp.end:\n";
            codeStatic += "beq $a2, $a3, _stringcmp.equals\n";
            codeStatic += "_stringcmp.differents:\n";
            codeStatic += "li $v0, 0\n";
            codeStatic += "jr $ra\n";
            codeStatic += "_stringcmp.equals:\n";
            codeStatic += "li $v0, 1\n";
            codeStatic += "jr $ra\n";
            codeStatic += "\n";

            codeStatic += "\nmain:\n";

            foreach (string s in Code)
                codeStatic += s + "\n";

            codeStatic += "li $v0, 10\n";
            codeStatic += "syscall\n";

            return codeStatic;
        }

        public void Visit(MyLabel line)
        {
            if (line.H[0] != '_')
            {
                current_function = line.Label;
                size = aux.sizeFunVar[current_function];
            }
            Code.Add($"\n");
            Code.Add($"{line.Label}:");
            Code.Add($"li $t9, 0");
        }

        public void Visit(Locate line)
        {
            Code.Add($"# Begin Allocate");
            Code.Add($"li $v0, 9");
            Code.Add($"li $a0, {4 * line.t}");
            Code.Add($"syscall");
            Code.Add($"sw $v0, {-4 * line.var}($sp)");
            Code.Add($"# End Allocate");
        }

        public void Visit(Jump line)
        {
            Code.Add($"j {line.Label.Label}");
        }

        public void Visit(Comments line)
        {
            return;
        }

        public void Visit(VarToMemory line)
        {
            Code.Add($"lw $a0, {-line.Rightie * 4}($sp)");
            Code.Add($"lw $a1, {-line.Leftie * 4}($sp)");
            Code.Add($"sw $a0, {line.Offset * 4}($a1)");
        }

        public void Visit(VarToVar line)
        {
            Code.Add($"lw $a0, {-line.Rightie * 4}($sp)");
            Code.Add($"sw $a0, {-line.Leftie * 4}($sp)");
        }

        public void Visit(ConstantToMemory line)
        {
            Code.Add($"lw $a0, {-line.Leftie * 4}($sp)");
            Code.Add($"li $a1, {line.Rightie}");
            Code.Add($"sw $a1, {line.offst * 4}($a0)");
        }

        public void Visit(MemoryToVar line)
        {
            Code.Add($"lw $a0, {-line.Rightie * 4}($sp)");
            Code.Add($"lw $a1, {line.offst * 4}($a0)");
            Code.Add($"sw $a1, {-line.Leftie * 4}($sp)");
        }

        public void Visit(ConstantToVar line)
        {
            Code.Add($"li $a0, {line.Rightie}");
            Code.Add($"sw $a0, {-line.Leftie * 4}($sp)");
        }

        public void Visit(StringToVar line)
        {
            Code.Add($"la $a0, str{aux.strCount[line.Rightie]}");
            Code.Add($"sw $a0, {-line.Leftie * 4}($sp)");
        }

        public void Visit(StringToMemory line)
        {
            Code.Add($"la $a0, str{aux.strCount[line.Rightie]}");
            Code.Add($"lw $a1, {-line.Leftie * 4}($sp)");
            Code.Add($"sw $a0, {line.offst * 4}($a1)");
        }


        public void Visit(LabelToVar line)
        {
            Code.Add($"la $a0, {line.Rightie.Label}");
            Code.Add($"sw $a0, {-line.Leftie * 4}($sp)");
        }

        public void Visit(LabelToMemory line)
        {
            Code.Add($"la $a0, {line.Rightie.Label}");
            Code.Add($"lw $a1, {-line.Leftie * 4}($sp)");
            Code.Add($"sw $a0, {line.offst * 4}($a1)");
        }
        public void Visit(NullToVar line)
        {
            Code.Add($"sw $zero, {-line.v * 4}($sp)");
        }

        public void Visit(Return line)
        {
            Code.Add($"lw $v0, {-line.v * 4}($sp)");
            Code.Add($"jr $ra");
        }

        public void Visit(Param line)
        {
            return;
        }

        public void Visit(OutParam line)
        {
            param_count = 0;
        }

        public void Visit(CondJump line)
        {
            Code.Add($"lw $a0, {-line.VarCond * 4}($sp)");
            Code.Add($"beqz $a0, {line.Label.Label}");
        }

        public void Visit(InParam line)
        {
            ++param_count;
            Code.Add($"lw $a0, {-line.v * 4}($sp)");
            Code.Add($"sw $a0, {-(size + param_count) * 4}($sp)");
        }

        public void Visit(LabelCall line)
        {
            Code.Add($"sw $ra, {-size * 4}($sp)");
            Code.Add($"addiu $sp, $sp, {-(size + 1) * 4}");
            Code.Add($"jal {line.M.Label}");
            Code.Add($"addiu $sp, $sp, {(size + 1) * 4}");
            Code.Add($"lw $ra, {-size * 4}($sp)");
            if (line.Res != -1)
                Code.Add($"sw $v0, {-line.Res * 4}($sp)");
        }

        public void Visit(CallAddr line)
        {
            Code.Add($"sw $ra, {-size * 4}($sp)");
            Code.Add($"lw $a0, {-line.Addr * 4}($sp)");
            Code.Add($"addiu $sp, $sp, {-(size + 1) * 4}");
            Code.Add($"jalr $ra, $a0");
            Code.Add($"addiu $sp, $sp, {(size + 1) * 4}");
            Code.Add($"lw $ra, {-size * 4}($sp)");
            if (line.Res != -1)
                Code.Add($"sw $v0, {-line.Res * 4}($sp)");
        }


        public void Visit(BinOp line)
        {
            Code.Add($"lw $a0, {-line.lov * 4}($sp)");
            Code.Add($"lw $a1, {-line.rov * 4}($sp)");

            switch (line.Symbol)
            {
                case "+":
                    Code.Add($"add $a0, $a0, $a1");
                    break;
                case "-":
                    Code.Add($"sub $a0, $a0, $a1");
                    break;
                case "*":
                    Code.Add($"mult $a0, $a1");
                    Code.Add($"mflo $a0");
                    break;
                case "/":
                    Code.Add($"div $a0, $a1");
                    Code.Add($"mflo $a0");
                    break;
                case "<":
                    Code.Add($"sge $a0, $a0, $a1");
                    Code.Add($"li $a1, 1");
                    Code.Add($"sub $a0, $a1, $a0");
                    break;
                case "<=":
                    Code.Add($"sle $a0, $a0, $a1");
                    break;
                case "=":
                    Code.Add($"seq $a0, $a0, $a1");
                    break;
                case "=:=":
                    Code.Add($"move $v1, $ra");
                    Code.Add($"jal _stringcmp");
                    Code.Add($"move $ra, $v1");
                    Code.Add($"move $a0, $v0");
                    break;
                case "inherit":
                    Code.Add($"move $v1, $ra");
                    Code.Add($"jal _inherit");
                    Code.Add($"move $ra, $v1");
                    Code.Add($"move $a0, $v0");
                    break;
                default:
                    throw new NotImplementedException();
            }

            Code.Add($"sw $a0, {-line.varAss * 4}($sp)");
        }

        public void Visit(UnaryOp line)
        {
            Code.Add($"lw $a0, {-line.varOp * 4}($sp)");

            switch (line.Symbol)
            {
                case "not":
                    Code.Add($"li $a1, 1");
                    Code.Add($"sub $a0, $a1, $a0");
                    break;
                case "isvoid":
                    Code.Add($"seq $a0, $a0, $zero");
                    break;
                case "~":
                    Code.Add($"not $a0, $a0");
                    break;
                default:
                    throw new NotImplementedException();
                    
            }

            Code.Add($"sw $a0, {-line.varassign * 4}($sp)");
        }

        public void Visit(Inherits line)
        {

            //throw new NotImplementedException();
        }

    }
}
