// See https://aka.ms/new-console-template for more information
namespace shisoku;
using System.CommandLine;
using System.IO;
class Program
{
    static Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("calculate application");
        var expOption = new Option<string>(
            name: "--exp",
            description: "一つの式だけ評価する時に使います"
        );

        var fileOption = new Option<string>(
            name: "--file",
            description: "ファイルから式を読み込みます"
        );
        rootCommand.AddOption(expOption);
        rootCommand.AddOption(fileOption);
        rootCommand.SetHandler((expinput, fileName) =>
        {
            if (expinput != null && fileName != null)
            {
                throw new Exception("ファイルと式の同時指定はできません");
            }
            if (fileName != null)
            {
                if (!File.Exists(fileName))
                {
                    throw new Exception("ファイルが存在しません");
                }
                var input = File.ReadAllText(fileName);
                Console.WriteLine(Calculate(input, new VariableEnvironment()));
            }
            else if (expinput != null)
            {
                Console.WriteLine(CalculateFromInput(expinput, new VariableEnvironment()));
            }
        }, expOption, fileOption);
        return rootCommand.InvokeAsync(args);

    }
    static void Repl()
    {
        var env = new VariableEnvironment();
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
                Console.WriteLine(CalculateFromInput(input, env));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
            }
        }

    }
    static Value CalculateFromInput(string input, VariableEnvironment env)
    {
        var tokens = Lexer.lex(input);
        if (tokens[0] is not TokenConst)
        {
            tokens.Insert(0, new TokenReturn());
        }
        var (statements, _) = ParseStatement.parse(tokens.ToArray());
        //TODO 諸々仕様固まってから考える
        if (statements.Length > 1)
        {
            throw new Exception("複数文は受け付けられません");
        }
        return CalcFunctionBody.Calc(statements, env);
    }
    static Value Calculate(string input, VariableEnvironment env)
    {
        var tokens = Lexer.lex(input);
        var (statements, _) = ParseStatement.parse(tokens.ToArray());
        //TODO 諸々仕様固まってから考える
        return CalcFunctionBody.Calc(statements, env);
    }
}
