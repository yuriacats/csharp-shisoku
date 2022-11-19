
namespace shisoku;
public abstract record Ast();
public record AstNumber(int Number):Ast;
public record AstPlus(Ast hidari, Ast migi):Ast;
public class ast{
    public static Ast parse(shisoku.Token[] input){
        switch(input){
            case [TokenNumber(var i), TokenPlus,.. var nokori]:
                return new AstPlus(new AstNumber(i), parse(nokori));
            case [TokenNumber(var i)]:
                return new AstNumber(i);
            default:
                throw new Exception("Token undifinde");
        }
    }
}