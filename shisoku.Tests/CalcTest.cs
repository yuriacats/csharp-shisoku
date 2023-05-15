namespace shisoku.Tests;
using Xunit;
using VariableEnvironment = System.Collections.Generic.Dictionary<string, int>;
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
        var expectedValue = new FunctionValue(new List<string>(), new Statement[] { });
        var value = shisoku.CalcExpression.Calc(new FunctionExpression(new List<string>(), new Statement[] { }), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
}
