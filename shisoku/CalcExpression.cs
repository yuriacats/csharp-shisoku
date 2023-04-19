namespace shisoku;

public class CalcExpression
{
    public static int toInt(Expression input, VariableEnvironment env)
    {
        switch (input)
        {
            case IntValue(var n):
                return n;
            default:
                throw new Exception("AST parse Error");
        }
    }

    public static Value Calc(Expression input)
    {
        switch (input)
        {
            case NumberExpression(var n):
                {

                    return new IntValue(n);
                }
            case BoolExpression(var n):
                {
                    return new BoolValue(n);
                }
            case ConstExpression(var name):
                {
                    return new IntValue(toInt(Calc(n)) + toInt(Calc(v)));

                }
            case AddExpression(var n, var v):
                {
                    return new IntValue(toInt(Calc(n)) - toInt(Calc(v)));
                }
            case SubExpression(var n, var v):
                {
                    return new IntValue(toInt(Calc(n)) * toInt(Calc(v)));
                }
            case MulExpression(var n, var v):
                {
                    return new IntValue(toInt(Calc(n)) / toInt(Calc(v)));
                }
            case AstEqual(var n, var v):
                {
                    var calked_n = Calc(n);
                    var calked_v = Calc(v);
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
