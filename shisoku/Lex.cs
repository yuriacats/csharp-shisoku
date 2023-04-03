namespace shisoku;
public abstract record Token();
public abstract record TokenSymbol() : Token;
public record TokenNumber(int Number) : Token;
public record TokenPlus() : TokenSymbol;
public record TokenSlash() : TokenSymbol;
public record TokenMinus() : TokenSymbol;
public record TokenAsterisk() : TokenSymbol;
public record TokenStartSection() : Token;
public record TokenEndSection() : Token;
public class Lexer
{


    public static List<Token> lex(String input)
    {
        List<Token> tokens = new List<Token> { };
        while (input.Length >= 1)
        {
            if (input[0] == '+')
            {
                tokens.Add(new TokenPlus());
                input = input[(1)..];
            }
            else if (input[0] == '-')
            {
                tokens.Add(new TokenMinus());
                input = input[(1)..];

            }
            else if (input[0] == '/')
            {
                tokens.Add(new TokenSlash());
                input = input[(1)..];
            }
            else if (input[0] == '*')
            {
                tokens.Add(new TokenAsterisk());
                input = input[(1)..];
            }
            else if (input[0] == '(')
            {
                tokens.Add(new TokenStartSection());
                input = input[(1)..];

            }
            else if (input[0] == ')')
            {
                tokens.Add(new TokenEndSection());
                input = input[(1)..];

            }
            else if (input[0] - '0' >= 0 && input[0] - '0' < 10)
            {
                (int target, int len) = lexInt(input);
                tokens.Add(new TokenNumber(target));
                input = input[(len)..];
            }
            else
            {
                throw new Exception("不正な文字が入力されています");
            }
        }
        return tokens;
    }
    /// 文字列を受け取って、数字としてトーカナイズできるところまでを読み込み、値を返す
    /// 読み込めない時は　０文字読めて０を返す
    public static (int, int) lexInt(String input)
    {
        int number_num = 0;
        int number = 0;
        foreach (char i in input)
        {
            int digit = i - '0';
            if (0 <= digit && digit < 10)
            {
                number = number * 10 + digit;
                number_num++;
            }
            else
            {
                break;
            }
        }
        return (number, number_num);
    }
}

