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

    public static (Statement, Token[]) parseStatement(Token[] tokens)
    {

        var (statement, token) = tokens switch
        {
            [TokenConst, ..] => parseConst(tokens),
            [TokenReturn, ..] => parseReturn(tokens),
            _ => parseExpressionStatement(tokens)
        };


        if (token is not [TokenSemicolon, ..])
        {
            throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', token)}");
        }

        return (statement, token[1..]);
    }
    private static (Statement, Token[]) parseExpressionStatement(Token[] tokens)
    {
        var (expression, rest) = ParseExpression.parse(tokens);
        return (new StatementExpression(expression), rest);
    }
    private static (Statement, Token[]) parseConst(Token[] tokens)
    {
        if (tokens is [TokenConst, TokenIdentifier(var exprName), TokenEqual, .. var restToken])
        {
            (var expression, var rest) = ParseExpression.parse(restToken);
            return (new StatementConst(exprName, expression), rest);
        }
        throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', tokens)}");
    }
    private static (Statement, Token[]) parseReturn(Token[] tokens)
    {
        if (tokens is [TokenReturn, .. var restToken])
        {
            (var expression, var rest) = ParseExpression.parse(restToken);
            return (new StatementReturn(expression), rest);
        }
        throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', tokens)}");
    }
}
