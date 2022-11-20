// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using shisoku;

while(true) {
    Console.Write("> ");
    string? input;
    do{
        input = Console.ReadLine();
    } while (input is null);
    var tokens  = lexer.lex(input);
        //tokens.ForEach(Console.WriteLine);

    var (tree, _) = ast.parse(tokens.ToArray());
    Console.WriteLine(tree);

    var answer = MyCalc.toInt(tree);
    Console.WriteLine(answer);

}