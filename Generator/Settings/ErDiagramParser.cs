// Base On: https://github.com/zaach/jison/blob/master/ports/csharp/Jison/Jison/Template.cs

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;


namespace MermaidParser.ErDiagram
{
	public class ErDiagramParser
	{
		public ParserSymbols Symbols;
		public Dictionary<int, ParserSymbol> Terminals;
		public Dictionary<int, ParserProduction> Productions;
		public Dictionary<int, ParserState> Table;
		public Dictionary<int, ParserAction> DefaultActions;
		public string Version = "0.4.2";
		public bool Debug = false;

		public const int None = 0;
		public const int Shift = 1;
		public const int Reduce = 2;
		public const int Accept = 3;

		public void Trace()
		{

		}

		public ErDiagramParser()
		{
			//Setup Parser
			
			var symbol0 = new ParserSymbol("accept", 0);
			var symbol1 = new ParserSymbol("end", 1);
			var symbol2 = new ParserSymbol("error", 2);
			var symbol3 = new ParserSymbol("start", 3);
			var symbol4 = new ParserSymbol("ER_DIAGRAM", 4);
			var symbol5 = new ParserSymbol("document", 5);
			var symbol6 = new ParserSymbol("EOF", 6);
			var symbol7 = new ParserSymbol("line", 7);
			var symbol8 = new ParserSymbol("SPACE", 8);
			var symbol9 = new ParserSymbol("statement", 9);
			var symbol10 = new ParserSymbol("NEWLINE", 10);
			var symbol11 = new ParserSymbol("entityName", 11);
			var symbol12 = new ParserSymbol("relSpec", 12);
			var symbol13 = new ParserSymbol(":", 13);
			var symbol14 = new ParserSymbol("role", 14);
			var symbol15 = new ParserSymbol("BLOCK_START", 15);
			var symbol16 = new ParserSymbol("attributes", 16);
			var symbol17 = new ParserSymbol("BLOCK_STOP", 17);
			var symbol18 = new ParserSymbol("SQS", 18);
			var symbol19 = new ParserSymbol("SQE", 19);
			var symbol20 = new ParserSymbol("title", 20);
			var symbol21 = new ParserSymbol("title_value", 21);
			var symbol22 = new ParserSymbol("acc_title", 22);
			var symbol23 = new ParserSymbol("acc_title_value", 23);
			var symbol24 = new ParserSymbol("acc_descr", 24);
			var symbol25 = new ParserSymbol("acc_descr_value", 25);
			var symbol26 = new ParserSymbol("acc_descr_multiline_value", 26);
			var symbol27 = new ParserSymbol("ALPHANUM", 27);
			var symbol28 = new ParserSymbol("ENTITY_NAME", 28);
			var symbol29 = new ParserSymbol("attribute", 29);
			var symbol30 = new ParserSymbol("attributeType", 30);
			var symbol31 = new ParserSymbol("attributeName", 31);
			var symbol32 = new ParserSymbol("attributeKeyTypeList", 32);
			var symbol33 = new ParserSymbol("attributeComment", 33);
			var symbol34 = new ParserSymbol("ATTRIBUTE_WORD", 34);
			var symbol35 = new ParserSymbol("attributeKeyType", 35);
			var symbol36 = new ParserSymbol("COMMA", 36);
			var symbol37 = new ParserSymbol("ATTRIBUTE_KEY", 37);
			var symbol38 = new ParserSymbol("COMMENT", 38);
			var symbol39 = new ParserSymbol("cardinality", 39);
			var symbol40 = new ParserSymbol("relType", 40);
			var symbol41 = new ParserSymbol("ZERO_OR_ONE", 41);
			var symbol42 = new ParserSymbol("ZERO_OR_MORE", 42);
			var symbol43 = new ParserSymbol("ONE_OR_MORE", 43);
			var symbol44 = new ParserSymbol("ONLY_ONE", 44);
			var symbol45 = new ParserSymbol("MD_PARENT", 45);
			var symbol46 = new ParserSymbol("NON_IDENTIFYING", 46);
			var symbol47 = new ParserSymbol("IDENTIFYING", 47);
			var symbol48 = new ParserSymbol("WORD", 48);


			Symbols = new ParserSymbols();
			Symbols.Add(symbol0);
			Symbols.Add(symbol1);
			Symbols.Add(symbol2);
			Symbols.Add(symbol3);
			Symbols.Add(symbol4);
			Symbols.Add(symbol5);
			Symbols.Add(symbol6);
			Symbols.Add(symbol7);
			Symbols.Add(symbol8);
			Symbols.Add(symbol9);
			Symbols.Add(symbol10);
			Symbols.Add(symbol11);
			Symbols.Add(symbol12);
			Symbols.Add(symbol13);
			Symbols.Add(symbol14);
			Symbols.Add(symbol15);
			Symbols.Add(symbol16);
			Symbols.Add(symbol17);
			Symbols.Add(symbol18);
			Symbols.Add(symbol19);
			Symbols.Add(symbol20);
			Symbols.Add(symbol21);
			Symbols.Add(symbol22);
			Symbols.Add(symbol23);
			Symbols.Add(symbol24);
			Symbols.Add(symbol25);
			Symbols.Add(symbol26);
			Symbols.Add(symbol27);
			Symbols.Add(symbol28);
			Symbols.Add(symbol29);
			Symbols.Add(symbol30);
			Symbols.Add(symbol31);
			Symbols.Add(symbol32);
			Symbols.Add(symbol33);
			Symbols.Add(symbol34);
			Symbols.Add(symbol35);
			Symbols.Add(symbol36);
			Symbols.Add(symbol37);
			Symbols.Add(symbol38);
			Symbols.Add(symbol39);
			Symbols.Add(symbol40);
			Symbols.Add(symbol41);
			Symbols.Add(symbol42);
			Symbols.Add(symbol43);
			Symbols.Add(symbol44);
			Symbols.Add(symbol45);
			Symbols.Add(symbol46);
			Symbols.Add(symbol47);
			Symbols.Add(symbol48);

			Terminals = new Dictionary<int, ParserSymbol>
				{
					{2, symbol2},
					{4, symbol4},
					{6, symbol6},
					{8, symbol8},
					{10, symbol10},
					{13, symbol13},
					{15, symbol15},
					{17, symbol17},
					{18, symbol18},
					{19, symbol19},
					{20, symbol20},
					{21, symbol21},
					{22, symbol22},
					{23, symbol23},
					{24, symbol24},
					{25, symbol25},
					{26, symbol26},
					{27, symbol27},
					{28, symbol28},
					{34, symbol34},
					{36, symbol36},
					{37, symbol37},
					{38, symbol38},
					{41, symbol41},
					{42, symbol42},
					{43, symbol43},
					{44, symbol44},
					{45, symbol45},
					{46, symbol46},
					{47, symbol47},
					{48, symbol48}
				};

			var table0 = new ParserState(0);
			var table1 = new ParserState(1);
			var table2 = new ParserState(2);
			var table3 = new ParserState(3);
			var table4 = new ParserState(4);
			var table5 = new ParserState(5);
			var table6 = new ParserState(6);
			var table7 = new ParserState(7);
			var table8 = new ParserState(8);
			var table9 = new ParserState(9);
			var table10 = new ParserState(10);
			var table11 = new ParserState(11);
			var table12 = new ParserState(12);
			var table13 = new ParserState(13);
			var table14 = new ParserState(14);
			var table15 = new ParserState(15);
			var table16 = new ParserState(16);
			var table17 = new ParserState(17);
			var table18 = new ParserState(18);
			var table19 = new ParserState(19);
			var table20 = new ParserState(20);
			var table21 = new ParserState(21);
			var table22 = new ParserState(22);
			var table23 = new ParserState(23);
			var table24 = new ParserState(24);
			var table25 = new ParserState(25);
			var table26 = new ParserState(26);
			var table27 = new ParserState(27);
			var table28 = new ParserState(28);
			var table29 = new ParserState(29);
			var table30 = new ParserState(30);
			var table31 = new ParserState(31);
			var table32 = new ParserState(32);
			var table33 = new ParserState(33);
			var table34 = new ParserState(34);
			var table35 = new ParserState(35);
			var table36 = new ParserState(36);
			var table37 = new ParserState(37);
			var table38 = new ParserState(38);
			var table39 = new ParserState(39);
			var table40 = new ParserState(40);
			var table41 = new ParserState(41);
			var table42 = new ParserState(42);
			var table43 = new ParserState(43);
			var table44 = new ParserState(44);
			var table45 = new ParserState(45);
			var table46 = new ParserState(46);
			var table47 = new ParserState(47);
			var table48 = new ParserState(48);
			var table49 = new ParserState(49);
			var table50 = new ParserState(50);
			var table51 = new ParserState(51);
			var table52 = new ParserState(52);
			var table53 = new ParserState(53);
			var table54 = new ParserState(54);
			var table55 = new ParserState(55);
			var table56 = new ParserState(56);
			var table57 = new ParserState(57);
			var table58 = new ParserState(58);
			var table59 = new ParserState(59);
			var table60 = new ParserState(60);
			var table61 = new ParserState(61);

			var tableDefinition0 = new Dictionary<int, ParserAction>
				{
					{3, new ParserAction(None, ref table1)},
					{4, new ParserAction(Shift, ref table2)}
				};

			var tableDefinition1 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Accept)}
				};

			var tableDefinition2 = new Dictionary<int, ParserAction>
				{
					{5, new ParserAction(None, ref table3)},
					{6, new ParserAction(Reduce, ref table2)},
					{8, new ParserAction(Reduce, ref table2)},
					{10, new ParserAction(Reduce, ref table2)},
					{20, new ParserAction(Reduce, ref table2)},
					{22, new ParserAction(Reduce, ref table2)},
					{24, new ParserAction(Reduce, ref table2)},
					{26, new ParserAction(Reduce, ref table2)},
					{27, new ParserAction(Reduce, ref table2)},
					{28, new ParserAction(Reduce, ref table2)}
				};

			var tableDefinition3 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Shift, ref table4)},
					{7, new ParserAction(None, ref table5)},
					{8, new ParserAction(Shift, ref table6)},
					{9, new ParserAction(None, ref table7)},
					{10, new ParserAction(Shift, ref table8)},
					{11, new ParserAction(None, ref table9)},
					{20, new ParserAction(Shift, ref table10)},
					{22, new ParserAction(Shift, ref table11)},
					{24, new ParserAction(Shift, ref table12)},
					{26, new ParserAction(Shift, ref table13)},
					{27, new ParserAction(Shift, ref table14)},
					{28, new ParserAction(Shift, ref table15)}
				};

			var tableDefinition4 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table1)},
					{6, new ParserAction(Reduce, ref table7)},
					{8, new ParserAction(Reduce, ref table7)},
					{10, new ParserAction(Reduce, ref table7)},
					{20, new ParserAction(Reduce, ref table7)},
					{22, new ParserAction(Reduce, ref table7)},
					{24, new ParserAction(Reduce, ref table7)},
					{26, new ParserAction(Reduce, ref table7)},
					{27, new ParserAction(Reduce, ref table7)},
					{28, new ParserAction(Reduce, ref table7)}
				};

			var tableDefinition5 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table3)},
					{8, new ParserAction(Reduce, ref table3)},
					{10, new ParserAction(Reduce, ref table3)},
					{20, new ParserAction(Reduce, ref table3)},
					{22, new ParserAction(Reduce, ref table3)},
					{24, new ParserAction(Reduce, ref table3)},
					{26, new ParserAction(Reduce, ref table3)},
					{27, new ParserAction(Reduce, ref table3)},
					{28, new ParserAction(Reduce, ref table3)}
				};

			var tableDefinition6 = new Dictionary<int, ParserAction>
				{
					{9, new ParserAction(None, ref table16)},
					{11, new ParserAction(None, ref table9)},
					{20, new ParserAction(Shift, ref table10)},
					{22, new ParserAction(Shift, ref table11)},
					{24, new ParserAction(Shift, ref table12)},
					{26, new ParserAction(Shift, ref table13)},
					{27, new ParserAction(Shift, ref table14)},
					{28, new ParserAction(Shift, ref table15)}
				};

			var tableDefinition7 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table5)},
					{8, new ParserAction(Reduce, ref table5)},
					{10, new ParserAction(Reduce, ref table5)},
					{20, new ParserAction(Reduce, ref table5)},
					{22, new ParserAction(Reduce, ref table5)},
					{24, new ParserAction(Reduce, ref table5)},
					{26, new ParserAction(Reduce, ref table5)},
					{27, new ParserAction(Reduce, ref table5)},
					{28, new ParserAction(Reduce, ref table5)}
				};

			var tableDefinition8 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table6)},
					{8, new ParserAction(Reduce, ref table6)},
					{10, new ParserAction(Reduce, ref table6)},
					{20, new ParserAction(Reduce, ref table6)},
					{22, new ParserAction(Reduce, ref table6)},
					{24, new ParserAction(Reduce, ref table6)},
					{26, new ParserAction(Reduce, ref table6)},
					{27, new ParserAction(Reduce, ref table6)},
					{28, new ParserAction(Reduce, ref table6)}
				};

			var tableDefinition9 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table11)},
					{8, new ParserAction(Reduce, ref table11)},
					{10, new ParserAction(Reduce, ref table11)},
					{12, new ParserAction(None, ref table17)},
					{15, new ParserAction(Shift, ref table18)},
					{18, new ParserAction(Shift, ref table19)},
					{20, new ParserAction(Reduce, ref table11)},
					{22, new ParserAction(Reduce, ref table11)},
					{24, new ParserAction(Reduce, ref table11)},
					{26, new ParserAction(Reduce, ref table11)},
					{27, new ParserAction(Reduce, ref table11)},
					{28, new ParserAction(Reduce, ref table11)},
					{39, new ParserAction(None, ref table20)},
					{41, new ParserAction(Shift, ref table21)},
					{42, new ParserAction(Shift, ref table22)},
					{43, new ParserAction(Shift, ref table23)},
					{44, new ParserAction(Shift, ref table24)},
					{45, new ParserAction(Shift, ref table25)}
				};

			var tableDefinition10 = new Dictionary<int, ParserAction>
				{
					{21, new ParserAction(Shift, ref table26)}
				};

			var tableDefinition11 = new Dictionary<int, ParserAction>
				{
					{23, new ParserAction(Shift, ref table27)}
				};

			var tableDefinition12 = new Dictionary<int, ParserAction>
				{
					{25, new ParserAction(Shift, ref table28)}
				};

			var tableDefinition13 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table18)},
					{8, new ParserAction(Reduce, ref table18)},
					{10, new ParserAction(Reduce, ref table18)},
					{20, new ParserAction(Reduce, ref table18)},
					{22, new ParserAction(Reduce, ref table18)},
					{24, new ParserAction(Reduce, ref table18)},
					{26, new ParserAction(Reduce, ref table18)},
					{27, new ParserAction(Reduce, ref table18)},
					{28, new ParserAction(Reduce, ref table18)}
				};

			var tableDefinition14 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table19)},
					{8, new ParserAction(Reduce, ref table19)},
					{10, new ParserAction(Reduce, ref table19)},
					{13, new ParserAction(Reduce, ref table19)},
					{15, new ParserAction(Reduce, ref table19)},
					{18, new ParserAction(Reduce, ref table19)},
					{19, new ParserAction(Reduce, ref table19)},
					{20, new ParserAction(Reduce, ref table19)},
					{22, new ParserAction(Reduce, ref table19)},
					{24, new ParserAction(Reduce, ref table19)},
					{26, new ParserAction(Reduce, ref table19)},
					{27, new ParserAction(Reduce, ref table19)},
					{28, new ParserAction(Reduce, ref table19)},
					{41, new ParserAction(Reduce, ref table19)},
					{42, new ParserAction(Reduce, ref table19)},
					{43, new ParserAction(Reduce, ref table19)},
					{44, new ParserAction(Reduce, ref table19)},
					{45, new ParserAction(Reduce, ref table19)}
				};

			var tableDefinition15 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table20)},
					{8, new ParserAction(Reduce, ref table20)},
					{10, new ParserAction(Reduce, ref table20)},
					{13, new ParserAction(Reduce, ref table20)},
					{15, new ParserAction(Reduce, ref table20)},
					{18, new ParserAction(Reduce, ref table20)},
					{19, new ParserAction(Reduce, ref table20)},
					{20, new ParserAction(Reduce, ref table20)},
					{22, new ParserAction(Reduce, ref table20)},
					{24, new ParserAction(Reduce, ref table20)},
					{26, new ParserAction(Reduce, ref table20)},
					{27, new ParserAction(Reduce, ref table20)},
					{28, new ParserAction(Reduce, ref table20)},
					{41, new ParserAction(Reduce, ref table20)},
					{42, new ParserAction(Reduce, ref table20)},
					{43, new ParserAction(Reduce, ref table20)},
					{44, new ParserAction(Reduce, ref table20)},
					{45, new ParserAction(Reduce, ref table20)}
				};

			var tableDefinition16 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table4)},
					{8, new ParserAction(Reduce, ref table4)},
					{10, new ParserAction(Reduce, ref table4)},
					{20, new ParserAction(Reduce, ref table4)},
					{22, new ParserAction(Reduce, ref table4)},
					{24, new ParserAction(Reduce, ref table4)},
					{26, new ParserAction(Reduce, ref table4)},
					{27, new ParserAction(Reduce, ref table4)},
					{28, new ParserAction(Reduce, ref table4)}
				};

			var tableDefinition17 = new Dictionary<int, ParserAction>
				{
					{11, new ParserAction(None, ref table29)},
					{27, new ParserAction(Shift, ref table14)},
					{28, new ParserAction(Shift, ref table15)}
				};

			var tableDefinition18 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table30)},
					{17, new ParserAction(Shift, ref table31)},
					{29, new ParserAction(None, ref table32)},
					{30, new ParserAction(None, ref table33)},
					{34, new ParserAction(Shift, ref table34)}
				};

			var tableDefinition19 = new Dictionary<int, ParserAction>
				{
					{11, new ParserAction(None, ref table35)},
					{27, new ParserAction(Shift, ref table14)},
					{28, new ParserAction(Shift, ref table15)}
				};

			var tableDefinition20 = new Dictionary<int, ParserAction>
				{
					{40, new ParserAction(None, ref table36)},
					{46, new ParserAction(Shift, ref table37)},
					{47, new ParserAction(Shift, ref table38)}
				};

			var tableDefinition21 = new Dictionary<int, ParserAction>
				{
					{27, new ParserAction(Reduce, ref table34)},
					{28, new ParserAction(Reduce, ref table34)},
					{46, new ParserAction(Reduce, ref table34)},
					{47, new ParserAction(Reduce, ref table34)}
				};

			var tableDefinition22 = new Dictionary<int, ParserAction>
				{
					{27, new ParserAction(Reduce, ref table35)},
					{28, new ParserAction(Reduce, ref table35)},
					{46, new ParserAction(Reduce, ref table35)},
					{47, new ParserAction(Reduce, ref table35)}
				};

			var tableDefinition23 = new Dictionary<int, ParserAction>
				{
					{27, new ParserAction(Reduce, ref table36)},
					{28, new ParserAction(Reduce, ref table36)},
					{46, new ParserAction(Reduce, ref table36)},
					{47, new ParserAction(Reduce, ref table36)}
				};

			var tableDefinition24 = new Dictionary<int, ParserAction>
				{
					{27, new ParserAction(Reduce, ref table37)},
					{28, new ParserAction(Reduce, ref table37)},
					{46, new ParserAction(Reduce, ref table37)},
					{47, new ParserAction(Reduce, ref table37)}
				};

			var tableDefinition25 = new Dictionary<int, ParserAction>
				{
					{27, new ParserAction(Reduce, ref table38)},
					{28, new ParserAction(Reduce, ref table38)},
					{46, new ParserAction(Reduce, ref table38)},
					{47, new ParserAction(Reduce, ref table38)}
				};

			var tableDefinition26 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table15)},
					{8, new ParserAction(Reduce, ref table15)},
					{10, new ParserAction(Reduce, ref table15)},
					{20, new ParserAction(Reduce, ref table15)},
					{22, new ParserAction(Reduce, ref table15)},
					{24, new ParserAction(Reduce, ref table15)},
					{26, new ParserAction(Reduce, ref table15)},
					{27, new ParserAction(Reduce, ref table15)},
					{28, new ParserAction(Reduce, ref table15)}
				};

			var tableDefinition27 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table16)},
					{8, new ParserAction(Reduce, ref table16)},
					{10, new ParserAction(Reduce, ref table16)},
					{20, new ParserAction(Reduce, ref table16)},
					{22, new ParserAction(Reduce, ref table16)},
					{24, new ParserAction(Reduce, ref table16)},
					{26, new ParserAction(Reduce, ref table16)},
					{27, new ParserAction(Reduce, ref table16)},
					{28, new ParserAction(Reduce, ref table16)}
				};

			var tableDefinition28 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table17)},
					{8, new ParserAction(Reduce, ref table17)},
					{10, new ParserAction(Reduce, ref table17)},
					{20, new ParserAction(Reduce, ref table17)},
					{22, new ParserAction(Reduce, ref table17)},
					{24, new ParserAction(Reduce, ref table17)},
					{26, new ParserAction(Reduce, ref table17)},
					{27, new ParserAction(Reduce, ref table17)},
					{28, new ParserAction(Reduce, ref table17)}
				};

			var tableDefinition29 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Shift, ref table39)}
				};

			var tableDefinition30 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Shift, ref table40)}
				};

			var tableDefinition31 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table10)},
					{8, new ParserAction(Reduce, ref table10)},
					{10, new ParserAction(Reduce, ref table10)},
					{20, new ParserAction(Reduce, ref table10)},
					{22, new ParserAction(Reduce, ref table10)},
					{24, new ParserAction(Reduce, ref table10)},
					{26, new ParserAction(Reduce, ref table10)},
					{27, new ParserAction(Reduce, ref table10)},
					{28, new ParserAction(Reduce, ref table10)}
				};

			var tableDefinition32 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table41)},
					{17, new ParserAction(Reduce, ref table21)},
					{29, new ParserAction(None, ref table32)},
					{30, new ParserAction(None, ref table33)},
					{34, new ParserAction(Shift, ref table34)}
				};

			var tableDefinition33 = new Dictionary<int, ParserAction>
				{
					{31, new ParserAction(None, ref table42)},
					{34, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition34 = new Dictionary<int, ParserAction>
				{
					{34, new ParserAction(Reduce, ref table27)}
				};

			var tableDefinition35 = new Dictionary<int, ParserAction>
				{
					{19, new ParserAction(Shift, ref table44)}
				};

			var tableDefinition36 = new Dictionary<int, ParserAction>
				{
					{39, new ParserAction(None, ref table45)},
					{41, new ParserAction(Shift, ref table21)},
					{42, new ParserAction(Shift, ref table22)},
					{43, new ParserAction(Shift, ref table23)},
					{44, new ParserAction(Shift, ref table24)},
					{45, new ParserAction(Shift, ref table25)}
				};

			var tableDefinition37 = new Dictionary<int, ParserAction>
				{
					{41, new ParserAction(Reduce, ref table39)},
					{42, new ParserAction(Reduce, ref table39)},
					{43, new ParserAction(Reduce, ref table39)},
					{44, new ParserAction(Reduce, ref table39)},
					{45, new ParserAction(Reduce, ref table39)}
				};

			var tableDefinition38 = new Dictionary<int, ParserAction>
				{
					{41, new ParserAction(Reduce, ref table40)},
					{42, new ParserAction(Reduce, ref table40)},
					{43, new ParserAction(Reduce, ref table40)},
					{44, new ParserAction(Reduce, ref table40)},
					{45, new ParserAction(Reduce, ref table40)}
				};

			var tableDefinition39 = new Dictionary<int, ParserAction>
				{
					{14, new ParserAction(None, ref table46)},
					{27, new ParserAction(Shift, ref table49)},
					{28, new ParserAction(Shift, ref table48)},
					{48, new ParserAction(Shift, ref table47)}
				};

			var tableDefinition40 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table9)},
					{8, new ParserAction(Reduce, ref table9)},
					{10, new ParserAction(Reduce, ref table9)},
					{20, new ParserAction(Reduce, ref table9)},
					{22, new ParserAction(Reduce, ref table9)},
					{24, new ParserAction(Reduce, ref table9)},
					{26, new ParserAction(Reduce, ref table9)},
					{27, new ParserAction(Reduce, ref table9)},
					{28, new ParserAction(Reduce, ref table9)}
				};

			var tableDefinition41 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Reduce, ref table22)}
				};

			var tableDefinition42 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Reduce, ref table23)},
					{32, new ParserAction(None, ref table50)},
					{33, new ParserAction(None, ref table51)},
					{34, new ParserAction(Reduce, ref table23)},
					{35, new ParserAction(None, ref table52)},
					{37, new ParserAction(Shift, ref table54)},
					{38, new ParserAction(Shift, ref table53)}
				};

			var tableDefinition43 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Reduce, ref table28)},
					{34, new ParserAction(Reduce, ref table28)},
					{37, new ParserAction(Reduce, ref table28)},
					{38, new ParserAction(Reduce, ref table28)}
				};

			var tableDefinition44 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table14)},
					{8, new ParserAction(Reduce, ref table14)},
					{10, new ParserAction(Reduce, ref table14)},
					{15, new ParserAction(Shift, ref table55)},
					{20, new ParserAction(Reduce, ref table14)},
					{22, new ParserAction(Reduce, ref table14)},
					{24, new ParserAction(Reduce, ref table14)},
					{26, new ParserAction(Reduce, ref table14)},
					{27, new ParserAction(Reduce, ref table14)},
					{28, new ParserAction(Reduce, ref table14)}
				};

			var tableDefinition45 = new Dictionary<int, ParserAction>
				{
					{27, new ParserAction(Reduce, ref table33)},
					{28, new ParserAction(Reduce, ref table33)}
				};

			var tableDefinition46 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table8)},
					{8, new ParserAction(Reduce, ref table8)},
					{10, new ParserAction(Reduce, ref table8)},
					{20, new ParserAction(Reduce, ref table8)},
					{22, new ParserAction(Reduce, ref table8)},
					{24, new ParserAction(Reduce, ref table8)},
					{26, new ParserAction(Reduce, ref table8)},
					{27, new ParserAction(Reduce, ref table8)},
					{28, new ParserAction(Reduce, ref table8)}
				};

			var tableDefinition47 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table41)},
					{8, new ParserAction(Reduce, ref table41)},
					{10, new ParserAction(Reduce, ref table41)},
					{20, new ParserAction(Reduce, ref table41)},
					{22, new ParserAction(Reduce, ref table41)},
					{24, new ParserAction(Reduce, ref table41)},
					{26, new ParserAction(Reduce, ref table41)},
					{27, new ParserAction(Reduce, ref table41)},
					{28, new ParserAction(Reduce, ref table41)}
				};

			var tableDefinition48 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table42)},
					{8, new ParserAction(Reduce, ref table42)},
					{10, new ParserAction(Reduce, ref table42)},
					{20, new ParserAction(Reduce, ref table42)},
					{22, new ParserAction(Reduce, ref table42)},
					{24, new ParserAction(Reduce, ref table42)},
					{26, new ParserAction(Reduce, ref table42)},
					{27, new ParserAction(Reduce, ref table42)},
					{28, new ParserAction(Reduce, ref table42)}
				};

			var tableDefinition49 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table43)},
					{8, new ParserAction(Reduce, ref table43)},
					{10, new ParserAction(Reduce, ref table43)},
					{20, new ParserAction(Reduce, ref table43)},
					{22, new ParserAction(Reduce, ref table43)},
					{24, new ParserAction(Reduce, ref table43)},
					{26, new ParserAction(Reduce, ref table43)},
					{27, new ParserAction(Reduce, ref table43)},
					{28, new ParserAction(Reduce, ref table43)}
				};

			var tableDefinition50 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Reduce, ref table24)},
					{33, new ParserAction(None, ref table56)},
					{34, new ParserAction(Reduce, ref table24)},
					{36, new ParserAction(Shift, ref table57)},
					{38, new ParserAction(Shift, ref table53)}
				};

			var tableDefinition51 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Reduce, ref table25)},
					{34, new ParserAction(Reduce, ref table25)}
				};

			var tableDefinition52 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Reduce, ref table29)},
					{34, new ParserAction(Reduce, ref table29)},
					{36, new ParserAction(Reduce, ref table29)},
					{38, new ParserAction(Reduce, ref table29)}
				};

			var tableDefinition53 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Reduce, ref table32)},
					{34, new ParserAction(Reduce, ref table32)}
				};

			var tableDefinition54 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Reduce, ref table31)},
					{34, new ParserAction(Reduce, ref table31)},
					{36, new ParserAction(Reduce, ref table31)},
					{38, new ParserAction(Reduce, ref table31)}
				};

			var tableDefinition55 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table58)},
					{17, new ParserAction(Shift, ref table59)},
					{29, new ParserAction(None, ref table32)},
					{30, new ParserAction(None, ref table33)},
					{34, new ParserAction(Shift, ref table34)}
				};

			var tableDefinition56 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Reduce, ref table26)},
					{34, new ParserAction(Reduce, ref table26)}
				};

			var tableDefinition57 = new Dictionary<int, ParserAction>
				{
					{35, new ParserAction(None, ref table60)},
					{37, new ParserAction(Shift, ref table54)}
				};

			var tableDefinition58 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Shift, ref table61)}
				};

			var tableDefinition59 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table13)},
					{8, new ParserAction(Reduce, ref table13)},
					{10, new ParserAction(Reduce, ref table13)},
					{20, new ParserAction(Reduce, ref table13)},
					{22, new ParserAction(Reduce, ref table13)},
					{24, new ParserAction(Reduce, ref table13)},
					{26, new ParserAction(Reduce, ref table13)},
					{27, new ParserAction(Reduce, ref table13)},
					{28, new ParserAction(Reduce, ref table13)}
				};

			var tableDefinition60 = new Dictionary<int, ParserAction>
				{
					{17, new ParserAction(Reduce, ref table30)},
					{34, new ParserAction(Reduce, ref table30)},
					{36, new ParserAction(Reduce, ref table30)},
					{38, new ParserAction(Reduce, ref table30)}
				};

			var tableDefinition61 = new Dictionary<int, ParserAction>
				{
					{6, new ParserAction(Reduce, ref table12)},
					{8, new ParserAction(Reduce, ref table12)},
					{10, new ParserAction(Reduce, ref table12)},
					{20, new ParserAction(Reduce, ref table12)},
					{22, new ParserAction(Reduce, ref table12)},
					{24, new ParserAction(Reduce, ref table12)},
					{26, new ParserAction(Reduce, ref table12)},
					{27, new ParserAction(Reduce, ref table12)},
					{28, new ParserAction(Reduce, ref table12)}
				};

			table0.SetActions(ref tableDefinition0);
			table1.SetActions(ref tableDefinition1);
			table2.SetActions(ref tableDefinition2);
			table3.SetActions(ref tableDefinition3);
			table4.SetActions(ref tableDefinition4);
			table5.SetActions(ref tableDefinition5);
			table6.SetActions(ref tableDefinition6);
			table7.SetActions(ref tableDefinition7);
			table8.SetActions(ref tableDefinition8);
			table9.SetActions(ref tableDefinition9);
			table10.SetActions(ref tableDefinition10);
			table11.SetActions(ref tableDefinition11);
			table12.SetActions(ref tableDefinition12);
			table13.SetActions(ref tableDefinition13);
			table14.SetActions(ref tableDefinition14);
			table15.SetActions(ref tableDefinition15);
			table16.SetActions(ref tableDefinition16);
			table17.SetActions(ref tableDefinition17);
			table18.SetActions(ref tableDefinition18);
			table19.SetActions(ref tableDefinition19);
			table20.SetActions(ref tableDefinition20);
			table21.SetActions(ref tableDefinition21);
			table22.SetActions(ref tableDefinition22);
			table23.SetActions(ref tableDefinition23);
			table24.SetActions(ref tableDefinition24);
			table25.SetActions(ref tableDefinition25);
			table26.SetActions(ref tableDefinition26);
			table27.SetActions(ref tableDefinition27);
			table28.SetActions(ref tableDefinition28);
			table29.SetActions(ref tableDefinition29);
			table30.SetActions(ref tableDefinition30);
			table31.SetActions(ref tableDefinition31);
			table32.SetActions(ref tableDefinition32);
			table33.SetActions(ref tableDefinition33);
			table34.SetActions(ref tableDefinition34);
			table35.SetActions(ref tableDefinition35);
			table36.SetActions(ref tableDefinition36);
			table37.SetActions(ref tableDefinition37);
			table38.SetActions(ref tableDefinition38);
			table39.SetActions(ref tableDefinition39);
			table40.SetActions(ref tableDefinition40);
			table41.SetActions(ref tableDefinition41);
			table42.SetActions(ref tableDefinition42);
			table43.SetActions(ref tableDefinition43);
			table44.SetActions(ref tableDefinition44);
			table45.SetActions(ref tableDefinition45);
			table46.SetActions(ref tableDefinition46);
			table47.SetActions(ref tableDefinition47);
			table48.SetActions(ref tableDefinition48);
			table49.SetActions(ref tableDefinition49);
			table50.SetActions(ref tableDefinition50);
			table51.SetActions(ref tableDefinition51);
			table52.SetActions(ref tableDefinition52);
			table53.SetActions(ref tableDefinition53);
			table54.SetActions(ref tableDefinition54);
			table55.SetActions(ref tableDefinition55);
			table56.SetActions(ref tableDefinition56);
			table57.SetActions(ref tableDefinition57);
			table58.SetActions(ref tableDefinition58);
			table59.SetActions(ref tableDefinition59);
			table60.SetActions(ref tableDefinition60);
			table61.SetActions(ref tableDefinition61);

			Table = new Dictionary<int, ParserState>
				{
					{0, table0},
					{1, table1},
					{2, table2},
					{3, table3},
					{4, table4},
					{5, table5},
					{6, table6},
					{7, table7},
					{8, table8},
					{9, table9},
					{10, table10},
					{11, table11},
					{12, table12},
					{13, table13},
					{14, table14},
					{15, table15},
					{16, table16},
					{17, table17},
					{18, table18},
					{19, table19},
					{20, table20},
					{21, table21},
					{22, table22},
					{23, table23},
					{24, table24},
					{25, table25},
					{26, table26},
					{27, table27},
					{28, table28},
					{29, table29},
					{30, table30},
					{31, table31},
					{32, table32},
					{33, table33},
					{34, table34},
					{35, table35},
					{36, table36},
					{37, table37},
					{38, table38},
					{39, table39},
					{40, table40},
					{41, table41},
					{42, table42},
					{43, table43},
					{44, table44},
					{45, table45},
					{46, table46},
					{47, table47},
					{48, table48},
					{49, table49},
					{50, table50},
					{51, table51},
					{52, table52},
					{53, table53},
					{54, table54},
					{55, table55},
					{56, table56},
					{57, table57},
					{58, table58},
					{59, table59},
					{60, table60},
					{61, table61}
				};

			DefaultActions = new Dictionary<int, ParserAction>
				{
					{34, new ParserAction(Reduce, ref table27)},
					{41, new ParserAction(Reduce, ref table22)}
				};

			Productions = new Dictionary<int, ParserProduction>
				{				
					{0, new ParserProduction(symbol0)},
					{1, new ParserProduction(symbol3,3)},
					{2, new ParserProduction(symbol5,0)},
					{3, new ParserProduction(symbol5,2)},
					{4, new ParserProduction(symbol7,2)},
					{5, new ParserProduction(symbol7,1)},
					{6, new ParserProduction(symbol7,1)},
					{7, new ParserProduction(symbol7,1)},
					{8, new ParserProduction(symbol9,5)},
					{9, new ParserProduction(symbol9,4)},
					{10, new ParserProduction(symbol9,3)},
					{11, new ParserProduction(symbol9,1)},
					{12, new ParserProduction(symbol9,7)},
					{13, new ParserProduction(symbol9,6)},
					{14, new ParserProduction(symbol9,4)},
					{15, new ParserProduction(symbol9,2)},
					{16, new ParserProduction(symbol9,2)},
					{17, new ParserProduction(symbol9,2)},
					{18, new ParserProduction(symbol9,1)},
					{19, new ParserProduction(symbol11,1)},
					{20, new ParserProduction(symbol11,1)},
					{21, new ParserProduction(symbol16,1)},
					{22, new ParserProduction(symbol16,2)},
					{23, new ParserProduction(symbol29,2)},
					{24, new ParserProduction(symbol29,3)},
					{25, new ParserProduction(symbol29,3)},
					{26, new ParserProduction(symbol29,4)},
					{27, new ParserProduction(symbol30,1)},
					{28, new ParserProduction(symbol31,1)},
					{29, new ParserProduction(symbol32,1)},
					{30, new ParserProduction(symbol32,3)},
					{31, new ParserProduction(symbol35,1)},
					{32, new ParserProduction(symbol33,1)},
					{33, new ParserProduction(symbol12,3)},
					{34, new ParserProduction(symbol39,1)},
					{35, new ParserProduction(symbol39,1)},
					{36, new ParserProduction(symbol39,1)},
					{37, new ParserProduction(symbol39,1)},
					{38, new ParserProduction(symbol39,1)},
					{39, new ParserProduction(symbol40,1)},
					{40, new ParserProduction(symbol40,1)},
					{41, new ParserProduction(symbol14,1)},
					{42, new ParserProduction(symbol14,1)},
					{43, new ParserProduction(symbol14,1)}
				};



			
			//Setup Lexer
			
			Rules = new Dictionary<int, Regex>
				{
					{0, new Regex(@"^(?:accTitle\s*:\s*)/")},
					{1, new Regex(@"^(?:(?!\n||)*[^\n]*)/")},
					{2, new Regex(@"^(?:accDescr\s*:\s*)/")},
					{3, new Regex(@"^(?:(?!\n||)*[^\n]*)/")},
					{4, new Regex(@"^(?:accDescr\s*\{\s*)/")},
					{5, new Regex(@"^(?:[\}])/")},
					{6, new Regex(@"^(?:[^\}]*)/")},
					{7, new Regex(@"^(?:[\n]+)/")},
					{8, new Regex(@"^(?:\s+)/")},
					{9, new Regex(@"^(?:[\s]+)/")},
					{10, new Regex(@"^(?:""[^""%\r\n\v\b\\]+"")/")},
					{11, new Regex(@"^(?:""[^""]*"")/")},
					{12, new Regex(@"^(?:erDiagram\b)/")},
					{13, new Regex(@"^(?:\{)/")},
					{14, new Regex(@"^(?:,)/")},
					{15, new Regex(@"^(?:\s+)/")},
					{16, new Regex(@"^(?:\b((?:PK)|(?:FK)|(?:UK))\b)/")},
					{17, new Regex(@"^(?:(.*?)[~](.*?)*[~])/")},
					{18, new Regex(@"^(?:[\*A-Za-z_][A-Za-z0-9\-_\[\]\(\)]*)/")},
					{19, new Regex(@"^(?:""[^""]*"")/")},
					{20, new Regex(@"^(?:[\n]+)/")},
					{21, new Regex(@"^(?:\})/")},
					{22, new Regex(@"^(?:.)/")},
					{23, new Regex(@"^(?:\[)/")},
					{24, new Regex(@"^(?:\])/")},
					{25, new Regex(@"^(?:one or zero\b)/")},
					{26, new Regex(@"^(?:one or more\b)/")},
					{27, new Regex(@"^(?:one or many\b)/")},
					{28, new Regex(@"^(?:1\+)/")},
					{29, new Regex(@"^(?:\|o\b)/")},
					{30, new Regex(@"^(?:zero or one\b)/")},
					{31, new Regex(@"^(?:zero or more\b)/")},
					{32, new Regex(@"^(?:zero or many\b)/")},
					{33, new Regex(@"^(?:0\+)/")},
					{34, new Regex(@"^(?:\}o\b)/")},
					{35, new Regex(@"^(?:many\(0\))/")},
					{36, new Regex(@"^(?:many\(1\))/")},
					{37, new Regex(@"^(?:many\b)/")},
					{38, new Regex(@"^(?:\}\|)/")},
					{39, new Regex(@"^(?:one\b)/")},
					{40, new Regex(@"^(?:only one\b)/")},
					{41, new Regex(@"^(?:1\b)/")},
					{42, new Regex(@"^(?:\|\|)/")},
					{43, new Regex(@"^(?:o\|)/")},
					{44, new Regex(@"^(?:o\{)/")},
					{45, new Regex(@"^(?:\|\{)/")},
					{46, new Regex(@"^(?:\s*u\b)/")},
					{47, new Regex(@"^(?:\.\.)/")},
					{48, new Regex(@"^(?:--)/")},
					{49, new Regex(@"^(?:to\b)/")},
					{50, new Regex(@"^(?:optionally to\b)/")},
					{51, new Regex(@"^(?:\.-)/")},
					{52, new Regex(@"^(?:-\.)/")},
					{53, new Regex(@"^(?:[A-Za-z_][A-Za-z0-9\-_]*)/")},
					{54, new Regex(@"^(?:.)/")},
					{55, new Regex(@"^(?:$)/")}
				};

			Conditions = new Dictionary<string, LexerConditions>
				{
					{"acc_descr_multiline", new LexerConditions(new List<int> { 5,6 }, false)},
					{"acc_descr", new LexerConditions(new List<int> { 3 }, false)},
					{"acc_title", new LexerConditions(new List<int> { 1 }, false)},
					{"block", new LexerConditions(new List<int> { 14,15,16,17,18,19,20,21,22 }, false)},
					{"INITIAL", new LexerConditions(new List<int> { 0,2,4,7,8,9,10,11,12,13,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55 }, true)}
				};


		}
		
		public ParserValue ParserPerformAction(ref ParserValue thisS, ref ParserValue yy, ref int yystate, ref JList<ParserValue> ss)
		{
			var so = ss.Count - 1;
/* this == yyval */


switch (yystate) {
case 1:
 /*('finished parsing');*/ 
break;
case 2:
 thisS = [] 
break;
case 3:
ss[so-1].push(ss[so]);thisS = ss[so-1]
break;
case 4: case 5:
 thisS = ss[so] 
break;
case 6: case 7:
 thisS=[];
break;
case 8:

          yy.addEntity(ss[so-4]);
          yy.addEntity(ss[so-2]);
          yy.addRelationship(ss[so-4], ss[so], ss[so-2], ss[so-3]);
      
break;
case 9:

          yy.addEntity(ss[so-3]);
          yy.addAttributes(ss[so-3], ss[so-1]);
      
break;
case 10:
 yy.addEntity(ss[so-2]); 
break;
case 11:
 yy.addEntity(ss[so]); 
break;
case 12:

          yy.addEntity(ss[so-6], ss[so-4]);
          yy.addAttributes(ss[so-6], ss[so-1]);
      
break;
case 13:
 yy.addEntity(ss[so-5], ss[so-3]); 
break;
case 14:
 yy.addEntity(ss[so-3], ss[so-1]); 
break;
case 15: case 16:
 thisS=ss[so].trim();yy.setAccTitle(thisS); 
break;
case 17: case 18:
 thisS=ss[so].trim();yy.setAccDescription(thisS); 
break;
case 19: case 43:
 thisS = ss[so]; 
break;
case 20: case 41: case 42:
 thisS = ss[so].replace(/"/g, ''); 
break;
case 21: case 29:
 thisS = [ss[so]]; 
break;
case 22:
 ss[so].push(ss[so-1]); thisS=ss[so]; 
break;
case 23:
 thisS = { attributeType: ss[so-1], attributeName: ss[so] }; 
break;
case 24:
 thisS = { attributeType: ss[so-2], attributeName: ss[so-1], attributeKeyTypeList: ss[so] }; 
break;
case 25:
 thisS = { attributeType: ss[so-2], attributeName: ss[so-1], attributeComment: ss[so] }; 
break;
case 26:
 thisS = { attributeType: ss[so-3], attributeName: ss[so-2], attributeKeyTypeList: ss[so-1], attributeComment: ss[so] }; 
break;
case 27: case 28: case 31:
 thisS=ss[so]; 
break;
case 30:
 ss[so-2].push(ss[so]); thisS = ss[so-2]; 
break;
case 32:
 thisS=ss[so].replace(/"/g, ''); 
break;
case 33:

        thisS = { cardA: ss[so], relType: ss[so-1], cardB: ss[so-2] };
        
      
break;
case 34:
 thisS = yy.Cardinality.ZERO_OR_ONE; 
break;
case 35:
 thisS = yy.Cardinality.ZERO_OR_MORE; 
break;
case 36:
 thisS = yy.Cardinality.ONE_OR_MORE; 
break;
case 37:
 thisS = yy.Cardinality.ONLY_ONE; 
break;
case 38:
 thisS = yy.Cardinality.MD_PARENT; 
break;
case 39:
 thisS = yy.Identification.NON_IDENTIFYING;  
break;
case 40:
 thisS = yy.Identification.IDENTIFYING; 
break;
}

			return null;
		}

		public ParserSymbol ParserLex()
		{
			var token = LexerLex();//end = 1

            if (token != null)
            {
                return token;
            }

			return Symbols["end"];
		}

		public void ParseError(string error, ParserError hash = null)
		{
			throw new InvalidOperationException(error);
		}

		public void LexerError(string error, LexerError hash = null)
		{
			throw new InvalidOperationException(error);
		}

		public ParserValue Parse(string input)
		{
			if (Table == null) {
				throw new Exception("Empty table");
			}
			var stack = new JList<ParserCachedAction>
			{
				new ParserCachedAction(new ParserAction(0, Table[0]))
			};
			var vstack = new JList<ParserValue>
			{
				new ParserValue()
			};
			var yy = new ParserValue();
			var _yy = new ParserValue();
			var v = new ParserValue();
			int recovering = 0;
			ParserSymbol symbol = null;
			ParserAction action = null;
			string errStr = "";
			ParserSymbol preErrorSymbol = null;
			ParserState state = null;

			SetInput(input);

			while (true)
			{
				// retreive state number from top of stack
				state = stack.Last().Action.State;

				// use default actions if available
				if (state != null && DefaultActions.ContainsKey(state.Index))
				{
					action = DefaultActions[state.Index];
				}
				else
				{
					if (symbol == null)
					{
						symbol = ParserLex();
					}
					// read action for current state and first input
					if (state != null && state.Actions.ContainsKey(symbol.Index))
					{
						action = state.Actions[symbol.Index];
					}
					else
					{
						action = null;
					}
				}

				if (action == null)
				{
					if (recovering > 0)
					{
						// Report error
						var expected = new Stack<string>{};
						foreach(var p in Table[state.Index].Actions)
						{
							expected.Push(Terminals[p.Value.Action].Name);
						}

						errStr = "Parse error on line " + (Yy.LineNo + 1).ToString() + ":" + '\n' +
							ShowPosition() + '\n' + 
								"Expecting " + String.Join(", ", expected) +
								", got '" +
								(symbol != null ? Terminals[symbol.Index].ToString() : "NOTHING") + "'";

						ParseError(errStr, new ParserError(Match, state, symbol, Yy.LineNo, yy.Loc, expected));
					}
				}

				/*if (state.IsArray()) {
					this.parseError("Parse Error: multiple actions possible at state: " + state + ", token: " + symbol);
				}*/

				if (state == null || action == null)
				{
					break;
				}

				switch (action.Action)
				{
					case Shift:
						stack.Push(new ParserCachedAction(action, symbol));
						vstack.Push(Yy.Clone());

						symbol = null;
						if (preErrorSymbol == null)
						{ // normal execution/no error
							yy = Yy.Clone();
							if (recovering > 0) recovering--;
						} else { // error just occurred, resume old lookahead f/ before error
							symbol = preErrorSymbol;
							preErrorSymbol = null;
						}
					break;

					case Reduce:
						int len = Productions[action.State.Index].Len;
						// perform semantic action
						_yy = vstack[vstack.Count - len];

						if (Ranges != null)
						{
							Yy.Loc.Range = new ParserRange(
								vstack[vstack.Count - len].Loc.Range.X,
								vstack.Last().Loc.Range.Y
							);
						}

						var value = ParserPerformAction(ref _yy, ref yy, ref action.State.Index, ref vstack);

						if (value != null)
						{
							return value;
						}

						// pop off stack
						while (len > 0)
						{
							stack.Pop();
							vstack.Pop();
							len--;
						}

				        if (_yy == null)
				        {
							vstack.Push(new ParserValue());
				        }
				        else
				        {
				            vstack.Push(_yy.Clone());
				        }
				        var nextSymbol = Productions[action.State.Index].Symbol;
						// goto new state = table[STATE][NONTERMINAL]
						var nextState = stack.Last().Action.State;
						var nextAction = nextState.Actions[nextSymbol.Index];

						stack.Push(new ParserCachedAction(nextAction, nextSymbol));

					break;
					case Accept:
						return v;
				}
			}

			return v;
		}

		/* Jison generated lexer */
		public ParserSymbol Eof = new ParserSymbol("Eof", 1);
		public ParserValue Yy = new ParserValue();
		public string Match = "";
		public string Matched = "";
		public Stack<string> ConditionStack;
		public Dictionary<int, Regex> Rules;
		public Dictionary<string, LexerConditions> Conditions;
		public bool Done = false;
		public bool Less;
		public bool _More;
		public string _Input;
		public int Offset;
		public Dictionary<int, ParserRange>Ranges;
		public bool Flex = false;

		public void SetInput(string input)
		{
			_Input = input;
			_More = Less = Done = false;
			Yy.LineNo = Yy.Leng = 0;
			Matched = Match = "";
			ConditionStack = new Stack<string>();
			ConditionStack.Push("INITIAL");

			if (Ranges != null)
			{
				Yy.Loc = new ParserLocation(new ParserRange(0,0));
			} else {
				Yy.Loc = new ParserLocation();
			}

			Offset = 0;
		}

		public string Input()
		{
			string ch = _Input[0].ToString();
			Yy.Text += ch;
			Yy.Leng++;
			Offset++;
			Match += ch;
			Matched += ch;
			Match lines = Regex.Match(ch, "/(?:\r\n?|\n).*/");
			if (lines.Success) {
				Yy.LineNo++;
				Yy.Loc.LastLine++;
			} else {
				Yy.Loc.LastColumn++;
			}

			if (Ranges != null)
			{
				Yy.Loc.Range.Y++;
			}

			_Input = _Input.Substring(1);
			return ch;
		}

		public void Unput(string ch)
		{
			int len = ch.Length;
			var lines = Regex.Split(ch, "/(?:\r\n?|\n)/");

			_Input = ch + _Input;
			Yy.Text = Yy.Text.Substring(0, len - 1);
			Offset -= len;
			var oldLines = Regex.Split(Match, "/(?:\r\n?|\n)/");
			Match = Match.Substring(0, Match.Length - 1);
			Matched = Matched.Substring(0, Matched.Length - 1);

			if ((lines.Length - 1) > 0) Yy.LineNo -= lines.Length - 1;
			var r = Yy.Loc.Range;

			Yy.Loc = new ParserLocation(
				Yy.Loc.FirstLine,
				Yy.LineNo + 1,
				Yy.Loc.FirstColumn,
				(
					lines.Length > 0 ?
					(
						lines.Length == oldLines.Length ?
							Yy.Loc.FirstColumn :
							0
					) + oldLines[oldLines.Length - lines.Length].Length - lines[0].Length :
					Yy.Loc.FirstColumn - len
				)
			);

			if (Ranges.Count > 0) {
				Yy.Loc.Range = new ParserRange(r.X, r.X + Yy.Leng - len);
			}
		}

		public void More()
		{
			_More = true;
		}

		public string PastInput()
		{
			var past = Matched.Substring(0, Matched.Length - Match.Length);
			return (past.Length > 20 ? "..." + Regex.Replace(past.Substring(-20), "/\n/", "") : "");
		}

		public string UpcomingInput()
		{
			var next = Match;
			if (next.Length < 20)
			{
				next += _Input.Substring(0, (next.Length > 20 ? 20 - next.Length : next.Length));
			}
			return Regex.Replace(next.Substring(0, (next.Length > 20 ? 20 - next.Length : next.Length)) + (next.Length > 20 ? "..." : ""), "/\n/", "");
		}

		public string ShowPosition()
		{
			var pre = PastInput();

			var c = "";
			for (var i = 0; i < pre.Length; i++)
			{
				c += "-";
			}

			return pre + UpcomingInput() + '\n' + c + "^";
		}

		public ParserSymbol Next()
		{
			if (Done == true)
			{
				return Eof;
			}

			if (String.IsNullOrEmpty(_Input))
			{
				Done = true;
			}

			if (_More == false)
			{
				Yy.Text = "";
				Match = "";
			}

			var rules = CurrentRules();
			string match = "";
			bool matched = false;
			int index = 0;
			Regex rule;
			for (int i = 0; i < rules.Count; i++)
			{
				rule = Rules[rules[i]];
				var tempMatch = rule.Match(_Input);
				if (tempMatch.Success == true && (match != null || tempMatch.Length > match.Length)) {
					match = tempMatch.Value;
					matched = true;
					index = i;
					if (!Flex) {
						break;
					}
				}
			}
			if ( matched )
			{
				Match lineCount = Regex.Match(match, "/\n.*/");

				Yy.LineNo += lineCount.Length;
				Yy.Loc.FirstLine = Yy.Loc.LastLine;
				Yy.Loc.LastLine = Yy.LineNo + 1;
				Yy.Loc.FirstColumn = Yy.Loc.LastColumn;
				Yy.Loc.LastColumn = lineCount.Length > 0 ? lineCount.Length - 1 : Yy.Loc.LastColumn + match.Length;

				Yy.Text += match;
				Match += match;
				Matched += match;

				Yy.Leng = Yy.Text.Length;
				if (Ranges != null)
				{
					Yy.Loc.Range = new ParserRange(Offset, Offset += Yy.Leng);
				}
				_More = false;
				_Input = _Input.Substring(match.Length);
                var ruleIndex = rules[index];
                var nextCondition = ConditionStack.Peek();
                dynamic action = LexerPerformAction(ruleIndex, nextCondition);
				ParserSymbol token = Symbols[action];

				if (Done == true && String.IsNullOrEmpty(_Input) == false)
				{
					Done = false;
				}

				if (token.Index > -1) {
					return token;
				} else {
					return null;
				}
			}

			if (String.IsNullOrEmpty(_Input)) {
				return Symbols["EOF"];
			} else
			{
				LexerError("Lexical error on line " + (Yy.LineNo + 1) + ". Unrecognized text.\n" + ShowPosition(), new LexerError("", -1, Yy.LineNo));
				return null;
			}
		}

		public ParserSymbol LexerLex()
		{
			var r = Next();

			while (r == null)
			{
			    r = Next();
			}

		    return r;
		}

		public void Begin(string condition)
		{
			ConditionStack.Push(condition);
		}

		public string PopState()
		{
			return ConditionStack.Pop();
		}

		public List<int> CurrentRules()
		{
			var peek = ConditionStack.Peek();
			return Conditions[peek].Rules;
		}

		public dynamic LexerPerformAction(int avoidingNameCollisions, string Yy_Start)
		{
			
;
switch(avoidingNameCollisions) {
case 0: this.begin("acc_title");return 22; 
break;
case 1: this.popState(); return "acc_title_value"; 
break;
case 2: this.begin("acc_descr");return 24; 
break;
case 3: this.popState(); return "acc_descr_value"; 
break;
case 4: this.begin("acc_descr_multiline");
break;
case 5: this.popState(); 
break;
case 6:return "acc_descr_multiline_value";
break;
case 7:return 10;
break;
case 8:/* skip whitespace */
break;
case 9:return 8;
break;
case 10:return 28;
break;
case 11:return 48;
break;
case 12:return 4;
break;
case 13: this.begin("block"); return 15; 
break;
case 14:return 36;
break;
case 15:/* skip whitespace in block */
break;
case 16:return 37;
break;
case 17:return 34;
break;
case 18:return 34;
break;
case 19:return 38;
break;
case 20:/* nothing */
break;
case 21: this.popState(); return 17; 
break;
case 22:return yy_.yytext[0];
break;
case 23:return 18;
break;
case 24:return 19;
break;
case 25:return 41;
break;
case 26:return 43;
break;
case 27:return 43;
break;
case 28:return 43;
break;
case 29:return 41;
break;
case 30:return 41;
break;
case 31:return 42;
break;
case 32:return 42;
break;
case 33:return 42;
break;
case 34:return 42;
break;
case 35:return 42;
break;
case 36:return 43;
break;
case 37:return 42;
break;
case 38:return 43;
break;
case 39:return 44;
break;
case 40:return 44;
break;
case 41:return 44;
break;
case 42:return 44;
break;
case 43:return 41;
break;
case 44:return 42;
break;
case 45:return 43;
break;
case 46:return 45;
break;
case 47:return 46;
break;
case 48:return 47;
break;
case 49:return 47;
break;
case 50:return 46;
break;
case 51:return 46;
break;
case 52:return 46;
break;
case 53:return 27;
break;
case 54:return yy_.yytext[0];
break;
case 55:return 6;
break;
}

			return -1;
		}
	}

	public class ParserLocation
	{
		public int FirstLine = 1;
		public int LastLine = 0;
		public int FirstColumn = 1;
		public int LastColumn = 0;
		public ParserRange Range;

		public ParserLocation()
		{
		}

		public ParserLocation(ParserRange range)
		{
			Range = range;
		}

		public ParserLocation(int firstLine, int lastLine, int firstColumn, int lastColumn)
		{
			FirstLine = firstLine;
			LastLine = lastLine;
			FirstColumn = firstColumn;
			LastColumn = lastColumn;
		}

		public ParserLocation(int firstLine, int lastLine, int firstColumn, int lastColumn, ParserRange range)
		{
			FirstLine = firstLine;
			LastLine = lastLine;
			FirstColumn = firstColumn;
			LastColumn = lastColumn;
			Range = range;
		}
	}

	public class LexerConditions
	{
		public List<int> Rules;
		public bool Inclusive;

		public LexerConditions(List<int> rules, bool inclusive)
		{
			Rules = rules;
			Inclusive = inclusive;
		}
	}

	public class ParserProduction
	{
		public int Len = 0;
		public ParserSymbol Symbol;

		public ParserProduction(ParserSymbol symbol)
		{
			Symbol = symbol;
		}

		public ParserProduction(ParserSymbol symbol, int len)
		{
			Symbol = symbol;
			Len = len;
		}
	}

	public class ParserCachedAction
	{
		public ParserAction Action;
		public ParserSymbol Symbol;

		public ParserCachedAction(ParserAction action)
		{
			Action = action;
		}

		public ParserCachedAction(ParserAction action, ParserSymbol symbol)
		{
			Action = action;
			Symbol = symbol;
		}
	}

	public class ParserAction
	{
		public int Action;
		public ParserState State;
		public ParserSymbol Symbol;

		public ParserAction(int action)
		{
			Action = action;
		}

		public ParserAction(int action, ref ParserState state)
		{
			Action = action;
			State = state;
		}

		public ParserAction(int action, ParserState state)
		{
			Action = action;
			State = state;
		}

		public ParserAction(int action, ref ParserSymbol symbol)
		{
			Action = action;
			Symbol = symbol;
		}
	}

	public class ParserSymbol
	{
		public string Name;
		public int Index = -1;
		public IDictionary<int, ParserSymbol> Symbols = new Dictionary<int, ParserSymbol>();
		public IDictionary<string, ParserSymbol> SymbolsByName = new Dictionary<string, ParserSymbol>();

		public ParserSymbol()
		{
		}

		public ParserSymbol(string name, int index)
		{
			Name = name;
			Index = index;
		}

		public void AddAction(ParserSymbol p)
		{
			Symbols.Add(p.Index, p);
			SymbolsByName.Add(p.Name, p);
		}
	}

	public class ParserError
	{
		public String Text;
		public ParserState State;
		public ParserSymbol Symbol;
		public int LineNo;
		public ParserLocation Loc;
		public Stack<string> Expected;

		public ParserError(String text, ParserState state, ParserSymbol symbol, int lineNo, ParserLocation loc, Stack<string> expected)
		{
			Text = text;
			State = state;
			Symbol = symbol;
			LineNo = lineNo;
			Loc = loc;
			Expected = expected;
		}
	}

	public class LexerError
	{
		public String Text;
		public int Token;
		public int LineNo;

		public LexerError(String text, int token, int lineNo)
		{
			Text = text;
			Token = token;
			LineNo = lineNo;
		}
	}

	public class ParserState
	{
		public int Index;
		public Dictionary<int, ParserAction> Actions = new Dictionary<int, ParserAction>();

		public ParserState(int index)
		{
			Index = index;
		}

		public void SetActions(ref Dictionary<int, ParserAction> actions)
		{
			Actions = actions;
		}
	}

	public class ParserRange
	{
		public int X;
		public int Y;

		public ParserRange(int x, int y)
		{
			X = x;
			Y = y;
		}
	}

	public class ParserSymbols
	{
		private Dictionary<string, ParserSymbol> SymbolsString = new Dictionary<string, ParserSymbol>();
		private Dictionary<int, ParserSymbol> SymbolsInt = new Dictionary<int, ParserSymbol>();

		public void Add(ParserSymbol symbol)
		{
			SymbolsInt.Add(symbol.Index, symbol);
			SymbolsString.Add(symbol.Name, symbol);
		}

		public ParserSymbol this[char name]
		{
			get
			{
				return SymbolsString[name.ToString()];
			}
		}

		public ParserSymbol this[string name]
		{
			get
			{
				return SymbolsString[name];
			}
		}

		public ParserSymbol this[int index]
		{
			get
			{
				if (index < 0)
				{
					return new ParserSymbol();
				}
				return SymbolsInt[index];
			}
		}
	}

	public class ParserValue
	{
		public string Text;
		public ParserLocation Loc;
		public int Leng = 0;
		public int LineNo = 0;
		
		public ParserValue()
		{
		}
		
		public ParserValue(ParserValue parserValue)
		{
			Text = parserValue.Text;
			Leng = parserValue.Leng;
			Loc = parserValue.Loc;
			LineNo = parserValue.LineNo;
		}
		
		public ParserValue Clone()
		{
			return new ParserValue(this);
		}
	}

	public class JList<T> : List<T> where T : class
	{
		public void Push(T item)
		{
			Add(item);
		}

		public void Pop()
		{
			RemoveAt(Count - 1);
		}

		new public T this[int index]
		{
			get
			{
				if (index >= Count || index < 0 || Count == 0)
				{
					return null;
				}
				return base[index];
			}
		}
	}
}