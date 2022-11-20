// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using shisoku;

var tokens  = lexer.lex("10+2*(9/3)-3");
//tokens.ForEach(Console.WriteLine);

var (tree, _) = ast.parse(tokens.ToArray());
Console.WriteLine(tree);

var answer = MyCalc.toInt(tree);
Console.WriteLine(answer);