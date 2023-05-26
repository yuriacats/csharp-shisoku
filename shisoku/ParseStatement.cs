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
    private static (Statement, Token[]) parseSwitch(Token[] tokens)
    {
        if (tokens is [TokenSwitch, .. var restToken])
        {
            (var expression, var rest) = ParseExpression.parse(restToken);
            if (rest is [TokenQuestion, .. var rest2])
            {
                (var targetExpression, var rest3) = ParseExpression.parse(rest2);
                if (rest3[0] is not TokenQuestion)
                {
                    throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', rest3)}");
                }
                var cases = new (Expression, Statement[])[] { };
                var (aCase, rest4) = ParseCase(rest3[1..]);
                cases.Append(aCase);
                if (aCase.Item1 is VariableExpression("default"))
                {
                    return (new StatementSwitch(expression, cases), rest);
                }
            }
        }
        throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', tokens)}");
    }

    private static ((Expression, Statement[]), Token[]) ParseCase(Token[] tokens)
    {
        (var CaseExpression, var rest) = ParseExpression.parse(tokens);
        if (rest is not [TokenColon, TokenCurlyBracketOpen, ..])
        {
            throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', rest)}");
        }
        var rest2 = rest[2..];
        var statements = new Statement[] { };
        while (rest2 is not [TokenCurlyBracketClose, TokenComma, ..])
        {
            (var statement, var rest3) = parseStatement(rest2);
            rest2 = rest3;
            statements = statements.Append(statement).ToArray();
        }
        return ((CaseExpression, statements), rest2[2..]);
    }

}
