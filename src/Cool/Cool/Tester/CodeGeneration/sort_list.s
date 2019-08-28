.data
buffer: .space 65536
strsubstrexception: .asciiz "Substring index exception
"
str0: .asciiz "Object"
str1: .asciiz "IO"
str2: .asciiz "Int"
str3: .asciiz "Bool"
str4: .asciiz "String"
str5: .asciiz "Main"
str6: .asciiz "How many numbers to sort?"
str7: .asciiz "List"
str8: .asciiz "Nil"
str9: .asciiz "Cons"
str10: .asciiz "\n"
_class.IO: .word str1, str0, 0
_class.Int: .word str2, str0, 0
_class.Bool: .word str3, str0, 0
_class.String: .word str4, str0, 0
_class.Main: .word str5, str1, str0, 0
_class.List: .word str7, str1, str0, 0
_class.Nil: .word str8, str7, str1, str0, 0
_class.Cons: .word str9, str7, str1, str0, 0

.globl main
.text
_inherit:
lw $a0, 8($a0)
_inherit.loop:
lw $a2, 0($a0)
beq $a1, $a2, _inherit_true
beq $a2, $zero, _inherit_false
addiu $a0, $a0, 4
j _inherit.loop
_inherit_false:
li $v0, 0
jr $ra
_inherit_true:
li $v0, 1
jr $ra

_copy:
lw $a1, 0($sp)
lw $a0, -4($sp)
li $v0, 9
syscall
lw $a1, 0($sp)
lw $a0, 4($a1)
move $a3, $v0
_copy.loop:
lw $a2, 0($a1)
sw $a2, 0($a3)
addiu $a0, $a0, -1
addiu $a1, $a1, 4
addiu $a3, $a3, 4
beq $a0, $zero, _copy.end
j _copy.loop
_copy.end:
jr $ra

_abort:
li $v0, 10
syscall

_out_string:
li $v0, 4
lw $a0, 0($sp)
syscall
jr $ra

_out_int:
li $v0, 1
lw $a0, 0($sp)
syscall
jr $ra

_in_string:
move $a3, $ra
la $a0, buffer
li $a1, 65536
li $v0, 8
syscall
addiu $sp, $sp, -4
sw $a0, 0($sp)
jal String.length
addiu $sp, $sp, 4
move $a2, $v0
addiu $a2, $a2, -1
move $a0, $v0
li $v0, 9
syscall
move $v1, $v0
la $a0, buffer
_in_string.loop:
beqz $a2, _in_string.end
lb $a1, 0($a0)
sb $a1, 0($v1)
addiu $a0, $a0, 1
addiu $v1, $v1, 1
addiu $a2, $a2, -1
j _in_string.loop
_in_string.end:
sb $zero, 0($v1)
move $ra, $a3
jr $ra

_in_int:
li $v0, 5
syscall
jr $ra

_stringlength:
lw $a0, 0($sp)
_stringlength.loop:
lb $a1, 0($a0)
beqz $a1, _stringlength.end
addiu $a0, $a0, 1
j _stringlength.loop
_stringlength.end:
lw $a1, 0($sp)
subu $v0, $a0, $a1
jr $ra

_stringconcat:
move $a2, $ra
jal _stringlength
move $v1, $v0
addiu $sp, $sp, -4
jal _stringlength
addiu $sp, $sp, 4
add $v1, $v1, $v0
addi $v1, $v1, 1
li $v0, 9
move $a0, $v1
syscall
move $v1, $v0
lw $a0, 0($sp)
_stringconcat.loop1:
lb $a1, 0($a0)
beqz $a1, _stringconcat.end1
sb $a1, 0($v1)
addiu $a0, $a0, 1
addiu $v1, $v1, 1
j _stringconcat.loop1
_stringconcat.end1:
lw $a0, -4($sp)
_stringconcat.loop2:
lb $a1, 0($a0)
beqz $a1, _stringconcat.end2
sb $a1, 0($v1)
addiu $a0, $a0, 1
addiu $v1, $v1, 1
j _stringconcat.loop2
_stringconcat.end2:
sb $zero, 0($v1)
move $ra, $a2
jr $ra

_stringsubstr:
lw $a0, -8($sp)
addiu $a0, $a0, 1
li $v0, 9
syscall
move $v1, $v0
lw $a0, 0($sp)
lw $a1, -4($sp)
add $a0, $a0, $a1
lw $a2, -8($sp)
_stringsubstr.loop:
beqz $a2, _stringsubstr.end
lb $a1, 0($a0)
beqz $a1, _substrexception
sb $a1, 0($v1)
addiu $a0, $a0, 1
addiu $v1, $v1, 1
addiu $a2, $a2, -1
j _stringsubstr.loop
_stringsubstr.end:
sb $zero, 0($v1)
jr $ra

_substrexception:
la $a0, strsubstrexception
li $v0, 4
syscall
li $v0, 10
syscall

_stringcmp:
li $v0, 1
_stringcmp.loop:
lb $a2, 0($a0)
lb $a3, 0($a1)
beqz $a2, _stringcmp.end
beq $a2, $zero, _stringcmp.end
beq $a3, $zero, _stringcmp.end
bne $a2, $a3, _stringcmp.differents
addiu $a0, $a0, 1
addiu $a1, $a1, 1
j _stringcmp.loop
_stringcmp.end:
beq $a2, $a3, _stringcmp.equals
_stringcmp.differents:
li $v0, 0
jr $ra
_stringcmp.equals:
li $v0, 1
jr $ra


main:
# Call start;
sw $ra, 0($sp)
addiu $sp, $sp, -4
jal start
addiu $sp, $sp, 4
lw $ra, 0($sp)
# Object.constructor:


Object.constructor:
li $t9, 0
# PARAM t0;
# // Method: Object.abort
# *(t0 + 3) = Label "Object.abort"
la $a0, Object.abort
lw $a1, 0($sp)
sw $a0, 12($a1)
# // Method: Object.type_name
# *(t0 + 4) = Label "Object.type_name"
la $a0, Object.type_name
lw $a1, 0($sp)
sw $a0, 16($a1)
# // Method: Object.copy
# *(t0 + 5) = Label "Object.copy"
la $a0, Object.copy
lw $a1, 0($sp)
sw $a0, 20($a1)
# // Class Name is Object
# *(t0 + 0) = "Object"
la $a0, str0
lw $a1, 0($sp)
sw $a0, 0($a1)
# // Class Large: 6
# *(t0 + 1) = 6
lw $a0, 0($sp)
li $a1, 6
sw $a1, 4($a0)
# Return ;

lw $v0, 4($sp)
jr $ra
# IO.constructor:


IO.constructor:
li $t9, 0
# PARAM t0;
# PushParam t0;
lw $a0, 0($sp)
sw $a0, -12($sp)
# Call Object.constructor;
sw $ra, -8($sp)
addiu $sp, $sp, -12
jal Object.constructor
addiu $sp, $sp, 12
lw $ra, -8($sp)
# PopParam 1;
# // Method: IO.out_string
# *(t0 + 6) = Label "IO.out_string"
la $a0, IO.out_string
lw $a1, 0($sp)
sw $a0, 24($a1)
# // Method: IO.out_int
# *(t0 + 7) = Label "IO.out_int"
la $a0, IO.out_int
lw $a1, 0($sp)
sw $a0, 28($a1)
# // Method: IO.in_string
# *(t0 + 8) = Label "IO.in_string"
la $a0, IO.in_string
lw $a1, 0($sp)
sw $a0, 32($a1)
# // Method: IO.in_int
# *(t0 + 9) = Label "IO.in_int"
la $a0, IO.in_int
lw $a1, 0($sp)
sw $a0, 36($a1)
# // Class name is Object
# *(t0 + 0) = "IO"
la $a0, str1
lw $a1, 0($sp)
sw $a0, 0($a1)
# // Class large: 10
# *(t0 + 1) = 10
lw $a0, 0($sp)
li $a1, 10
sw $a1, 4($a0)
# // Class Label
# *(t0 + 2) = Label "_class.IO"
la $a0, _class.IO
lw $a1, 0($sp)
sw $a0, 8($a1)
# Return ;

lw $v0, 4($sp)
jr $ra
# _class.IO: _class.Object
# _class.Int: _class.Object
# _class.Bool: _class.Object
# _class.String: _class.Object
# _wrapper.Int:


_wrapper.Int:
li $t9, 0
# PARAM t0;
# t1 = Alloc 7;
# Begin Allocate
li $v0, 9
li $a0, 28
syscall
sw $v0, -4($sp)
# End Allocate
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -12($sp)
# Call Object.constructor;
sw $ra, -8($sp)
addiu $sp, $sp, -12
jal Object.constructor
addiu $sp, $sp, 12
lw $ra, -8($sp)
# PopParam 1;
# *(t1 + 0) = "Int"
la $a0, str2
lw $a1, -4($sp)
sw $a0, 0($a1)
# *(t1 + 6) = t0
lw $a0, 0($sp)
lw $a1, -4($sp)
sw $a0, 24($a1)
# *(t1 + 2) = Label "_class.Int"
la $a0, _class.Int
lw $a1, -4($sp)
sw $a0, 8($a1)
# Return t1;

lw $v0, -4($sp)
jr $ra
# _wrapper.Bool:


_wrapper.Bool:
li $t9, 0
# PARAM t0;
# t1 = Alloc 7;
# Begin Allocate
li $v0, 9
li $a0, 28
syscall
sw $v0, -4($sp)
# End Allocate
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -12($sp)
# Call Object.constructor;
sw $ra, -8($sp)
addiu $sp, $sp, -12
jal Object.constructor
addiu $sp, $sp, 12
lw $ra, -8($sp)
# PopParam 1;
# *(t1 + 0) = "Bool"
la $a0, str3
lw $a1, -4($sp)
sw $a0, 0($a1)
# *(t1 + 6) = t0
lw $a0, 0($sp)
lw $a1, -4($sp)
sw $a0, 24($a1)
# *(t1 + 2) = Label "_class.Bool"
la $a0, _class.Bool
lw $a1, -4($sp)
sw $a0, 8($a1)
# Return t1;

lw $v0, -4($sp)
jr $ra
# _wrapper.String:


_wrapper.String:
li $t9, 0
# PARAM t0;
# t1 = Alloc 10;
# Begin Allocate
li $v0, 9
li $a0, 40
syscall
sw $v0, -4($sp)
# End Allocate
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -12($sp)
# Call Object.constructor;
sw $ra, -8($sp)
addiu $sp, $sp, -12
jal Object.constructor
addiu $sp, $sp, 12
lw $ra, -8($sp)
# PopParam 1;
# *(t1 + 0) = "String"
la $a0, str4
lw $a1, -4($sp)
sw $a0, 0($a1)
# *(t1 + 9) = t0
lw $a0, 0($sp)
lw $a1, -4($sp)
sw $a0, 36($a1)
# *(t1 + 2) = Label "_class.String"
la $a0, _class.String
lw $a1, -4($sp)
sw $a0, 8($a1)
# Return t1;

lw $v0, -4($sp)
jr $ra
# Object.abort:


Object.abort:
li $t9, 0
# Goto _abort
j _abort
# Object.type_name:


Object.type_name:
li $t9, 0
# PARAM t0;
# t0 = *(t0 + 0)
lw $a0, 0($sp)
lw $a1, 0($a0)
sw $a1, 0($sp)
# Return t0;

lw $v0, 0($sp)
jr $ra
# Object.copy:


Object.copy:
li $t9, 0
# PARAM t0;
# t1 = *(t0 + 1)
lw $a0, 0($sp)
lw $a1, 4($a0)
sw $a1, -4($sp)
# t2 = 4
li $a0, 4
sw $a0, -8($sp)
# t1 = t1 * t2
lw $a0, -4($sp)
lw $a1, -8($sp)
mult $a0, $a1
mflo $a0
sw $a0, -4($sp)
# PushParam t0;
lw $a0, 0($sp)
sw $a0, -16($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -20($sp)
# t0 = Call _copy;
sw $ra, -12($sp)
addiu $sp, $sp, -16
jal _copy
addiu $sp, $sp, 16
lw $ra, -12($sp)
sw $v0, 0($sp)
# PopParam 2;
# Return t0;

lw $v0, 0($sp)
jr $ra
# IO.out_string:


IO.out_string:
li $t9, 0
# PARAM t0;
# PARAM t1;
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -12($sp)
# t0 = Call _out_string;
sw $ra, -8($sp)
addiu $sp, $sp, -12
jal _out_string
addiu $sp, $sp, 12
lw $ra, -8($sp)
sw $v0, 0($sp)
# PopParam 1;
# Return t0;

lw $v0, 0($sp)
jr $ra
# IO.out_int:


IO.out_int:
li $t9, 0
# PARAM t0;
# PARAM t1;
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -12($sp)
# t0 = Call _out_int;
sw $ra, -8($sp)
addiu $sp, $sp, -12
jal _out_int
addiu $sp, $sp, 12
lw $ra, -8($sp)
sw $v0, 0($sp)
# PopParam 1;
# Return t0;

lw $v0, 0($sp)
jr $ra
# IO.in_string:


IO.in_string:
li $t9, 0
# PARAM t0;
# t0 = Call _in_string;
sw $ra, -4($sp)
addiu $sp, $sp, -8
jal _in_string
addiu $sp, $sp, 8
lw $ra, -4($sp)
sw $v0, 0($sp)
# Return t0;

lw $v0, 0($sp)
jr $ra
# IO.in_int:


IO.in_int:
li $t9, 0
# PARAM t0;
# t0 = Call _in_int;
sw $ra, -4($sp)
addiu $sp, $sp, -8
jal _in_int
addiu $sp, $sp, 8
lw $ra, -4($sp)
sw $v0, 0($sp)
# Return t0;

lw $v0, 0($sp)
jr $ra
# String.length:


String.length:
li $t9, 0
# PARAM t0;
# PushParam t0;
lw $a0, 0($sp)
sw $a0, -8($sp)
# t0 = Call _stringlength;
sw $ra, -4($sp)
addiu $sp, $sp, -8
jal _stringlength
addiu $sp, $sp, 8
lw $ra, -4($sp)
sw $v0, 0($sp)
# PopParam 1;
# Return t0;

lw $v0, 0($sp)
jr $ra
# String.concat:


String.concat:
li $t9, 0
# PARAM t0;
# PARAM t1;
# PushParam t0;
lw $a0, 0($sp)
sw $a0, -12($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -16($sp)
# t0 = Call _stringconcat;
sw $ra, -8($sp)
addiu $sp, $sp, -12
jal _stringconcat
addiu $sp, $sp, 12
lw $ra, -8($sp)
sw $v0, 0($sp)
# PopParam 2;
# Return t0;

lw $v0, 0($sp)
jr $ra
# String.substr:


String.substr:
li $t9, 0
# PARAM t0;
# PARAM t1;
# PARAM t2;
# PushParam t0;
lw $a0, 0($sp)
sw $a0, -16($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -20($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -24($sp)
# t0 = Call _stringsubstr;
sw $ra, -12($sp)
addiu $sp, $sp, -16
jal _stringsubstr
addiu $sp, $sp, 16
lw $ra, -12($sp)
sw $v0, 0($sp)
# PopParam 3;
# Return t0;

lw $v0, 0($sp)
jr $ra
# _class.Main: _class.IO
# Main.iota:


Main.iota:
li $t9, 0
# PARAM t0;
# PARAM t1;
# t2 = Alloc 19;
# Begin Allocate
li $v0, 9
li $a0, 76
syscall
sw $v0, -8($sp)
# End Allocate
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -36($sp)
# Call Nil.constructor;
sw $ra, -32($sp)
addiu $sp, $sp, -36
jal Nil.constructor
addiu $sp, $sp, 36
lw $ra, -32($sp)
# PopParam 1;
# *(t0 + 12) = t2
lw $a0, -8($sp)
lw $a1, 0($sp)
sw $a0, 48($a1)
# t3 = 0
li $a0, 0
sw $a0, -12($sp)
# _whilecondition.140:


_whilecondition.140:
li $t9, 0
# // Var: j
# t5 = t3
lw $a0, -12($sp)
sw $a0, -20($sp)
# // Var: i
# t6 = t1
lw $a0, -4($sp)
sw $a0, -24($sp)
# t2 = t5 < t6
lw $a0, -20($sp)
lw $a1, -24($sp)
sge $a0, $a0, $a1
li $a1, 1
sub $a0, $a1, $a0
sw $a0, -8($sp)
# IfZ t2 Goto _endwhile.140
lw $a0, -8($sp)
beqz $a0, _endwhile.140
# t2 = Alloc 22;
# Begin Allocate
li $v0, 9
li $a0, 88
syscall
sw $v0, -8($sp)
# End Allocate
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -36($sp)
# Call Cons.constructor;
sw $ra, -32($sp)
addiu $sp, $sp, -36
jal Cons.constructor
addiu $sp, $sp, 36
lw $ra, -32($sp)
# PopParam 1;
# // Var: j
# t6 = t3
lw $a0, -12($sp)
sw $a0, -24($sp)
# // Attr: Main.l
# t7 = *(t0 + 12)
lw $a0, 0($sp)
lw $a1, 48($a0)
sw $a1, -28($sp)
# // Method: Cons.init
# t5 = *(t2 + 19)
lw $a0, -8($sp)
lw $a1, 76($a0)
sw $a1, -20($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -36($sp)
# PushParam t6;
lw $a0, -24($sp)
sw $a0, -40($sp)
# PushParam t7;
lw $a0, -28($sp)
sw $a0, -44($sp)
# t2 = Call t5;
sw $ra, -32($sp)
lw $a0, -20($sp)
addiu $sp, $sp, -36
jalr $ra, $a0
addiu $sp, $sp, 36
lw $ra, -32($sp)
sw $v0, -8($sp)
# PopParam 3;
# *(t0 + 12) = t2
lw $a0, -8($sp)
lw $a1, 0($sp)
sw $a0, 48($a1)
# // Var: j
# t5 = t3
lw $a0, -12($sp)
sw $a0, -20($sp)
# t6 = 1
li $a0, 1
sw $a0, -24($sp)
# t2 = t5 + t6
lw $a0, -20($sp)
lw $a1, -24($sp)
add $a0, $a0, $a1
sw $a0, -8($sp)
# t3 = t2
lw $a0, -8($sp)
sw $a0, -12($sp)
# Goto _whilecondition.140
j _whilecondition.140
# _endwhile.140:


_endwhile.140:
li $t9, 0
# // Attr: Main.l
# t2 = *(t0 + 12)
lw $a0, 0($sp)
lw $a1, 48($a0)
sw $a1, -8($sp)
# Return t2;

lw $v0, -8($sp)
jr $ra
# Main.main:


Main.main:
li $t9, 0
# PARAM t0;
# t2 = t0
lw $a0, 0($sp)
sw $a0, -8($sp)
# t4 = "How many numbers to sort?"
la $a0, str6
sw $a0, -16($sp)
# //  String is the return type
# t1 = "String"
la $a0, str4
sw $a0, -4($sp)
# // Method: Main.out_string
# t3 = *(t2 + 6)
lw $a0, -8($sp)
lw $a1, 24($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -28($sp)
# PushParam t4;
lw $a0, -16($sp)
sw $a0, -32($sp)
# t2 = Call t3;
sw $ra, -24($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -28
jalr $ra, $a0
addiu $sp, $sp, 28
lw $ra, -24($sp)
sw $v0, -8($sp)
# // Object is the return type
# t1 = "Object"
la $a0, str0
sw $a0, -4($sp)
# PopParam 2;
# t2 = t0
lw $a0, 0($sp)
sw $a0, -8($sp)
# t4 = t0
lw $a0, 0($sp)
sw $a0, -16($sp)
# // Method: Main.in_int
# t5 = *(t4 + 9)
lw $a0, -16($sp)
lw $a1, 36($a0)
sw $a1, -20($sp)
# PushParam t4;
lw $a0, -16($sp)
sw $a0, -28($sp)
# t4 = Call t5;
sw $ra, -24($sp)
lw $a0, -20($sp)
addiu $sp, $sp, -28
jalr $ra, $a0
addiu $sp, $sp, 28
lw $ra, -24($sp)
sw $v0, -16($sp)
# //  Int is the return type
# t1 = "Int"
la $a0, str2
sw $a0, -4($sp)
# PopParam 1;
# // Method: Main.iota
# t3 = *(t2 + 10)
lw $a0, -8($sp)
lw $a1, 40($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -28($sp)
# PushParam t4;
lw $a0, -16($sp)
sw $a0, -32($sp)
# t2 = Call t3;
sw $ra, -24($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -28
jalr $ra, $a0
addiu $sp, $sp, 28
lw $ra, -24($sp)
sw $v0, -8($sp)
# // Object is the return type
# t1 = "Object"
la $a0, str0
sw $a0, -4($sp)
# PopParam 2;
# // Method: List.rev
# t3 = *(t2 + 14)
lw $a0, -8($sp)
lw $a1, 56($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -28($sp)
# t2 = Call t3;
sw $ra, -24($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -28
jalr $ra, $a0
addiu $sp, $sp, 28
lw $ra, -24($sp)
sw $v0, -8($sp)
# // Object is the return type
# t1 = "Object"
la $a0, str0
sw $a0, -4($sp)
# PopParam 1;
# // Method: List.sort
# t3 = *(t2 + 15)
lw $a0, -8($sp)
lw $a1, 60($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -28($sp)
# t2 = Call t3;
sw $ra, -24($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -28
jalr $ra, $a0
addiu $sp, $sp, 28
lw $ra, -24($sp)
sw $v0, -8($sp)
# // Object is the return type
# t1 = "Object"
la $a0, str0
sw $a0, -4($sp)
# PopParam 1;
# // Method: List.print_list
# t3 = *(t2 + 18)
lw $a0, -8($sp)
lw $a1, 72($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -28($sp)
# t2 = Call t3;
sw $ra, -24($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -28
jalr $ra, $a0
addiu $sp, $sp, 28
lw $ra, -24($sp)
sw $v0, -8($sp)
# // Object is the return type
# t1 = "Object"
la $a0, str0
sw $a0, -4($sp)
# PopParam 1;
# t3 = "Int"
la $a0, str2
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _attempt_bool.225
lw $a0, -12($sp)
beqz $a0, _attempt_bool.225
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -28($sp)
# t2 = Call _wrapper.Int;
sw $ra, -24($sp)
addiu $sp, $sp, -28
jal _wrapper.Int
addiu $sp, $sp, 28
lw $ra, -24($sp)
sw $v0, -8($sp)
# PopParam 1;
# Goto _not_more_attempt.225
j _not_more_attempt.225
# _attempt_bool.225:


_attempt_bool.225:
li $t9, 0
# t3 = "Bool"
la $a0, str3
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _attempt_string.225
lw $a0, -12($sp)
beqz $a0, _attempt_string.225
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -28($sp)
# t2 = Call _wrapper.Bool;
sw $ra, -24($sp)
addiu $sp, $sp, -28
jal _wrapper.Bool
addiu $sp, $sp, 28
lw $ra, -24($sp)
sw $v0, -8($sp)
# PopParam 1;
# Goto _not_more_attempt.225
j _not_more_attempt.225
# _attempt_string.225:


_attempt_string.225:
li $t9, 0
# t3 = "String"
la $a0, str4
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _not_more_attempt.225
lw $a0, -12($sp)
beqz $a0, _not_more_attempt.225
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -28($sp)
# t2 = Call _wrapper.String;
sw $ra, -24($sp)
addiu $sp, $sp, -28
jal _wrapper.String
addiu $sp, $sp, 28
lw $ra, -24($sp)
sw $v0, -8($sp)
# PopParam 1;
# _not_more_attempt.225:


_not_more_attempt.225:
li $t9, 0
# Return t2;

lw $v0, -8($sp)
jr $ra
# Main.constructor:


Main.constructor:
li $t9, 0
# PARAM t0;
# PushParam t0;
lw $a0, 0($sp)
sw $a0, -8($sp)
# Call IO.constructor;
sw $ra, -4($sp)
addiu $sp, $sp, -8
jal IO.constructor
addiu $sp, $sp, 8
lw $ra, -4($sp)
# PopParam 1;
# // Method: Main.iota
# *(t0 + 10) = Label "Main.iota"
la $a0, Main.iota
lw $a1, 0($sp)
sw $a0, 40($a1)
# // Method: Main.main
# *(t0 + 11) = Label "Main.main"
la $a0, Main.main
lw $a1, 0($sp)
sw $a0, 44($a1)
# t2 = NULL;
sw $zero, -8($sp)
# // Attr: l
# *(t0 + 12) = t1
lw $a0, -4($sp)
lw $a1, 0($sp)
sw $a0, 48($a1)
# // Class Name: Main
# *(t0 + 0) = "Main"
la $a0, str5
lw $a1, 0($sp)
sw $a0, 0($a1)
# // Class Large: 13
# *(t0 + 1) = 13
lw $a0, 0($sp)
li $a1, 13
sw $a1, 4($a0)
# // Class Label
# *(t0 + 2) = Label "_class.Main"
la $a0, _class.Main
lw $a1, 0($sp)
sw $a0, 8($a1)
# Return ;

lw $v0, 4($sp)
jr $ra
# _class.List: _class.IO
# List.isNil:


List.isNil:
li $t9, 0
# PARAM t0;
# t1 = t0
lw $a0, 0($sp)
sw $a0, -4($sp)
# // Method: List.abort
# t2 = *(t1 + 3)
lw $a0, -4($sp)
lw $a1, 12($a0)
sw $a1, -8($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -16($sp)
# t1 = Call t2;
sw $ra, -12($sp)
lw $a0, -8($sp)
addiu $sp, $sp, -16
jalr $ra, $a0
addiu $sp, $sp, 16
lw $ra, -12($sp)
sw $v0, -4($sp)
# PopParam 1;
# t1 = 1
li $a0, 1
sw $a0, -4($sp)
# Return t1;

lw $v0, -4($sp)
jr $ra
# List.cons:


List.cons:
li $t9, 0
# PARAM t0;
# PARAM t1;
# t3 = Alloc 22;
# Begin Allocate
li $v0, 9
li $a0, 88
syscall
sw $v0, -12($sp)
# End Allocate
# PushParam t3;
lw $a0, -12($sp)
sw $a0, -36($sp)
# Call Cons.constructor;
sw $ra, -32($sp)
addiu $sp, $sp, -36
jal Cons.constructor
addiu $sp, $sp, 36
lw $ra, -32($sp)
# PopParam 1;
# // Var: new_cell
# t2 = t3
lw $a0, -12($sp)
sw $a0, -8($sp)
# // Var: hd
# t6 = t1
lw $a0, -4($sp)
sw $a0, -24($sp)
# t7 = t0
lw $a0, 0($sp)
sw $a0, -28($sp)
# // Method: Cons.init
# t5 = *(t2 + 19)
lw $a0, -8($sp)
lw $a1, 76($a0)
sw $a1, -20($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -36($sp)
# PushParam t6;
lw $a0, -24($sp)
sw $a0, -40($sp)
# PushParam t7;
lw $a0, -28($sp)
sw $a0, -44($sp)
# t2 = Call t5;
sw $ra, -32($sp)
lw $a0, -20($sp)
addiu $sp, $sp, -36
jalr $ra, $a0
addiu $sp, $sp, 36
lw $ra, -32($sp)
sw $v0, -8($sp)
# PopParam 3;
# Return t2;

lw $v0, -8($sp)
jr $ra
# List.car:


List.car:
li $t9, 0
# PARAM t0;
# t1 = t0
lw $a0, 0($sp)
sw $a0, -4($sp)
# // Method: List.abort
# t2 = *(t1 + 3)
lw $a0, -4($sp)
lw $a1, 12($a0)
sw $a1, -8($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -16($sp)
# t1 = Call t2;
sw $ra, -12($sp)
lw $a0, -8($sp)
addiu $sp, $sp, -16
jalr $ra, $a0
addiu $sp, $sp, 16
lw $ra, -12($sp)
sw $v0, -4($sp)
# PopParam 1;
# t1 = 0
li $a0, 0
sw $a0, -4($sp)
# Return t1;

lw $v0, -4($sp)
jr $ra
# List.cdr:


List.cdr:
li $t9, 0
# PARAM t0;
# t1 = t0
lw $a0, 0($sp)
sw $a0, -4($sp)
# // Method: List.abort
# t2 = *(t1 + 3)
lw $a0, -4($sp)
lw $a1, 12($a0)
sw $a1, -8($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -16($sp)
# t1 = Call t2;
sw $ra, -12($sp)
lw $a0, -8($sp)
addiu $sp, $sp, -16
jalr $ra, $a0
addiu $sp, $sp, 16
lw $ra, -12($sp)
sw $v0, -4($sp)
# PopParam 1;
# t1 = Alloc 19;
# Begin Allocate
li $v0, 9
li $a0, 76
syscall
sw $v0, -4($sp)
# End Allocate
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -16($sp)
# Call List.constructor;
sw $ra, -12($sp)
addiu $sp, $sp, -16
jal List.constructor
addiu $sp, $sp, 16
lw $ra, -12($sp)
# PopParam 1;
# Return t1;

lw $v0, -4($sp)
jr $ra
# List.rev:


List.rev:
li $t9, 0
# PARAM t0;
# t1 = t0
lw $a0, 0($sp)
sw $a0, -4($sp)
# // Method: List.cdr
# t2 = *(t1 + 13)
lw $a0, -4($sp)
lw $a1, 52($a0)
sw $a1, -8($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -16($sp)
# t1 = Call t2;
sw $ra, -12($sp)
lw $a0, -8($sp)
addiu $sp, $sp, -16
jalr $ra, $a0
addiu $sp, $sp, 16
lw $ra, -12($sp)
sw $v0, -4($sp)
# PopParam 1;
# Return t1;

lw $v0, -4($sp)
jr $ra
# List.sort:


List.sort:
li $t9, 0
# PARAM t0;
# t1 = t0
lw $a0, 0($sp)
sw $a0, -4($sp)
# // Method: List.cdr
# t2 = *(t1 + 13)
lw $a0, -4($sp)
lw $a1, 52($a0)
sw $a1, -8($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -16($sp)
# t1 = Call t2;
sw $ra, -12($sp)
lw $a0, -8($sp)
addiu $sp, $sp, -16
jalr $ra, $a0
addiu $sp, $sp, 16
lw $ra, -12($sp)
sw $v0, -4($sp)
# PopParam 1;
# Return t1;

lw $v0, -4($sp)
jr $ra
# List.insert:


List.insert:
li $t9, 0
# PARAM t0;
# PARAM t1;
# t2 = t0
lw $a0, 0($sp)
sw $a0, -8($sp)
# // Method: List.cdr
# t3 = *(t2 + 13)
lw $a0, -8($sp)
lw $a1, 52($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -20($sp)
# t2 = Call t3;
sw $ra, -16($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -20
jalr $ra, $a0
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -8($sp)
# PopParam 1;
# Return t2;

lw $v0, -8($sp)
jr $ra
# List.rcons:


List.rcons:
li $t9, 0
# PARAM t0;
# PARAM t1;
# t2 = t0
lw $a0, 0($sp)
sw $a0, -8($sp)
# // Method: List.cdr
# t3 = *(t2 + 13)
lw $a0, -8($sp)
lw $a1, 52($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -20($sp)
# t2 = Call t3;
sw $ra, -16($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -20
jalr $ra, $a0
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -8($sp)
# PopParam 1;
# Return t2;

lw $v0, -8($sp)
jr $ra
# List.print_list:


List.print_list:
li $t9, 0
# PARAM t0;
# t2 = t0
lw $a0, 0($sp)
sw $a0, -8($sp)
# // Method: List.abort
# t3 = *(t2 + 3)
lw $a0, -8($sp)
lw $a1, 12($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -20($sp)
# t2 = Call t3;
sw $ra, -16($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -20
jalr $ra, $a0
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -8($sp)
# // Object is the return type
# t1 = "Object"
la $a0, str0
sw $a0, -4($sp)
# PopParam 1;
# t3 = "Int"
la $a0, str2
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _attempt_bool.370
lw $a0, -12($sp)
beqz $a0, _attempt_bool.370
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -20($sp)
# t2 = Call _wrapper.Int;
sw $ra, -16($sp)
addiu $sp, $sp, -20
jal _wrapper.Int
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -8($sp)
# PopParam 1;
# Goto _not_more_attempt.370
j _not_more_attempt.370
# _attempt_bool.370:


_attempt_bool.370:
li $t9, 0
# t3 = "Bool"
la $a0, str3
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _attempt_string.370
lw $a0, -12($sp)
beqz $a0, _attempt_string.370
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -20($sp)
# t2 = Call _wrapper.Bool;
sw $ra, -16($sp)
addiu $sp, $sp, -20
jal _wrapper.Bool
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -8($sp)
# PopParam 1;
# Goto _not_more_attempt.370
j _not_more_attempt.370
# _attempt_string.370:


_attempt_string.370:
li $t9, 0
# t3 = "String"
la $a0, str4
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _not_more_attempt.370
lw $a0, -12($sp)
beqz $a0, _not_more_attempt.370
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -20($sp)
# t2 = Call _wrapper.String;
sw $ra, -16($sp)
addiu $sp, $sp, -20
jal _wrapper.String
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -8($sp)
# PopParam 1;
# _not_more_attempt.370:


_not_more_attempt.370:
li $t9, 0
# Return t2;

lw $v0, -8($sp)
jr $ra
# List.constructor:


List.constructor:
li $t9, 0
# PARAM t0;
# PushParam t0;
lw $a0, 0($sp)
sw $a0, -8($sp)
# Call IO.constructor;
sw $ra, -4($sp)
addiu $sp, $sp, -8
jal IO.constructor
addiu $sp, $sp, 8
lw $ra, -4($sp)
# PopParam 1;
# // Method: List.isNil
# *(t0 + 10) = Label "List.isNil"
la $a0, List.isNil
lw $a1, 0($sp)
sw $a0, 40($a1)
# // Method: List.cons
# *(t0 + 11) = Label "List.cons"
la $a0, List.cons
lw $a1, 0($sp)
sw $a0, 44($a1)
# // Method: List.car
# *(t0 + 12) = Label "List.car"
la $a0, List.car
lw $a1, 0($sp)
sw $a0, 48($a1)
# // Method: List.cdr
# *(t0 + 13) = Label "List.cdr"
la $a0, List.cdr
lw $a1, 0($sp)
sw $a0, 52($a1)
# // Method: List.rev
# *(t0 + 14) = Label "List.rev"
la $a0, List.rev
lw $a1, 0($sp)
sw $a0, 56($a1)
# // Method: List.sort
# *(t0 + 15) = Label "List.sort"
la $a0, List.sort
lw $a1, 0($sp)
sw $a0, 60($a1)
# // Method: List.insert
# *(t0 + 16) = Label "List.insert"
la $a0, List.insert
lw $a1, 0($sp)
sw $a0, 64($a1)
# // Method: List.rcons
# *(t0 + 17) = Label "List.rcons"
la $a0, List.rcons
lw $a1, 0($sp)
sw $a0, 68($a1)
# // Method: List.print_list
# *(t0 + 18) = Label "List.print_list"
la $a0, List.print_list
lw $a1, 0($sp)
sw $a0, 72($a1)
# // Class Name: List
# *(t0 + 0) = "List"
la $a0, str7
lw $a1, 0($sp)
sw $a0, 0($a1)
# // Class Large: 19
# *(t0 + 1) = 19
lw $a0, 0($sp)
li $a1, 19
sw $a1, 4($a0)
# // Class Label
# *(t0 + 2) = Label "_class.List"
la $a0, _class.List
lw $a1, 0($sp)
sw $a0, 8($a1)
# Return ;

lw $v0, 4($sp)
jr $ra
# _class.Nil: _class.List
# Nil.isNil:


Nil.isNil:
li $t9, 0
# PARAM t0;
# t1 = 1
li $a0, 1
sw $a0, -4($sp)
# Return t1;

lw $v0, -4($sp)
jr $ra
# Nil.rev:


Nil.rev:
li $t9, 0
# PARAM t0;
# t1 = t0
lw $a0, 0($sp)
sw $a0, -4($sp)
# Return t1;

lw $v0, -4($sp)
jr $ra
# Nil.sort:


Nil.sort:
li $t9, 0
# PARAM t0;
# t1 = t0
lw $a0, 0($sp)
sw $a0, -4($sp)
# Return t1;

lw $v0, -4($sp)
jr $ra
# Nil.insert:


Nil.insert:
li $t9, 0
# PARAM t0;
# PARAM t1;
# t2 = t0
lw $a0, 0($sp)
sw $a0, -8($sp)
# // Var: i
# t4 = t1
lw $a0, -4($sp)
sw $a0, -16($sp)
# // Method: Nil.rcons
# t3 = *(t2 + 17)
lw $a0, -8($sp)
lw $a1, 68($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -24($sp)
# PushParam t4;
lw $a0, -16($sp)
sw $a0, -28($sp)
# t2 = Call t3;
sw $ra, -20($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -24
jalr $ra, $a0
addiu $sp, $sp, 24
lw $ra, -20($sp)
sw $v0, -8($sp)
# PopParam 2;
# Return t2;

lw $v0, -8($sp)
jr $ra
# Nil.rcons:


Nil.rcons:
li $t9, 0
# PARAM t0;
# PARAM t1;
# t2 = Alloc 22;
# Begin Allocate
li $v0, 9
li $a0, 88
syscall
sw $v0, -8($sp)
# End Allocate
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -28($sp)
# Call Cons.constructor;
sw $ra, -24($sp)
addiu $sp, $sp, -28
jal Cons.constructor
addiu $sp, $sp, 28
lw $ra, -24($sp)
# PopParam 1;
# // Var: i
# t4 = t1
lw $a0, -4($sp)
sw $a0, -16($sp)
# t5 = t0
lw $a0, 0($sp)
sw $a0, -20($sp)
# // Method: Cons.init
# t3 = *(t2 + 19)
lw $a0, -8($sp)
lw $a1, 76($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -28($sp)
# PushParam t4;
lw $a0, -16($sp)
sw $a0, -32($sp)
# PushParam t5;
lw $a0, -20($sp)
sw $a0, -36($sp)
# t2 = Call t3;
sw $ra, -24($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -28
jalr $ra, $a0
addiu $sp, $sp, 28
lw $ra, -24($sp)
sw $v0, -8($sp)
# PopParam 3;
# Return t2;

lw $v0, -8($sp)
jr $ra
# Nil.print_list:


Nil.print_list:
li $t9, 0
# PARAM t0;
# t2 = 1
li $a0, 1
sw $a0, -8($sp)
# //  Bool is the return type
# t1 = "Bool"
la $a0, str3
sw $a0, -4($sp)
# t3 = "Int"
la $a0, str2
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _attempt_bool.473
lw $a0, -12($sp)
beqz $a0, _attempt_bool.473
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -20($sp)
# t2 = Call _wrapper.Int;
sw $ra, -16($sp)
addiu $sp, $sp, -20
jal _wrapper.Int
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -8($sp)
# PopParam 1;
# Goto _not_more_attempt.473
j _not_more_attempt.473
# _attempt_bool.473:


_attempt_bool.473:
li $t9, 0
# t3 = "Bool"
la $a0, str3
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _attempt_string.473
lw $a0, -12($sp)
beqz $a0, _attempt_string.473
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -20($sp)
# t2 = Call _wrapper.Bool;
sw $ra, -16($sp)
addiu $sp, $sp, -20
jal _wrapper.Bool
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -8($sp)
# PopParam 1;
# Goto _not_more_attempt.473
j _not_more_attempt.473
# _attempt_string.473:


_attempt_string.473:
li $t9, 0
# t3 = "String"
la $a0, str4
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _not_more_attempt.473
lw $a0, -12($sp)
beqz $a0, _not_more_attempt.473
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -20($sp)
# t2 = Call _wrapper.String;
sw $ra, -16($sp)
addiu $sp, $sp, -20
jal _wrapper.String
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -8($sp)
# PopParam 1;
# _not_more_attempt.473:


_not_more_attempt.473:
li $t9, 0
# Return t2;

lw $v0, -8($sp)
jr $ra
# Nil.constructor:


Nil.constructor:
li $t9, 0
# PARAM t0;
# PushParam t0;
lw $a0, 0($sp)
sw $a0, -8($sp)
# Call List.constructor;
sw $ra, -4($sp)
addiu $sp, $sp, -8
jal List.constructor
addiu $sp, $sp, 8
lw $ra, -4($sp)
# PopParam 1;
# // Method: Nil.isNil
# *(t0 + 10) = Label "Nil.isNil"
la $a0, Nil.isNil
lw $a1, 0($sp)
sw $a0, 40($a1)
# // Method: Nil.rev
# *(t0 + 14) = Label "Nil.rev"
la $a0, Nil.rev
lw $a1, 0($sp)
sw $a0, 56($a1)
# // Method: Nil.sort
# *(t0 + 15) = Label "Nil.sort"
la $a0, Nil.sort
lw $a1, 0($sp)
sw $a0, 60($a1)
# // Method: Nil.insert
# *(t0 + 16) = Label "Nil.insert"
la $a0, Nil.insert
lw $a1, 0($sp)
sw $a0, 64($a1)
# // Method: Nil.rcons
# *(t0 + 17) = Label "Nil.rcons"
la $a0, Nil.rcons
lw $a1, 0($sp)
sw $a0, 68($a1)
# // Method: Nil.print_list
# *(t0 + 18) = Label "Nil.print_list"
la $a0, Nil.print_list
lw $a1, 0($sp)
sw $a0, 72($a1)
# // Class Name: Nil
# *(t0 + 0) = "Nil"
la $a0, str8
lw $a1, 0($sp)
sw $a0, 0($a1)
# // Class Large: 19
# *(t0 + 1) = 19
lw $a0, 0($sp)
li $a1, 19
sw $a1, 4($a0)
# // Class Label
# *(t0 + 2) = Label "_class.Nil"
la $a0, _class.Nil
lw $a1, 0($sp)
sw $a0, 8($a1)
# Return ;

lw $v0, 4($sp)
jr $ra
# _class.Cons: _class.List
# Cons.isNil:


Cons.isNil:
li $t9, 0
# PARAM t0;
# t1 = 0
li $a0, 0
sw $a0, -4($sp)
# Return t1;

lw $v0, -4($sp)
jr $ra
# Cons.init:


Cons.init:
li $t9, 0
# PARAM t0;
# PARAM t1;
# PARAM t2;
# // Var: hd
# t3 = t1
lw $a0, -4($sp)
sw $a0, -12($sp)
# *(t0 + 20) = t3
lw $a0, -12($sp)
lw $a1, 0($sp)
sw $a0, 80($a1)
# // Var: tl
# t3 = t2
lw $a0, -8($sp)
sw $a0, -12($sp)
# *(t0 + 21) = t3
lw $a0, -12($sp)
lw $a1, 0($sp)
sw $a0, 84($a1)
# t3 = t0
lw $a0, 0($sp)
sw $a0, -12($sp)
# Return t3;

lw $v0, -12($sp)
jr $ra
# Cons.car:


Cons.car:
li $t9, 0
# PARAM t0;
# // Attr: Cons.xcar
# t1 = *(t0 + 20)
lw $a0, 0($sp)
lw $a1, 80($a0)
sw $a1, -4($sp)
# Return t1;

lw $v0, -4($sp)
jr $ra
# Cons.cdr:


Cons.cdr:
li $t9, 0
# PARAM t0;
# // Attr: Cons.xcdr
# t1 = *(t0 + 21)
lw $a0, 0($sp)
lw $a1, 84($a0)
sw $a1, -4($sp)
# Return t1;

lw $v0, -4($sp)
jr $ra
# Cons.rev:


Cons.rev:
li $t9, 0
# PARAM t0;
# // Attr: Cons.xcdr
# t1 = *(t0 + 21)
lw $a0, 0($sp)
lw $a1, 84($a0)
sw $a1, -4($sp)
# // Method: List.rev
# t2 = *(t1 + 14)
lw $a0, -4($sp)
lw $a1, 56($a0)
sw $a1, -8($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -20($sp)
# t1 = Call t2;
sw $ra, -16($sp)
lw $a0, -8($sp)
addiu $sp, $sp, -20
jalr $ra, $a0
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -4($sp)
# PopParam 1;
# // Attr: Cons.xcar
# t3 = *(t0 + 20)
lw $a0, 0($sp)
lw $a1, 80($a0)
sw $a1, -12($sp)
# // Method: List.rcons
# t2 = *(t1 + 17)
lw $a0, -4($sp)
lw $a1, 68($a0)
sw $a1, -8($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -20($sp)
# PushParam t3;
lw $a0, -12($sp)
sw $a0, -24($sp)
# t1 = Call t2;
sw $ra, -16($sp)
lw $a0, -8($sp)
addiu $sp, $sp, -20
jalr $ra, $a0
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -4($sp)
# PopParam 2;
# Return t1;

lw $v0, -4($sp)
jr $ra
# Cons.sort:


Cons.sort:
li $t9, 0
# PARAM t0;
# // Attr: Cons.xcdr
# t1 = *(t0 + 21)
lw $a0, 0($sp)
lw $a1, 84($a0)
sw $a1, -4($sp)
# // Method: List.sort
# t2 = *(t1 + 15)
lw $a0, -4($sp)
lw $a1, 60($a0)
sw $a1, -8($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -20($sp)
# t1 = Call t2;
sw $ra, -16($sp)
lw $a0, -8($sp)
addiu $sp, $sp, -20
jalr $ra, $a0
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -4($sp)
# PopParam 1;
# // Attr: Cons.xcar
# t3 = *(t0 + 20)
lw $a0, 0($sp)
lw $a1, 80($a0)
sw $a1, -12($sp)
# // Method: List.insert
# t2 = *(t1 + 16)
lw $a0, -4($sp)
lw $a1, 64($a0)
sw $a1, -8($sp)
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -20($sp)
# PushParam t3;
lw $a0, -12($sp)
sw $a0, -24($sp)
# t1 = Call t2;
sw $ra, -16($sp)
lw $a0, -8($sp)
addiu $sp, $sp, -20
jalr $ra, $a0
addiu $sp, $sp, 20
lw $ra, -16($sp)
sw $v0, -4($sp)
# PopParam 2;
# Return t1;

lw $v0, -4($sp)
jr $ra
# Cons.insert:


Cons.insert:
li $t9, 0
# PARAM t0;
# PARAM t1;
# // Var: i
# t3 = t1
lw $a0, -4($sp)
sw $a0, -12($sp)
# // Attr: Cons.xcar
# t4 = *(t0 + 20)
lw $a0, 0($sp)
lw $a1, 80($a0)
sw $a1, -16($sp)
# t2 = t3 < t4
lw $a0, -12($sp)
lw $a1, -16($sp)
sge $a0, $a0, $a1
li $a1, 1
sub $a0, $a1, $a0
sw $a0, -8($sp)
# IfZ t2 Goto _else.587
lw $a0, -8($sp)
beqz $a0, _else.587
# t2 = Alloc 22;
# Begin Allocate
li $v0, 9
li $a0, 88
syscall
sw $v0, -8($sp)
# End Allocate
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -36($sp)
# Call Cons.constructor;
sw $ra, -32($sp)
addiu $sp, $sp, -36
jal Cons.constructor
addiu $sp, $sp, 36
lw $ra, -32($sp)
# PopParam 1;
# // Var: i
# t4 = t1
lw $a0, -4($sp)
sw $a0, -16($sp)
# t5 = t0
lw $a0, 0($sp)
sw $a0, -20($sp)
# // Method: Cons.init
# t3 = *(t2 + 19)
lw $a0, -8($sp)
lw $a1, 76($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -36($sp)
# PushParam t4;
lw $a0, -16($sp)
sw $a0, -40($sp)
# PushParam t5;
lw $a0, -20($sp)
sw $a0, -44($sp)
# t2 = Call t3;
sw $ra, -32($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -36
jalr $ra, $a0
addiu $sp, $sp, 36
lw $ra, -32($sp)
sw $v0, -8($sp)
# PopParam 3;
# Goto _endif.587
j _endif.587
# _else.587:


_else.587:
li $t9, 0
# t2 = Alloc 22;
# Begin Allocate
li $v0, 9
li $a0, 88
syscall
sw $v0, -8($sp)
# End Allocate
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -36($sp)
# Call Cons.constructor;
sw $ra, -32($sp)
addiu $sp, $sp, -36
jal Cons.constructor
addiu $sp, $sp, 36
lw $ra, -32($sp)
# PopParam 1;
# // Attr: Cons.xcar
# t4 = *(t0 + 20)
lw $a0, 0($sp)
lw $a1, 80($a0)
sw $a1, -16($sp)
# // Attr: Cons.xcdr
# t5 = *(t0 + 21)
lw $a0, 0($sp)
lw $a1, 84($a0)
sw $a1, -20($sp)
# // Var: i
# t7 = t1
lw $a0, -4($sp)
sw $a0, -28($sp)
# // Method: List.insert
# t6 = *(t5 + 16)
lw $a0, -20($sp)
lw $a1, 64($a0)
sw $a1, -24($sp)
# PushParam t5;
lw $a0, -20($sp)
sw $a0, -36($sp)
# PushParam t7;
lw $a0, -28($sp)
sw $a0, -40($sp)
# t5 = Call t6;
sw $ra, -32($sp)
lw $a0, -24($sp)
addiu $sp, $sp, -36
jalr $ra, $a0
addiu $sp, $sp, 36
lw $ra, -32($sp)
sw $v0, -20($sp)
# PopParam 2;
# // Method: Cons.init
# t3 = *(t2 + 19)
lw $a0, -8($sp)
lw $a1, 76($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -36($sp)
# PushParam t4;
lw $a0, -16($sp)
sw $a0, -40($sp)
# PushParam t5;
lw $a0, -20($sp)
sw $a0, -44($sp)
# t2 = Call t3;
sw $ra, -32($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -36
jalr $ra, $a0
addiu $sp, $sp, 36
lw $ra, -32($sp)
sw $v0, -8($sp)
# PopParam 3;
# _endif.587:


_endif.587:
li $t9, 0
# Return t2;

lw $v0, -8($sp)
jr $ra
# Cons.rcons:


Cons.rcons:
li $t9, 0
# PARAM t0;
# PARAM t1;
# t2 = Alloc 22;
# Begin Allocate
li $v0, 9
li $a0, 88
syscall
sw $v0, -8($sp)
# End Allocate
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -36($sp)
# Call Cons.constructor;
sw $ra, -32($sp)
addiu $sp, $sp, -36
jal Cons.constructor
addiu $sp, $sp, 36
lw $ra, -32($sp)
# PopParam 1;
# // Attr: Cons.xcar
# t4 = *(t0 + 20)
lw $a0, 0($sp)
lw $a1, 80($a0)
sw $a1, -16($sp)
# // Attr: Cons.xcdr
# t5 = *(t0 + 21)
lw $a0, 0($sp)
lw $a1, 84($a0)
sw $a1, -20($sp)
# // Var: i
# t7 = t1
lw $a0, -4($sp)
sw $a0, -28($sp)
# // Method: List.rcons
# t6 = *(t5 + 17)
lw $a0, -20($sp)
lw $a1, 68($a0)
sw $a1, -24($sp)
# PushParam t5;
lw $a0, -20($sp)
sw $a0, -36($sp)
# PushParam t7;
lw $a0, -28($sp)
sw $a0, -40($sp)
# t5 = Call t6;
sw $ra, -32($sp)
lw $a0, -24($sp)
addiu $sp, $sp, -36
jalr $ra, $a0
addiu $sp, $sp, 36
lw $ra, -32($sp)
sw $v0, -20($sp)
# PopParam 2;
# // Method: Cons.init
# t3 = *(t2 + 19)
lw $a0, -8($sp)
lw $a1, 76($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -36($sp)
# PushParam t4;
lw $a0, -16($sp)
sw $a0, -40($sp)
# PushParam t5;
lw $a0, -20($sp)
sw $a0, -44($sp)
# t2 = Call t3;
sw $ra, -32($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -36
jalr $ra, $a0
addiu $sp, $sp, 36
lw $ra, -32($sp)
sw $v0, -8($sp)
# PopParam 3;
# Return t2;

lw $v0, -8($sp)
jr $ra
# Cons.print_list:


Cons.print_list:
li $t9, 0
# PARAM t0;
# t2 = t0
lw $a0, 0($sp)
sw $a0, -8($sp)
# // Attr: Cons.xcar
# t4 = *(t0 + 20)
lw $a0, 0($sp)
lw $a1, 80($a0)
sw $a1, -16($sp)
# // Object is the return type
# t1 = "Object"
la $a0, str0
sw $a0, -4($sp)
# // Method: Cons.out_int
# t3 = *(t2 + 7)
lw $a0, -8($sp)
lw $a1, 28($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -24($sp)
# PushParam t4;
lw $a0, -16($sp)
sw $a0, -28($sp)
# t2 = Call t3;
sw $ra, -20($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -24
jalr $ra, $a0
addiu $sp, $sp, 24
lw $ra, -20($sp)
sw $v0, -8($sp)
# // Object is the return type
# t1 = "Object"
la $a0, str0
sw $a0, -4($sp)
# PopParam 2;
# t2 = t0
lw $a0, 0($sp)
sw $a0, -8($sp)
# t4 = "\n"
la $a0, str10
sw $a0, -16($sp)
# //  String is the return type
# t1 = "String"
la $a0, str4
sw $a0, -4($sp)
# // Method: Cons.out_string
# t3 = *(t2 + 6)
lw $a0, -8($sp)
lw $a1, 24($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -24($sp)
# PushParam t4;
lw $a0, -16($sp)
sw $a0, -28($sp)
# t2 = Call t3;
sw $ra, -20($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -24
jalr $ra, $a0
addiu $sp, $sp, 24
lw $ra, -20($sp)
sw $v0, -8($sp)
# // Object is the return type
# t1 = "Object"
la $a0, str0
sw $a0, -4($sp)
# PopParam 2;
# // Attr: Cons.xcdr
# t2 = *(t0 + 21)
lw $a0, 0($sp)
lw $a1, 84($a0)
sw $a1, -8($sp)
# // Object is the return type
# t1 = "Object"
la $a0, str0
sw $a0, -4($sp)
# // Method: List.print_list
# t3 = *(t2 + 18)
lw $a0, -8($sp)
lw $a1, 72($a0)
sw $a1, -12($sp)
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -24($sp)
# t2 = Call t3;
sw $ra, -20($sp)
lw $a0, -12($sp)
addiu $sp, $sp, -24
jalr $ra, $a0
addiu $sp, $sp, 24
lw $ra, -20($sp)
sw $v0, -8($sp)
# // Object is the return type
# t1 = "Object"
la $a0, str0
sw $a0, -4($sp)
# PopParam 1;
# t3 = "Int"
la $a0, str2
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _attempt_bool.699
lw $a0, -12($sp)
beqz $a0, _attempt_bool.699
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -24($sp)
# t2 = Call _wrapper.Int;
sw $ra, -20($sp)
addiu $sp, $sp, -24
jal _wrapper.Int
addiu $sp, $sp, 24
lw $ra, -20($sp)
sw $v0, -8($sp)
# PopParam 1;
# Goto _not_more_attempt.699
j _not_more_attempt.699
# _attempt_bool.699:


_attempt_bool.699:
li $t9, 0
# t3 = "Bool"
la $a0, str3
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _attempt_string.699
lw $a0, -12($sp)
beqz $a0, _attempt_string.699
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -24($sp)
# t2 = Call _wrapper.Bool;
sw $ra, -20($sp)
addiu $sp, $sp, -24
jal _wrapper.Bool
addiu $sp, $sp, 24
lw $ra, -20($sp)
sw $v0, -8($sp)
# PopParam 1;
# Goto _not_more_attempt.699
j _not_more_attempt.699
# _attempt_string.699:


_attempt_string.699:
li $t9, 0
# t3 = "String"
la $a0, str4
sw $a0, -12($sp)
# t3 = t1 = t3
lw $a0, -4($sp)
lw $a1, -12($sp)
seq $a0, $a0, $a1
sw $a0, -12($sp)
# IfZ t3 Goto _not_more_attempt.699
lw $a0, -12($sp)
beqz $a0, _not_more_attempt.699
# PushParam t2;
lw $a0, -8($sp)
sw $a0, -24($sp)
# t2 = Call _wrapper.String;
sw $ra, -20($sp)
addiu $sp, $sp, -24
jal _wrapper.String
addiu $sp, $sp, 24
lw $ra, -20($sp)
sw $v0, -8($sp)
# PopParam 1;
# _not_more_attempt.699:


_not_more_attempt.699:
li $t9, 0
# Return t2;

lw $v0, -8($sp)
jr $ra
# Cons.constructor:


Cons.constructor:
li $t9, 0
# PARAM t0;
# PushParam t0;
lw $a0, 0($sp)
sw $a0, -16($sp)
# Call List.constructor;
sw $ra, -12($sp)
addiu $sp, $sp, -16
jal List.constructor
addiu $sp, $sp, 16
lw $ra, -12($sp)
# PopParam 1;
# // Method: Cons.isNil
# *(t0 + 10) = Label "Cons.isNil"
la $a0, Cons.isNil
lw $a1, 0($sp)
sw $a0, 40($a1)
# // Method: Cons.init
# *(t0 + 19) = Label "Cons.init"
la $a0, Cons.init
lw $a1, 0($sp)
sw $a0, 76($a1)
# // Method: Cons.car
# *(t0 + 12) = Label "Cons.car"
la $a0, Cons.car
lw $a1, 0($sp)
sw $a0, 48($a1)
# // Method: Cons.cdr
# *(t0 + 13) = Label "Cons.cdr"
la $a0, Cons.cdr
lw $a1, 0($sp)
sw $a0, 52($a1)
# // Method: Cons.rev
# *(t0 + 14) = Label "Cons.rev"
la $a0, Cons.rev
lw $a1, 0($sp)
sw $a0, 56($a1)
# // Method: Cons.sort
# *(t0 + 15) = Label "Cons.sort"
la $a0, Cons.sort
lw $a1, 0($sp)
sw $a0, 60($a1)
# // Method: Cons.insert
# *(t0 + 16) = Label "Cons.insert"
la $a0, Cons.insert
lw $a1, 0($sp)
sw $a0, 64($a1)
# // Method: Cons.rcons
# *(t0 + 17) = Label "Cons.rcons"
la $a0, Cons.rcons
lw $a1, 0($sp)
sw $a0, 68($a1)
# // Method: Cons.print_list
# *(t0 + 18) = Label "Cons.print_list"
la $a0, Cons.print_list
lw $a1, 0($sp)
sw $a0, 72($a1)
# t2 = 0
li $a0, 0
sw $a0, -8($sp)
# // Attr: xcar
# *(t0 + 20) = t1
lw $a0, -4($sp)
lw $a1, 0($sp)
sw $a0, 80($a1)
# t2 = NULL;
sw $zero, -8($sp)
# // Attr: xcdr
# *(t0 + 21) = t1
lw $a0, -4($sp)
lw $a1, 0($sp)
sw $a0, 84($a1)
# // Class Name: Cons
# *(t0 + 0) = "Cons"
la $a0, str9
lw $a1, 0($sp)
sw $a0, 0($a1)
# // Class Large: 22
# *(t0 + 1) = 22
lw $a0, 0($sp)
li $a1, 22
sw $a1, 4($a0)
# // Class Label
# *(t0 + 2) = Label "_class.Cons"
la $a0, _class.Cons
lw $a1, 0($sp)
sw $a0, 8($a1)
# Return ;

lw $v0, 4($sp)
jr $ra
# start:


start:
li $t9, 0
# t1 = Alloc 13;
# Begin Allocate
li $v0, 9
li $a0, 52
syscall
sw $v0, -4($sp)
# End Allocate
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -12($sp)
# Call Main.constructor;
sw $ra, -8($sp)
addiu $sp, $sp, -12
jal Main.constructor
addiu $sp, $sp, 12
lw $ra, -8($sp)
# PopParam 1;
# PushParam t1;
lw $a0, -4($sp)
sw $a0, -12($sp)
# Call Main.main;
sw $ra, -8($sp)
addiu $sp, $sp, -12
jal Main.main
addiu $sp, $sp, 12
lw $ra, -8($sp)
# PopParam 1;
li $v0, 10
syscall
