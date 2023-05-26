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
            [TokenSwitch, ..] => parseSwitch(tokens),
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
        if (tokens is not [TokenSwitch, .. var restToken])
        {
            throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', tokens)}");
        }
        (var expression, var rest) = ParseExpression.parse(tokens[1..]);
        if (rest is not [TokenQuestion, ..])
        {
            throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', rest)}");
        }
        var cases = new (Expression, Statement[])[] { };
        rest = rest[1..];
        while (rest is not [])
        {
            var (aCase, rest4) = ParseCase(rest);
            cases = cases.Append(aCase).ToArray();
            if (aCase.Item1 is VariableExpression("default"))
            // TODO defaultをキーワードとしてTokerneizerのところから特別扱いする。ちょっと処理が特殊になりそうなので一旦放置
            {
                return (new StatementSwitch(expression, cases), rest4);
            }
            rest = rest4;
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
        rest = rest[2..];
        var statements = new Statement[] { };
        while (rest is not [TokenCurlyBracketClose, TokenComma, ..])
        {
            (var statement, var rest2) = parseStatement(rest);
            rest = rest2;
            statements = statements.Append(statement).ToArray();
        }
        return ((CaseExpression, statements), rest[2..]);
    }

}
