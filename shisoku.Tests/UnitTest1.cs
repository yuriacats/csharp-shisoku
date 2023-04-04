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
        var teacher_token = new List<Token> { };
        teacher_token.Add(new TokenNumber(12));
        var tokens = shisoku.Lexer.lex("12");
        Assert.Equal<Token>(teacher_token, tokens);

    }
    [Fact]
    public void lexInputFirstNumberAndAdd()
    {
        var teacher_token = new List<Token> { };
        teacher_token.Add(new TokenNumber(12));
        teacher_token.Add(new TokenPlus());
        teacher_token.Add(new TokenNumber(12));
        var tokens = shisoku.Lexer.lex("12+12");
        Assert.Equal<Token>(teacher_token, tokens);

    }
    [Fact]
    public void lexInputFirstNumberAndSub()
    {
        var teacher_token = new List<Token> { };
        teacher_token.Add(new TokenNumber(12));
        teacher_token.Add(new TokenMinus());
        teacher_token.Add(new TokenNumber(12));
        var tokens = shisoku.Lexer.lex("12-12");
        Assert.Equal<Token>(teacher_token, tokens);

    }
    [Fact]
    public void lexInputFirstNumberAndMul()
    {
        var teacher_token = new List<Token> { };
        teacher_token.Add(new TokenNumber(12));
        teacher_token.Add(new TokenAsterisk());
        teacher_token.Add(new TokenNumber(12));
        var tokens = shisoku.Lexer.lex("12*12");
        Assert.Equal<Token>(teacher_token, tokens);
    }
    [Fact]
    public void lexInputFirstNumberAndDiv()
    {
        var teacher_token = new List<Token> { };
        teacher_token.Add(new TokenNumber(12));
        teacher_token.Add(new TokenSlash());
        teacher_token.Add(new TokenNumber(12));
        var tokens = shisoku.Lexer.lex("12/12");
        Assert.Equal<Token>(teacher_token, tokens);
    }
    [Fact]
    public void lexInputNotEndNumber()
    {
        var teacher_token = new List<Token> { };
        teacher_token.Add(new TokenNumber(12));
        teacher_token.Add(new TokenSlash());
        var tokens = shisoku.Lexer.lex("12/");
        Assert.Equal<Token>(teacher_token, tokens);
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
        var teacher_token = new List<Token> { };
        teacher_token.Add(new TokenNumber(12));
        teacher_token.Add(new TokenPlus());
        teacher_token.Add(new TokenNumber(12));
        var tokens = shisoku.Lexer.lex("12 + 12");
        Assert.Equal<Token>(teacher_token, tokens);
    }
    public void lexInputSplitNumberWithWhitespace()
    {
        Assert.Throws<Exception>(() => shisoku.Lexer.lex("12 12"));
    }
    public void lexInputStartParenthesisAfterEndParenthesis()
    {
        Assert.Throws<Exception>(() => shisoku.Lexer.lex("(12+12)(12+12)"));
    }
}
