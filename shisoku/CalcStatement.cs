namespace shisoku;

public class CalcStatement
{
    public static void toInt(Statement[] input)
    {
        foreach (Statement statement in input)
        {

            switch (statement)
            {
                case AstExpression(var expr):
                    Console.WriteLine(CalcExpression.toInt(expr));
                    break;
                case AstConst(var name, var expr):
                    Console.WriteLine($"{name} =");
                    Console.WriteLine(CalcExpression.toInt(expr));
                    break;
                default:
                    throw new Exception("AST parse Error");
            }
        }
    }

}
