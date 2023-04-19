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
