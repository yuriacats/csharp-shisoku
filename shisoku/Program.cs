// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using shisoku;

while (true)
{
    try
    {
        string? input;
        do
        {
            Console.Write("> ");
            input = Console.ReadLine();
        } while (input is null || input.Length == 0);
        var tokens = lexer.lex(input);
        //tokens.ForEach(Console.WriteLine);

        var (tree, _) = ast.parse(tokens.ToArray());
        //Console.WriteLine(tree);
        Console.WriteLine(PrettyPrinter.PrettyPrint(tree));

        var answer = MyCalc.toInt(tree);
        Console.WriteLine(answer);
    }
    catch (Exception) { }
}
public static class PrettyPrinter
{
    public static string Indent(string str)
    {
        return string.Join('\n', str.Split("\n").Where(s => s.Length != 0).Select(s => "  " + s)) + '\n';
    }
    public static string PrettyPrint(Ast e)
    {
        return e switch
        {
            AstNumber(var n) => $"{n}\n",
            AstAdd(var lhs, var rhs) => "+\n" + Indent("左:" + PrettyPrint(lhs)) + Indent("右:" + PrettyPrint(rhs)),
            AstSub(var lhs, var rhs) => "-\n" + Indent("左:" + PrettyPrint(lhs)) + Indent("右:" + PrettyPrint(rhs)),
            AstMul(var lhs, var rhs) => "*\n" + Indent("左:" + PrettyPrint(lhs)) + Indent("右:" + PrettyPrint(rhs)),
            AstDiv(var lhs, var rhs) => "/\n" + Indent("左:" + PrettyPrint(lhs)) + Indent("右:" + PrettyPrint(rhs)),
            _ => throw new NotImplementedException(),
        };
    }
}
