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
            else if (input[0] == ' ')
            {
                input = input[(1)..];
            }
            else if (input[0] == '=')
            {
                tokens.Add(new TokenEqual());
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
        int read_length = 0;
        string split_word = "";
        foreach (char i in input)
        {
            if (Char.IsLetter(i) || Char.IsNumber(i))
            {
                split_word += Char.ToString(i);
                read_length++;
            }
            else
            {
                break;
            }
        }

        switch (split_word)
        {
            case "const":
                return (new TokenConst(), read_length);
            case "var":
                return (new TokenVariable(), read_length);
            default:
                return (new TokenIdentifier(split_word), read_length);
        }

    }


    /// 文字列を受け取って、数字としてトーカナイズできるところまでを読み込み、値を返す
    /// 読み込めない時は　０文字読めて０を返す
    private static (TokenNumber, int) lexInt(String input)
    {
        int number_num = 0;
        int number = 0;
        foreach (char i in input)
        {
            if (Char.IsNumber(i))
            {
                int digit = i - '0';
                number = number * 10 + digit;
                number_num++;
            }
            else
            {
                break;
            }
        }
        return (new TokenNumber(number), number_num);
    }
}

