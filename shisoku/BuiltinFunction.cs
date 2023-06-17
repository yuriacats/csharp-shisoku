namespace shisoku;

public class BiltinFunctions
{
    public static Value calc(string FuncName, (string, Value)[] argumentValues, VariableEnvironment env)
    {
        switch (FuncName)
        {
            case "print":
                Console.WriteLine(string.Join("", argumentValues.Select(x => x.Item2.ToString())));
                return new UnitValue();
            default:
                throw new Exception($"{FuncName} is not builtinFunction Member");
        }
        throw new Exception($"作成中です。");
    }

}
