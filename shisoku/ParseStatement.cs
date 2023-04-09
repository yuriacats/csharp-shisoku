namespace shisoku;
public class ParseStatement
{
    public static (Statement, Token[]) parse(Token[] tokens)
    {
        return parseStatement(tokens);
    }

    private static (Statement, Token[]) parseStatement(Token[] tokens)
    {
        var (statement, token) = tokens switch
        {
            [TokenConst, ..] => parseConst(tokens),
            _ => parseExpressionStatement(tokens)
        };

        if (token is [TokenSemicolon, .. var rest2])
        {
            return (statement, rest2);
        }

        throw new Exception($"Token undefined: {token}");
    }
    private static (Statement, Token[]) parseExpressionStatement(Token[] tokens)
    {
        var (expression, rest) = ParseExpression.parse(tokens);
        return (new AstExpression(expression), rest);
    }
    private static (Statement, Token[]) parseConst(Token[] tokens)
    {
        if (tokens is [TokenConst, TokenIdentifier(var exprName), TokenEqual, .. var rest2])
        {
            (var expression, var rest) = ParseExpression.parse(rest2);
            return (new AstConst(exprName, expression), rest);
        }
        throw new Exception($"Token undefined: {tokens}");
    }
}
