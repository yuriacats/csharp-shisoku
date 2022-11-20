// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using shisoku;

while(true) {
    try {
    string? input;
    do{
        Console.Write("> ");
        input = Console.ReadLine();
    } while (input is null || input.Length == 0);
    var tokens  = lexer.lex(input);
        //tokens.ForEach(Console.WriteLine);

    var (tree, _) = ast.parse(tokens.ToArray());
    Console.WriteLine(tree);

    var answer = MyCalc.toInt(tree);
    Console.WriteLine(answer);
    } catch (Exception){}
}