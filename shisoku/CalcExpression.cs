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
            case FunctionValue(var argumentNames, var body):
                return new FunctionValue(argumentNames, body);
            default:
                throw new Exception($"Evaluation Error:augment type is not FunctionValue({input})");
        }
    }

    public static Value Calc(Expression input, VariableEnvironment env)
    {
        switch (input)
        {
            case NumberExpression(var value):
                {

                    return new IntValue(value);
                }
            case VariableExpression(var name):
                {
                    return env[name];
                }
            case BoolExpression(var value):
                {
                    return new BoolValue(value);
                }
            case AddExpression(var lhs, var rhs):
                {
                    return new IntValue(toInt(Calc(lhs, env)) + toInt(Calc(rhs, env)));
                }
            case SubExpression(var lhs, var rhs):
                {
                    return new IntValue(toInt(Calc(lhs, env)) - toInt(Calc(rhs, env)));
                }
            case MulExpression(var lhs, var rhs):
                {
                    return new IntValue(toInt(Calc(lhs, env)) * toInt(Calc(rhs, env)));
                }
            case DivExpression(var lhs, var rhs):
                {
                    return new IntValue(toInt(Calc(lhs, env)) / toInt(Calc(rhs, env)));
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
            case FunctionExpression(var argumentNames, var body):
                {
                    return new FunctionValue(argumentNames, body);
                }
            case CallExpression(var arguments, var function):
                {
                    var newEnv = env.WithNewContext();
                    var targetValue = Calc(function, newEnv);
                    switch (targetValue)
                    {
                        case FunctionValue(var argumentNames, var body):
                            for (int i = 0; i < arguments.Length; i++)
                            {
                                if (argumentNames.Contains(arguments[i].Item1))
                                {
                                    newEnv.Add(arguments[i].Item1, Calc(arguments[i].Item2, newEnv));
                                }
                                else
                                {
                                    throw new Exception($"{arguments[i].Item1} is not defined. in {argumentNames}");
                                }
                            }
                            return CalcFunctionBody.Calc(body, newEnv);
                        default:
                            throw new Exception($"Method not implemented.");
                    }
                }
            default:
                throw new Exception($"Evaluation Error:{input}");
        }

    }

}
