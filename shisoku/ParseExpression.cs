using System.Linq;
namespace shisoku;
public class ParseExpression
{
    public static (Expression, shisoku.Token[]) parse(shisoku.Token[] input)
    {
        return parseComparator(input);
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
                    result = new AddExpression(result, addRhs, new Unchecked());
                    rest = addRest;
                    break;
                case TokenMinus:
                    var (subRhs, subRest) = parseMulDiv(rest2);
                    result = new SubExpression(result, subRhs ,new Unchecked());
                    rest = subRest;
                    break;
            }
        }
        return (result, rest);
    }
    static (Expression, shisoku.Token[]) parseMulDiv(shisoku.Token[] input)
    {
        (var result, var rest) = parseCall(input);
        while (rest is [TokenSlash or TokenAsterisk or TokenPercent, .. var rest2])
        {
            switch (rest[0])
            {
                case TokenAsterisk:
                    var (malRhs, malRest) = parseCall(rest2);
                    result = new MulExpression(result, malRhs, new Unchecked());
                    rest = malRest;
                    break;
                case TokenSlash:
                    var (divRhs, divRest) = parseCall(rest2);
                    result = new DivExpression(result, divRhs,new Unchecked());
                    rest = divRest;
                    break;
                case TokenPercent:
                    var (modRhs, modRest) = parseCall(rest2);
                    result = new ModExpression(result, modRhs, new Unchecked());
                    rest = modRest;
                    break;
            }
        }
        return (result, rest);
    }

    static (Expression, shisoku.Token[]) parseCall(shisoku.Token[] input)
    {
        (var result, var rest) = parseNumOrSection(input);

        while (rest is [TokenBracketOpen, .. var innerRest])
        {
            (var arguments, rest) = parseArguments(innerRest);
            result = new CallExpression(arguments, result ,new Unchecked());
        }
        return (result, rest);
    }

    static ((string, Expression)[], shisoku.Token[]) parseArguments(shisoku.Token[] input)
    {
        var target = input;
        var arguments = new (string, Expression)[] { };
        while (target is not [])
        {
            switch (target)
            {
                case [TokenIdentifier(var argumentName), TokenEqual, .. var target2]:
                    var (argumentExpression, rest2) = parse(target2);
                    arguments = arguments.Append((argumentName, argumentExpression)).ToArray();
                    switch (rest2)
                    {
                        case [TokenBracketClose, .. var rest3]:
                            {
                                return (arguments, rest3);
                            }
                        case [TokenComma, .. var rest3]:
                            target = rest3;
                            continue;
                    }
                    throw new Exception($"Cannot parse arguments in: {String.Join<Token>(',', target)}");
                case [TokenBracketClose, .. var target2]:
                    return (arguments, target2);
                default:
                    throw new Exception($"Cannot parse arguments in: {String.Join<Token>(',', target)}");
            }
        }
        throw new Exception($"Cannot parse arguments in: {String.Join<Token>(',', target)}");
    }
    static (Expression, shisoku.Token[]) parseNumOrSection(shisoku.Token[] input)
    {
        switch (input)
        {
            case [TokenNumber(var num), .. var rest]:
                return (new NumberExpression(num), rest);
            case [TokenBracketOpen, .. var target]:
                (var inner_ast, var token_rest) = parse(target);
                if (token_rest[0] is TokenBracketClose)
                {
                    return (inner_ast, token_rest[1..]);
                }
                else
                {
                    throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
                }
            case [TokenDef, TokenIdentifier(var name), TokenEqual , .. var target]:
                (var defArguments,var defReturnType, var defBodyTokens) = ParseType.functionTypeParser(target);
                var defBodys = new List<Statement> { };
                if (defBodyTokens.Length == 0)
                {
                    throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
                }
                if (defBodyTokens[0] is not TokenCurlyBracketOpen)
                {
                    throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
                }
                defBodyTokens = defBodyTokens[1..];
                while (defBodyTokens is not [TokenCurlyBracketClose, ..])
                {
                    (var defBody, defBodyTokens) = ParseStatement.parseStatement(defBodyTokens);
                    defBodys.Add(defBody);
                }
                return (new RecursiveFunctionExpression(defArguments, defBodys.ToArray(), name, defReturnType), defBodyTokens[1..]);
            case [TokenPipe, ..]:
                (var arguments,var returnType, var bodyTokens) = ParseType.functionTypeParser(input);
                var bodys = new List<Statement> { };
                bodyTokens = bodyTokens[1..];
                while (bodyTokens is not [TokenCurlyBracketClose, ..])
                {
                    (var body, bodyTokens) = ParseStatement.parseStatement(bodyTokens);
                    bodys.Add(body);
                }
                return (new FunctionExpression(arguments, bodys.ToArray(), returnType), bodyTokens[1..]);
            case [TokenTrue, .. var rest]:
                return (new BoolExpression(true), rest);
            case [TokenFalse, .. var rest]:
                return (new BoolExpression(false), rest);
            case [TokenIdentifier(var name), .. var rest]:
                return (new VariableExpression(name, new Unchecked()), rest);

            default:
                throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
        }

    }

}
