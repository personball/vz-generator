// Base On: https://github.com/zaach/jison/blob/master/ports/csharp/Jison/Jison/Template.cs

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;


namespace MermaidParser.ClassDiagram
{
	public class ClassDiagramParser
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

		public ClassDiagramParser()
		{
			//Setup Parser
			
			var symbol0 = new ParserSymbol("accept", 0);
			var symbol1 = new ParserSymbol("end", 1);
			var symbol2 = new ParserSymbol("error", 2);
			var symbol3 = new ParserSymbol("start", 3);
			var symbol4 = new ParserSymbol("mermaidDoc", 4);
			var symbol5 = new ParserSymbol("statements", 5);
			var symbol6 = new ParserSymbol("graphConfig", 6);
			var symbol7 = new ParserSymbol("CLASS_DIAGRAM", 7);
			var symbol8 = new ParserSymbol("NEWLINE", 8);
			var symbol9 = new ParserSymbol("EOF", 9);
			var symbol10 = new ParserSymbol("statement", 10);
			var symbol11 = new ParserSymbol("classLabel", 11);
			var symbol12 = new ParserSymbol("SQS", 12);
			var symbol13 = new ParserSymbol("STR", 13);
			var symbol14 = new ParserSymbol("SQE", 14);
			var symbol15 = new ParserSymbol("namespaceName", 15);
			var symbol16 = new ParserSymbol("alphaNumToken", 16);
			var symbol17 = new ParserSymbol("className", 17);
			var symbol18 = new ParserSymbol("classLiteralName", 18);
			var symbol19 = new ParserSymbol("GENERICTYPE", 19);
			var symbol20 = new ParserSymbol("relationStatement", 20);
			var symbol21 = new ParserSymbol("LABEL", 21);
			var symbol22 = new ParserSymbol("namespaceStatement", 22);
			var symbol23 = new ParserSymbol("classStatement", 23);
			var symbol24 = new ParserSymbol("memberStatement", 24);
			var symbol25 = new ParserSymbol("annotationStatement", 25);
			var symbol26 = new ParserSymbol("clickStatement", 26);
			var symbol27 = new ParserSymbol("cssClassStatement", 27);
			var symbol28 = new ParserSymbol("noteStatement", 28);
			var symbol29 = new ParserSymbol("direction", 29);
			var symbol30 = new ParserSymbol("acc_title", 30);
			var symbol31 = new ParserSymbol("acc_title_value", 31);
			var symbol32 = new ParserSymbol("acc_descr", 32);
			var symbol33 = new ParserSymbol("acc_descr_value", 33);
			var symbol34 = new ParserSymbol("acc_descr_multiline_value", 34);
			var symbol35 = new ParserSymbol("namespaceIdentifier", 35);
			var symbol36 = new ParserSymbol("STRUCT_START", 36);
			var symbol37 = new ParserSymbol("classStatements", 37);
			var symbol38 = new ParserSymbol("STRUCT_STOP", 38);
			var symbol39 = new ParserSymbol("NAMESPACE", 39);
			var symbol40 = new ParserSymbol("classIdentifier", 40);
			var symbol41 = new ParserSymbol("STYLE_SEPARATOR", 41);
			var symbol42 = new ParserSymbol("members", 42);
			var symbol43 = new ParserSymbol("CLASS", 43);
			var symbol44 = new ParserSymbol("ANNOTATION_START", 44);
			var symbol45 = new ParserSymbol("ANNOTATION_END", 45);
			var symbol46 = new ParserSymbol("MEMBER", 46);
			var symbol47 = new ParserSymbol("SEPARATOR", 47);
			var symbol48 = new ParserSymbol("relation", 48);
			var symbol49 = new ParserSymbol("NOTE_FOR", 49);
			var symbol50 = new ParserSymbol("noteText", 50);
			var symbol51 = new ParserSymbol("NOTE", 51);
			var symbol52 = new ParserSymbol("direction_tb", 52);
			var symbol53 = new ParserSymbol("direction_bt", 53);
			var symbol54 = new ParserSymbol("direction_rl", 54);
			var symbol55 = new ParserSymbol("direction_lr", 55);
			var symbol56 = new ParserSymbol("relationType", 56);
			var symbol57 = new ParserSymbol("lineType", 57);
			var symbol58 = new ParserSymbol("AGGREGATION", 58);
			var symbol59 = new ParserSymbol("EXTENSION", 59);
			var symbol60 = new ParserSymbol("COMPOSITION", 60);
			var symbol61 = new ParserSymbol("DEPENDENCY", 61);
			var symbol62 = new ParserSymbol("LOLLIPOP", 62);
			var symbol63 = new ParserSymbol("LINE", 63);
			var symbol64 = new ParserSymbol("DOTTED_LINE", 64);
			var symbol65 = new ParserSymbol("CALLBACK", 65);
			var symbol66 = new ParserSymbol("LINK", 66);
			var symbol67 = new ParserSymbol("LINK_TARGET", 67);
			var symbol68 = new ParserSymbol("CLICK", 68);
			var symbol69 = new ParserSymbol("CALLBACK_NAME", 69);
			var symbol70 = new ParserSymbol("CALLBACK_ARGS", 70);
			var symbol71 = new ParserSymbol("HREF", 71);
			var symbol72 = new ParserSymbol("CSSCLASS", 72);
			var symbol73 = new ParserSymbol("commentToken", 73);
			var symbol74 = new ParserSymbol("textToken", 74);
			var symbol75 = new ParserSymbol("graphCodeTokens", 75);
			var symbol76 = new ParserSymbol("textNoTagsToken", 76);
			var symbol77 = new ParserSymbol("TAGSTART", 77);
			var symbol78 = new ParserSymbol("TAGEND", 78);
			var symbol79 = new ParserSymbol("==", 79);
			var symbol80 = new ParserSymbol("--", 80);
			var symbol81 = new ParserSymbol("PCT", 81);
			var symbol82 = new ParserSymbol("DEFAULT", 82);
			var symbol83 = new ParserSymbol("SPACE", 83);
			var symbol84 = new ParserSymbol("MINUS", 84);
			var symbol85 = new ParserSymbol("keywords", 85);
			var symbol86 = new ParserSymbol("UNICODE_TEXT", 86);
			var symbol87 = new ParserSymbol("NUM", 87);
			var symbol88 = new ParserSymbol("ALPHA", 88);
			var symbol89 = new ParserSymbol("BQUOTE_STR", 89);


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
			Symbols.Add(symbol49);
			Symbols.Add(symbol50);
			Symbols.Add(symbol51);
			Symbols.Add(symbol52);
			Symbols.Add(symbol53);
			Symbols.Add(symbol54);
			Symbols.Add(symbol55);
			Symbols.Add(symbol56);
			Symbols.Add(symbol57);
			Symbols.Add(symbol58);
			Symbols.Add(symbol59);
			Symbols.Add(symbol60);
			Symbols.Add(symbol61);
			Symbols.Add(symbol62);
			Symbols.Add(symbol63);
			Symbols.Add(symbol64);
			Symbols.Add(symbol65);
			Symbols.Add(symbol66);
			Symbols.Add(symbol67);
			Symbols.Add(symbol68);
			Symbols.Add(symbol69);
			Symbols.Add(symbol70);
			Symbols.Add(symbol71);
			Symbols.Add(symbol72);
			Symbols.Add(symbol73);
			Symbols.Add(symbol74);
			Symbols.Add(symbol75);
			Symbols.Add(symbol76);
			Symbols.Add(symbol77);
			Symbols.Add(symbol78);
			Symbols.Add(symbol79);
			Symbols.Add(symbol80);
			Symbols.Add(symbol81);
			Symbols.Add(symbol82);
			Symbols.Add(symbol83);
			Symbols.Add(symbol84);
			Symbols.Add(symbol85);
			Symbols.Add(symbol86);
			Symbols.Add(symbol87);
			Symbols.Add(symbol88);
			Symbols.Add(symbol89);

			Terminals = new Dictionary<int, ParserSymbol>
				{
					{2, symbol2},
					{7, symbol7},
					{8, symbol8},
					{9, symbol9},
					{12, symbol12},
					{13, symbol13},
					{14, symbol14},
					{19, symbol19},
					{21, symbol21},
					{30, symbol30},
					{31, symbol31},
					{32, symbol32},
					{33, symbol33},
					{34, symbol34},
					{36, symbol36},
					{38, symbol38},
					{39, symbol39},
					{41, symbol41},
					{43, symbol43},
					{44, symbol44},
					{45, symbol45},
					{46, symbol46},
					{47, symbol47},
					{49, symbol49},
					{51, symbol51},
					{52, symbol52},
					{53, symbol53},
					{54, symbol54},
					{55, symbol55},
					{58, symbol58},
					{59, symbol59},
					{60, symbol60},
					{61, symbol61},
					{62, symbol62},
					{63, symbol63},
					{64, symbol64},
					{65, symbol65},
					{66, symbol66},
					{67, symbol67},
					{68, symbol68},
					{69, symbol69},
					{70, symbol70},
					{71, symbol71},
					{72, symbol72},
					{75, symbol75},
					{77, symbol77},
					{78, symbol78},
					{79, symbol79},
					{80, symbol80},
					{81, symbol81},
					{82, symbol82},
					{83, symbol83},
					{84, symbol84},
					{85, symbol85},
					{86, symbol86},
					{87, symbol87},
					{88, symbol88},
					{89, symbol89}
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
			var table62 = new ParserState(62);
			var table63 = new ParserState(63);
			var table64 = new ParserState(64);
			var table65 = new ParserState(65);
			var table66 = new ParserState(66);
			var table67 = new ParserState(67);
			var table68 = new ParserState(68);
			var table69 = new ParserState(69);
			var table70 = new ParserState(70);
			var table71 = new ParserState(71);
			var table72 = new ParserState(72);
			var table73 = new ParserState(73);
			var table74 = new ParserState(74);
			var table75 = new ParserState(75);
			var table76 = new ParserState(76);
			var table77 = new ParserState(77);
			var table78 = new ParserState(78);
			var table79 = new ParserState(79);
			var table80 = new ParserState(80);
			var table81 = new ParserState(81);
			var table82 = new ParserState(82);
			var table83 = new ParserState(83);
			var table84 = new ParserState(84);
			var table85 = new ParserState(85);
			var table86 = new ParserState(86);
			var table87 = new ParserState(87);
			var table88 = new ParserState(88);
			var table89 = new ParserState(89);
			var table90 = new ParserState(90);
			var table91 = new ParserState(91);
			var table92 = new ParserState(92);
			var table93 = new ParserState(93);
			var table94 = new ParserState(94);
			var table95 = new ParserState(95);
			var table96 = new ParserState(96);
			var table97 = new ParserState(97);
			var table98 = new ParserState(98);
			var table99 = new ParserState(99);
			var table100 = new ParserState(100);
			var table101 = new ParserState(101);
			var table102 = new ParserState(102);
			var table103 = new ParserState(103);
			var table104 = new ParserState(104);
			var table105 = new ParserState(105);
			var table106 = new ParserState(106);
			var table107 = new ParserState(107);
			var table108 = new ParserState(108);
			var table109 = new ParserState(109);
			var table110 = new ParserState(110);
			var table111 = new ParserState(111);
			var table112 = new ParserState(112);
			var table113 = new ParserState(113);
			var table114 = new ParserState(114);
			var table115 = new ParserState(115);
			var table116 = new ParserState(116);
			var table117 = new ParserState(117);
			var table118 = new ParserState(118);
			var table119 = new ParserState(119);
			var table120 = new ParserState(120);
			var table121 = new ParserState(121);
			var table122 = new ParserState(122);
			var table123 = new ParserState(123);
			var table124 = new ParserState(124);
			var table125 = new ParserState(125);
			var table126 = new ParserState(126);
			var table127 = new ParserState(127);
			var table128 = new ParserState(128);
			var table129 = new ParserState(129);
			var table130 = new ParserState(130);

			var tableDefinition0 = new Dictionary<int, ParserAction>
				{
					{3, new ParserAction(None, ref table1)},
					{4, new ParserAction(None, ref table2)},
					{5, new ParserAction(None, ref table3)},
					{6, new ParserAction(None, ref table4)},
					{7, new ParserAction(Shift, ref table6)},
					{10, new ParserAction(None, ref table5)},
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table19)},
					{18, new ParserAction(None, ref table36)},
					{20, new ParserAction(None, ref table7)},
					{22, new ParserAction(None, ref table8)},
					{23, new ParserAction(None, ref table9)},
					{24, new ParserAction(None, ref table10)},
					{25, new ParserAction(None, ref table11)},
					{26, new ParserAction(None, ref table12)},
					{27, new ParserAction(None, ref table13)},
					{28, new ParserAction(None, ref table14)},
					{29, new ParserAction(None, ref table15)},
					{30, new ParserAction(Shift, ref table16)},
					{32, new ParserAction(Shift, ref table17)},
					{34, new ParserAction(Shift, ref table18)},
					{35, new ParserAction(None, ref table20)},
					{39, new ParserAction(Shift, ref table37)},
					{40, new ParserAction(None, ref table21)},
					{43, new ParserAction(Shift, ref table38)},
					{44, new ParserAction(Shift, ref table24)},
					{46, new ParserAction(Shift, ref table22)},
					{47, new ParserAction(Shift, ref table23)},
					{49, new ParserAction(Shift, ref table29)},
					{51, new ParserAction(Shift, ref table30)},
					{52, new ParserAction(Shift, ref table31)},
					{53, new ParserAction(Shift, ref table32)},
					{54, new ParserAction(Shift, ref table33)},
					{55, new ParserAction(Shift, ref table34)},
					{65, new ParserAction(Shift, ref table25)},
					{66, new ParserAction(Shift, ref table26)},
					{68, new ParserAction(Shift, ref table27)},
					{72, new ParserAction(Shift, ref table28)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition1 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Accept)}
				};

			var tableDefinition2 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table1)}
				};

			var tableDefinition3 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table2)}
				};

			var tableDefinition4 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table3)}
				};

			var tableDefinition5 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table5)},
					{8, new ParserAction(Shift, ref table44)},
					{9, new ParserAction(Reduce, ref table5)}
				};

			var tableDefinition6 = new Dictionary<int, ParserAction>
				{
					{8, new ParserAction(Shift, ref table45)}
				};

			var tableDefinition7 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table16)},
					{8, new ParserAction(Reduce, ref table16)},
					{9, new ParserAction(Reduce, ref table16)},
					{21, new ParserAction(Shift, ref table46)}
				};

			var tableDefinition8 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table18)},
					{8, new ParserAction(Reduce, ref table18)},
					{9, new ParserAction(Reduce, ref table18)}
				};

			var tableDefinition9 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table19)},
					{8, new ParserAction(Reduce, ref table19)},
					{9, new ParserAction(Reduce, ref table19)}
				};

			var tableDefinition10 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table20)},
					{8, new ParserAction(Reduce, ref table20)},
					{9, new ParserAction(Reduce, ref table20)}
				};

			var tableDefinition11 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table21)},
					{8, new ParserAction(Reduce, ref table21)},
					{9, new ParserAction(Reduce, ref table21)}
				};

			var tableDefinition12 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table22)},
					{8, new ParserAction(Reduce, ref table22)},
					{9, new ParserAction(Reduce, ref table22)}
				};

			var tableDefinition13 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table23)},
					{8, new ParserAction(Reduce, ref table23)},
					{9, new ParserAction(Reduce, ref table23)}
				};

			var tableDefinition14 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table24)},
					{8, new ParserAction(Reduce, ref table24)},
					{9, new ParserAction(Reduce, ref table24)}
				};

			var tableDefinition15 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table25)},
					{8, new ParserAction(Reduce, ref table25)},
					{9, new ParserAction(Reduce, ref table25)}
				};

			var tableDefinition16 = new Dictionary<int, ParserAction>
				{
					{31, new ParserAction(Shift, ref table47)}
				};

			var tableDefinition17 = new Dictionary<int, ParserAction>
				{
					{33, new ParserAction(Shift, ref table48)}
				};

			var tableDefinition18 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table28)},
					{8, new ParserAction(Reduce, ref table28)},
					{9, new ParserAction(Reduce, ref table28)}
				};

			var tableDefinition19 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table44)},
					{8, new ParserAction(Reduce, ref table44)},
					{9, new ParserAction(Reduce, ref table44)},
					{13, new ParserAction(Shift, ref table50)},
					{21, new ParserAction(Shift, ref table51)},
					{48, new ParserAction(None, ref table49)},
					{56, new ParserAction(None, ref table52)},
					{57, new ParserAction(None, ref table53)},
					{58, new ParserAction(Shift, ref table54)},
					{59, new ParserAction(Shift, ref table55)},
					{60, new ParserAction(Shift, ref table56)},
					{61, new ParserAction(Shift, ref table57)},
					{62, new ParserAction(Shift, ref table58)},
					{63, new ParserAction(Shift, ref table59)},
					{64, new ParserAction(Shift, ref table60)}
				};

			var tableDefinition20 = new Dictionary<int, ParserAction>
				{
					{36, new ParserAction(Shift, ref table61)}
				};

			var tableDefinition21 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table35)},
					{8, new ParserAction(Reduce, ref table35)},
					{9, new ParserAction(Reduce, ref table35)},
					{36, new ParserAction(Shift, ref table63)},
					{38, new ParserAction(Reduce, ref table35)},
					{41, new ParserAction(Shift, ref table62)}
				};

			var tableDefinition22 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table46)},
					{8, new ParserAction(Reduce, ref table46)},
					{9, new ParserAction(Reduce, ref table46)}
				};

			var tableDefinition23 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table47)},
					{8, new ParserAction(Reduce, ref table47)},
					{9, new ParserAction(Reduce, ref table47)}
				};

			var tableDefinition24 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table64)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)}
				};

			var tableDefinition25 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table65)},
					{18, new ParserAction(None, ref table36)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition26 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table66)},
					{18, new ParserAction(None, ref table36)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition27 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table67)},
					{18, new ParserAction(None, ref table36)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition28 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Shift, ref table68)}
				};

			var tableDefinition29 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table69)},
					{18, new ParserAction(None, ref table36)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition30 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Shift, ref table71)},
					{50, new ParserAction(None, ref table70)}
				};

			var tableDefinition31 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table54)},
					{8, new ParserAction(Reduce, ref table54)},
					{9, new ParserAction(Reduce, ref table54)}
				};

			var tableDefinition32 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table55)},
					{8, new ParserAction(Reduce, ref table55)},
					{9, new ParserAction(Reduce, ref table55)}
				};

			var tableDefinition33 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table56)},
					{8, new ParserAction(Reduce, ref table56)},
					{9, new ParserAction(Reduce, ref table56)}
				};

			var tableDefinition34 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table57)},
					{8, new ParserAction(Reduce, ref table57)},
					{9, new ParserAction(Reduce, ref table57)}
				};

			var tableDefinition35 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table11)},
					{8, new ParserAction(Reduce, ref table11)},
					{9, new ParserAction(Reduce, ref table11)},
					{12, new ParserAction(Reduce, ref table11)},
					{13, new ParserAction(Reduce, ref table11)},
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table72)},
					{18, new ParserAction(None, ref table36)},
					{19, new ParserAction(Shift, ref table73)},
					{21, new ParserAction(Reduce, ref table11)},
					{36, new ParserAction(Reduce, ref table11)},
					{38, new ParserAction(Reduce, ref table11)},
					{41, new ParserAction(Reduce, ref table11)},
					{58, new ParserAction(Reduce, ref table11)},
					{59, new ParserAction(Reduce, ref table11)},
					{60, new ParserAction(Reduce, ref table11)},
					{61, new ParserAction(Reduce, ref table11)},
					{62, new ParserAction(Reduce, ref table11)},
					{63, new ParserAction(Reduce, ref table11)},
					{64, new ParserAction(Reduce, ref table11)},
					{69, new ParserAction(Reduce, ref table11)},
					{71, new ParserAction(Reduce, ref table11)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition36 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table12)},
					{8, new ParserAction(Reduce, ref table12)},
					{9, new ParserAction(Reduce, ref table12)},
					{12, new ParserAction(Reduce, ref table12)},
					{13, new ParserAction(Reduce, ref table12)},
					{19, new ParserAction(Shift, ref table74)},
					{21, new ParserAction(Reduce, ref table12)},
					{36, new ParserAction(Reduce, ref table12)},
					{38, new ParserAction(Reduce, ref table12)},
					{41, new ParserAction(Reduce, ref table12)},
					{58, new ParserAction(Reduce, ref table12)},
					{59, new ParserAction(Reduce, ref table12)},
					{60, new ParserAction(Reduce, ref table12)},
					{61, new ParserAction(Reduce, ref table12)},
					{62, new ParserAction(Reduce, ref table12)},
					{63, new ParserAction(Reduce, ref table12)},
					{64, new ParserAction(Reduce, ref table12)},
					{69, new ParserAction(Reduce, ref table12)},
					{71, new ParserAction(Reduce, ref table12)}
				};

			var tableDefinition37 = new Dictionary<int, ParserAction>
				{
					{15, new ParserAction(None, ref table75)},
					{16, new ParserAction(None, ref table76)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)}
				};

			var tableDefinition38 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table77)},
					{18, new ParserAction(None, ref table36)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition39 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table97)},
					{8, new ParserAction(Reduce, ref table97)},
					{9, new ParserAction(Reduce, ref table97)},
					{12, new ParserAction(Reduce, ref table97)},
					{13, new ParserAction(Reduce, ref table97)},
					{19, new ParserAction(Reduce, ref table97)},
					{21, new ParserAction(Reduce, ref table97)},
					{36, new ParserAction(Reduce, ref table97)},
					{38, new ParserAction(Reduce, ref table97)},
					{41, new ParserAction(Reduce, ref table97)},
					{45, new ParserAction(Reduce, ref table97)},
					{58, new ParserAction(Reduce, ref table97)},
					{59, new ParserAction(Reduce, ref table97)},
					{60, new ParserAction(Reduce, ref table97)},
					{61, new ParserAction(Reduce, ref table97)},
					{62, new ParserAction(Reduce, ref table97)},
					{63, new ParserAction(Reduce, ref table97)},
					{64, new ParserAction(Reduce, ref table97)},
					{69, new ParserAction(Reduce, ref table97)},
					{71, new ParserAction(Reduce, ref table97)},
					{84, new ParserAction(Reduce, ref table97)},
					{86, new ParserAction(Reduce, ref table97)},
					{87, new ParserAction(Reduce, ref table97)},
					{88, new ParserAction(Reduce, ref table97)},
					{89, new ParserAction(Reduce, ref table97)}
				};

			var tableDefinition40 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table98)},
					{8, new ParserAction(Reduce, ref table98)},
					{9, new ParserAction(Reduce, ref table98)},
					{12, new ParserAction(Reduce, ref table98)},
					{13, new ParserAction(Reduce, ref table98)},
					{19, new ParserAction(Reduce, ref table98)},
					{21, new ParserAction(Reduce, ref table98)},
					{36, new ParserAction(Reduce, ref table98)},
					{38, new ParserAction(Reduce, ref table98)},
					{41, new ParserAction(Reduce, ref table98)},
					{45, new ParserAction(Reduce, ref table98)},
					{58, new ParserAction(Reduce, ref table98)},
					{59, new ParserAction(Reduce, ref table98)},
					{60, new ParserAction(Reduce, ref table98)},
					{61, new ParserAction(Reduce, ref table98)},
					{62, new ParserAction(Reduce, ref table98)},
					{63, new ParserAction(Reduce, ref table98)},
					{64, new ParserAction(Reduce, ref table98)},
					{69, new ParserAction(Reduce, ref table98)},
					{71, new ParserAction(Reduce, ref table98)},
					{84, new ParserAction(Reduce, ref table98)},
					{86, new ParserAction(Reduce, ref table98)},
					{87, new ParserAction(Reduce, ref table98)},
					{88, new ParserAction(Reduce, ref table98)},
					{89, new ParserAction(Reduce, ref table98)}
				};

			var tableDefinition41 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table99)},
					{8, new ParserAction(Reduce, ref table99)},
					{9, new ParserAction(Reduce, ref table99)},
					{12, new ParserAction(Reduce, ref table99)},
					{13, new ParserAction(Reduce, ref table99)},
					{19, new ParserAction(Reduce, ref table99)},
					{21, new ParserAction(Reduce, ref table99)},
					{36, new ParserAction(Reduce, ref table99)},
					{38, new ParserAction(Reduce, ref table99)},
					{41, new ParserAction(Reduce, ref table99)},
					{45, new ParserAction(Reduce, ref table99)},
					{58, new ParserAction(Reduce, ref table99)},
					{59, new ParserAction(Reduce, ref table99)},
					{60, new ParserAction(Reduce, ref table99)},
					{61, new ParserAction(Reduce, ref table99)},
					{62, new ParserAction(Reduce, ref table99)},
					{63, new ParserAction(Reduce, ref table99)},
					{64, new ParserAction(Reduce, ref table99)},
					{69, new ParserAction(Reduce, ref table99)},
					{71, new ParserAction(Reduce, ref table99)},
					{84, new ParserAction(Reduce, ref table99)},
					{86, new ParserAction(Reduce, ref table99)},
					{87, new ParserAction(Reduce, ref table99)},
					{88, new ParserAction(Reduce, ref table99)},
					{89, new ParserAction(Reduce, ref table99)}
				};

			var tableDefinition42 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table100)},
					{8, new ParserAction(Reduce, ref table100)},
					{9, new ParserAction(Reduce, ref table100)},
					{12, new ParserAction(Reduce, ref table100)},
					{13, new ParserAction(Reduce, ref table100)},
					{19, new ParserAction(Reduce, ref table100)},
					{21, new ParserAction(Reduce, ref table100)},
					{36, new ParserAction(Reduce, ref table100)},
					{38, new ParserAction(Reduce, ref table100)},
					{41, new ParserAction(Reduce, ref table100)},
					{45, new ParserAction(Reduce, ref table100)},
					{58, new ParserAction(Reduce, ref table100)},
					{59, new ParserAction(Reduce, ref table100)},
					{60, new ParserAction(Reduce, ref table100)},
					{61, new ParserAction(Reduce, ref table100)},
					{62, new ParserAction(Reduce, ref table100)},
					{63, new ParserAction(Reduce, ref table100)},
					{64, new ParserAction(Reduce, ref table100)},
					{69, new ParserAction(Reduce, ref table100)},
					{71, new ParserAction(Reduce, ref table100)},
					{84, new ParserAction(Reduce, ref table100)},
					{86, new ParserAction(Reduce, ref table100)},
					{87, new ParserAction(Reduce, ref table100)},
					{88, new ParserAction(Reduce, ref table100)},
					{89, new ParserAction(Reduce, ref table100)}
				};

			var tableDefinition43 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table101)},
					{8, new ParserAction(Reduce, ref table101)},
					{9, new ParserAction(Reduce, ref table101)},
					{12, new ParserAction(Reduce, ref table101)},
					{13, new ParserAction(Reduce, ref table101)},
					{19, new ParserAction(Reduce, ref table101)},
					{21, new ParserAction(Reduce, ref table101)},
					{36, new ParserAction(Reduce, ref table101)},
					{38, new ParserAction(Reduce, ref table101)},
					{41, new ParserAction(Reduce, ref table101)},
					{58, new ParserAction(Reduce, ref table101)},
					{59, new ParserAction(Reduce, ref table101)},
					{60, new ParserAction(Reduce, ref table101)},
					{61, new ParserAction(Reduce, ref table101)},
					{62, new ParserAction(Reduce, ref table101)},
					{63, new ParserAction(Reduce, ref table101)},
					{64, new ParserAction(Reduce, ref table101)},
					{69, new ParserAction(Reduce, ref table101)},
					{71, new ParserAction(Reduce, ref table101)}
				};

			var tableDefinition44 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table6)},
					{5, new ParserAction(None, ref table78)},
					{9, new ParserAction(Reduce, ref table6)},
					{10, new ParserAction(None, ref table5)},
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table19)},
					{18, new ParserAction(None, ref table36)},
					{20, new ParserAction(None, ref table7)},
					{22, new ParserAction(None, ref table8)},
					{23, new ParserAction(None, ref table9)},
					{24, new ParserAction(None, ref table10)},
					{25, new ParserAction(None, ref table11)},
					{26, new ParserAction(None, ref table12)},
					{27, new ParserAction(None, ref table13)},
					{28, new ParserAction(None, ref table14)},
					{29, new ParserAction(None, ref table15)},
					{30, new ParserAction(Shift, ref table16)},
					{32, new ParserAction(Shift, ref table17)},
					{34, new ParserAction(Shift, ref table18)},
					{35, new ParserAction(None, ref table20)},
					{39, new ParserAction(Shift, ref table37)},
					{40, new ParserAction(None, ref table21)},
					{43, new ParserAction(Shift, ref table38)},
					{44, new ParserAction(Shift, ref table24)},
					{46, new ParserAction(Shift, ref table22)},
					{47, new ParserAction(Shift, ref table23)},
					{49, new ParserAction(Shift, ref table29)},
					{51, new ParserAction(Shift, ref table30)},
					{52, new ParserAction(Shift, ref table31)},
					{53, new ParserAction(Shift, ref table32)},
					{54, new ParserAction(Shift, ref table33)},
					{55, new ParserAction(Shift, ref table34)},
					{65, new ParserAction(Shift, ref table25)},
					{66, new ParserAction(Shift, ref table26)},
					{68, new ParserAction(Shift, ref table27)},
					{72, new ParserAction(Shift, ref table28)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition45 = new Dictionary<int, ParserAction>
				{
					{5, new ParserAction(None, ref table79)},
					{10, new ParserAction(None, ref table5)},
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table19)},
					{18, new ParserAction(None, ref table36)},
					{20, new ParserAction(None, ref table7)},
					{22, new ParserAction(None, ref table8)},
					{23, new ParserAction(None, ref table9)},
					{24, new ParserAction(None, ref table10)},
					{25, new ParserAction(None, ref table11)},
					{26, new ParserAction(None, ref table12)},
					{27, new ParserAction(None, ref table13)},
					{28, new ParserAction(None, ref table14)},
					{29, new ParserAction(None, ref table15)},
					{30, new ParserAction(Shift, ref table16)},
					{32, new ParserAction(Shift, ref table17)},
					{34, new ParserAction(Shift, ref table18)},
					{35, new ParserAction(None, ref table20)},
					{39, new ParserAction(Shift, ref table37)},
					{40, new ParserAction(None, ref table21)},
					{43, new ParserAction(Shift, ref table38)},
					{44, new ParserAction(Shift, ref table24)},
					{46, new ParserAction(Shift, ref table22)},
					{47, new ParserAction(Shift, ref table23)},
					{49, new ParserAction(Shift, ref table29)},
					{51, new ParserAction(Shift, ref table30)},
					{52, new ParserAction(Shift, ref table31)},
					{53, new ParserAction(Shift, ref table32)},
					{54, new ParserAction(Shift, ref table33)},
					{55, new ParserAction(Shift, ref table34)},
					{65, new ParserAction(Shift, ref table25)},
					{66, new ParserAction(Shift, ref table26)},
					{68, new ParserAction(Shift, ref table27)},
					{72, new ParserAction(Shift, ref table28)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition46 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table17)},
					{8, new ParserAction(Reduce, ref table17)},
					{9, new ParserAction(Reduce, ref table17)}
				};

			var tableDefinition47 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table26)},
					{8, new ParserAction(Reduce, ref table26)},
					{9, new ParserAction(Reduce, ref table26)}
				};

			var tableDefinition48 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table27)},
					{8, new ParserAction(Reduce, ref table27)},
					{9, new ParserAction(Reduce, ref table27)}
				};

			var tableDefinition49 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Shift, ref table81)},
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table80)},
					{18, new ParserAction(None, ref table36)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition50 = new Dictionary<int, ParserAction>
				{
					{48, new ParserAction(None, ref table82)},
					{56, new ParserAction(None, ref table52)},
					{57, new ParserAction(None, ref table53)},
					{58, new ParserAction(Shift, ref table54)},
					{59, new ParserAction(Shift, ref table55)},
					{60, new ParserAction(Shift, ref table56)},
					{61, new ParserAction(Shift, ref table57)},
					{62, new ParserAction(Shift, ref table58)},
					{63, new ParserAction(Shift, ref table59)},
					{64, new ParserAction(Shift, ref table60)}
				};

			var tableDefinition51 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table45)},
					{8, new ParserAction(Reduce, ref table45)},
					{9, new ParserAction(Reduce, ref table45)}
				};

			var tableDefinition52 = new Dictionary<int, ParserAction>
				{
					{57, new ParserAction(None, ref table83)},
					{63, new ParserAction(Shift, ref table59)},
					{64, new ParserAction(Shift, ref table60)}
				};

			var tableDefinition53 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Reduce, ref table61)},
					{56, new ParserAction(None, ref table84)},
					{58, new ParserAction(Shift, ref table54)},
					{59, new ParserAction(Shift, ref table55)},
					{60, new ParserAction(Shift, ref table56)},
					{61, new ParserAction(Shift, ref table57)},
					{62, new ParserAction(Shift, ref table58)},
					{84, new ParserAction(Reduce, ref table61)},
					{86, new ParserAction(Reduce, ref table61)},
					{87, new ParserAction(Reduce, ref table61)},
					{88, new ParserAction(Reduce, ref table61)},
					{89, new ParserAction(Reduce, ref table61)}
				};

			var tableDefinition54 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Reduce, ref table62)},
					{63, new ParserAction(Reduce, ref table62)},
					{64, new ParserAction(Reduce, ref table62)},
					{84, new ParserAction(Reduce, ref table62)},
					{86, new ParserAction(Reduce, ref table62)},
					{87, new ParserAction(Reduce, ref table62)},
					{88, new ParserAction(Reduce, ref table62)},
					{89, new ParserAction(Reduce, ref table62)}
				};

			var tableDefinition55 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Reduce, ref table63)},
					{63, new ParserAction(Reduce, ref table63)},
					{64, new ParserAction(Reduce, ref table63)},
					{84, new ParserAction(Reduce, ref table63)},
					{86, new ParserAction(Reduce, ref table63)},
					{87, new ParserAction(Reduce, ref table63)},
					{88, new ParserAction(Reduce, ref table63)},
					{89, new ParserAction(Reduce, ref table63)}
				};

			var tableDefinition56 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Reduce, ref table64)},
					{63, new ParserAction(Reduce, ref table64)},
					{64, new ParserAction(Reduce, ref table64)},
					{84, new ParserAction(Reduce, ref table64)},
					{86, new ParserAction(Reduce, ref table64)},
					{87, new ParserAction(Reduce, ref table64)},
					{88, new ParserAction(Reduce, ref table64)},
					{89, new ParserAction(Reduce, ref table64)}
				};

			var tableDefinition57 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Reduce, ref table65)},
					{63, new ParserAction(Reduce, ref table65)},
					{64, new ParserAction(Reduce, ref table65)},
					{84, new ParserAction(Reduce, ref table65)},
					{86, new ParserAction(Reduce, ref table65)},
					{87, new ParserAction(Reduce, ref table65)},
					{88, new ParserAction(Reduce, ref table65)},
					{89, new ParserAction(Reduce, ref table65)}
				};

			var tableDefinition58 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Reduce, ref table66)},
					{63, new ParserAction(Reduce, ref table66)},
					{64, new ParserAction(Reduce, ref table66)},
					{84, new ParserAction(Reduce, ref table66)},
					{86, new ParserAction(Reduce, ref table66)},
					{87, new ParserAction(Reduce, ref table66)},
					{88, new ParserAction(Reduce, ref table66)},
					{89, new ParserAction(Reduce, ref table66)}
				};

			var tableDefinition59 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Reduce, ref table67)},
					{58, new ParserAction(Reduce, ref table67)},
					{59, new ParserAction(Reduce, ref table67)},
					{60, new ParserAction(Reduce, ref table67)},
					{61, new ParserAction(Reduce, ref table67)},
					{62, new ParserAction(Reduce, ref table67)},
					{84, new ParserAction(Reduce, ref table67)},
					{86, new ParserAction(Reduce, ref table67)},
					{87, new ParserAction(Reduce, ref table67)},
					{88, new ParserAction(Reduce, ref table67)},
					{89, new ParserAction(Reduce, ref table67)}
				};

			var tableDefinition60 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Reduce, ref table68)},
					{58, new ParserAction(Reduce, ref table68)},
					{59, new ParserAction(Reduce, ref table68)},
					{60, new ParserAction(Reduce, ref table68)},
					{61, new ParserAction(Reduce, ref table68)},
					{62, new ParserAction(Reduce, ref table68)},
					{84, new ParserAction(Reduce, ref table68)},
					{86, new ParserAction(Reduce, ref table68)},
					{87, new ParserAction(Reduce, ref table68)},
					{88, new ParserAction(Reduce, ref table68)},
					{89, new ParserAction(Reduce, ref table68)}
				};

			var tableDefinition61 = new Dictionary<int, ParserAction>
				{
					{8, new ParserAction(Shift, ref table86)},
					{23, new ParserAction(None, ref table87)},
					{37, new ParserAction(None, ref table85)},
					{40, new ParserAction(None, ref table21)},
					{43, new ParserAction(Shift, ref table38)}
				};

			var tableDefinition62 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table88)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)}
				};

			var tableDefinition63 = new Dictionary<int, ParserAction>
				{
					{42, new ParserAction(None, ref table89)},
					{46, new ParserAction(Shift, ref table90)}
				};

			var tableDefinition64 = new Dictionary<int, ParserAction>
				{
					{45, new ParserAction(Shift, ref table91)}
				};

			var tableDefinition65 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Shift, ref table92)}
				};

			var tableDefinition66 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Shift, ref table93)}
				};

			var tableDefinition67 = new Dictionary<int, ParserAction>
				{
					{69, new ParserAction(Shift, ref table94)},
					{71, new ParserAction(Shift, ref table95)}
				};

			var tableDefinition68 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table96)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)}
				};

			var tableDefinition69 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Shift, ref table71)},
					{50, new ParserAction(None, ref table97)}
				};

			var tableDefinition70 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table53)},
					{8, new ParserAction(Reduce, ref table53)},
					{9, new ParserAction(Reduce, ref table53)}
				};

			var tableDefinition71 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table102)},
					{8, new ParserAction(Reduce, ref table102)},
					{9, new ParserAction(Reduce, ref table102)}
				};

			var tableDefinition72 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table13)},
					{8, new ParserAction(Reduce, ref table13)},
					{9, new ParserAction(Reduce, ref table13)},
					{12, new ParserAction(Reduce, ref table13)},
					{13, new ParserAction(Reduce, ref table13)},
					{21, new ParserAction(Reduce, ref table13)},
					{36, new ParserAction(Reduce, ref table13)},
					{38, new ParserAction(Reduce, ref table13)},
					{41, new ParserAction(Reduce, ref table13)},
					{58, new ParserAction(Reduce, ref table13)},
					{59, new ParserAction(Reduce, ref table13)},
					{60, new ParserAction(Reduce, ref table13)},
					{61, new ParserAction(Reduce, ref table13)},
					{62, new ParserAction(Reduce, ref table13)},
					{63, new ParserAction(Reduce, ref table13)},
					{64, new ParserAction(Reduce, ref table13)},
					{69, new ParserAction(Reduce, ref table13)},
					{71, new ParserAction(Reduce, ref table13)}
				};

			var tableDefinition73 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table14)},
					{8, new ParserAction(Reduce, ref table14)},
					{9, new ParserAction(Reduce, ref table14)},
					{12, new ParserAction(Reduce, ref table14)},
					{13, new ParserAction(Reduce, ref table14)},
					{21, new ParserAction(Reduce, ref table14)},
					{36, new ParserAction(Reduce, ref table14)},
					{38, new ParserAction(Reduce, ref table14)},
					{41, new ParserAction(Reduce, ref table14)},
					{58, new ParserAction(Reduce, ref table14)},
					{59, new ParserAction(Reduce, ref table14)},
					{60, new ParserAction(Reduce, ref table14)},
					{61, new ParserAction(Reduce, ref table14)},
					{62, new ParserAction(Reduce, ref table14)},
					{63, new ParserAction(Reduce, ref table14)},
					{64, new ParserAction(Reduce, ref table14)},
					{69, new ParserAction(Reduce, ref table14)},
					{71, new ParserAction(Reduce, ref table14)}
				};

			var tableDefinition74 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table15)},
					{8, new ParserAction(Reduce, ref table15)},
					{9, new ParserAction(Reduce, ref table15)},
					{12, new ParserAction(Reduce, ref table15)},
					{13, new ParserAction(Reduce, ref table15)},
					{21, new ParserAction(Reduce, ref table15)},
					{36, new ParserAction(Reduce, ref table15)},
					{38, new ParserAction(Reduce, ref table15)},
					{41, new ParserAction(Reduce, ref table15)},
					{58, new ParserAction(Reduce, ref table15)},
					{59, new ParserAction(Reduce, ref table15)},
					{60, new ParserAction(Reduce, ref table15)},
					{61, new ParserAction(Reduce, ref table15)},
					{62, new ParserAction(Reduce, ref table15)},
					{63, new ParserAction(Reduce, ref table15)},
					{64, new ParserAction(Reduce, ref table15)},
					{69, new ParserAction(Reduce, ref table15)},
					{71, new ParserAction(Reduce, ref table15)}
				};

			var tableDefinition75 = new Dictionary<int, ParserAction>
				{
					{36, new ParserAction(Reduce, ref table31)}
				};

			var tableDefinition76 = new Dictionary<int, ParserAction>
				{
					{15, new ParserAction(None, ref table98)},
					{16, new ParserAction(None, ref table76)},
					{36, new ParserAction(Reduce, ref table9)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)}
				};

			var tableDefinition77 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table39)},
					{8, new ParserAction(Reduce, ref table39)},
					{9, new ParserAction(Reduce, ref table39)},
					{11, new ParserAction(None, ref table99)},
					{12, new ParserAction(Shift, ref table100)},
					{36, new ParserAction(Reduce, ref table39)},
					{38, new ParserAction(Reduce, ref table39)},
					{41, new ParserAction(Reduce, ref table39)}
				};

			var tableDefinition78 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table7)},
					{9, new ParserAction(Reduce, ref table7)}
				};

			var tableDefinition79 = new Dictionary<int, ParserAction>
				{
					{9, new ParserAction(Shift, ref table101)}
				};

			var tableDefinition80 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table48)},
					{8, new ParserAction(Reduce, ref table48)},
					{9, new ParserAction(Reduce, ref table48)},
					{21, new ParserAction(Reduce, ref table48)}
				};

			var tableDefinition81 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table102)},
					{18, new ParserAction(None, ref table36)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition82 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Shift, ref table104)},
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table103)},
					{18, new ParserAction(None, ref table36)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition83 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Reduce, ref table60)},
					{56, new ParserAction(None, ref table105)},
					{58, new ParserAction(Shift, ref table54)},
					{59, new ParserAction(Shift, ref table55)},
					{60, new ParserAction(Shift, ref table56)},
					{61, new ParserAction(Shift, ref table57)},
					{62, new ParserAction(Shift, ref table58)},
					{84, new ParserAction(Reduce, ref table60)},
					{86, new ParserAction(Reduce, ref table60)},
					{87, new ParserAction(Reduce, ref table60)},
					{88, new ParserAction(Reduce, ref table60)},
					{89, new ParserAction(Reduce, ref table60)}
				};

			var tableDefinition84 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Reduce, ref table59)},
					{84, new ParserAction(Reduce, ref table59)},
					{86, new ParserAction(Reduce, ref table59)},
					{87, new ParserAction(Reduce, ref table59)},
					{88, new ParserAction(Reduce, ref table59)},
					{89, new ParserAction(Reduce, ref table59)}
				};

			var tableDefinition85 = new Dictionary<int, ParserAction>
				{
					{38, new ParserAction(Shift, ref table106)}
				};

			var tableDefinition86 = new Dictionary<int, ParserAction>
				{
					{23, new ParserAction(None, ref table87)},
					{37, new ParserAction(None, ref table107)},
					{40, new ParserAction(None, ref table21)},
					{43, new ParserAction(Shift, ref table38)}
				};

			var tableDefinition87 = new Dictionary<int, ParserAction>
				{
					{8, new ParserAction(Shift, ref table108)},
					{38, new ParserAction(Reduce, ref table32)}
				};

			var tableDefinition88 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table36)},
					{8, new ParserAction(Reduce, ref table36)},
					{9, new ParserAction(Reduce, ref table36)},
					{36, new ParserAction(Shift, ref table109)},
					{38, new ParserAction(Reduce, ref table36)}
				};

			var tableDefinition89 = new Dictionary<int, ParserAction>
				{
					{38, new ParserAction(Shift, ref table110)}
				};

			var tableDefinition90 = new Dictionary<int, ParserAction>
				{
					{38, new ParserAction(Reduce, ref table42)},
					{42, new ParserAction(None, ref table111)},
					{46, new ParserAction(Shift, ref table90)}
				};

			var tableDefinition91 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table112)},
					{18, new ParserAction(None, ref table36)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition92 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table69)},
					{8, new ParserAction(Reduce, ref table69)},
					{9, new ParserAction(Reduce, ref table69)},
					{13, new ParserAction(Shift, ref table113)}
				};

			var tableDefinition93 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table71)},
					{8, new ParserAction(Reduce, ref table71)},
					{9, new ParserAction(Reduce, ref table71)},
					{13, new ParserAction(Shift, ref table115)},
					{67, new ParserAction(Shift, ref table114)}
				};

			var tableDefinition94 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table75)},
					{8, new ParserAction(Reduce, ref table75)},
					{9, new ParserAction(Reduce, ref table75)},
					{13, new ParserAction(Shift, ref table116)},
					{70, new ParserAction(Shift, ref table117)}
				};

			var tableDefinition95 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Shift, ref table118)}
				};

			var tableDefinition96 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table83)},
					{8, new ParserAction(Reduce, ref table83)},
					{9, new ParserAction(Reduce, ref table83)}
				};

			var tableDefinition97 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table52)},
					{8, new ParserAction(Reduce, ref table52)},
					{9, new ParserAction(Reduce, ref table52)}
				};

			var tableDefinition98 = new Dictionary<int, ParserAction>
				{
					{36, new ParserAction(Reduce, ref table10)}
				};

			var tableDefinition99 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table40)},
					{8, new ParserAction(Reduce, ref table40)},
					{9, new ParserAction(Reduce, ref table40)},
					{36, new ParserAction(Reduce, ref table40)},
					{38, new ParserAction(Reduce, ref table40)},
					{41, new ParserAction(Reduce, ref table40)}
				};

			var tableDefinition100 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Shift, ref table119)}
				};

			var tableDefinition101 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table4)}
				};

			var tableDefinition102 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table50)},
					{8, new ParserAction(Reduce, ref table50)},
					{9, new ParserAction(Reduce, ref table50)},
					{21, new ParserAction(Reduce, ref table50)}
				};

			var tableDefinition103 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table49)},
					{8, new ParserAction(Reduce, ref table49)},
					{9, new ParserAction(Reduce, ref table49)},
					{21, new ParserAction(Reduce, ref table49)}
				};

			var tableDefinition104 = new Dictionary<int, ParserAction>
				{
					{16, new ParserAction(None, ref table35)},
					{17, new ParserAction(None, ref table120)},
					{18, new ParserAction(None, ref table36)},
					{84, new ParserAction(Shift, ref table42)},
					{86, new ParserAction(Shift, ref table39)},
					{87, new ParserAction(Shift, ref table40)},
					{88, new ParserAction(Shift, ref table41)},
					{89, new ParserAction(Shift, ref table43)}
				};

			var tableDefinition105 = new Dictionary<int, ParserAction>
				{
					{13, new ParserAction(Reduce, ref table58)},
					{84, new ParserAction(Reduce, ref table58)},
					{86, new ParserAction(Reduce, ref table58)},
					{87, new ParserAction(Reduce, ref table58)},
					{88, new ParserAction(Reduce, ref table58)},
					{89, new ParserAction(Reduce, ref table58)}
				};

			var tableDefinition106 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table29)},
					{8, new ParserAction(Reduce, ref table29)},
					{9, new ParserAction(Reduce, ref table29)}
				};

			var tableDefinition107 = new Dictionary<int, ParserAction>
				{
					{38, new ParserAction(Shift, ref table121)}
				};

			var tableDefinition108 = new Dictionary<int, ParserAction>
				{
					{23, new ParserAction(None, ref table87)},
					{37, new ParserAction(None, ref table122)},
					{38, new ParserAction(Reduce, ref table33)},
					{40, new ParserAction(None, ref table21)},
					{43, new ParserAction(Shift, ref table38)}
				};

			var tableDefinition109 = new Dictionary<int, ParserAction>
				{
					{42, new ParserAction(None, ref table123)},
					{46, new ParserAction(Shift, ref table90)}
				};

			var tableDefinition110 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table37)},
					{8, new ParserAction(Reduce, ref table37)},
					{9, new ParserAction(Reduce, ref table37)},
					{38, new ParserAction(Reduce, ref table37)}
				};

			var tableDefinition111 = new Dictionary<int, ParserAction>
				{
					{38, new ParserAction(Reduce, ref table43)}
				};

			var tableDefinition112 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table41)},
					{8, new ParserAction(Reduce, ref table41)},
					{9, new ParserAction(Reduce, ref table41)}
				};

			var tableDefinition113 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table70)},
					{8, new ParserAction(Reduce, ref table70)},
					{9, new ParserAction(Reduce, ref table70)}
				};

			var tableDefinition114 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table72)},
					{8, new ParserAction(Reduce, ref table72)},
					{9, new ParserAction(Reduce, ref table72)}
				};

			var tableDefinition115 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table73)},
					{8, new ParserAction(Reduce, ref table73)},
					{9, new ParserAction(Reduce, ref table73)},
					{67, new ParserAction(Shift, ref table124)}
				};

			var tableDefinition116 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table76)},
					{8, new ParserAction(Reduce, ref table76)},
					{9, new ParserAction(Reduce, ref table76)}
				};

			var tableDefinition117 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table77)},
					{8, new ParserAction(Reduce, ref table77)},
					{9, new ParserAction(Reduce, ref table77)},
					{13, new ParserAction(Shift, ref table125)}
				};

			var tableDefinition118 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table79)},
					{8, new ParserAction(Reduce, ref table79)},
					{9, new ParserAction(Reduce, ref table79)},
					{13, new ParserAction(Shift, ref table127)},
					{67, new ParserAction(Shift, ref table126)}
				};

			var tableDefinition119 = new Dictionary<int, ParserAction>
				{
					{14, new ParserAction(Shift, ref table128)}
				};

			var tableDefinition120 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table51)},
					{8, new ParserAction(Reduce, ref table51)},
					{9, new ParserAction(Reduce, ref table51)},
					{21, new ParserAction(Reduce, ref table51)}
				};

			var tableDefinition121 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table30)},
					{8, new ParserAction(Reduce, ref table30)},
					{9, new ParserAction(Reduce, ref table30)}
				};

			var tableDefinition122 = new Dictionary<int, ParserAction>
				{
					{38, new ParserAction(Reduce, ref table34)}
				};

			var tableDefinition123 = new Dictionary<int, ParserAction>
				{
					{38, new ParserAction(Shift, ref table129)}
				};

			var tableDefinition124 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table74)},
					{8, new ParserAction(Reduce, ref table74)},
					{9, new ParserAction(Reduce, ref table74)}
				};

			var tableDefinition125 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table78)},
					{8, new ParserAction(Reduce, ref table78)},
					{9, new ParserAction(Reduce, ref table78)}
				};

			var tableDefinition126 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table80)},
					{8, new ParserAction(Reduce, ref table80)},
					{9, new ParserAction(Reduce, ref table80)}
				};

			var tableDefinition127 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table81)},
					{8, new ParserAction(Reduce, ref table81)},
					{9, new ParserAction(Reduce, ref table81)},
					{67, new ParserAction(Shift, ref table130)}
				};

			var tableDefinition128 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table8)},
					{8, new ParserAction(Reduce, ref table8)},
					{9, new ParserAction(Reduce, ref table8)},
					{36, new ParserAction(Reduce, ref table8)},
					{38, new ParserAction(Reduce, ref table8)},
					{41, new ParserAction(Reduce, ref table8)}
				};

			var tableDefinition129 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table38)},
					{8, new ParserAction(Reduce, ref table38)},
					{9, new ParserAction(Reduce, ref table38)},
					{38, new ParserAction(Reduce, ref table38)}
				};

			var tableDefinition130 = new Dictionary<int, ParserAction>
				{
					{1, new ParserAction(Reduce, ref table82)},
					{8, new ParserAction(Reduce, ref table82)},
					{9, new ParserAction(Reduce, ref table82)}
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
			table62.SetActions(ref tableDefinition62);
			table63.SetActions(ref tableDefinition63);
			table64.SetActions(ref tableDefinition64);
			table65.SetActions(ref tableDefinition65);
			table66.SetActions(ref tableDefinition66);
			table67.SetActions(ref tableDefinition67);
			table68.SetActions(ref tableDefinition68);
			table69.SetActions(ref tableDefinition69);
			table70.SetActions(ref tableDefinition70);
			table71.SetActions(ref tableDefinition71);
			table72.SetActions(ref tableDefinition72);
			table73.SetActions(ref tableDefinition73);
			table74.SetActions(ref tableDefinition74);
			table75.SetActions(ref tableDefinition75);
			table76.SetActions(ref tableDefinition76);
			table77.SetActions(ref tableDefinition77);
			table78.SetActions(ref tableDefinition78);
			table79.SetActions(ref tableDefinition79);
			table80.SetActions(ref tableDefinition80);
			table81.SetActions(ref tableDefinition81);
			table82.SetActions(ref tableDefinition82);
			table83.SetActions(ref tableDefinition83);
			table84.SetActions(ref tableDefinition84);
			table85.SetActions(ref tableDefinition85);
			table86.SetActions(ref tableDefinition86);
			table87.SetActions(ref tableDefinition87);
			table88.SetActions(ref tableDefinition88);
			table89.SetActions(ref tableDefinition89);
			table90.SetActions(ref tableDefinition90);
			table91.SetActions(ref tableDefinition91);
			table92.SetActions(ref tableDefinition92);
			table93.SetActions(ref tableDefinition93);
			table94.SetActions(ref tableDefinition94);
			table95.SetActions(ref tableDefinition95);
			table96.SetActions(ref tableDefinition96);
			table97.SetActions(ref tableDefinition97);
			table98.SetActions(ref tableDefinition98);
			table99.SetActions(ref tableDefinition99);
			table100.SetActions(ref tableDefinition100);
			table101.SetActions(ref tableDefinition101);
			table102.SetActions(ref tableDefinition102);
			table103.SetActions(ref tableDefinition103);
			table104.SetActions(ref tableDefinition104);
			table105.SetActions(ref tableDefinition105);
			table106.SetActions(ref tableDefinition106);
			table107.SetActions(ref tableDefinition107);
			table108.SetActions(ref tableDefinition108);
			table109.SetActions(ref tableDefinition109);
			table110.SetActions(ref tableDefinition110);
			table111.SetActions(ref tableDefinition111);
			table112.SetActions(ref tableDefinition112);
			table113.SetActions(ref tableDefinition113);
			table114.SetActions(ref tableDefinition114);
			table115.SetActions(ref tableDefinition115);
			table116.SetActions(ref tableDefinition116);
			table117.SetActions(ref tableDefinition117);
			table118.SetActions(ref tableDefinition118);
			table119.SetActions(ref tableDefinition119);
			table120.SetActions(ref tableDefinition120);
			table121.SetActions(ref tableDefinition121);
			table122.SetActions(ref tableDefinition122);
			table123.SetActions(ref tableDefinition123);
			table124.SetActions(ref tableDefinition124);
			table125.SetActions(ref tableDefinition125);
			table126.SetActions(ref tableDefinition126);
			table127.SetActions(ref tableDefinition127);
			table128.SetActions(ref tableDefinition128);
			table129.SetActions(ref tableDefinition129);
			table130.SetActions(ref tableDefinition130);

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
					{61, table61},
					{62, table62},
					{63, table63},
					{64, table64},
					{65, table65},
					{66, table66},
					{67, table67},
					{68, table68},
					{69, table69},
					{70, table70},
					{71, table71},
					{72, table72},
					{73, table73},
					{74, table74},
					{75, table75},
					{76, table76},
					{77, table77},
					{78, table78},
					{79, table79},
					{80, table80},
					{81, table81},
					{82, table82},
					{83, table83},
					{84, table84},
					{85, table85},
					{86, table86},
					{87, table87},
					{88, table88},
					{89, table89},
					{90, table90},
					{91, table91},
					{92, table92},
					{93, table93},
					{94, table94},
					{95, table95},
					{96, table96},
					{97, table97},
					{98, table98},
					{99, table99},
					{100, table100},
					{101, table101},
					{102, table102},
					{103, table103},
					{104, table104},
					{105, table105},
					{106, table106},
					{107, table107},
					{108, table108},
					{109, table109},
					{110, table110},
					{111, table111},
					{112, table112},
					{113, table113},
					{114, table114},
					{115, table115},
					{116, table116},
					{117, table117},
					{118, table118},
					{119, table119},
					{120, table120},
					{121, table121},
					{122, table122},
					{123, table123},
					{124, table124},
					{125, table125},
					{126, table126},
					{127, table127},
					{128, table128},
					{129, table129},
					{130, table130}
				};

			DefaultActions = new Dictionary<int, ParserAction>
				{
					{2, new ParserAction(Reduce, ref table1)},
					{3, new ParserAction(Reduce, ref table2)},
					{4, new ParserAction(Reduce, ref table3)},
					{75, new ParserAction(Reduce, ref table31)},
					{98, new ParserAction(Reduce, ref table10)},
					{101, new ParserAction(Reduce, ref table4)},
					{111, new ParserAction(Reduce, ref table43)},
					{122, new ParserAction(Reduce, ref table34)}
				};

			Productions = new Dictionary<int, ParserProduction>
				{				
					{0, new ParserProduction(symbol0)},
					{1, new ParserProduction(symbol3,1)},
					{2, new ParserProduction(symbol3,1)},
					{3, new ParserProduction(symbol4,1)},
					{4, new ParserProduction(symbol6,4)},
					{5, new ParserProduction(symbol5,1)},
					{6, new ParserProduction(symbol5,2)},
					{7, new ParserProduction(symbol5,3)},
					{8, new ParserProduction(symbol11,3)},
					{9, new ParserProduction(symbol15,1)},
					{10, new ParserProduction(symbol15,2)},
					{11, new ParserProduction(symbol17,1)},
					{12, new ParserProduction(symbol17,1)},
					{13, new ParserProduction(symbol17,2)},
					{14, new ParserProduction(symbol17,2)},
					{15, new ParserProduction(symbol17,2)},
					{16, new ParserProduction(symbol10,1)},
					{17, new ParserProduction(symbol10,2)},
					{18, new ParserProduction(symbol10,1)},
					{19, new ParserProduction(symbol10,1)},
					{20, new ParserProduction(symbol10,1)},
					{21, new ParserProduction(symbol10,1)},
					{22, new ParserProduction(symbol10,1)},
					{23, new ParserProduction(symbol10,1)},
					{24, new ParserProduction(symbol10,1)},
					{25, new ParserProduction(symbol10,1)},
					{26, new ParserProduction(symbol10,2)},
					{27, new ParserProduction(symbol10,2)},
					{28, new ParserProduction(symbol10,1)},
					{29, new ParserProduction(symbol22,4)},
					{30, new ParserProduction(symbol22,5)},
					{31, new ParserProduction(symbol35,2)},
					{32, new ParserProduction(symbol37,1)},
					{33, new ParserProduction(symbol37,2)},
					{34, new ParserProduction(symbol37,3)},
					{35, new ParserProduction(symbol23,1)},
					{36, new ParserProduction(symbol23,3)},
					{37, new ParserProduction(symbol23,4)},
					{38, new ParserProduction(symbol23,6)},
					{39, new ParserProduction(symbol40,2)},
					{40, new ParserProduction(symbol40,3)},
					{41, new ParserProduction(symbol25,4)},
					{42, new ParserProduction(symbol42,1)},
					{43, new ParserProduction(symbol42,2)},
					{44, new ParserProduction(symbol24,1)},
					{45, new ParserProduction(symbol24,2)},
					{46, new ParserProduction(symbol24,1)},
					{47, new ParserProduction(symbol24,1)},
					{48, new ParserProduction(symbol20,3)},
					{49, new ParserProduction(symbol20,4)},
					{50, new ParserProduction(symbol20,4)},
					{51, new ParserProduction(symbol20,5)},
					{52, new ParserProduction(symbol28,3)},
					{53, new ParserProduction(symbol28,2)},
					{54, new ParserProduction(symbol29,1)},
					{55, new ParserProduction(symbol29,1)},
					{56, new ParserProduction(symbol29,1)},
					{57, new ParserProduction(symbol29,1)},
					{58, new ParserProduction(symbol48,3)},
					{59, new ParserProduction(symbol48,2)},
					{60, new ParserProduction(symbol48,2)},
					{61, new ParserProduction(symbol48,1)},
					{62, new ParserProduction(symbol56,1)},
					{63, new ParserProduction(symbol56,1)},
					{64, new ParserProduction(symbol56,1)},
					{65, new ParserProduction(symbol56,1)},
					{66, new ParserProduction(symbol56,1)},
					{67, new ParserProduction(symbol57,1)},
					{68, new ParserProduction(symbol57,1)},
					{69, new ParserProduction(symbol26,3)},
					{70, new ParserProduction(symbol26,4)},
					{71, new ParserProduction(symbol26,3)},
					{72, new ParserProduction(symbol26,4)},
					{73, new ParserProduction(symbol26,4)},
					{74, new ParserProduction(symbol26,5)},
					{75, new ParserProduction(symbol26,3)},
					{76, new ParserProduction(symbol26,4)},
					{77, new ParserProduction(symbol26,4)},
					{78, new ParserProduction(symbol26,5)},
					{79, new ParserProduction(symbol26,4)},
					{80, new ParserProduction(symbol26,5)},
					{81, new ParserProduction(symbol26,5)},
					{82, new ParserProduction(symbol26,6)},
					{83, new ParserProduction(symbol27,3)},
					{84, new ParserProduction(symbol73,1)},
					{85, new ParserProduction(symbol73,1)},
					{86, new ParserProduction(symbol74,1)},
					{87, new ParserProduction(symbol74,1)},
					{88, new ParserProduction(symbol74,1)},
					{89, new ParserProduction(symbol74,1)},
					{90, new ParserProduction(symbol74,1)},
					{91, new ParserProduction(symbol74,1)},
					{92, new ParserProduction(symbol74,1)},
					{93, new ParserProduction(symbol76,1)},
					{94, new ParserProduction(symbol76,1)},
					{95, new ParserProduction(symbol76,1)},
					{96, new ParserProduction(symbol76,1)},
					{97, new ParserProduction(symbol16,1)},
					{98, new ParserProduction(symbol16,1)},
					{99, new ParserProduction(symbol16,1)},
					{100, new ParserProduction(symbol16,1)},
					{101, new ParserProduction(symbol18,1)},
					{102, new ParserProduction(symbol50,1)}
				};



			
			//Setup Lexer
			
			Rules = new Dictionary<int, Regex>
				{
					{0, new Regex(@"^(?:.*direction\s+TB[^\n]*)")},
					{1, new Regex(@"^(?:.*direction\s+BT[^\n]*)")},
					{2, new Regex(@"^(?:.*direction\s+RL[^\n]*)")},
					{3, new Regex(@"^(?:.*direction\s+LR[^\n]*)")},
					{4, new Regex(@"^(?:%%(?!\{)*[^\n]*(\r?\n?)+)")},
					{5, new Regex(@"^(?:%%[^\n]*(\r?\n)*)")},
					{6, new Regex(@"^(?:accTitle\s*:\s*)")},
					{7, new Regex(@"^(?:(?!\n||)*[^\n]*)")},
					{8, new Regex(@"^(?:accDescr\s*:\s*)")},
					{9, new Regex(@"^(?:(?!\n||)*[^\n]*)")},
					{10, new Regex(@"^(?:accDescr\s*\{\s*)")},
					{11, new Regex(@"^(?:[\}])")},
					{12, new Regex(@"^(?:[^\}]*)")},
					{13, new Regex(@"^(?:\s*(\r?\n)+)")},
					{14, new Regex(@"^(?:\s+)")},
					{15, new Regex(@"^(?:classDiagram-v2\b)")},
					{16, new Regex(@"^(?:classDiagram\b)")},
					{17, new Regex(@"^(?:\[\*\])")},
					{18, new Regex(@"^(?:call[\s]+)")},
					{19, new Regex(@"^(?:\([\s]*\))")},
					{20, new Regex(@"^(?:\()")},
					{21, new Regex(@"^(?:[^(]*)")},
					{22, new Regex(@"^(?:\))")},
					{23, new Regex(@"^(?:[^)]*)")},
					{24, new Regex(@"^(?:[""])")},
					{25, new Regex(@"^(?:[^""]*)")},
					{26, new Regex(@"^(?:[""])")},
					{27, new Regex(@"^(?:namespace\b)")},
					{28, new Regex(@"^(?:\s*(\r?\n)+)")},
					{29, new Regex(@"^(?:\s+)")},
					{30, new Regex(@"^(?:[{])")},
					{31, new Regex(@"^(?:[}])")},
					{32, new Regex(@"^(?:$)")},
					{33, new Regex(@"^(?:\s*(\r?\n)+)")},
					{34, new Regex(@"^(?:\s+)")},
					{35, new Regex(@"^(?:\[\*\])")},
					{36, new Regex(@"^(?:class\b)")},
					{37, new Regex(@"^(?:\s*(\r?\n)+)")},
					{38, new Regex(@"^(?:\s+)")},
					{39, new Regex(@"^(?:[}])")},
					{40, new Regex(@"^(?:[{])")},
					{41, new Regex(@"^(?:[}])")},
					{42, new Regex(@"^(?:$)")},
					{43, new Regex(@"^(?:\[\*\])")},
					{44, new Regex(@"^(?:[{])")},
					{45, new Regex(@"^(?:[\n])")},
					{46, new Regex(@"^(?:[^{}\n]*)")},
					{47, new Regex(@"^(?:cssClass\b)")},
					{48, new Regex(@"^(?:callback\b)")},
					{49, new Regex(@"^(?:link\b)")},
					{50, new Regex(@"^(?:click\b)")},
					{51, new Regex(@"^(?:note for\b)")},
					{52, new Regex(@"^(?:note\b)")},
					{53, new Regex(@"^(?:<<)")},
					{54, new Regex(@"^(?:>>)")},
					{55, new Regex(@"^(?:href\b)")},
					{56, new Regex(@"^(?:[~])")},
					{57, new Regex(@"^(?:[^~]*)")},
					{58, new Regex(@"^(?:~)")},
					{59, new Regex(@"^(?:[`])")},
					{60, new Regex(@"^(?:[^`]+)")},
					{61, new Regex(@"^(?:[`])")},
					{62, new Regex(@"^(?:_self\b)")},
					{63, new Regex(@"^(?:_blank\b)")},
					{64, new Regex(@"^(?:_parent\b)")},
					{65, new Regex(@"^(?:_top\b)")},
					{66, new Regex(@"^(?:\s*<\|)")},
					{67, new Regex(@"^(?:\s*\|>)")},
					{68, new Regex(@"^(?:\s*>)")},
					{69, new Regex(@"^(?:\s*<)")},
					{70, new Regex(@"^(?:\s*\*)")},
					{71, new Regex(@"^(?:\s*o\b)")},
					{72, new Regex(@"^(?:\s*\(\))")},
					{73, new Regex(@"^(?:--)")},
					{74, new Regex(@"^(?:\.\.)")},
					{75, new Regex(@"^(?::{1}[^:\n;]+)")},
					{76, new Regex(@"^(?::{3})")},
					{77, new Regex(@"^(?:-)")},
					{78, new Regex(@"^(?:\.)")},
					{79, new Regex(@"^(?:\+)")},
					{80, new Regex(@"^(?:%)")},
					{81, new Regex(@"^(?:=)")},
					{82, new Regex(@"^(?:=)")},
					{83, new Regex(@"^(?:\w+)")},
					{84, new Regex(@"^(?:\[)")},
					{85, new Regex(@"^(?:\])")},
					{86, new Regex(@"^(?:[!""#$%&'*+,-.`?\\/])")},
					{87, new Regex(@"^(?:[0-9]+)")},
					{88, new Regex(@"^(?:[\u00AA\u00B5\u00BA\u00C0-\u00D6\u00D8-\u00F6]|[\u00F8-\u02C1\u02C6-\u02D1\u02E0-\u02E4\u02EC\u02EE\u0370-\u0374\u0376\u0377]|[\u037A-\u037D\u0386\u0388-\u038A\u038C\u038E-\u03A1\u03A3-\u03F5]|[\u03F7-\u0481\u048A-\u0527\u0531-\u0556\u0559\u0561-\u0587\u05D0-\u05EA]|[\u05F0-\u05F2\u0620-\u064A\u066E\u066F\u0671-\u06D3\u06D5\u06E5\u06E6\u06EE]|[\u06EF\u06FA-\u06FC\u06FF\u0710\u0712-\u072F\u074D-\u07A5\u07B1\u07CA-\u07EA]|[\u07F4\u07F5\u07FA\u0800-\u0815\u081A\u0824\u0828\u0840-\u0858\u08A0]|[\u08A2-\u08AC\u0904-\u0939\u093D\u0950\u0958-\u0961\u0971-\u0977]|[\u0979-\u097F\u0985-\u098C\u098F\u0990\u0993-\u09A8\u09AA-\u09B0\u09B2]|[\u09B6-\u09B9\u09BD\u09CE\u09DC\u09DD\u09DF-\u09E1\u09F0\u09F1\u0A05-\u0A0A]|[\u0A0F\u0A10\u0A13-\u0A28\u0A2A-\u0A30\u0A32\u0A33\u0A35\u0A36\u0A38\u0A39]|[\u0A59-\u0A5C\u0A5E\u0A72-\u0A74\u0A85-\u0A8D\u0A8F-\u0A91\u0A93-\u0AA8]|[\u0AAA-\u0AB0\u0AB2\u0AB3\u0AB5-\u0AB9\u0ABD\u0AD0\u0AE0\u0AE1\u0B05-\u0B0C]|[\u0B0F\u0B10\u0B13-\u0B28\u0B2A-\u0B30\u0B32\u0B33\u0B35-\u0B39\u0B3D\u0B5C]|[\u0B5D\u0B5F-\u0B61\u0B71\u0B83\u0B85-\u0B8A\u0B8E-\u0B90\u0B92-\u0B95\u0B99]|[\u0B9A\u0B9C\u0B9E\u0B9F\u0BA3\u0BA4\u0BA8-\u0BAA\u0BAE-\u0BB9\u0BD0]|[\u0C05-\u0C0C\u0C0E-\u0C10\u0C12-\u0C28\u0C2A-\u0C33\u0C35-\u0C39\u0C3D]|[\u0C58\u0C59\u0C60\u0C61\u0C85-\u0C8C\u0C8E-\u0C90\u0C92-\u0CA8\u0CAA-\u0CB3]|[\u0CB5-\u0CB9\u0CBD\u0CDE\u0CE0\u0CE1\u0CF1\u0CF2\u0D05-\u0D0C\u0D0E-\u0D10]|[\u0D12-\u0D3A\u0D3D\u0D4E\u0D60\u0D61\u0D7A-\u0D7F\u0D85-\u0D96\u0D9A-\u0DB1]|[\u0DB3-\u0DBB\u0DBD\u0DC0-\u0DC6\u0E01-\u0E30\u0E32\u0E33\u0E40-\u0E46\u0E81]|[\u0E82\u0E84\u0E87\u0E88\u0E8A\u0E8D\u0E94-\u0E97\u0E99-\u0E9F\u0EA1-\u0EA3]|[\u0EA5\u0EA7\u0EAA\u0EAB\u0EAD-\u0EB0\u0EB2\u0EB3\u0EBD\u0EC0-\u0EC4\u0EC6]|[\u0EDC-\u0EDF\u0F00\u0F40-\u0F47\u0F49-\u0F6C\u0F88-\u0F8C\u1000-\u102A]|[\u103F\u1050-\u1055\u105A-\u105D\u1061\u1065\u1066\u106E-\u1070\u1075-\u1081]|[\u108E\u10A0-\u10C5\u10C7\u10CD\u10D0-\u10FA\u10FC-\u1248\u124A-\u124D]|[\u1250-\u1256\u1258\u125A-\u125D\u1260-\u1288\u128A-\u128D\u1290-\u12B0]|[\u12B2-\u12B5\u12B8-\u12BE\u12C0\u12C2-\u12C5\u12C8-\u12D6\u12D8-\u1310]|[\u1312-\u1315\u1318-\u135A\u1380-\u138F\u13A0-\u13F4\u1401-\u166C]|[\u166F-\u167F\u1681-\u169A\u16A0-\u16EA\u1700-\u170C\u170E-\u1711]|[\u1720-\u1731\u1740-\u1751\u1760-\u176C\u176E-\u1770\u1780-\u17B3\u17D7]|[\u17DC\u1820-\u1877\u1880-\u18A8\u18AA\u18B0-\u18F5\u1900-\u191C]|[\u1950-\u196D\u1970-\u1974\u1980-\u19AB\u19C1-\u19C7\u1A00-\u1A16]|[\u1A20-\u1A54\u1AA7\u1B05-\u1B33\u1B45-\u1B4B\u1B83-\u1BA0\u1BAE\u1BAF]|[\u1BBA-\u1BE5\u1C00-\u1C23\u1C4D-\u1C4F\u1C5A-\u1C7D\u1CE9-\u1CEC]|[\u1CEE-\u1CF1\u1CF5\u1CF6\u1D00-\u1DBF\u1E00-\u1F15\u1F18-\u1F1D]|[\u1F20-\u1F45\u1F48-\u1F4D\u1F50-\u1F57\u1F59\u1F5B\u1F5D\u1F5F-\u1F7D]|[\u1F80-\u1FB4\u1FB6-\u1FBC\u1FBE\u1FC2-\u1FC4\u1FC6-\u1FCC\u1FD0-\u1FD3]|[\u1FD6-\u1FDB\u1FE0-\u1FEC\u1FF2-\u1FF4\u1FF6-\u1FFC\u2071\u207F]|[\u2090-\u209C\u2102\u2107\u210A-\u2113\u2115\u2119-\u211D\u2124\u2126\u2128]|[\u212A-\u212D\u212F-\u2139\u213C-\u213F\u2145-\u2149\u214E\u2183\u2184]|[\u2C00-\u2C2E\u2C30-\u2C5E\u2C60-\u2CE4\u2CEB-\u2CEE\u2CF2\u2CF3]|[\u2D00-\u2D25\u2D27\u2D2D\u2D30-\u2D67\u2D6F\u2D80-\u2D96\u2DA0-\u2DA6]|[\u2DA8-\u2DAE\u2DB0-\u2DB6\u2DB8-\u2DBE\u2DC0-\u2DC6\u2DC8-\u2DCE]|[\u2DD0-\u2DD6\u2DD8-\u2DDE\u2E2F\u3005\u3006\u3031-\u3035\u303B\u303C]|[\u3041-\u3096\u309D-\u309F\u30A1-\u30FA\u30FC-\u30FF\u3105-\u312D]|[\u3131-\u318E\u31A0-\u31BA\u31F0-\u31FF\u3400-\u4DB5\u4E00-\u9FCC]|[\uA000-\uA48C\uA4D0-\uA4FD\uA500-\uA60C\uA610-\uA61F\uA62A\uA62B]|[\uA640-\uA66E\uA67F-\uA697\uA6A0-\uA6E5\uA717-\uA71F\uA722-\uA788]|[\uA78B-\uA78E\uA790-\uA793\uA7A0-\uA7AA\uA7F8-\uA801\uA803-\uA805]|[\uA807-\uA80A\uA80C-\uA822\uA840-\uA873\uA882-\uA8B3\uA8F2-\uA8F7\uA8FB]|[\uA90A-\uA925\uA930-\uA946\uA960-\uA97C\uA984-\uA9B2\uA9CF\uAA00-\uAA28]|[\uAA40-\uAA42\uAA44-\uAA4B\uAA60-\uAA76\uAA7A\uAA80-\uAAAF\uAAB1\uAAB5]|[\uAAB6\uAAB9-\uAABD\uAAC0\uAAC2\uAADB-\uAADD\uAAE0-\uAAEA\uAAF2-\uAAF4]|[\uAB01-\uAB06\uAB09-\uAB0E\uAB11-\uAB16\uAB20-\uAB26\uAB28-\uAB2E]|[\uABC0-\uABE2\uAC00-\uD7A3\uD7B0-\uD7C6\uD7CB-\uD7FB\uF900-\uFA6D]|[\uFA70-\uFAD9\uFB00-\uFB06\uFB13-\uFB17\uFB1D\uFB1F-\uFB28\uFB2A-\uFB36]|[\uFB38-\uFB3C\uFB3E\uFB40\uFB41\uFB43\uFB44\uFB46-\uFBB1\uFBD3-\uFD3D]|[\uFD50-\uFD8F\uFD92-\uFDC7\uFDF0-\uFDFB\uFE70-\uFE74\uFE76-\uFEFC]|[\uFF21-\uFF3A\uFF41-\uFF5A\uFF66-\uFFBE\uFFC2-\uFFC7\uFFCA-\uFFCF]|[\uFFD2-\uFFD7\uFFDA-\uFFDC])")},
					{89, new Regex(@"^(?:\s)")},
					{90, new Regex(@"^(?:$)")}
				};

			Conditions = new Dictionary<string, LexerConditions>
				{
					{"namespace-body", new LexerConditions(new List<int> { 26,31,32,33,34,35,36,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"namespace", new LexerConditions(new List<int> { 26,27,28,29,30,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"class-body", new LexerConditions(new List<int> { 26,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"class", new LexerConditions(new List<int> { 26,37,38,39,40,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"acc_descr_multiline", new LexerConditions(new List<int> { 11,12,26,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"acc_descr", new LexerConditions(new List<int> { 9,26,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"acc_title", new LexerConditions(new List<int> { 7,26,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"callback_args", new LexerConditions(new List<int> { 22,23,26,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"callback_name", new LexerConditions(new List<int> { 19,20,21,26,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"href", new LexerConditions(new List<int> { 26,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"struct", new LexerConditions(new List<int> { 26,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"generic", new LexerConditions(new List<int> { 26,47,48,49,50,51,52,53,54,55,56,57,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"bqstring", new LexerConditions(new List<int> { 26,47,48,49,50,51,52,53,54,55,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"string", new LexerConditions(new List<int> { 24,25,26,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, false)},
					{"INITIAL", new LexerConditions(new List<int> { 0,1,2,3,4,5,6,8,10,13,14,15,16,17,18,26,27,36,47,48,49,50,51,52,53,54,55,58,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90 }, true)}
				};


		}
		
		public ParserValue ParserPerformAction(ref ParserValue thisS, ref ParserValue yy, ref int yystate, ref JList<ParserValue> ss)
		{
			var so = ss.Count - 1;
/* this == yyval */


switch (yystate) {
case 8:
 thisS=ss[so-1]; 
break;
case 9: case 11: case 12:
 thisS=ss[so]; 
break;
case 10: case 13:
 thisS=ss[so-1]+ss[so]; 
break;
case 14: case 15:
 thisS=ss[so-1]+'~'+ss[so]+'~'; 
break;
case 16:
 yy.addRelation(ss[so]); 
break;
case 17:
 ss[so-1].title =  yy.cleanupLabel(ss[so]); yy.addRelation(ss[so-1]);        
break;
case 26:
 thisS=ss[so].trim();yy.setAccTitle(thisS); 
break;
case 27: case 28:
 thisS=ss[so].trim();yy.setAccDescription(thisS); 
break;
case 29:
yy.addClassesToNamespace(ss[so-3], ss[so-1]);
break;
case 30:
yy.addClassesToNamespace(ss[so-4], ss[so-1]);
break;
case 31:
thisS=ss[so]; yy.addNamespace(ss[so]);
break;
case 32:
thisS=[ss[so]]
break;
case 33:
thisS=[ss[so-1]]
break;
case 34:
ss[so].unshift(ss[so-2]); thisS=ss[so]
break;
case 36:
yy.setCssClass(ss[so-2], ss[so]);
break;
case 37:
yy.addMembers(ss[so-3],ss[so-1]);
break;
case 38:
yy.setCssClass(ss[so-5], ss[so-3]);yy.addMembers(ss[so-5],ss[so-1]);
break;
case 39:
thisS=ss[so]; yy.addClass(ss[so]);
break;
case 40:
thisS=ss[so-1]; yy.addClass(ss[so-1]);yy.setClassLabel(ss[so-1], ss[so]);
break;
case 41:
 yy.addAnnotation(ss[so],ss[so-2]); 
break;
case 42:
 thisS = [ss[so]]; 
break;
case 43:
 ss[so].push(ss[so-1]);thisS=ss[so];
break;
case 44:
/*('Rel found',ss[so]);*/
break;
case 45:
yy.addMember(ss[so-1],yy.cleanupLabel(ss[so]));
break;
case 46:

break;
case 47:

break;
case 48:
 thisS = {'id1':ss[so-2],'id2':ss[so], relation:ss[so-1], relationTitle1:'none', relationTitle2:'none'}; 
break;
case 49:
 thisS = {id1:ss[so-3], id2:ss[so], relation:ss[so-1], relationTitle1:ss[so-2], relationTitle2:'none'}
break;
case 50:
 thisS = {id1:ss[so-3], id2:ss[so], relation:ss[so-2], relationTitle1:'none', relationTitle2:ss[so-1]}; 
break;
case 51:
 thisS = {id1:ss[so-4], id2:ss[so], relation:ss[so-2], relationTitle1:ss[so-3], relationTitle2:ss[so-1]} 
break;
case 52:
 yy.addNote(ss[so], ss[so-1]); 
break;
case 53:
 yy.addNote(ss[so]); 
break;
case 54:
 yy.setDirection('TB');
break;
case 55:
 yy.setDirection('BT');
break;
case 56:
 yy.setDirection('RL');
break;
case 57:
 yy.setDirection('LR');
break;
case 58:
 thisS={type1:ss[so-2],type2:ss[so],lineType:ss[so-1]}; 
break;
case 59:
 thisS={type1:'none',type2:ss[so],lineType:ss[so-1]}; 
break;
case 60:
 thisS={type1:ss[so-1],type2:'none',lineType:ss[so]}; 
break;
case 61:
 thisS={type1:'none',type2:'none',lineType:ss[so]}; 
break;
case 62:
 thisS=yy.relationType.AGGREGATION;
break;
case 63:
 thisS=yy.relationType.EXTENSION;
break;
case 64:
 thisS=yy.relationType.COMPOSITION;
break;
case 65:
 thisS=yy.relationType.DEPENDENCY;
break;
case 66:
 thisS=yy.relationType.LOLLIPOP;
break;
case 67:
thisS=yy.lineType.LINE;
break;
case 68:
thisS=yy.lineType.DOTTED_LINE;
break;
case 69: case 75:
thisS = ss[so-2];yy.setClickEvent(ss[so-1], ss[so]);
break;
case 70: case 76:
thisS = ss[so-3];yy.setClickEvent(ss[so-2], ss[so-1]);yy.setTooltip(ss[so-2], ss[so]);
break;
case 71:
thisS = ss[so-2];yy.setLink(ss[so-1], ss[so]);
break;
case 72:
thisS = ss[so-3];yy.setLink(ss[so-2], ss[so-1],ss[so]);
break;
case 73:
thisS = ss[so-3];yy.setLink(ss[so-2], ss[so-1]);yy.setTooltip(ss[so-2], ss[so]);
break;
case 74:
thisS = ss[so-4];yy.setLink(ss[so-3], ss[so-2], ss[so]);yy.setTooltip(ss[so-3], ss[so-1]);
break;
case 77:
thisS = ss[so-3];yy.setClickEvent(ss[so-2], ss[so-1], ss[so]);
break;
case 78:
thisS = ss[so-4];yy.setClickEvent(ss[so-3], ss[so-2], ss[so-1]);yy.setTooltip(ss[so-3], ss[so]);
break;
case 79:
thisS = ss[so-3];yy.setLink(ss[so-2], ss[so]);
break;
case 80:
thisS = ss[so-4];yy.setLink(ss[so-3], ss[so-1], ss[so]);
break;
case 81:
thisS = ss[so-4];yy.setLink(ss[so-3], ss[so-1]);yy.setTooltip(ss[so-3], ss[so]);
break;
case 82:
thisS = ss[so-5];yy.setLink(ss[so-4], ss[so-2], ss[so]);yy.setTooltip(ss[so-4], ss[so-1]);
break;
case 83:
yy.setCssClass(ss[so-1], ss[so]);
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
case 0:return 52;
break;
case 1:return 53;
break;
case 2:return 54;
break;
case 3:return 55;
break;
case 4:/* skip comments */
break;
case 5:/* skip comments */
break;
case 6: this.begin("acc_title");return 30; 
break;
case 7: this.popState(); return "acc_title_value"; 
break;
case 8: this.begin("acc_descr");return 32; 
break;
case 9: this.popState(); return "acc_descr_value"; 
break;
case 10: this.begin("acc_descr_multiline");
break;
case 11: this.popState(); 
break;
case 12:return "acc_descr_multiline_value";
break;
case 13:return 8;
break;
case 14:/* skip whitespace */
break;
case 15:return 7;
break;
case 16:return 7;
break;
case 17:return 'EDGE_STATE';
break;
case 18:this.begin("callback_name");
break;
case 19:this.popState();
break;
case 20:this.popState(); this.begin("callback_args");
break;
case 21:return 69;
break;
case 22:this.popState();
break;
case 23:return 70;
break;
case 24:this.popState();
break;
case 25:return 13;
break;
case 26:this.begin("string");
break;
case 27: this.begin('namespace'); return 39; 
break;
case 28: this.popState(); return 8; 
break;
case 29:/* skip whitespace */
break;
case 30: this.begin("namespace-body"); return 36;
break;
case 31: this.popState(); return 38; 
break;
case 32:return "EOF_IN_STRUCT";
break;
case 33:return 8;
break;
case 34:/* skip whitespace */
break;
case 35:return 'EDGE_STATE';
break;
case 36: this.begin('class'); return 43;
break;
case 37: this.popState(); return 8; 
break;
case 38:/* skip whitespace */
break;
case 39: this.popState(); this.popState(); return 38;
break;
case 40: this.begin("class-body"); return 36;
break;
case 41: this.popState(); return 38; 
break;
case 42:return "EOF_IN_STRUCT";
break;
case 43: return 'EDGE_STATE';
break;
case 44:return "OPEN_IN_STRUCT";
break;
case 45:/* nothing */
break;
case 46: return 46;
break;
case 47:return 72;
break;
case 48:return 65;
break;
case 49:return 66;
break;
case 50:return 68;
break;
case 51:return 49;
break;
case 52:return 51;
break;
case 53:return 44;
break;
case 54:return 45;
break;
case 55:return 71;
break;
case 56:this.popState();
break;
case 57:return 19;
break;
case 58:this.begin("generic");
break;
case 59:this.popState();
break;
case 60:return "BQUOTE_STR";
break;
case 61:this.begin("bqstring");
break;
case 62:return 67;
break;
case 63:return 67;
break;
case 64:return 67;
break;
case 65:return 67;
break;
case 66:return 59;
break;
case 67:return 59;
break;
case 68:return 61;
break;
case 69:return 61;
break;
case 70:return 60;
break;
case 71:return 58;
break;
case 72:return 62;
break;
case 73:return 63;
break;
case 74:return 64;
break;
case 75:return 21;
break;
case 76:return 41;
break;
case 77:return 84;
break;
case 78:return "DOT";
break;
case 79:return "PLUS";
break;
case 80:return 81;
break;
case 81:return "EQUALS";
break;
case 82:return "EQUALS";
break;
case 83:return 88;
break;
case 84:return 12;
break;
case 85:return 14;
break;
case 86:return "PUNCTUATION";
break;
case 87:return 87;
break;
case 88:return 86;
break;
case 89:return 83;
break;
case 90:return 9;
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