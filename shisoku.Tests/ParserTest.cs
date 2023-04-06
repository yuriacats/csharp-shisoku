namespace shisoku.Tests;
using Xunit;
using shisoku;

public class ParserTest
{
    [Fact]
    public void numberCanParse()
    {
        var inputToken = new List<Token> {
            new TokenNumber(12)
        };
        var (outputAst, _) = Parser.parse(inputToken.ToArray());
        var expectedAst = new AstNumber(12);
        Assert.Equal(expectedAst, outputAst);
    }

}
