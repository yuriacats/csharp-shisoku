namespace shisoku.Tests;
using Xunit;
using shisoku;
public class lexerTest
{
    [Fact]
    public void lexIntOkTest()
    {
        var (number, numbar_length) = shisoku.lexer.lexInt("11+");
        Assert.Equal(11, number);
        Assert.Equal(2, numbar_length);
    }
    [Fact]
    public void lexIntFirstNotANumberTest()
    {
        var (number, numbar_length) = shisoku.lexer.lexInt("+");
        Assert.Equal(0, number);
        Assert.Equal(0, numbar_length);
    }
}
