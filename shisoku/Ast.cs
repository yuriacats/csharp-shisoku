
namespace shisoku;
public abstract record Ast();
public record AstNumber(int Number) : Ast;
public record AstAdd(Ast left, Ast right) : Ast;
public record AstSub(Ast left, Ast right) : Ast;
public record AstMul(Ast left, Ast right) : Ast;
public record AstDiv(Ast left, Ast right) : Ast;
