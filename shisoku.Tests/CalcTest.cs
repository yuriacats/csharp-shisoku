namespace shisoku.Tests;
using Xunit;
using shisoku;

public class CalcTest
{
    [Fact]
    public void calcInputOnlyNumber()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new AstNumber(12));
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputAdd()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new AstAdd(new AstNumber(5), new AstNumber(7)));
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputSub()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new AstSub(new AstNumber(19), new AstNumber(7)));
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputMul()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new AstMul(new AstNumber(3), new AstNumber(4)));
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputDiv()
    {
        var expectedValue = new IntValue(12);
        var value = shisoku.CalcExpression.Calc(new AstDiv(new AstNumber(24), new AstNumber(2)));
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputEqual()
    {
        var expectedValue = new BoolValue(true);
        var value = shisoku.CalcExpression.Calc(new AstEqual(new AstNumber(12), new AstNumber(12)));
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputEqualFailed()
    {
        var expectedValue = new BoolValue(false);
        var value = shisoku.CalcExpression.Calc(new AstEqual(new AstNumber(11), new AstNumber(12)));
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputEqualBool()
    {
        var expectedValue = new BoolValue(true);
        var value = shisoku.CalcExpression.Calc(new AstEqual(new BoolExpression(true), new BoolExpression(true)));
        Assert.Equal<Value>(expectedValue, value);
    }
    [Fact]
    public void calcInputEqualOtherTypes()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new AstEqual(new BoolExpression(true), new AstNumber(12))));
    }
    [Fact]
    public void calcInputAddCannotBool()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new AstAdd(new BoolExpression(true), new BoolExpression(true))));
    }
    [Fact]
    public void calcInputSubCannotBool()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new AstSub(new BoolExpression(true), new BoolExpression(true))));
    }
    [Fact]
    public void calcInputMulCannotBool()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new AstMul(new BoolExpression(true), new BoolExpression(true))));
    }
    [Fact]
    public void calcInputDivCannotBool()
    {
        Assert.Throws<Exception>(() => shisoku.CalcExpression.Calc(new AstDiv(new BoolExpression(true), new BoolExpression(true))));
    }
}


