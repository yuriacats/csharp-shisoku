namespace shisoku.Tests;
using Xunit;
using shisoku;
public class LexerTest
{
    [Fact]
    public void lexIntTestInputOnlyNumber()
    {
        var (number, number_length) = shisoku.Lexer.lexInt("11+");
        Assert.Equal(11, number);
        Assert.Equal(2, number_length);
    }
    [Fact]
    public void lexIntFirstNotANumberTest()
    {
        var (number, number_length) = shisoku.Lexer.lexInt("+");
        Assert.Equal(0, number);
        Assert.Equal(0, number_length);
    }

    [Fact]
    public void lexInputOnlyNumber()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12)
         };
        var tokens = shisoku.Lexer.lex("12");
        Assert.Equal<Token>(expectedToken, tokens);

    }
    [Fact]
    public void lexInputFirstNumberAndAdd()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12)
        };
        var tokens = shisoku.Lexer.lex("12+12");
        Assert.Equal<Token>(expectedToken, tokens);

    }
    [Fact]
    public void lexInputFirstNumberAndSub()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenMinus(),
            new TokenNumber(12)
         };
        var tokens = shisoku.Lexer.lex("12-12");
        Assert.Equal<Token>(expectedToken, tokens);

    }
    [Fact]
    public void lexInputFirstNumberAndMul()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenAsterisk(),
            new TokenNumber(12)
        };
        var tokens = shisoku.Lexer.lex("12*12");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void lexInputFirstNumberAndDiv()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenSlash(),
            new TokenNumber(12)
         };
        var tokens = shisoku.Lexer.lex("12/12");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void lexInputNotEndNumber()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenSlash()
         };
        var tokens = shisoku.Lexer.lex("12/");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void lexInputOtherChars()
    {
        Assert.Throws<Exception>(() => shisoku.Lexer.lex("12aaa/a"));
    }
    [Fact]
    public void lexInputOnlyOtherChars()
    {
        Assert.Throws<Exception>(() => shisoku.Lexer.lex("aaaaa"));
    }
    [Fact]
    public void lexInputOnlyStringAndNumber()
    {
        Assert.Throws<Exception>(() => shisoku.Lexer.lex("12a"));
    }
    [Fact]
    public void lexInputWithWhiteSpace()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12)
         };
        var tokens = shisoku.Lexer.lex("12 + 12");
        Assert.Equal<Token>(expectedToken, tokens);
    }
}
