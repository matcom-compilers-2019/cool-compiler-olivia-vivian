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
_class.Board: _class.IOBoard.size_of_board:PARAM t0;PARAM t1;// Var: initialt2 = t1PushParam t2;t2 = Call String.length;PopParam 1;Return t2;
Board.board_init:PARAM t0;PARAM t1;t3 = t0// Var: startt5 = t1// Method: Board.size_of_boardt4 = *(t3 + 10)PushParam t3;PushParam t5;t3 = Call t4;PopParam 2;// Var: sizet5 = t3t6 = 15t2 = t5 = t6IfZ t2 Goto _else.152t2 = 3*(t0 + 12) = t2t2 = 5*(t0 + 13) = t2// Var: sizet2 = t3*(t0 + 14) = t2Goto _endif.152_else.152:// Var: sizet5 = t3t6 = 16t2 = t5 = t6IfZ t2 Goto _else.166t2 = 4*(t0 + 12) = t2t2 = 4*(t0 + 13) = t2// Var: sizet2 = t3*(t0 + 14) = t2Goto _endif.166_else.166:// Var: sizet5 = t3t6 = 20t2 = t5 = t6IfZ t2 Goto _else.180t2 = 4*(t0 + 12) = t2t2 = 5*(t0 + 13) = t2// Var: sizet2 = t3*(t0 + 14) = t2Goto _endif.180_else.180:// Var: sizet5 = t3t6 = 21t2 = t5 = t6IfZ t2 Goto _else.194t2 = 3*(t0 + 12) = t2t2 = 7*(t0 + 13) = t2// Var: sizet2 = t3*(t0 + 14) = t2Goto _endif.194_else.194:// Var: sizet5 = t3t6 = 25t2 = t5 = t6IfZ t2 Goto _else.208t2 = 5*(t0 + 12) = t2t2 = 5*(t0 + 13) = t2// Var: sizet2 = t3*(t0 + 14) = t2Goto _endif.208_else.208:// Var: sizet5 = t3t6 = 28t2 = t5 = t6IfZ t2 Goto _else.222t2 = 7*(t0 + 12) = t2t2 = 4*(t0 + 13) = t2// Var: sizet2 = t3*(t0 + 14) = t2Goto _endif.222_else.222:t2 = 5*(t0 + 12) = t2t2 = 5*(t0 + 13) = t2// Var: sizet2 = t3*(t0 + 14) = t2_endif.222:_endif.208:_endif.194:_endif.180:_endif.166:_endif.152:t2 = t0Return t2;
Board.constructor:PARAM t0;PushParam t0;Call IO.constructor;PopParam 1;// Method: Board.size_of_board*(t0 + 10) = Label "Board.size_of_board"// Method: Board.board_init*(t0 + 11) = Label "Board.board_init"t2 = 0// Attr: rows*(t0 + 12) = t1t2 = 0// Attr: columns*(t0 + 13) = t1t2 = 0// Attr: board_size*(t0 + 14) = t1// Class Name: Board*(t0 + 0) = "Board"// Class Large: 15*(t0 + 1) = 15// Class Label*(t0 + 2) = Label "_class.Board"Return ;
_class.CellularAutomaton: _class.BoardCellularAutomaton.init:PARAM t0;PARAM t1;// Var: mapt2 = t1*(t0 + 33) = t2t2 = t0// Var: mapt4 = t1// Method: CellularAutomaton.board_initt3 = *(t2 + 11)PushParam t2;PushParam t4;t2 = Call t3;PopParam 2;t2 = t0Return t2;
CellularAutomaton.print:PARAM t0;t2 = 0// Attr: CellularAutomaton.board_sizet4 = *(t0 + 14)t3 = t0t7 = "\n"// Method: CellularAutomaton.out_stringt6 = *(t3 + 6)PushParam t3;PushParam t7;t3 = Call t6;PopParam 2;_whilecondition.307:// Var: it6 = t2// Var: numt7 = t4t3 = t6 < t7IfZ t3 Goto _endwhile.307t3 = t0// Attr: CellularAutomaton.population_mapt7 = *(t0 + 33)// Var: it9 = t2// Attr: CellularAutomaton.columnst10 = *(t0 + 13)PushParam t7;PushParam t9;PushParam t10;t7 = Call String.substr;PopParam 3;// Method: CellularAutomaton.out_stringt6 = *(t3 + 6)PushParam t3;PushParam t7;t3 = Call t6;PopParam 2;t3 = t0t7 = "\n"// Method: CellularAutomaton.out_stringt6 = *(t3 + 6)PushParam t3;PushParam t7;t3 = Call t6;PopParam 2;// Var: it6 = t2// Attr: CellularAutomaton.columnst7 = *(t0 + 13)t3 = t6 + t7t2 = t3Goto _whilecondition.307_endwhile.307:t3 = t0t7 = "\n"// Method: CellularAutomaton.out_stringt6 = *(t3 + 6)PushParam t3;PushParam t7;t3 = Call t6;PopParam 2;t3 = t0Return t1;
CellularAutomaton.num_cells:PARAM t0;// Attr: CellularAutomaton.population_mapt1 = *(t0 + 33)PushParam t1;t1 = Call String.length;PopParam 1;Return t1;
CellularAutomaton.cell:PARAM t0;PARAM t1;// Attr: CellularAutomaton.board_sizet4 = *(t0 + 14)t5 = 1t3 = t4 - t5// Var: positiont4 = t1t2 = t3 < t4IfZ t2 Goto _else.369t2 = " "Goto _endif.369_else.369:// Attr: CellularAutomaton.population_mapt2 = *(t0 + 33)// Var: positiont4 = t1t5 = 1PushParam t2;PushParam t4;PushParam t5;t2 = Call String.substr;PopParam 3;_endif.369:Return t2;
CellularAutomaton.north:PARAM t0;PARAM t1;// Var: positiont4 = t1// Attr: CellularAutomaton.columnst5 = *(t0 + 13)t3 = t4 - t5t4 = 0t2 = t3 < t4IfZ t2 Goto _else.395t2 = " "Goto _endif.395_else.395:t2 = t0// Var: positiont5 = t1// Attr: CellularAutomaton.columnst6 = *(t0 + 13)t4 = t5 - t6// Method: CellularAutomaton.cellt3 = *(t2 + 18)PushParam t2;PushParam t4;t2 = Call t3;PopParam 2;_endif.395:Return t2;
CellularAutomaton.south:PARAM t0;PARAM t1;// Attr: CellularAutomaton.board_sizet3 = *(t0 + 14)// Var: positiont5 = t1// Attr: CellularAutomaton.columnst6 = *(t0 + 13)t4 = t5 + t6t2 = t3 < t4IfZ t2 Goto _else.423t2 = " "Goto _endif.423_else.423:t2 = t0// Var: positiont5 = t1// Attr: CellularAutomaton.columnst6 = *(t0 + 13)t4 = t5 + t6// Method: CellularAutomaton.cellt3 = *(t2 + 18)PushParam t2;PushParam t4;t2 = Call t3;PopParam 2;_endif.423:Return t2;
CellularAutomaton.east:PARAM t0;PARAM t1;// Var: positiont6 = t1t7 = 1t5 = t6 + t7// Attr: CellularAutomaton.columnst6 = *(t0 + 13)t4 = t5 / t6// Attr: CellularAutomaton.columnst5 = *(t0 + 13)t3 = t4 * t5// Var: positiont5 = t1t6 = 1t4 = t5 + t6t2 = t3 = t4IfZ t2 Goto _else.452t2 = " "Goto _endif.452_else.452:t2 = t0// Var: positiont5 = t1t6 = 1t4 = t5 + t6// Method: CellularAutomaton.cellt3 = *(t2 + 18)PushParam t2;PushParam t4;t2 = Call t3;PopParam 2;_endif.452:Return t2;
CellularAutomaton.west:PARAM t0;PARAM t1;// Var: positiont3 = t1t4 = 0t2 = t3 = t4IfZ t2 Goto _else.487t2 = " "Goto _endif.487_else.487:// Var: positiont5 = t1// Attr: CellularAutomaton.columnst6 = *(t0 + 13)t4 = t5 / t6// Attr: CellularAutomaton.columnst5 = *(t0 + 13)t3 = t4 * t5// Var: positiont4 = t1t2 = t3 = t4IfZ t2 Goto _else.495t2 = " "Goto _endif.495_else.495:t2 = t0// Var: positiont5 = t1t6 = 1t4 = t5 - t6// Method: CellularAutomaton.cellt3 = *(t2 + 18)PushParam t2;PushParam t4;t2 = Call t3;PopParam 2;_endif.495:_endif.487:Return t2;
CellularAutomaton.northwest:PARAM t0;PARAM t1;// Var: positiont4 = t1// Attr: CellularAutomaton.columnst5 = *(t0 + 13)t3 = t4 - t5t4 = 0t2 = t3 < t4IfZ t2 Goto _else.527t2 = " "Goto _endif.527_else.527:// Var: positiont5 = t1// Attr: CellularAutomaton.columnst6 = *(t0 + 13)t4 = t5 / t6// Attr: CellularAutomaton.columnst5 = *(t0 + 13)t3 = t4 * t5// Var: positiont4 = t1t2 = t3 = t4IfZ t2 Goto _else.538t2 = " "Goto _endif.538_else.538:t2 = t0// Var: positiont5 = t1t6 = 1t4 = t5 - t6// Method: CellularAutomaton.northt3 = *(t2 + 19)PushParam t2;PushParam t4;t2 = Call t3;PopParam 2;_endif.538:_endif.527:Return t2;
CellularAutomaton.northeast:PARAM t0;PARAM t1;// Var: positiont4 = t1// Attr: CellularAutomaton.columnst5 = *(t0 + 13)t3 = t4 - t5t4 = 0t2 = t3 < t4IfZ t2 Goto _else.570t2 = " "Goto _endif.570_else.570:// Var: positiont6 = t1t7 = 1t5 = t6 + t7// Attr: CellularAutomaton.columnst6 = *(t0 + 13)t4 = t5 / t6// Attr: CellularAutomaton.columnst5 = *(t0 + 13)t3 = t4 * t5// Var: positiont5 = t1t6 = 1t4 = t5 + t6t2 = t3 = t4IfZ t2 Goto _else.581t2 = " "Goto _endif.581_else.581:t2 = t0// Var: positiont5 = t1t6 = 1t4 = t5 + t6// Method: CellularAutomaton.northt3 = *(t2 + 19)PushParam t2;PushParam t4;t2 = Call t3;PopParam 2;_endif.581:_endif.570:Return t2;
CellularAutomaton.southeast:PARAM t0;PARAM t1;// Attr: CellularAutomaton.board_sizet3 = *(t0 + 14)// Var: positiont5 = t1// Attr: CellularAutomaton.columnst6 = *(t0 + 13)t4 = t5 + t6t2 = t3 < t4IfZ t2 Goto _else.617t2 = " "Goto _endif.617_else.617:// Var: positiont6 = t1t7 = 1t5 = t6 + t7// Attr: CellularAutomaton.columnst6 = *(t0 + 13)t4 = t5 / t6// Attr: CellularAutomaton.columnst5 = *(t0 + 13)t3 = t4 * t5// Var: positiont5 = t1t6 = 1t4 = t5 + t6t2 = t3 = t4IfZ t2 Goto _else.629t2 = " "Goto _endif.629_else.629:t2 = t0// Var: positiont5 = t1t6 = 1t4 = t5 + t6// Method: CellularAutomaton.southt3 = *(t2 + 20)PushParam t2;PushParam t4;t2 = Call t3;PopParam 2;_endif.629:_endif.617:Return t2;
CellularAutomaton.southwest:PARAM t0;PARAM t1;// Attr: CellularAutomaton.board_sizet3 = *(t0 + 14)// Var: positiont5 = t1// Attr: CellularAutomaton.columnst6 = *(t0 + 13)t4 = t5 + t6t2 = t3 < t4IfZ t2 Goto _else.665t2 = " "Goto _endif.665_else.665:// Var: positiont5 = t1// Attr: CellularAutomaton.columnst6 = *(t0 + 13)t4 = t5 / t6// Attr: CellularAutomaton.columnst5 = *(t0 + 13)t3 = t4 * t5// Var: positiont4 = t1t2 = t3 = t4IfZ t2 Goto _else.677t2 = " "Goto _endif.677_else.677:t2 = t0// Var: positiont5 = t1t6 = 1t4 = t5 - t6// Method: CellularAutomaton.southt3 = *(t2 + 20)PushParam t2;PushParam t4;t2 = Call t3;PopParam 2;_endif.677:_endif.665:Return t2;
CellularAutomaton.neighbors:PARAM t0;PARAM t1;t10 = t0// Var: positiont12 = t1// Method: CellularAutomaton.northt11 = *(t10 + 19)PushParam t10;PushParam t12;t10 = Call t11;PopParam 2;t11 = "X"t9 = t10 =:= t11IfZ t9 Goto _else.709t9 = 1Goto _endif.709_else.709:t9 = 0_endif.709:t11 = t0// Var: positiont13 = t1// Method: CellularAutomaton.southt12 = *(t11 + 20)PushParam t11;PushParam t13;t11 = Call t12;PopParam 2;t12 = "X"t10 = t11 =:= t12IfZ t10 Goto _else.726t10 = 1Goto _endif.726_else.726:t10 = 0_endif.726:t8 = t9 + t10t10 = t0// Var: positiont12 = t1// Method: CellularAutomaton.eastt11 = *(t10 + 21)PushParam t10;PushParam t12;t10 = Call t11;PopParam 2;t11 = "X"t9 = t10 =:= t11IfZ t9 Goto _else.744t9 = 1Goto _endif.744_else.744:t9 = 0_endif.744:t7 = t8 + t9t9 = t0// Var: positiont11 = t1// Method: CellularAutomaton.westt10 = *(t9 + 22)PushParam t9;PushParam t11;t9 = Call t10;PopParam 2;t10 = "X"t8 = t9 =:= t10IfZ t8 Goto _else.762t8 = 1Goto _endif.762_else.762:t8 = 0_endif.762:t6 = t7 + t8t8 = t0// Var: positiont10 = t1// Method: CellularAutomaton.northeastt9 = *(t8 + 24)PushParam t8;PushParam t10;t8 = Call t9;PopParam 2;t9 = "X"t7 = t8 =:= t9IfZ t7 Goto _else.780t7 = 1Goto _endif.780_else.780:t7 = 0_endif.780:t5 = t6 + t7t7 = t0// Var: positiont9 = t1// Method: CellularAutomaton.northwestt8 = *(t7 + 23)PushParam t7;PushParam t9;t7 = Call t8;PopParam 2;t8 = "X"t6 = t7 =:= t8IfZ t6 Goto _else.798t6 = 1Goto _endif.798_else.798:t6 = 0_endif.798:t4 = t5 + t6t6 = t0// Var: positiont8 = t1// Method: CellularAutomaton.southeastt7 = *(t6 + 25)PushParam t6;PushParam t8;t6 = Call t7;PopParam 2;t7 = "X"t5 = t6 =:= t7IfZ t5 Goto _else.816t5 = 1Goto _endif.816_else.816:t5 = 0_endif.816:t3 = t4 + t5t5 = t0// Var: positiont7 = t1// Method: CellularAutomaton.southwestt6 = *(t5 + 26)PushParam t5;PushParam t7;t5 = Call t6;PopParam 2;t6 = "X"t4 = t5 =:= t6IfZ t4 Goto _else.834t4 = 1Goto _endif.834_else.834:t4 = 0_endif.834:t2 = t3 + t4Return t2;
CellularAutomaton.cell_at_next_evolution:PARAM t0;PARAM t1;t3 = t0// Var: positiont5 = t1// Method: CellularAutomaton.neighborst4 = *(t3 + 27)PushParam t3;PushParam t5;t3 = Call t4;PopParam 2;t4 = 3t2 = t3 = t4IfZ t2 Goto _else.856t2 = "X"Goto _endif.856_else.856:t3 = t0// Var: positiont5 = t1// Method: CellularAutomaton.neighborst4 = *(t3 + 27)PushParam t3;PushParam t5;t3 = Call t4;PopParam 2;t4 = 2t2 = t3 = t4IfZ t2 Goto _else.871t3 = t0// Var: positiont5 = t1// Method: CellularAutomaton.cellt4 = *(t3 + 18)PushParam t3;PushParam t5;t3 = Call t4;PopParam 2;t4 = "X"t2 = t3 =:= t4IfZ t2 Goto _else.883t2 = "X"Goto _endif.883_else.883:t2 = "-"_endif.883:Goto _endif.871_else.871:t2 = "-"_endif.871:_endif.856:Return t2;
CellularAutomaton.evolve:PARAM t0;t2 = 0t4 = t0// Method: CellularAutomaton.num_cellst5 = *(t4 + 17)PushParam t4;t4 = Call t5;PopParam 1;t6 = ""_whilecondition.916:// Var: positiont8 = t2// Var: numt9 = t4t5 = t8 < t9IfZ t5 Goto _endwhile.916// Var: tempt5 = t6t9 = t0// Var: positiont11 = t2// Method: CellularAutomaton.cell_at_next_evolutiont10 = *(t9 + 28)PushParam t9;PushParam t11;t9 = Call t10;PopParam 2;PushParam t5;PushParam t9;t5 = Call String.concat;PopParam 2;t6 = t5// Var: positiont8 = t2t9 = 1t5 = t8 + t9t2 = t5Goto _whilecondition.916_endwhile.916:// Var: tempt5 = t6*(t0 + 33) = t5t5 = t0Return t1;
CellularAutomaton.option:PARAM t0;t2 = 0t1 = t0t5 = "\nPlease chose a number:\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t1: A cross\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t2: A slash from the upper left to lower right\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t3: A slash from the upper right to lower left\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t4: An X\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t5: A greater than sign \n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t6: A less than sign\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t7: Two greater than signs\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t8: Two less than signs\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t9: A 'V'\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t10: An inverse 'V'\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t11: Numbers 9 and 10 combined\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t12: A full grid\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t13: A 'T'\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t14: A plus '+'\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t15: A 'W'\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t16: An 'M'\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t17: An 'E'\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t18: A '3'\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t19: An 'O'\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t20: An '8'\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "\t21: An 'S'\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "Your choice => "// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0// Method: CellularAutomaton.in_intt4 = *(t1 + 9)PushParam t1;t1 = Call t4;PopParam 1;t2 = t1t1 = t0t5 = "\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;// Var: numt4 = t2t5 = 1t1 = t4 = t5IfZ t1 Goto _else.1153t1 = " XX  XXXX XXXX  XX  "Goto _endif.1153_else.1153:// Var: numt4 = t2t5 = 2t1 = t4 = t5IfZ t1 Goto _else.1161t1 = "    X   X   X   X   X    "Goto _endif.1161_else.1161:// Var: numt4 = t2t5 = 3t1 = t4 = t5IfZ t1 Goto _else.1169t1 = "X     X     X     X     X"Goto _endif.1169_else.1169:// Var: numt4 = t2t5 = 4t1 = t4 = t5IfZ t1 Goto _else.1177t1 = "X   X X X   X   X X X   X"Goto _endif.1177_else.1177:// Var: numt4 = t2t5 = 5t1 = t4 = t5IfZ t1 Goto _else.1185t1 = "X     X     X   X   X    "Goto _endif.1185_else.1185:// Var: numt4 = t2t5 = 6t1 = t4 = t5IfZ t1 Goto _else.1193t1 = "    X   X   X     X     X"Goto _endif.1193_else.1193:// Var: numt4 = t2t5 = 7t1 = t4 = t5IfZ t1 Goto _else.1201t1 = "X  X  X  XX  X      "Goto _endif.1201_else.1201:// Var: numt4 = t2t5 = 8t1 = t4 = t5IfZ t1 Goto _else.1209t1 = " X  XX  X  X  X     "Goto _endif.1209_else.1209:// Var: numt4 = t2t5 = 9t1 = t4 = t5IfZ t1 Goto _else.1217t1 = "X   X X X   X  "Goto _endif.1217_else.1217:// Var: numt4 = t2t5 = 10t1 = t4 = t5IfZ t1 Goto _else.1225t1 = "  X   X X X   X"Goto _endif.1225_else.1225:// Var: numt4 = t2t5 = 11t1 = t4 = t5IfZ t1 Goto _else.1233t1 = "X X X X X X X X"Goto _endif.1233_else.1233:// Var: numt4 = t2t5 = 12t1 = t4 = t5IfZ t1 Goto _else.1241t1 = "XXXXXXXXXXXXXXXXXXXXXXXXX"Goto _endif.1241_else.1241:// Var: numt4 = t2t5 = 13t1 = t4 = t5IfZ t1 Goto _else.1249t1 = "XXXXX  X    X    X    X  "Goto _endif.1249_else.1249:// Var: numt4 = t2t5 = 14t1 = t4 = t5IfZ t1 Goto _else.1257t1 = "  X    X  XXXXX  X    X  "Goto _endif.1257_else.1257:// Var: numt4 = t2t5 = 15t1 = t4 = t5IfZ t1 Goto _else.1265t1 = "X     X X X X   X X  "Goto _endif.1265_else.1265:// Var: numt4 = t2t5 = 16t1 = t4 = t5IfZ t1 Goto _else.1273t1 = "  X X   X X X X     X"Goto _endif.1273_else.1273:// Var: numt4 = t2t5 = 17t1 = t4 = t5IfZ t1 Goto _else.1281t1 = "XXXXX   X   XXXXX   X   XXXX"Goto _endif.1281_else.1281:// Var: numt4 = t2t5 = 18t1 = t4 = t5IfZ t1 Goto _else.1289t1 = "XXX    X   X  X    X   XXXX "Goto _endif.1289_else.1289:// Var: numt4 = t2t5 = 19t1 = t4 = t5IfZ t1 Goto _else.1297t1 = " XX X  XX  X XX "Goto _endif.1297_else.1297:// Var: numt4 = t2t5 = 20t1 = t4 = t5IfZ t1 Goto _else.1305t1 = " XX X  XX  X XX X  XX  X XX "Goto _endif.1305_else.1305:// Var: numt4 = t2t5 = 21t1 = t4 = t5IfZ t1 Goto _else.1313t1 = " XXXX   X    XX    X   XXXX "Goto _endif.1313_else.1313:t1 = "                         "_endif.1313:_endif.1305:_endif.1297:_endif.1289:_endif.1281:_endif.1273:_endif.1265:_endif.1257:_endif.1249:_endif.1241:_endif.1233:_endif.1225:_endif.1217:_endif.1209:_endif.1201:_endif.1193:_endif.1185:_endif.1177:_endif.1169:_endif.1161:_endif.1153:Return t1;
CellularAutomaton.prompt:PARAM t0;t2 = ""t1 = t0t5 = "Would you like to continue with the next generation? \n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "Please use lowercase y or n for your answer [y]: "// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0// Method: CellularAutomaton.in_stringt4 = *(t1 + 8)PushParam t1;t1 = Call t4;PopParam 1;t2 = t1t1 = t0t5 = "\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;// Var: anst4 = t2t5 = "n"t1 = t4 =:= t5IfZ t1 Goto _else.1378t1 = 0Goto _endif.1378_else.1378:t1 = 1_endif.1378:Return t1;
CellularAutomaton.prompt2:PARAM t0;t2 = ""t1 = t0t5 = "\n\n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "Would you like to choose a background pattern? \n"// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0t5 = "Please use lowercase y or n for your answer [n]: "// Method: CellularAutomaton.out_stringt4 = *(t1 + 6)PushParam t1;PushParam t5;t1 = Call t4;PopParam 2;t1 = t0// Method: CellularAutomaton.in_stringt4 = *(t1 + 8)PushParam t1;t1 = Call t4;PopParam 1;t2 = t1// Var: anst4 = t2t5 = "y"t1 = t4 =:= t5IfZ t1 Goto _else.1423t1 = 1Goto _endif.1423_else.1423:t1 = 0_endif.1423:Return t1;
CellularAutomaton.constructor:PARAM t0;PushParam t0;Call Board.constructor;PopParam 1;// Method: CellularAutomaton.init*(t0 + 15) = Label "CellularAutomaton.init"// Method: CellularAutomaton.print*(t0 + 16) = Label "CellularAutomaton.print"// Method: CellularAutomaton.num_cells*(t0 + 17) = Label "CellularAutomaton.num_cells"// Method: CellularAutomaton.cell*(t0 + 18) = Label "CellularAutomaton.cell"// Method: CellularAutomaton.north*(t0 + 19) = Label "CellularAutomaton.north"// Method: CellularAutomaton.south*(t0 + 20) = Label "CellularAutomaton.south"// Method: CellularAutomaton.east*(t0 + 21) = Label "CellularAutomaton.east"// Method: CellularAutomaton.west*(t0 + 22) = Label "CellularAutomaton.west"// Method: CellularAutomaton.northwest*(t0 + 23) = Label "CellularAutomaton.northwest"// Method: CellularAutomaton.northeast*(t0 + 24) = Label "CellularAutomaton.northeast"// Method: CellularAutomaton.southeast*(t0 + 25) = Label "CellularAutomaton.southeast"// Method: CellularAutomaton.southwest*(t0 + 26) = Label "CellularAutomaton.southwest"// Method: CellularAutomaton.neighbors*(t0 + 27) = Label "CellularAutomaton.neighbors"// Method: CellularAutomaton.cell_at_next_evolution*(t0 + 28) = Label "CellularAutomaton.cell_at_next_evolution"// Method: CellularAutomaton.evolve*(t0 + 29) = Label "CellularAutomaton.evolve"// Method: CellularAutomaton.option*(t0 + 30) = Label "CellularAutomaton.option"// Method: CellularAutomaton.prompt*(t0 + 31) = Label "CellularAutomaton.prompt"// Method: CellularAutomaton.prompt2*(t0 + 32) = Label "CellularAutomaton.prompt2"t1 = ""// Attr: population_map*(t0 + 33) = t1// Class Name: CellularAutomaton*(t0 + 0) = "CellularAutomaton"// Class Large: 34*(t0 + 1) = 34// Class Label*(t0 + 2) = Label "_class.CellularAutomaton"Return ;
_class.Main: _class.CellularAutomatonMain.main:PARAM t0;t2 = 0t4 = ""t3 = t0t7 = "Welcome to the Game of Life.\n"// Method: Main.out_stringt6 = *(t3 + 6)PushParam t3;PushParam t7;t3 = Call t6;PopParam 2;t3 = t0t7 = "There are many initial states to choose from. \n"// Method: Main.out_stringt6 = *(t3 + 6)PushParam t3;PushParam t7;t3 = Call t6;PopParam 2;_whilecondition.1506:t3 = t0// Method: Main.prompt2t6 = *(t3 + 32)PushParam t3;t3 = Call t6;PopParam 1;IfZ t3 Goto _endwhile.1506t3 = 1t2 = t3t3 = t0// Method: Main.optiont6 = *(t3 + 30)PushParam t3;t3 = Call t6;PopParam 1;t4 = t3t3 = Alloc 34;PushParam t3;Call CellularAutomaton.constructor;PopParam 1;// Var: choicet7 = t4// Method: CellularAutomaton.initt6 = *(t3 + 15)PushParam t3;PushParam t7;t3 = Call t6;PopParam 2;*(t0 + 35) = t3// Attr: Main.cellst3 = *(t0 + 35)// Method: CellularAutomaton.printt6 = *(t3 + 16)PushParam t3;t3 = Call t6;PopParam 1;_whilecondition.1543:// Var: continuet3 = t2IfZ t3 Goto _endwhile.1543t3 = t0// Method: Main.promptt6 = *(t3 + 31)PushParam t3;t3 = Call t6;PopParam 1;IfZ t3 Goto _else.1547// Attr: Main.cellst3 = *(t0 + 35)// Method: CellularAutomaton.evolvet6 = *(t3 + 29)PushParam t3;t3 = Call t6;PopParam 1;// Attr: Main.cellst3 = *(t0 + 35)// Method: CellularAutomaton.printt6 = *(t3 + 16)PushParam t3;t3 = Call t6;PopParam 1;Goto _endif.1547_else.1547:t3 = 0t2 = t3_endif.1547:Goto _whilecondition.1543_endwhile.1543:Goto _whilecondition.1506_endwhile.1506:t3 = t0Return t1;
Main.constructor:PARAM t0;PushParam t0;Call CellularAutomaton.constructor;PopParam 1;// Method: Main.main*(t0 + 34) = Label "Main.main"t1 = NULL;// Attr: cells*(t0 + 35) = t1// Class Name: Main*(t0 + 0) = "Main"// Class Large: 36*(t0 + 1) = 36// Class Label*(t0 + 2) = Label "_class.Main"Return ;
start:t1 = Alloc 36;PushParam t1;Call Main.constructor;PopParam 1;PushParam t1;Call Main.main;PopParam 1;