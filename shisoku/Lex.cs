using System;
namespace shisoku;
public class lexer{

public abstract record Token();
public record TokenNumber(int Number):Token;
public record TokenPlus():Token;

public static Token[] lex(String input){
    Token hoge = new TokenPlus();
    return new[] {hoge};
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

