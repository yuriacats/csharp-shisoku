
namespace shisoku;
public abstract record Ast();
public record AstNumber(int Number) : Ast;
public record AstAdd(Ast hidari, Ast migi) : Ast;
public record AstSub(Ast hidari, Ast migi) : Ast;
public record AstMul(Ast hidari, Ast migi) : Ast;
public record AstDiv(Ast hidari, Ast migi) : Ast;
public class Parser
{
    public static (Ast, shisoku.Token[]) parse(shisoku.Token[] input)
    {
        return parseAddSub(input);
    }
    public static (Ast, shisoku.Token[]) parseAddSub(shisoku.Token[] input)
    {
        (var result, var rest) = parseMaldiv(input);
        while (rest is [TokenPlus or TokenMinus, .. var rest2])
        {
            switch (rest[0])
            {
                case TokenPlus:
                    var (addRhs, addRest) = parseMaldiv(rest2);
                    result = new AstAdd(result, addRhs);
                    rest = addRest;
                    break;
                case TokenMinus:
                    var (subRhs, subRest) = parseMaldiv(rest2);
                    result = new AstSub(result, subRhs);
                    rest = subRest;
                    break;
            }
        }
        return (result, rest);
    }
    public static (Ast, shisoku.Token[]) parseMaldiv(shisoku.Token[] input)
    {
        (var result, var rest) = parseNumOrSection(input);
        while (rest is [TokenSlash or TokenAsterisk, .. var rest2])
        {
            switch (rest[0])
            {
                case TokenAsterisk:
                    var (malRhs, malRest) = parseNumOrSection(rest2);
                    result = new AstMul(result, malRhs);
                    rest = malRest;
                    break;
                case TokenSlash:
                    var (divRhs, divRest) = parseNumOrSection(rest2);
                    result = new AstDiv(result, divRhs);
                    rest = divRest;
                    break;
            }
        }
        return (result, rest);
    }
    public static (Ast, shisoku.Token[]) parseNumOrSection(shisoku.Token[] input)
    {
        switch (input)
        {
            case [TokenNumber(var num), .. var nokori]:
                return (new AstNumber(num), nokori);
            case [TokenStartSection, .. var target]:
                (var innner_ast, var token_nokori) = parseAddSub(target);
                if (token_nokori[0] is TokenEndSection)
                {
                    return (innner_ast, token_nokori[1..]);
                }
                else
                {
                    input.ToList().ForEach(Console.WriteLine);
                    throw new Exception($"Token undifinde: {input}");
                }
            default:
                input.ToList().ForEach(Console.WriteLine);
                throw new Exception($"Token undifinde: {input}");
        }

    }
    static string ViewToken(Token token)
    {
        return token switch
        {
            TokenNumber(var n) => n.ToString(),
            TokenPlus => "+",
            TokenMinus => "-",
            TokenAsterisk => "*",
            TokenSlash => "/",
            TokenStartSection => "(",
            TokenEndSection => ")",
            _ => throw new Exception("sonna token nai")
        };
    }

    static string ViewTokens(Token[] tokens) =>
        string.Join("", tokens.Select(ViewToken));
}
