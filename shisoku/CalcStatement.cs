namespace shisoku;
public class CalcStatement
{
    //TODO ToIntの命名の変換を行う
    public static void toInt(Statement[] input, VariableEnvironment env)
    {
        foreach (Statement statement in input)
        {

            switch (statement)
            {
                case StatementExpression(var expr):
                    Console.WriteLine(CalcExpression.Calc(expr, env));
                    break;
                case StatementConst(var name, var expr):
                    env.Add(name, CalcExpression.toInt(CalcExpression.Calc(expr, env)));
                    break;
                default:
                    throw new Exception("AST parse Error");
            }
        }
    }

}
