namespace shisoku;

public class BiltinFunctions
{
    public static Value calc(string FuncName, (string, Value)[] argumentValues, VariableEnvironment env)
    {
        switch (FuncName)
        {
            case "print":
                // TODO printに対応する名前付き引数の名前を対応させる.print(message="")が適切そう
                Console.WriteLine(string.Join("", argumentValues.Select(x => x.Item2.ToString())));
                return new UnitValue();
            default:
                throw new Exception($"{FuncName} is not bultinFunction Menber");
        }
        throw new Exception($"作成中です。");
    }

}
