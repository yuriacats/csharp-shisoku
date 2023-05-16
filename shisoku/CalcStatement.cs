namespace shisoku;
public class CalcFunctionBody
{
    //TODO ToIntの命名の変換を行う
    public static Value Calc(Statement[] input, VariableEnvironment env)
    {
        foreach (Statement statement in input)
        {

            switch (statement)
            {
                case StatementExpression(var expr):
                    CalcExpression.Calc(expr, env);
                    continue;
                case StatementConst(var name, var expr):
                    //Envのリストに対して思った通りの挿入をする関数を作る必要がある。
                    env.Add(name, CalcExpression.Calc(expr, env));
                    continue;
                case StatementReturn(var expr):
                    return (CalcExpression.Calc(expr, env));
                default:
                    throw new Exception("Statemtnt parse Error");
            }
        }
        throw new Exception("Return undifinde");
    }

}
