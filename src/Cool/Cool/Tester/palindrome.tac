Call start;Object.constructor:PARAM t0;// Method: Object.abort*(t0 + 3) = Label "Object.abort"// Method: Object.type_name*(t0 + 4) = Label "Object.type_name"// Method: Object.copy*(t0 + 5) = Label "Object.copy"// Class Name is Object*(t0 + 0) = "Object"// Class Large: 6*(t0 + 1) = 6Return ;
IO.constructor:PARAM t0;PushParam t0;Call Object.constructor;PopParam 1;// Method: IO.out_string*(t0 + 6) = Label "IO.out_string"// Method: IO.out_int*(t0 + 7) = Label "IO.out_int"// Method: IO.in_string*(t0 + 8) = Label "IO.in_string"// Method: IO.in_int*(t0 + 9) = Label "IO.in_int"// Class name is Object*(t0 + 0) = "IO"// Class large: 10*(t0 + 1) = 10// Class Label*(t0 + 2) = Label "_class.IO"Return ;
_class.IO: _class.Object_class.Int: _class.Object_class.Bool: _class.Object_class.String: _class.Object_wrapper.Int:PARAM t0;t1 = Alloc 7;PushParam t1;Call Object.constructor;PopParam 1;*(t1 + 0) = "Int"*(t1 + 6) = t0*(t1 + 2) = Label "_class.Int"Return t1;
_wrapper.Bool:PARAM t0;t1 = Alloc 7;PushParam t1;Call Object.constructor;PopParam 1;*(t1 + 0) = "Bool"*(t1 + 6) = t0*(t1 + 2) = Label "_class.Bool"Return t1;
_wrapper.String:PARAM t0;t1 = Alloc 10;PushParam t1;Call Object.constructor;PopParam 1;*(t1 + 0) = "String"*(t1 + 9) = t0*(t1 + 2) = Label "_class.String"Return t1;
Object.abort:Goto _abortObject.type_name:PARAM t0;t0 = *(t0 + 0)Return t0;
Object.copy:PARAM t0;t1 = *(t0 + 1)t2 = 4t1 = t1 * t2PushParam t0;PushParam t1;t0 = Call _copy;PopParam 2;Return t0;
IO.out_string:PARAM t0;PARAM t1;PushParam t1;t0 = Call _out_string;PopParam 1;Return t0;
IO.out_int:PARAM t0;PARAM t1;PushParam t1;t0 = Call _out_int;PopParam 1;Return t0;
IO.in_string:PARAM t0;t0 = Call _in_string;Return t0;
IO.in_int:PARAM t0;t0 = Call _in_int;Return t0;
String.length:PARAM t0;PushParam t0;t0 = Call _stringlength;PopParam 1;Return t0;
String.concat:PARAM t0;PARAM t1;PushParam t0;PushParam t1;t0 = Call _stringconcat;PopParam 2;Return t0;
String.substr:PARAM t0;PARAM t1;PARAM t2;PushParam t0;PushParam t1;PushParam t2;t0 = Call _stringsubstr;PopParam 3;Return t0;
_class.Main: _class.IOMain.pal:PARAM t0;PARAM t1;// Var: st3 = t1PushParam t3;t3 = Call String.length;PopParam 1;t4 = 0t2 = t3 = t4IfZ t2 Goto _else.134t2 = 1Goto _endif.134_else.134:// Var: st3 = t1PushParam t3;t3 = Call String.length;PopParam 1;t4 = 1t2 = t3 = t4IfZ t2 Goto _else.145t2 = 1Goto _endif.145_else.145:// Var: st3 = t1t5 = 0t6 = 1PushParam t3;PushParam t5;PushParam t6;t3 = Call String.substr;PopParam 3;// Var: st4 = t1// Var: st7 = t1PushParam t7;t7 = Call String.length;PopParam 1;t8 = 1t6 = t7 - t8t7 = 1PushParam t4;PushParam t6;PushParam t7;t4 = Call String.substr;PopParam 3;t2 = t3 =:= t4IfZ t2 Goto _else.156t2 = t0// Var: st4 = t1t6 = 1// Var: st8 = t1PushParam t8;t8 = Call String.length;PopParam 1;t9 = 2t7 = t8 - t9PushParam t4;PushParam t6;PushParam t7;t4 = Call String.substr;PopParam 3;// Method: Main.palt3 = *(t2 + 10)PushParam t2;PushParam t4;t2 = Call t3;PopParam 2;Goto _endif.156_else.156:t2 = 0_endif.156:_endif.145:_endif.134:Return t2;
Main.main:PARAM t0;t2 = 1t1 = ~ t2*(t0 + 12) = t1t1 = t0t4 = "enter a string\n"// Method: Main.out_stringt3 = *(t1 + 6)PushParam t1;PushParam t4;t1 = Call t3;PopParam 2;t1 = t0t4 = t0// Method: Main.in_stringt5 = *(t4 + 8)PushParam t4;t4 = Call t5;PopParam 1;// Method: Main.palt3 = *(t1 + 10)PushParam t1;PushParam t4;t1 = Call t3;PopParam 2;IfZ t1 Goto _else.224t1 = t0t4 = "that was a palindrome\n"// Method: Main.out_stringt3 = *(t1 + 6)PushParam t1;PushParam t4;t1 = Call t3;PopParam 2;Goto _endif.224_else.224:t1 = t0t4 = "that was not a palindrome\n"// Method: Main.out_stringt3 = *(t1 + 6)PushParam t1;PushParam t4;t1 = Call t3;PopParam 2;_endif.224:Return t1;
Main.constructor:PARAM t0;PushParam t0;Call IO.constructor;PopParam 1;// Method: Main.pal*(t0 + 10) = Label "Main.pal"// Method: Main.main*(t0 + 11) = Label "Main.main"t1 = 0// Attr: i*(t0 + 12) = t1// Class Name: Main*(t0 + 0) = "Main"// Class Large: 13*(t0 + 1) = 13// Class Label*(t0 + 2) = Label "_class.Main"Return ;
start:t1 = Alloc 13;PushParam t1;Call Main.constructor;PopParam 1;PushParam t1;Call Main.main;PopParam 1;