namespace shisoku;
public class ParseStatement
{
    public static (Statement[], Token[]) parse(Token[] tokens)
    {
        return parseStatements(tokens);
    }
    private static (Statement[], Token[]) parseStatements(Token[] tokens)
    {
        var statements = new Statement[] { };
        while (tokens is not [])
        {
            var (statement, restToken) = parseStatement(tokens);
            statements = statements.Append(statement).ToArray();
            tokens = restToken;
        }
        return (statements, tokens);
    }

    private static (Statement, Token[]) parseStatement(Token[] tokens)
    {

        var (statement, token) = tokens switch
        {
            [TokenConst, ..] => parseConst(tokens),
            _ => parseExpressionStatement(tokens)
        };


        if (token is not [TokenSemicolon, ..])
        {
            throw new Exception($"Token undefined: {token}");
        }

        return (statement, token[1..]);
    }
    private static (Statement, Token[]) parseExpressionStatement(Token[] tokens)
    {
        var (expression, rest) = ParseExpression.parse(tokens);
        return (new AstExpression(expression), rest);
    }
    private static (Statement, Token[]) parseConst(Token[] tokens)
    {
        if (tokens is [TokenConst, TokenIdentifier(var exprName), TokenEqual, .. var rest_token])
        {
            (var expression, var rest) = ParseExpression.parse(rest_token);
            return (new AstConst(exprName, expression), rest);
        }
        throw new Exception($"Token undefined: {tokens}");
    }
}
