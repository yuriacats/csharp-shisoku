namespace shisoku;
public abstract record Value();
public record IntValue(int Value) : Value
{
    public override string ToString() => Value.ToString();
};
public record BoolValue(bool Value) : Value
{
    public override string ToString() => Value.ToString();
};
public record FunctionValue(List<string> argumentNames, Statement[] body) : Value
{
    public override string ToString() => $"FunctionValue({string.Join(", ", argumentNames)})";
};
