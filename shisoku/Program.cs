﻿// See https://aka.ms/new-console-template for more information
using shisoku;
namespace calc;
using System.CommandLine;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("calclate application");
        var expOption = new Option<string>(
            name: "--exp",
            description: "一つの式だけ評価する時に使います"
        );

        var varboseOption = new Option<bool>(
            name: "--varbose",
            description: "詳しいAST構造等も表示します",
            getDefaultValue: () => false
        );
        rootCommand.AddOption(expOption);
        rootCommand.AddOption(varboseOption);
        rootCommand.SetHandler((isVarbose) =>
        {
            Repl(isVarbose);
        }, varboseOption
        );
        rootCommand.SetHandler((input, isVarbose) =>
        {
            Calculate(input, isVarbose);
        }, expOption, varboseOption);
        return await rootCommand.InvokeAsync(args);

    }
    static void Repl(bool isVerboseOption)
    {
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
                Calculate(input, isVerboseOption);
            }
            catch (Exception) { }
        }

    }
    static void Calculate(string input, bool isVerboseOption)
    {
        var tokens = Lexer.lex(input);

        var (tree, _) = Parser.parse(tokens.ToArray());
        if (isVerboseOption)
        {
            Console.WriteLine("木構造イメージ図");
            Console.WriteLine(PrettyPrinter.PrettyPrint(tree));
            Console.WriteLine("データ構造");
            Console.WriteLine(tree);
        }
        var answer = MyCalc.toInt(tree);
        Console.WriteLine(answer);
    }
}
// -eの時のみ省略して行う

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
