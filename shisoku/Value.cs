namespace shisoku;
using System.Linq;
public abstract record Value();
public record IntValue(int Value) : Value
{
    public override string ToString() => Value.ToString();
};
public record BoolValue(bool Value) : Value
{
    public override string ToString() => Value.ToString();
};
public record FunctionValue(List<string> argumentNames, Statement[] body, VariableEnvironment env) : Value
{
    public override string ToString() => $"FunctionValue({string.Join(", ", argumentNames)})";
};
public record BuiltInFunctionValue(string FunctionName, List<string> argumentNames) : Value
{
    public override string ToString() => $"{FunctionName}({string.Join(", ", argumentNames)})";
}
public record UnitValue() : Value
{
    public override string ToString() => "Unit";
}


public class VariableEnvironment
{
    List<System.Collections.Generic.Dictionary<string, Value>> envDictionaries;
    public VariableEnvironment(List<Dictionary<string, Value>> envDictionaries)
    {
        this.envDictionaries = envDictionaries;
    }
    public VariableEnvironment()
    {
        this.envDictionaries = new List<Dictionary<string, Value>> { new Dictionary<string, Value>(){
            {"print", new BuiltInFunctionValue("print", new List<string>(){ "message" })}
        },
         };
    }
    public void Add(string name, Value value)
    {
        envDictionaries.Last().Add(name, value);
    }
    public VariableEnvironment WithNewContext()
    {
        var newEnvDictionaries = new List<Dictionary<string, Value>>(envDictionaries);
        newEnvDictionaries.Add(new Dictionary<string, Value>());
        return new VariableEnvironment(newEnvDictionaries);
    }
    public VariableEnvironment WithNewContext((string, Value)[] givenEnvDictionaries)
    {
        var newEnvDictionaries = new List<Dictionary<string, Value>>(envDictionaries);
        newEnvDictionaries.Add(givenEnvDictionaries.ToDictionary(x => x.Item1, x => x.Item2));
        return new VariableEnvironment(newEnvDictionaries);
    }
    public Value this[string name]
    {
        get
        {
            for (var counter = envDictionaries.Count - 1; counter >= 0; counter--)
            {
                var env = envDictionaries[counter];
                if (env.ContainsKey(name))
                {
                    return env[name];
                }
            }
            throw new Exception($"Evaluation Error:variable {name} is not defined");
        }
    }
}

