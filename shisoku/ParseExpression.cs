
namespace shisoku;
public class ParseExpression
{
    public static (Expression, shisoku.Token[]) parse(shisoku.Token[] input)
    {
        return parseAddSub(input);
    }
    //TODO PublicクラスになってるのをPrivateにする
    public static (Expression, shisoku.Token[]) parseAddSub(shisoku.Token[] input)
    {
        (var result, var rest) = parseMulDiv(input);
        while (rest is [TokenPlus or TokenMinus, .. var rest2])
        {
            switch (rest[0])
            {
                case TokenPlus:
                    var (addRhs, addRest) = parseMulDiv(rest2);
                    result = new AstAdd(result, addRhs);
                    rest = addRest;
                    break;
                case TokenMinus:
                    var (subRhs, subRest) = parseMulDiv(rest2);
                    result = new AstSub(result, subRhs);
                    rest = subRest;
                    break;
            }
        }
        return (result, rest);
    }
    public static (Expression, shisoku.Token[]) parseMulDiv(shisoku.Token[] input)
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
    public static (Expression, shisoku.Token[]) parseNumOrSection(shisoku.Token[] input)
    {
        switch (input)
        {
            case [TokenNumber(var num), .. var rest]:
                return (new AstNumber(num), rest);
            case [TokenBracketOpen, .. var target]:
                (var inner_ast, var token_rest) = parseAddSub(target);
                if (token_rest[0] is TokenBracketClose)
                {
                    return (inner_ast, token_rest[1..]);
                }
                else
                {
                    input.ToList().ForEach(Console.WriteLine);
                    throw new Exception($"Token undefined: {input}");
                }
            default:
                input.ToList().ForEach(Console.WriteLine);
                throw new Exception($"Token undefined: {input}");
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
            TokenBracketOpen => "(",
            TokenBracketClose => ")",
            _ => throw new Exception("Token is not found")
        };
    }

    static string ViewTokens(Token[] tokens) =>
        string.Join("", tokens.Select(ViewToken));
}