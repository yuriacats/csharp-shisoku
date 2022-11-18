// See https://aka.ms/new-console-template for more information
using System;

Console.WriteLine("hello");

static (int,int) hoge(String input){
    int number_num=0;
    int number = 0;
    foreach (char i in input){
        int digit = i - '0';
        if (0 <= digit && digit < 10){
            number = number* 10 + digit;
            number_num++;
        }else{
            break;
        }
    }
    return(number,number_num);
}
Console.WriteLine(hoge("123abc"));
