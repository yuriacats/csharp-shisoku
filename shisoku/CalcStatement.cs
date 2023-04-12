namespace shisoku;
public class CalcStatement
{
    //TODO ToIntの命名の変換を行う
    public static void toInt(Statement[] input, Dictionary<string, int> env)
    {
        foreach (Statement statement in input)
        {

            switch (statement)
            {
                case AstExpression(var expr):
                    Console.WriteLine(CalcExpression.toInt(expr, env));
                    break;
                case AstConst(var name, var expr):
                    env.Add(name, CalcExpression.toInt(expr, env));
                    break;
                default:
                    throw new Exception("AST parse Error");
            }
        }
    }

}
