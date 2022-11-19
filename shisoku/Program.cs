// See https://aka.ms/new-console-template for more information

using System;
using shisoku;

var tokens  = lexer.lex("123+34+56");
var tree = ast.parse(tokens);
Console.WriteLine(tree);