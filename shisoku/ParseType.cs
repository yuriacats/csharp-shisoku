namespace shisoku;
public class ParseType
{
    public static (Type, shisoku.Token[]) parse(shisoku.Token[] input)
    {
        return typeParser(input);
    }
    public static (List<(string, Type)>, Type, shisoku.Token[]) functionTypeParser(shisoku.Token[] input)
    {
        return innerFunctionTypeParser(input);

    }
    static (List<(string, Type)>, Type, shisoku.Token[]) innerFunctionTypeParser(shisoku.Token[] input)
    {
        var target = input;
        var result = new List<(string, Type)>();
        if (target[0] is not TokenPipe)
        {
            throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
        }
        target = target[1..];
        while (target is not [])
        {
            switch (target)
            {
                case [TokenIdentifier(var name), TokenColon, .. var rest]:
                    (var type, var typedRest) = typeParser(rest);
                    result.Add((name, type));
                    target = typedRest;
                    continue;
                case [TokenComma, .. var rest]:
                    target = rest;
                    continue;
                case [TokenPipe, TokenArrow, .. var rest]:
                    (var returnType, var returnTypedRest) = typeParser(rest);
                    return (result, returnType, returnTypedRest);
                default:
                    throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
            }
        }
        throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
    }

    static (Type, shisoku.Token[]) typeParser(shisoku.Token[] input)
    {
        var target = input;
        Type result;
        while (target is not [])
        {
            switch (target)
            {
                case [TokenIdentifier(var name), .. var body]:
                    switch (name)
                    {
                        case "int":
                            result = new IntType();
                            return (result, body);
                        case "bool":
                            result = new BoolType();
                            return (result, body);
                        case "unit":
                            result = new UnitType();
                            return (result, body);
                        default:
                            throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
                    }
                case [TokenPipe, ..]:
                    (var innerArguments, var innerRerutn, var rest) = functionTypeParser(target);
                    return (new FunctionType(innerArguments, innerRerutn), rest);
                    throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
                default:
                    throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
            }
            throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
        }
        throw new Exception($"Unexpected Tokens: {String.Join<Token>(',', input)}");
    }
}