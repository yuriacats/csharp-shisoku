namespace shisoku.Tests;
using Xunit;
using VariableEnvironment = System.Collections.Generic.Dictionary<string, int>;
using shisoku;

public class CalcTest
{
    [Fact]
    public void calcInputOnlyNumberExpression()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new NumberExpression(12), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputAdd()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new AddExpression(new NumberExpression(5), new NumberExpression(7)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputSubExpression()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new SubExpression(new NumberExpression(16), new NumberExpression(4)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputMulExpression()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new MulExpression(new NumberExpression(3), new NumberExpression(4)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputDivExpression()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new DivExpression(new NumberExpression(24), new NumberExpression(2)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputEqual()
    {
        var expectedValue = new BoolValue(true);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new NumberExpression(12), new NumberExpression(12)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputEqualFailed()
    {
        var expectedValue = new BoolValue(false);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new NumberExpression(11), new NumberExpression(12)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputEqualTrue()
    {
        var expectedValue = new BoolValue(true);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new BoolExpression(true), new BoolExpression(true)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputEqualFalse()
    {
        var expectedValue = new BoolValue(true);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new BoolExpression(false), new BoolExpression(false)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputEqualTrueFalse()
    {
        var expectedValue = new BoolValue(false);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new BoolExpression(false), new BoolExpression(true)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputEqualFalseTrue()
    {
        var expectedValue = new BoolValue(false);
        var value = shisoku.CalcExpression.Calc(new EqualExpression(new BoolExpression(true), new BoolExpression(false)), new VariableEnvironment());
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputEqualOtherTypes()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new EqualExpression(new BoolExpression(true), new NumberExpression(12)), new VariableEnvironment()));
    }
    [Fact]
    public void calcInputAddCannotBool()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new AddExpression(new BoolExpression(true), new BoolExpression(true)), new VariableEnvironment()));
    }
    [Fact]
    public void calcInputSubCannotBool()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new SubExpression(new BoolExpression(true), new BoolExpression(true)), new VariableEnvironment()));
    }
    [Fact]
    public void calcInputMulCannotBool()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new MulExpression(new BoolExpression(true), new BoolExpression(true)), new VariableEnvironment()));
    }
    [Fact]
    public void calcInputDivCannotBool()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new DivExpression(new BoolExpression(true), new BoolExpression(true)), new VariableEnvironment()));
    }
}
