namespace shisoku;

public class CalcExpression
{
    public static int toInt(Value input)
    {
        switch (input)
        {
            case IntValue(var n):
                return n;
            default:
                throw new Exception($"Evaluation Error:augment type is not int({input})");
        }
    }
    public static FunctionValue toFunc(Value input)
    {
        switch (input)
        {
            case FunctionValue(var argumentNames, var body, var env):
                return new FunctionValue(argumentNames, body, env);
            default:
                throw new Exception($"Evaluation Error:augment type is not FunctionValue({input})");
        }
    }
    private static List<(string, Value)> argumentList((string, Expression)[] argumentsExpressions, List<string> argumentNames, VariableEnvironment env)
    {
        var givenArgumentNames = argumentsExpressions.Select(x => x.Item1).ToList();
        var givenArgumentNameSet = new HashSet<string>(givenArgumentNames);
        var expectedArgumentNames = new HashSet<string>(argumentNames);
        if (!expectedArgumentNames.SetEquals(givenArgumentNames))
        {
            throw new Exception($"Method not implemented.");
        }
        var givenArgumentsValues = argumentsExpressions.Select(
        argument => (argument.Item1, Calc(argument.Item2, env))).ToList();
        return givenArgumentsValues;

    }

    public static Value Calc(Expression input, VariableEnvironment env)
    {
        switch (input)
        {
            case NumberExpression(var value):
                {

                    return new IntValue(value);
                }
            case VariableExpression(var name,_):
                {
                    return env[name];
                }
            case BoolExpression(var value):
                {
                    return new BoolValue(value);
                }
            case AddExpression(var lhs, var rhs,_):
                {
                    return new IntValue(toInt(Calc(lhs, env)) + toInt(Calc(rhs, env)));
                }
            case SubExpression(var lhs, var rhs, _):
                {
                    return new IntValue(toInt(Calc(lhs, env)) - toInt(Calc(rhs, env)));
                }
            case MulExpression(var lhs, var rhs,_):
                {
                    return new IntValue(toInt(Calc(lhs, env)) * toInt(Calc(rhs, env)));
                }
            case DivExpression(var lhs, var rhs,_):
                {
                    return new IntValue(toInt(Calc(lhs, env)) / toInt(Calc(rhs, env)));
                }
            case ModExpression(var lhs, var rhs,_):
                {
                    return new IntValue(toInt(Calc(lhs, env)) % toInt(Calc(rhs, env)));
                }
            case EqualExpression(var lhs, var rhs):
                {
                    var valueOfLhs = Calc(lhs, env);
                    var valueOfRhs = Calc(rhs, env);
                    switch (valueOfLhs, valueOfRhs)
                    {
                        case (IntValue, IntValue):
                            return new BoolValue(valueOfLhs == valueOfRhs);
                        case (BoolValue, BoolValue):
                            return new BoolValue(valueOfLhs == valueOfRhs);
                        default:
                            throw new Exception($"Evaluation Error:Argment types differ.({lhs.ToString},{rhs.ToString})");
                    }
                }
            case FunctionExpression(var argumentNames, var body,_):
                {
                    return new FunctionValue((argumentNames.Select((x) => x.Item1).ToList()), body, env);
                }
            case RecursiveFunctionExpression(var argumentNames, var body, var ValueName,_):
                {
                    env.Add(ValueName, new FunctionValue(argumentNames.Select((x) => x.Item1).ToList(), body, env));
                    return new FunctionValue(argumentNames.Select(x => x.Item1).ToList(), body, env);
                }
            case CallExpression(var argumentsExpressions, var function,_):
                {
                    var functionValue = Calc(function, env);
                    switch (functionValue)
                    {
                        case FunctionValue(var argumentNames, var body, var funcEnv):
                            {
                                var givenArgumentsValues = argumentList(argumentsExpressions, argumentNames, env);
                                var newEnv = funcEnv.WithNewContext(givenArgumentsValues.ToArray());
                                return CalcFunctionBody.CalcFunction(body, newEnv);
                            }
                        case BuiltInFunctionValue(var functionName, var argumentNames):
                            {
                                var givenArgumentsValues = argumentList(argumentsExpressions, argumentNames, env);
                                return BiltinFunctions.calc(functionName, givenArgumentsValues.ToArray(), env);
                            }
                        default:
                            throw new Exception($"Method not implemented.");
                    }
                }
            default:
                throw new Exception($"Evaluation Error:{input}");
        }

    }

}
