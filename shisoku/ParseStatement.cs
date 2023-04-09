namespace shisoku;
public class ParseStatement
{
    public static (Statement, Token[]) parse(Token[] tokens)
    {
        return parseStatement(tokens);
    }

    private static (Statement, Token[]) parseStatement(Token[] tokens)
    {
        switch (tokens)
        {
            case [TokenConst, ..]:
                return parseConst(tokens);
            default:
                return parseExpressionStatement(tokens);
        }
    }
    private static (Statement, Token[]) parseExpressionStatement(Token[] tokens)
    {
        var (expression, rest) = ParseExpression.parse(tokens);
        if (rest is [TokenSemicolon, .. var rest2])
        {
            return (new AstExpression(expression), rest2);
        }
        throw new Exception($"Token undefined: {rest}");
    }
    private static (Statement, Token[]) parseConst(Token[] tokens)
    {
        if (tokens is [TokenConst, TokenIdentifier(var exprName), TokenEqual, .. var rest2])
        {
            (var expression, var rest) = ParseExpression.parse(rest2);
            if (rest is [TokenSemicolon, ..])
            {
                return (new AstConst(exprName, expression), rest);
            }
            throw new Exception($"Token undefined: {rest}");
        }
        throw new Exception($"Token undefined: {tokens}");
    }
}
