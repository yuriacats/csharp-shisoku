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
                throw new Exception("AST parse Error");
        }
    }

    public static Value Calc(Expression input, VariableEnvironment env)
    {
        switch (input)
        {
            case NumberExpression(var n):
                {

                    return new IntValue(n);
                }
            case VariableExpression(var name):
                {
                    return new IntValue(env[name]);
                }
            case BoolExpression(var n):
                {
                    return new BoolValue(n);
                }
            case AddExpression(var n, var v):
                {
                    return new IntValue(toInt(Calc(n, env)) + toInt(Calc(v, env)));
                }
            case SubExpression(var n, var v):
                {
                    return new IntValue(toInt(Calc(n, env)) - toInt(Calc(v, env)));
                }
            case MulExpression(var n, var v):
                {
                    return new IntValue(toInt(Calc(n, env)) * toInt(Calc(v, env)));
                }
            case DivExpression(var n, var v):
                {
                    return new IntValue(toInt(Calc(n, env)) / toInt(Calc(v, env)));
                }
            case EqualExpression(var n, var v):
                {
                    var calked_n = Calc(n, env);
                    var calked_v = Calc(v, env);
                    switch (calked_n, calked_v)
                    {
                        case (IntValue(var i), IntValue(var j)):
                            return new BoolValue(i == j);
                        case (BoolValue(var i), BoolValue(var j)):
                            return new BoolValue(i == j);
                        default:
                            throw new Exception("AST parse Error");
                    }
                }
            default:
                throw new Exception("AST parse Error");
        }

    }

}
