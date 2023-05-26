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
                    env.Add(name, CalcExpression.Calc(expr, env));
                    continue;
                case StatementReturn(var expr):
                    return (CalcExpression.Calc(expr, env));
                case StatementSwitch(var targetExpression, var cases):
                    var target = CalcExpression.Calc(targetExpression, env);
                    var newEnv = env.WithNewContext();
                    foreach (var aCase in cases)
                    {
                        var caseExpression = CalcExpression.Calc(aCase.Item1, newEnv);
                        if (caseExpression == target)
                        {
                            Calc(aCase.Item2, newEnv);
                            break;
                        }

                    }
                    continue;
                default:
                    throw new Exception("Statemtnt parse Error");
            }
        }
        throw new Exception("Return undifinde");
    }

}
