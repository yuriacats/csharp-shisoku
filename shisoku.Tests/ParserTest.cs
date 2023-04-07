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

    [Fact]
    public void SimpleAddCanParse()
    {
        var inputToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(13)
        };
        var (outputAst, _) = Parser.parse(inputToken.ToArray());
        var expectedAst = new AstAdd(new AstNumber(12), new AstNumber(13));
        Assert.Equal(expectedAst, outputAst);
    }
    [Fact]
    public void SimpleMulCanParse()
    {
        var inputToken = new List<Token> {
            new TokenNumber(12),
            new TokenAsterisk(),
            new TokenNumber(13)
        };
        var (outputAst, _) = Parser.parse(inputToken.ToArray());
        var expectedAst = new AstMul(new AstNumber(12), new AstNumber(13));
        Assert.Equal(expectedAst, outputAst);
    }
    [Fact]
    public void SimpleSubCanParse()
    {
        var inputToken = new List<Token> {
            new TokenNumber(12),
            new TokenMinus(),
            new TokenNumber(13)
        };
        var (outputAst, _) = Parser.parse(inputToken.ToArray());
        var expectedAst = new AstSub(new AstNumber(12), new AstNumber(13));
        Assert.Equal(expectedAst, outputAst);
    }
    [Fact]
    public void SimpleDivCanParse()
    {
        var inputToken = new List<Token> {
            new TokenNumber(12),
            new TokenSlash(),
            new TokenNumber(13)
        };
        var (outputAst, _) = Parser.parse(inputToken.ToArray());
        var expectedAst = new AstDiv(new AstNumber(12), new AstNumber(13));
        Assert.Equal(expectedAst, outputAst);
    }
    [Fact]
    public void MultipleSubCanParse()
    {
        var inputToken = new List<Token> {
            new TokenNumber(12),
            new TokenMinus(),
            new TokenNumber(13),
            new TokenMinus(),
            new TokenNumber(14)
        };
        var (outputAst, _) = Parser.parse(inputToken.ToArray());
        var expectedAst = new AstSub(
            new AstSub(new AstNumber(12), new AstNumber(13)),
            new AstNumber(14)
        );
        Assert.Equal(expectedAst, outputAst);
    }
    [Fact]
    public void AloneMinusTokenCannotParse()
    {
        var inputToken = new List<Token> {
            new TokenMinus()
        };
        Assert.Throws<Exception>(() => Parser.parse(inputToken.ToArray()));
    }
}
