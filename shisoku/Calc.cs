namespace shisoku;

public class MyCalc
{
    public static int toInt(Ast input)
    {
        switch (input)
        {
            case AstNumber(var n):
                {

                    return n;
                }
            case AstAdd(var n, var v):
                {
                    return toInt(n) + toInt(v);
                }
            case AstSub(var n, var v):
                {
                    return toInt(n) - toInt(v);
                }
            case AstMul(var n, var v):
                {
                    return toInt(n) * toInt(v);
                }
            case AstDiv(var n, var v):
                {
                    return toInt(n) / toInt(v);
                }
            default:
                throw new Exception("AST parse Error");
        }

    }

}
