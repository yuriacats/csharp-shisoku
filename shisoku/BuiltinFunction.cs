namespace shisoku;

public class BiltinFunctions
{
    public static Value calc(string FuncName, (string, Value)[] argumentValues, VariableEnvironment env)
    {
        switch (FuncName)
        {
            case "print":
                break;
            default:
                throw new Exception($"{FuncName} is not bultinFunction Menber");
        }
        throw new Exception($"作成中です。");
    }

}
