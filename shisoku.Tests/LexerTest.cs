namespace shisoku.Tests;
using Xunit;
using shisoku;
public class LexerTest
{
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
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenIdentifier("aaa"),
            new TokenSlash(),
            new TokenIdentifier("a"),
         };
        var tokens = shisoku.Lexer.lex("12aaa/a");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void lexInputOnlyOtherChars()
    {
        var expectedToken = new List<Token> {
            new TokenIdentifier("aaaa"),
         };
        var tokens = shisoku.Lexer.lex("aaaa");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void lexInputOnlyStringAndNumber()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenIdentifier("a"),
         };
        var tokens = shisoku.Lexer.lex("12a");
        Assert.Equal<Token>(expectedToken, tokens);
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
    [Fact]
    public void lexInputConst()
    {
        var expectedToken = new List<Token> {
            new TokenConst()
         };
        var tokens = shisoku.Lexer.lex("const");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void lexInputConstAndIdentifier()
    {
        var expectedToken = new List<Token> {
            new TokenConst(),
            new TokenIdentifier("constA")
         };
        var tokens = shisoku.Lexer.lex("const constA");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void lexInputVarAndIdentifier()
    {
        var expectedToken = new List<Token> {
            new TokenVariable(),
            new TokenIdentifier("constA")
         };
        var tokens = shisoku.Lexer.lex("var constA");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void lexInputIdentifierInNumber()
    {
        var expectedToken = new List<Token> {
            new TokenIdentifier("const12")
         };
        var tokens = shisoku.Lexer.lex("const12");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void lexInputDefinition()
    {
        var expectedToken = new List<Token> {
            new TokenEqual(),
         };
        var tokens = shisoku.Lexer.lex("=");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void lexInputTab()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12)
         };
        var tokens = shisoku.Lexer.lex("12\t+\t12");
        Assert.Equal<Token>(expectedToken, tokens);
    }


}
