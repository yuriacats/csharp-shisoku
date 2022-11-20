
namespace shisoku;
public abstract record Ast();
public record AstNumber(int Number):Ast;
public record AstAdd(Ast hidari, Ast migi):Ast;
public record AstSub(Ast hidari, Ast migi):Ast;
public record AstMul(Ast hidari, Ast migi):Ast;
public record AstDiv(Ast hidari, Ast migi):Ast;
public class ast{
    public static (Ast, shisoku.Token[]) parse(shisoku.Token[] input){
        Console.WriteLine($"parse: {ViewTokens(input)}");
        (var defualt_ast,var default_token )=parseMaldiv(input);
        switch(default_token){
            case [TokenPlus,..var nokori]:
                {
                    (var migi_ast, var other)=parse(nokori);
                    return (new AstAdd(defualt_ast,migi_ast), other);
                }
            case [TokenMinus,..var nokori]:
                {
                    (var migi_ast, var other)=parse(nokori);
                    return (new AstSub(defualt_ast,migi_ast), other);
                }
            default:
                return (defualt_ast,default_token);
        }
    }
    public static (Ast, shisoku.Token[]) parseMaldiv(shisoku.Token[] input){
        Console.WriteLine($"parseMaldiv: {ViewTokens(input)}");
        (var defualt_ast,var default_token )=parseNumOrSection(input);
        switch(default_token){
            case [TokenAsterisk,..var nokori]:
                {
                    (var migi_ast, var other)=parse(nokori);
                    return (new AstMul(defualt_ast,migi_ast), other);
                }
            case [TokenSlash,..var nokori]:
                {
                    (var migi_ast, var other)=parse(nokori);
                    return (new AstDiv(defualt_ast,migi_ast), other);
                }
            default:
                return (defualt_ast,default_token);
        }
    }
    public static (Ast, shisoku.Token[]) parseNumOrSection(shisoku.Token[] input){
        Console.WriteLine($"parseNumOrSection: {ViewTokens(input)}");
        switch(input){
            case [TokenNumber(var num),..var nokori]:
                return (new AstNumber(num), nokori);
            case [TokenStartSection, .. var target]:
                (var innner_ast,var token_nokori)=parse(target);
                if(token_nokori[0] is TokenEndSection){
                    return (innner_ast,token_nokori[1..]);
                }else{
                    input.ToList().ForEach(Console.WriteLine);
                    throw new Exception($"Token undifinde: {input}");
                }
            default:
                input.ToList().ForEach(Console.WriteLine);
                throw new Exception($"Token undifinde: {input}");
        }

    } 
static string ViewToken(Token token){
    return token switch
    {
        TokenNumber(var n) => n.ToString(),
        TokenPlus => "+",
        TokenMinus => "-",
        TokenAsterisk => "*",
        TokenSlash => "/",
        TokenStartSection => "(",
        TokenEndSection => ")",  
        _ => throw new Exception("sonna token nai")
    };
}

static string ViewTokens(Token[] tokens) => 
    string.Join( "", tokens.Select(ViewToken)) ;
}