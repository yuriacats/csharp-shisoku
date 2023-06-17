namespace shisoku;
public class CalcFunctionBody
{
    public static Value CalcFunction(Statement[] input, VariableEnvironment env)
    {
        return CalcStatements(input, env) ?? new UnitValue();
    }
    //TODO ToIntの命名の変換を行う
    public static Value? CalcStatements(Statement[] input, VariableEnvironment env)
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
                        if (aCase.Item1 == new VariableExpression("default"))
                        {
                            var result = CalcStatements(aCase.Item2, newEnv);
                            switch (result)
                            {
                                case Value v:
                                    return v;
                                default:
                                    break;
                            }
                        }
                        var caseExpression = CalcExpression.Calc(aCase.Item1, newEnv);
                        if (caseExpression == target)
                        {
                            var result = CalcStatements(aCase.Item2, newEnv);
                            switch (result)
                            {
                                case Value v:
                                    return v;
                                default:
                                    Console.WriteLine("test");
                                    break;
                            }
                            break;
                        }
                    }
                    continue;
                default:
                    throw new Exception("Unknown Statement");
            }
        }
        return null;
    }

}
