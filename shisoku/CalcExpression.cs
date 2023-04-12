namespace shisoku;

public class CalcExpression
{
    public static int toInt(Expression input, VariableEnvironment env)
    {
        switch (input)
        {
            case NumberExpression(var n):
                {

                    return n;
                }
            case ConstExpression(var name):
                {
                    return env[name];
                }
            case AddExpression(var n, var v):
                {
                    return toInt(n, env) + toInt(v, env);
                }
            case SubExpression(var n, var v):
                {
                    return toInt(n, env) - toInt(v, env);
                }
            case MulExpression(var n, var v):
                {
                    return toInt(n, env) * toInt(v, env);
                }
            case DivExpression(var n, var v):
                {
                    return toInt(n, env) / toInt(v, env);
                }
            default:
                throw new Exception("AST parse Error");
        }

    }

}
