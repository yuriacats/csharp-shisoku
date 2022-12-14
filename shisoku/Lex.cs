using System;
namespace shisoku;
public abstract record Token();
public abstract record TokenSymble():Token;
public record TokenNumber(int Number):Token;
public record TokenPlus():TokenSymble;
public record TokenSlash():TokenSymble;
public record TokenMinus():TokenSymble;
public record TokenAsterisk():TokenSymble;
public record TokenStartSection():Token;
public record TokenEndSection():Token;
public class lexer{


public static List<Token> lex(String input){
    List<string> raw_tokens= new List<string>{}; 
    int x = 0;
    while(input.Length >= 1){
        if(input[x] == '+'){
            raw_tokens.Add(input[..x]);
            raw_tokens.Add("+");
            input = input[(x+1)..];
            x = 0;
        }else if(input[x] == '-'){
            raw_tokens.Add(input[..x]);
            raw_tokens.Add("-");
            input = input[(x+1)..];
            x = 0;

        }else if(input[x] == '/'){
            raw_tokens.Add(input[..x]);
            raw_tokens.Add("/");
            input = input[(x+1)..];
            x = 0;
        }else if(input[x] == '*'){
            raw_tokens.Add(input[..x]);
            raw_tokens.Add("*");
            input = input[(x+1)..];
            x = 0;
        }else if(input[x] == '('){
            raw_tokens.Add(input[..x]);
            raw_tokens.Add("(");
            input = input[(x+1)..];
            x = 0;

        }else if(input[x] == ')'){
            raw_tokens.Add(input[..x]);
            raw_tokens.Add(")");
            input = input[(x+1)..];
            x = 0;

        }else{
            if(input.Length <= x+1){
                raw_tokens.Add(input);
                break;
            }
            x++;
        }
    }
    List<Token> tokens = new List<Token>{};
    foreach(string raw_token in raw_tokens){
        switch(raw_token){
            case string  i when raw_token == "+" :
                tokens.Add(new TokenPlus());
                break;
            case string  i when raw_token == "-" :
                tokens.Add(new TokenMinus());
                break;
            case string  i when raw_token == "*" :
                tokens.Add(new TokenAsterisk());
                break;
            case string  i when raw_token == "/" :
                tokens.Add(new TokenSlash());
                break;
            case string  i when raw_token == "(" :
                tokens.Add(new TokenStartSection());
                break;
            case string  i when raw_token == ")" :
                tokens.Add(new TokenEndSection());
                break;
            case string  i when raw_token == "" :
                break;
            default:
                (int target, int _) = lexInt(raw_token);
                Token t_n = new TokenNumber(target);
                tokens.Add(t_n);
                break;
        }
    }
    return tokens;
}

public static (int,int) lexInt(String input){
    int number_num=0;
    int number = 0;
    foreach (char i in input){
        int digit = i - '0';
        if (0 <= digit && digit < 10){
            number = number* 10 + digit;
            number_num++;
        }
        else{
            break;
        }
    }
    return(number,number_num);
}
}

