﻿// See https://aka.ms/new-console-template for more information
using shisoku;
using System.CommandLine;

class Program
{
    static Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("calculate application");
        var expOption = new Option<string>(
            name: "--exp",
            description: "一つの式だけ評価する時に使います"
        );

        var verboseOption = new Option<bool>(
            name: "--verbose",
            description: "詳しいAST構造等も表示します",
            getDefaultValue: () => false
        );
        rootCommand.AddOption(expOption);
        rootCommand.AddOption(verboseOption);
        rootCommand.SetHandler((input, isVerbose) =>
        {
            if (input != null)
            {
                Calculate(input, isVerbose, new Dictionary<string, int>());
            }
            else
            {
                Repl(isVerbose);
            }
        }, expOption, verboseOption);
        return rootCommand.InvokeAsync(args);

    }
    static void Repl(bool isVerboseOption)
    {
        var env = new Dictionary<string, int>();
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
                Calculate(input, isVerboseOption, env);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
            }
        }

    }
    static void Calculate(string input, bool isVerboseOption, Dictionary<string, int> env)
    {
        var tokens = Lexer.lex(input);

        var (tree, _) = ParseStatement.parse(tokens.ToArray());
        //TODO 諸々仕様固まってから考える
        if (isVerboseOption)
        {
            Console.WriteLine("木構造イメージ図");
            //Console.WriteLine(PrettyPrinter.PrettyPrint(tree));
            //TODO PrettyPrintのStatement対応
            Console.WriteLine("データ構造");
            Console.WriteLine(tree);
        }
        CalcStatement.toInt(tree, env);
    }
}
// -eの時のみ省略して行う

//public static class PrettyPrinter
//{
//    public static string Indent(string str)
//    {
//        return string.Join('\n', str.Split("\n").Where(s => s.Length != 0).Select(s => "  " + s)) + '\n';
//    }
//    public static string PrettyPrint(Expression e)
//    {
//        return e switch
//        {
//            AstNumber(var n) => $"{n}\n",
//            AstAdd(var lhs, var rhs) => "+\n" + Indent("左:" + PrettyPrint(lhs)) + Indent("右:" + PrettyPrint(rhs)),
//            AstSub(var lhs, var rhs) => "-\n" + Indent("左:" + PrettyPrint(lhs)) + Indent("右:" + PrettyPrint(rhs)),
//            AstMul(var lhs, var rhs) => "*\n" + Indent("左:" + PrettyPrint(lhs)) + Indent("右:" + PrettyPrint(rhs)),
//            AstDiv(var lhs, var rhs) => "/\n" + Indent("左:" + PrettyPrint(lhs)) + Indent("右:" + PrettyPrint(rhs)),
//            _ => throw new NotImplementedException(),
//        };
//    }
//}
