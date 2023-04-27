namespace shisoku.Tests;
using Xunit;
using shisoku;
//TODO Lexerのテストの命名を全体的に変更する
public class LexerTest
{
    [Fact]
    public void NumberCanBeTokenized()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12)
         };
        var tokens = shisoku.Lexer.lex("12");
        Assert.Equal<Token>(expectedToken, tokens);

    }
    [Fact]
    public void PlusCanBeTokenized()
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
    public void MinusCanBeTokenized()
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
    public void AsteriskCanbeTokenized()
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
    public void SlashCanBeTokenized()
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
    public void SlashOfEndCanBeTokenized()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenSlash()
         };
        var tokens = shisoku.Lexer.lex("12/");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void WordsCanBeTokenized()
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
    public void AWordCanBeTokenized()
    {
        var expectedToken = new List<Token> {
            new TokenIdentifier("aaaa"),
         };
        var tokens = shisoku.Lexer.lex("aaaa");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void NumberAndIdentifierTokenizedSeparatedlyWhenANumberIsGivenBeforString()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenIdentifier("a"),
         };
        var tokens = shisoku.Lexer.lex("12a");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void TokenizedIgnoringSpaces()
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
    public void ConstCanBeTokernized()
    {
        var expectedToken = new List<Token> {
            new TokenConst()
         };
        var tokens = shisoku.Lexer.lex("const");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void WordStartingWithConstCanBeTokernizedasIdentifier()
    {
        var expectedToken = new List<Token> {
            new TokenConst(),
            new TokenIdentifier("constA")
         };
        var tokens = shisoku.Lexer.lex("const constA");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void varCanBeTokernized()
    {
        var expectedToken = new List<Token> {
            new TokenVariable(),
            new TokenIdentifier("constA")
         };
        var tokens = shisoku.Lexer.lex("var constA");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void TrueCanBeTokernized()
    {
        var expectedToken = new List<Token> {
            new TokenVariable(),
            new TokenIdentifier("constA"),
            new TokenTrue()
         };
        var tokens = shisoku.Lexer.lex("var constA true");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void FalseCanBeTokenized()
    {
        var expectedToken = new List<Token> {
            new TokenVariable(),
            new TokenIdentifier("constA"),
            new TokenFalse()
         };
        var tokens = shisoku.Lexer.lex("var constA false");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void EqualEqualCanBeTokenized()
    {
        var expectedToken = new List<Token> {
            new TokenTrue(),
            new TokenEqualEqual(),
            new TokenTrue()

         };
        var tokens = shisoku.Lexer.lex("true == true");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void WordWithNumberCanBeTokernizedasIdentifier()
    {
        var expectedToken = new List<Token> {
            new TokenIdentifier("const12")
         };
        var tokens = shisoku.Lexer.lex("const12");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void EqualCanbeTokenized()
    {
        var expectedToken = new List<Token> {
            new TokenEqual(),
         };
        var tokens = shisoku.Lexer.lex("=");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void TokenizedIgnoringTab()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12)
         };
        var tokens = shisoku.Lexer.lex("12\t+\t12");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void TokenizedIgnoringNewLine()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12)
         };
        var tokens = shisoku.Lexer.lex("12\n+\n12");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void TokenizedIgnoringNewLineAndTab()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12)
         };
        var tokens = shisoku.Lexer.lex("12\n\t+\n\t12");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void SemicolonCanBeTokernized()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12),
            new TokenSemicolon(),
         };
        var tokens = shisoku.Lexer.lex("12+12;");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void CommaCanBeTokernized()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12),
            new TokenComma(),
         };
        var tokens = shisoku.Lexer.lex("12+12,");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void PipeCanBeTokenized()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12),
            new TokenPipe(),
         };
        var tokens = shisoku.Lexer.lex("12+12|");
        Assert.Equal<Token>(expectedToken, tokens);
        //TODO Pipeを使う構文ができたら、テストを改変する
    }
    [Fact]
    public void ColonCanBeTokernized()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12),
            new TokenColon(),
         };
        var tokens = shisoku.Lexer.lex("12+12:");
        Assert.Equal<Token>(expectedToken, tokens);
        //TODO Colonを使う構文ができたら、テストを改変する
    }
    [Fact]
    public void ArrowCanBeTokenized()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12),
            new TokenArrow(),
         };
        var tokens = shisoku.Lexer.lex("12+12->");
        Assert.Equal<Token>(expectedToken, tokens);
        //TODO Arrowを使う構文ができたら、テストを改変する
    }
    [Fact]
    public void BracketCanBeTokernized()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12),
            new TokenBracketOpen(),
            new TokenNumber(12),
            new TokenBracketClose(),
         };
        var tokens = shisoku.Lexer.lex("12+12(12)");
        Assert.Equal<Token>(expectedToken, tokens);
        //TODO Bracketを使う構文ができたら、テストを改変する
    }
    [Fact]
    public void CurlyBracketCanBeTokernized()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenPlus(),
            new TokenNumber(12),
            new TokenCurlyBracketOpen(),
            new TokenNumber(12),
            new TokenCurlyBracketClose(),
         };
        var tokens = shisoku.Lexer.lex("12+12{12}");
        Assert.Equal<Token>(expectedToken, tokens);
        //TODO Bracketを使う構文ができたら、テストを改変する
    }
    [Fact]
    public void QuestionCanBeTokenized()
    {
        var expectedToken = new List<Token> {
            new TokenNumber(12),
            new TokenQuestion(),
        };
        var tokens = shisoku.Lexer.lex("12?");
        Assert.Equal<Token>(expectedToken, tokens);
    }
    [Fact]
    public void returnCanBeTokenized()
    {
        // Given
        var expectedToken = new List<Token>{
            new TokenReturn(),
            new TokenNumber(12),
        };

        // When
        var tokens = shisoku.Lexer.lex("return 12");
        Assert.Equal<Token>(expectedToken, tokens);

        // Then
    }
}
