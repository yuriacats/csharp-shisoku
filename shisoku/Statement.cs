namespace shisoku;
public class ParseStatement
{
    public void test()
    {
        var tokens = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12)
        };
    }
}
