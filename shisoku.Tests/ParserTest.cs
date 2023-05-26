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
        var (outputAst, _) = ParseExpression.parse(inputToken.ToArray());
        var expectedAst = new NumberExpression(12);
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
        var (outputAst, _) = ParseExpression.parse(inputToken.ToArray());
        var expectedAst = new AddExpression(new NumberExpression(12), new NumberExpression(13));
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
        var (outputAst, _) = ParseExpression.parse(inputToken.ToArray());
        var expectedAst = new MulExpression(new NumberExpression(12), new NumberExpression(13));
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
        var (outputAst, _) = ParseExpression.parse(inputToken.ToArray());
        var expectedAst = new SubExpression(new NumberExpression(12), new NumberExpression(13));
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
        var (outputAst, _) = ParseExpression.parse(inputToken.ToArray());
        var expectedAst = new DivExpression(new NumberExpression(12), new NumberExpression(13));
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
        var (outputAst, _) = ParseExpression.parse(inputToken.ToArray());
        var expectedAst = new SubExpression(
            new SubExpression(new NumberExpression(12), new NumberExpression(13)),
            new NumberExpression(14)
        );
        Assert.Equal(expectedAst, outputAst);
    }
    [Fact]
    public void AloneMinusTokenCannotParse()
    {
        var inputToken = new List<Token> {
            new TokenMinus()
        };
        Assert.Throws<Exception>(() => ParseExpression.parse(inputToken.ToArray()));
    }
    [Fact]
    public void CanParseStatement()
    {
        var inputToken = new List<Token>{
            new TokenNumber(12),
            new TokenMinus(),
            new TokenNumber(13),
            new TokenSemicolon()
        };
        var expectedAst = new Statement[]{
            new StatementExpression(
                new SubExpression(
                    new NumberExpression(12), new NumberExpression(13)
                )
        )};
        var (outputAst, _) = ParseStatement.parse(inputToken.ToArray());
        Assert.Equal(expectedAst, outputAst);
    }
    [Fact]
    public void CannotParseExpressionStatementWithoutSemicolon()
    {
        var inputToken = new List<Token>{
            new TokenNumber(12),
            new TokenMinus(),
            new TokenNumber(13),
        };
        Assert.Throws<Exception>(() => ParseStatement.parse(inputToken.ToArray()));
    }
    [Fact]
    public void CanParseConst()
    {
        var inputToken = new List<Token>{
        new TokenConst(),
        new TokenIdentifier("test"),
        new TokenEqual(),
        new TokenNumber(12),
        new TokenSemicolon()
    };
        var expectedAst = new Statement[] {
            new StatementConst(
                "test",
                new NumberExpression(12)
            )
        };
        (var outputAst, _) = ParseStatement.parse(inputToken.ToArray());
    }
    [Fact]
    public void CannotParseConstWithoutSemicolon()
    {
        var inputToken = new List<Token>{
        new TokenConst(),
        new TokenIdentifier("test"),
        new TokenEqual(),
        new TokenNumber(12),
    };
        Assert.Throws<Exception>(() => ParseStatement.parse(inputToken.ToArray()));
    }

    [Fact]
    public void CanParseTwoConst()
    {
        var inputToken = new List<Token>{
        new TokenConst(),
        new TokenIdentifier("test"),
        new TokenEqual(),
        new TokenNumber(12),
        new TokenSemicolon(),
        new TokenConst(),
        new TokenIdentifier("test2"),
        new TokenEqual(),
        new TokenNumber(12),
        new TokenSemicolon()
    };
        var expectedAst = new Statement[] {
            new StatementConst(
                "test",
                new NumberExpression(12)
            ), new StatementConst(
                "test2",
                new NumberExpression(12)
            )
        };
        (var outputAst, _) = ParseStatement.parse(inputToken.ToArray());
    }
    [Fact]
    public void CanParseFunction()
    {
        var inputToken = new List<Token>{
            new TokenCurlyBracketOpen(),
            new TokenPipe(),
            new TokenPipe(),
            new TokenArrow(),
            new TokenConst(),
            new TokenIdentifier("test"),
            new TokenEqual(),
            new TokenNumber(12),
            new TokenSemicolon(),
            new TokenCurlyBracketClose()
        };
        var expectedAst = new FunctionExpression(new List<string>(), new Statement[]{
            new StatementConst("test", new NumberExpression(12))
        });
        (var result, _) = ParseExpression.parse(inputToken.ToArray());
        switch (result)
        {
            case FunctionExpression(var arguments, var body):
                Assert.Equal(expectedAst.body, body);
                Assert.Equal(expectedAst.argumentNames, arguments);
                break;

            default:
                Assert.Fail("result is not make FunctionExpression");
                break;
        }
    }
    [Fact]
    public void CanNotParseFunction()
    {
        var inputToken = new List<Token>{
            new TokenCurlyBracketOpen(),
            new TokenPipe(),
            new TokenPipe(),
            new TokenArrow(),
        };
        Assert.Throws<Exception>(() => ParseExpression.parse(inputToken.ToArray()));
    }
    [Fact]
    public void twoArgumentsCanParse()
    {
        var inputToken = new List<Token>{
                new TokenCurlyBracketOpen(),
                new TokenPipe(),
                new TokenComma(),
                new TokenIdentifier("hoge"),
                new TokenComma(),
                new TokenIdentifier("huga"),
                new TokenArrow(),
                new TokenConst(),
                new TokenIdentifier("test"),
                new TokenEqual(),
                new TokenNumber(12),
                new TokenSemicolon(),
                new TokenCurlyBracketClose()
            };
        var expectedArguments = new List<String>();
        expectedArguments.Add("hoge");
        expectedArguments.Add("huga");
        var expectedAst = new FunctionExpression(expectedArguments, new Statement[]{
                new StatementConst("test", new NumberExpression(12))
            });
        (var result, _) = ParseExpression.parse(inputToken.ToArray());
        switch (result)
        {
            case FunctionExpression(var arguments, var body):
                Assert.Equal(expectedAst.body, body);
                Assert.Equal(expectedAst.argumentNames, arguments);
                break;

            default:
                Assert.Fail("result is not make FunctionExpression");
                break;
        }
    }
    [Fact]
    public void CanParseFunctionWithReturn()
    {
        var inputToken = new List<Token>{
            new TokenReturn(),
            new TokenNumber(12),
            new TokenSemicolon()
        };
        var expectedAst = new Statement[]{
            new StatementReturn(new NumberExpression(12))
        };

        (var result, _) = ParseStatement.parse(inputToken.ToArray());
        Assert.Equal(expectedAst, result);
    }
    [Fact]
    public void CallExpressionWithoutArgumentsCanParse()
    {
        var inputToken = new List<Token>{
                new TokenIdentifier("hoge"),
                new TokenBracketOpen(),
                new TokenBracketClose()
            };
        var expectedAst = new CallExpression(new (string, Expression)[] { }, new VariableExpression("hoge"));
        (var result, _) = ParseExpression.parse(inputToken.ToArray());
        Console.WriteLine(result);
        switch (result)
        {
            case CallExpression(var arguments, var body):
                Assert.Equal(expectedAst.arguments, arguments);
                Assert.Equal(expectedAst.functionBody, body);
                break;

            default:
                Assert.Fail("Tokens Can not parse CallExpression");
                break;
        }
    }
    [Fact]
    public void CallExpressionWithArgumentCanParse()
    {

        var inputToken = new List<Token>{
                new TokenIdentifier("hoge"),
                new TokenBracketOpen(),
                new TokenIdentifier("huga"),
                new TokenEqual(),
                new TokenNumber(12),
                new TokenBracketClose()
            };
        var expectedAst = new CallExpression(new (string, Expression)[] { ("huga", new NumberExpression(12)) }, new VariableExpression("hoge"));
        (var result, _) = ParseExpression.parse(inputToken.ToArray());
        Console.WriteLine(result);
        switch (result)
        {
            case CallExpression(var arguments, var body):
                Assert.Equal(expectedAst.arguments, arguments);
                Assert.Equal(expectedAst.functionBody, body);
                break;

            default:
                Assert.Fail("Tokens Can not parse CallExpression");
                break;
        }

    }
    [Fact]
    public void CallExpressionWithArgumentsCanParse()
    {

        var inputToken = new List<Token>{
                new TokenIdentifier("hoge"),
                new TokenBracketOpen(),
                new TokenIdentifier("huga"),
                new TokenEqual(),
                new TokenNumber(12),
                new TokenComma(),
                new TokenIdentifier("piyo"),
                new TokenEqual(),
                new TokenNumber(13),
                new TokenBracketClose()
            };
        var expectedAst = new CallExpression(new (string, Expression)[] { ("huga", new NumberExpression(12)), ("piyo", new NumberExpression(13)) }, new VariableExpression("hoge"));
        (var result, _) = ParseExpression.parse(inputToken.ToArray());
        switch (result)
        {
            case CallExpression(var arguments, var body):
                Assert.Equal(expectedAst.arguments, arguments);
                Assert.Equal(expectedAst.functionBody, body);
                break;

            default:
                Assert.Fail("Tokens Cannot parse CallExpression");
                break;
        }
    }
    [Fact]
    public void CallExpressionWithArgumentsWithoutCommaCannotParse()
    {

        var inputToken = new List<Token>{
                new TokenIdentifier("hoge"),
                new TokenBracketOpen(),
                new TokenIdentifier("huga"),
                new TokenEqual(),
                new TokenNumber(12),
                new TokenIdentifier("piyo"),
                new TokenEqual(),
                new TokenNumber(13),
                new TokenBracketClose()
            };
        Assert.Throws<Exception>(() => ParseExpression.parse(inputToken.ToArray()));
    }
    public void switchCanParse()
    {
        var inputToken = new List<Token>{
            new TokenSwitch(),
            new TokenIdentifier("a"),
            new TokenEqualEqual(),
            new TokenNumber(12),
            new TokenQuestion(),
            new TokenTrue(),
            new TokenColon(),
            new TokenCurlyBracketOpen(),
            new TokenReturn(),
            new TokenNumber(12),
            new TokenSemicolon(),
            new TokenCurlyBracketClose(),
            new TokenComma(),
            new TokenIdentifier("default"),
            new TokenColon(),
            new TokenCurlyBracketOpen(),
            new TokenReturn(),
            new TokenNumber(13),
            new TokenCurlyBracketClose()
        };
        (var result, _) = ParseStatement.parse(inputToken.ToArray());
        var exprectedAst = new StatementSwitch(
            new EqualExpression(new VariableExpression("a"), new NumberExpression(12)),
            new List<(Expression, Statement[])>{
            new (new BoolExpression(true),new List<Statement>{new StatementReturn(new NumberExpression(12))}.ToArray()),
            new (new VariableExpression("default"), new List<Statement>{new StatementReturn(new NumberExpression(13))}.ToArray())
            }.ToArray()
        );
        if (result.Length != 1)
        {
            Assert.Fail("Tokens Cannot parse SwitchStatement");
        }
        switch (result[0])
        {
            case StatementSwitch(var targetExpression, var cases):
                Assert.Equal(exprectedAst.targetExpression, targetExpression);
                Assert.Equal(exprectedAst.cases[0].Item1, cases[0].Item1);
                Assert.Equal(exprectedAst.cases[0].Item2[0], cases[0].Item2[0]);
                break;

            default:
                Assert.Fail("Tokens Cannot parse SwitchStatement");
                break;
        }

    }
}

