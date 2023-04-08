namespace shisoku;
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
            else if (input[0] == '=')
            {
                tokens.Add(new TokenEqual());
                input = input[(1)..];

            }
            else if (Char.IsWhiteSpace(input[0]) || Char.IsControl(input[0]))
            {
                input = input[(1)..];
            }
            else if (Char.IsNumber(input[0]))
            {
                (TokenNumber target, int len) = lexInt(input);
                tokens.Add(target);
                input = input[(len)..];
            }
            else if (Char.IsLetter(input[0]))
            {
                (Token target, int len) = lexString(input);
                tokens.Add(target);
                input = input[(len)..];
            }
            else
            {
                throw new Exception("不正な文字が入力されています");
            }
        }
        return tokens;
    }

    private static (Token target, int len) lexString(string input)
    {
        string targetWord = "";
        foreach (char i in input)
        {
            if (Char.IsLetter(i) || Char.IsNumber(i))
            {
                targetWord += Char.ToString(i);
            }
            else
            {
                break;
            }
        }

        switch (targetWord)
        {
            case "const":
                return (new TokenConst(), targetWord.Length);
            case "var":
                return (new TokenVariable(), targetWord.Length);
            default:
                return (new TokenIdentifier(targetWord), targetWord.Length);
        }

    }


    /// 文字列を受け取って、数字としてトーカナイズできるところまでを読み込み、値を返す
    /// 読み込めない時は　０文字読めて０を返す
    private static (TokenNumber, int) lexInt(String input)
    {
        int lexedLength = 0;
        int number = 0;
        foreach (char i in input)
        {
            if (Char.IsNumber(i))
            {
                int digit = i - '0';
                number = number * 10 + digit;
                lexedLength++;
            }
            else
            {
                break;
            }
        }
        return (new TokenNumber(number), lexedLength);
    }
}

