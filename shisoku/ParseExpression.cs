
namespace shisoku;
public class ParseExpression
{
    public static (Expression, shisoku.Token[]) parse(shisoku.Token[] input)
    {
        return parseCall(input);
    }
    static (Expression, shisoku.Token[]) parseCall(shisoku.Token[] input)
    {
        (var result, var rest) = parseComparator(input);
        while (rest is [TokenBracketOpen, .. var innerRest])
        {
            var arguments = new (string, Expression)[] { };
            while (innerRest[0] is not TokenBracketClose)
            {
                (var argument, var otherTokens) = parse(innerRest);
                switch (otherTokens[0])
                {
                    case TokenComma:
                        innerRest = otherTokens[1..];
                        break;
                    case TokenBracketClose:
                        innerRest = otherTokens;
                        break;
                    default:
                        throw new Exception("関数の引数の区切りが不正です。");
                }
                arguments = arguments.Append(("hoge", argument)).ToArray();
            }
            result = new CallExpression(arguments, result);
            rest = innerRest;
        }
        return (result, rest);
    }
    static (Expression, shisoku.Token[]) parseComparator(shisoku.Token[] input)
    {
        (var result, var rest) = parseAddSub(input);
        while (rest is [TokenEqualEqual, .. var rest2])
        {
            switch (rest[0])
            {
                case TokenEqualEqual:
                    var (eqRhs, eqRest) = parseAddSub(rest2);
                    result = new EqualExpression(result, eqRhs);
                    rest = eqRest;
                    break;
            }
        }
        return (result, rest);

    }
    static (Expression, shisoku.Token[]) parseAddSub(shisoku.Token[] input)
    {
        (var result, var rest) = parseMulDiv(input);
        while (rest is [TokenPlus or TokenMinus, .. var rest2])
        {
            switch (rest[0])
            {
                case TokenPlus:
                    var (addRhs, addRest) = parseMulDiv(rest2);
                    result = new AddExpression(result, addRhs);
                    rest = addRest;
                    break;
                case TokenMinus:
                    var (subRhs, subRest) = parseMulDiv(rest2);
                    result = new SubExpression(result, subRhs);
                    rest = subRest;
                    break;
            }
        }
        return (result, rest);
    }
    static (Expression, shisoku.Token[]) parseMulDiv(shisoku.Token[] input)
    {
        (var result, var rest) = parseNumOrSection(input);
        while (rest is [TokenSlash or TokenAsterisk, .. var rest2])
        {
            switch (rest[0])
            {
                case TokenAsterisk:
                    var (malRhs, malRest) = parseNumOrSection(rest2);
                    result = new MulExpression(result, malRhs);
                    rest = malRest;
                    break;
                case TokenSlash:
                    var (divRhs, divRest) = parseNumOrSection(rest2);
                    result = new DivExpression(result, divRhs);
                    rest = divRest;
                    break;
            }
        }
        return (result, rest);
    }

    static (Expression, shisoku.Token[]) parseNumOrSection(shisoku.Token[] input)
    {
        switch (input)
        {
            case [TokenNumber(var num), .. var rest]:
                return (new NumberExpression(num), rest);
            case [TokenBracketOpen, .. var target]:
                (var inner_ast, var token_rest) = parseComparator(target);
                if (token_rest[0] is TokenBracketClose)
                {
                    return (inner_ast, token_rest[1..]);
                }
                else
                {
                    throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
                }
            case [TokenCurlyBracketOpen, TokenPipe, .. var target]:
                (var argumentName, var bodyTokens) = argumentPaser(target);
                var bodyRest = new Token[] { };
                var bodys = new Statement[] { };
                while (true)
                {
                    (var body, var otherTokens) = ParseStatement.parseStatement(bodyTokens);
                    bodyRest = otherTokens;
                    bodys = bodys.Append(body).ToArray();

                    if (bodyRest[0] is TokenCurlyBracketClose)
                    {
                        return (new FunctionExpression(argumentName, bodys), otherTokens[1..]);
                    }
                }
                throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
            case [TokenTrue, .. var rest]:
                return (new BoolExpression(true), rest);
            case [TokenFalse, .. var rest]:
                return (new BoolExpression(false), rest);
            case [TokenIdentifier(var name), .. var rest]:
                return (new VariableExpression(name), rest);

            default:
                throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
        }

    }

    static (List<string>, shisoku.Token[]) argumentPaser(shisoku.Token[] input)
    {
        var target = input;
        var result = new List<string>();
        while (target is not [])
        {
            switch (target)
            {
                case [TokenIdentifier(var name), .. var rest]:
                    result.Add(name);
                    target = rest;

                    continue;
                case [TokenPipe, .. var rest]:
                    target = rest;
                    continue;
                case [TokenComma, .. var rest]:
                    target = rest;
                    continue;
                case [TokenArrow, .. var body]:
                    return (result, body);
                default:
                    throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
            }
        }
        throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
    }
}
