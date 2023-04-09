
namespace shisoku;
public abstract record Expression();
public record AstNumber(int Number) : Expression;
public record AstAdd(Expression left, Expression right) : Expression;
public record AstSub(Expression left, Expression right) : Expression;
public record AstMul(Expression left, Expression right) : Expression;
public record AstDiv(Expression left, Expression right) : Expression;

public abstract record Statement();
public record AsrExpression(Expression Expression) : Statement;
public record AstConst(string Name, Expression Value) : Statement;
