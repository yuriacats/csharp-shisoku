namespace shisoku.Tests;
using Xunit;
using shisoku;

public class CalcTest
{
    [Fact]
    public void NumberEvaluates()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new NumberExpression(12), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void AddEvaluates()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new AddExpression(new NumberExpression(5), new NumberExpression(7)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void SubEvaluates()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new SubExpression(new NumberExpression(16), new NumberExpression(4)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void MulEvaluates()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new MulExpression(new NumberExpression(3), new NumberExpression(4)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void DivEvaluates()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new DivExpression(new NumberExpression(24), new NumberExpression(2)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void ModEvaluates()
    {
        var expectedValue = new IntValue(1);
        var value = shisoku.CalcExpression.Calc(new ModExpression(new NumberExpression(25), new NumberExpression(2)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void EqualEvaluatesToTrueWhenInputsAreSameNumbers()
    {
        var expectedValue = new BoolValue(true);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new NumberExpression(12), new NumberExpression(12)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void EqualEvaluatesToFalseWhenInputsAreDifferentNumbers()
    {
        var expectedValue = new BoolValue(false);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new NumberExpression(11), new NumberExpression(12)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void EqualEvaluatesToTrueWhenBothBooleansAreTrue()
    {
        var expectedValue = new BoolValue(true);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new BoolExpression(true), new BoolExpression(true)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void EqualEvaluatesToTrueWhenBothBooleansAreFalse()
    {
        var expectedValue = new BoolValue(true);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new BoolExpression(false), new BoolExpression(false)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void EqualEvaluatesToFalseWhenFalseAndTrueAreGiven()
    {
        var expectedValue = new BoolValue(false);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new BoolExpression(false), new BoolExpression(true)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void EqualEvaluatesToFalseWhenTrueAndFalseAreGiven()
    {
        var expectedValue = new BoolValue(false);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new BoolExpression(true), new BoolExpression(false)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void EqualExpressionDoesNotEvaluateWhenDiffrentTypeArgmentsAreGiven()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new EqualExpression(new BoolExpression(true), new NumberExpression(12)), new VariableEnvironment()));
    }
    [Fact]
    public void AddExpressionDoesNotEvaluateWhenBooleanTypeArgmentsAreGiven()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new AddExpression(new BoolExpression(true), new BoolExpression(true)), new VariableEnvironment()));
    }
    [Fact]
    public void SubExpressionDoesNotEvaluateWhenBooleanTypeArgmentsAreGiven()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new SubExpression(new BoolExpression(true), new BoolExpression(true)), new VariableEnvironment()));
    }
    [Fact]
    public void MulExpressionDoesNotEvaluateWhenBooleanTypeArgmentsAreGiven()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new MulExpression(new BoolExpression(true), new BoolExpression(true)), new VariableEnvironment()));
    }
    [Fact]
    public void DivExpressionDoesNotEvaluateWhenBooleanTypeArgmentsAreGiven()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new DivExpression(new BoolExpression(true), new BoolExpression(true)), new VariableEnvironment()));
    }
    [Fact]
    public void functionExpressionCanEvaluate()
    {
        var expectedValue = new FunctionValue(new List<string>(), new Statement[] { }, new VariableEnvironment());
        var result = shisoku.CalcExpression.Calc(new FunctionExpression(new List<string>(), new Statement[] { }), new VariableEnvironment());
        switch (result)
        {
            case FunctionValue(var arguments, var body, var env):
                Assert.Equal(expectedValue.argumentNames, arguments);
                Assert.Equal(expectedValue.body, body);
                break;

            default:
                Assert.Fail("result is not make CallExpression");
                break;
        }
    }
    [Fact]
    public void FunctionExpressionCanEvaluate()
    {
        var env = new VariableEnvironment();
        env.Add("func", new FunctionValue(new List<string>(), new Statement[] { }, new VariableEnvironment()));
        var expectedValue = new FunctionValue(new List<string>(), new Statement[] { }, env);
        var result = shisoku.CalcExpression.Calc(new RecursiveFunctionExpression(new List<string>(), new Statement[] { }, "fanc"), new VariableEnvironment());
        switch (result)
        {
            case FunctionValue(var arguments, var body, _):
                Assert.Equal(expectedValue.argumentNames, arguments);
                Assert.Equal(expectedValue.body, body);
                break;

            default:
                Assert.Fail("result is not make CallExpression");
                break;
        }
    }
    [Fact]
    public void FunctionCallCanEvaluate()
    {
        var expression = new CallExpression(
            new (string, Expression)[] { },
                new FunctionExpression(new List<string>(),
                new Statement[] {
                    new StatementReturn(new AddExpression(
                        new NumberExpression(1),new NumberExpression(2)
                        ))
            }
            )
        );
        var expectedValue = new IntValue(3);
        var result = shisoku.CalcExpression.Calc(expression, new VariableEnvironment());
        Assert.Equal(expectedValue, result);
    }
    [Fact]
    public void functionCanHaveConst()
    {
        var expectedValue = new FunctionValue(
            new List<string>(),
            new Statement[] {
                new StatementConst("x",new NumberExpression(12)),
                new StatementReturn(new VariableExpression("x"))
            },
            new VariableEnvironment()
        );
        var result = shisoku.CalcExpression.Calc(new FunctionExpression(
            new List<string>(),
            new Statement[] {
                new StatementConst("x",new NumberExpression(12)),
                new StatementReturn(new VariableExpression("x"))
            }),
            new VariableEnvironment()
        );
        switch (result)
        {
            case FunctionValue(var arguments, var body, var env):
                Assert.Equal(expectedValue.argumentNames, arguments);
                Assert.Equal(expectedValue.body, body);
                break;

            default:
                Assert.Fail("result is not make CallExpression");
                break;
        }
    }
    [Fact]
    public void CallFunctionCanCalc()
    {
        var expectedValue = new IntValue(12);
        var result = shisoku.CalcExpression.Calc(
        new CallExpression(
                new (string, Expression)[] { ("x", new NumberExpression(12)) },
                new FunctionExpression(
                    new List<string>() { "x" },
                    new Statement[] {
                            new StatementReturn(new VariableExpression("x"))
                    }
                )
            ),
            new VariableEnvironment()
        );
        Assert.Equal(expectedValue, result);
    }
    [Fact]
    public void CallFunctionCannotCalcWithExcessiveArguments()
    {
        var expectedValue = new IntValue(12);
        Assert.Throws<Exception>(() =>
            shisoku.CalcExpression.Calc(
            new CallExpression(
                    new (string, Expression)[] { ("x", new NumberExpression(12)), ("y", new NumberExpression(13)) },
                    new FunctionExpression(
                        new List<string>() { "x" },
                        new Statement[] {
                                new StatementReturn(new VariableExpression("x"))
                        }
                    )
                ),
                new VariableEnvironment()
            )
        );
    }
    [Fact]
    public void CallFunctionCannotCalcWithInsufficientArguments()
    {
        var expectedValue = new IntValue(12);
        Assert.Throws<Exception>(() =>
            shisoku.CalcExpression.Calc(
            new CallExpression(
                    new (string, Expression)[] { ("x", new NumberExpression(12)) },
                    new FunctionExpression(
                        new List<string>() { "x", "y" },
                        new Statement[] {
                                new StatementReturn(new AddExpression(
                                    new VariableExpression("x"),
                                    new VariableExpression("y")
                                )
                            )
                        }
                    )
                ),
                new VariableEnvironment()
            )
        );
    }
    [Fact]
    public void CallSwitchCanCalclate()
    {
        var statement = new Statement[] {
            new StatementConst("a",new NumberExpression(12)),
            new StatementSwitch(
                new EqualExpression(new VariableExpression("a"), new NumberExpression(12)),
                new List<(Expression, Statement[])>{
                new (new BoolExpression(true),new List<Statement>{new StatementReturn(new NumberExpression(12))}.ToArray()),
                new (new VariableExpression("default"), new List<Statement>{new StatementReturn(new NumberExpression(13))}.ToArray()) }.ToArray()
            ),
            };
        var expectedValue = new IntValue(12);
        var value = CalcFunctionBody.CalcStatements(statement, new VariableEnvironment());
        Assert.Equal(value, expectedValue);
    }
    [Fact]
    public void CallSwitchCanCalclateNotRerurn()
    {
        var statement = new Statement[] {
            new StatementConst("a",new NumberExpression(12)),
            new StatementSwitch(
                new EqualExpression(new VariableExpression("a"), new NumberExpression(12)),
                new List<(Expression, Statement[])>{
                new (new BoolExpression(true),new Statement[]{}),
                new (new VariableExpression("default"), new List<Statement>{new StatementReturn(new NumberExpression(13))}.ToArray()) }.ToArray()
            ),
            };
        var value = CalcFunctionBody.CalcStatements(statement, new VariableEnvironment());
        Assert.Null(value);
    }
}
