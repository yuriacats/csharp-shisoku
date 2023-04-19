
namespace shisoku;
public abstract record Expression();
public record NumberExpression(int number) : Expression;
public record BoolExpression(bool Bool) : Expression;
public record ConstExpression(string name) : Expression;
public record AddExpression(Expression left, Expression right) : Expression;
public record SubExpression(Expression left, Expression right) : Expression;
public record MulExpression(Expression left, Expression right) : Expression;
public record DivExpression(Expression left, Expression right) : Expression;

public abstract record Statement();
public record AstExpression(Expression expr) : Statement;
public record AstConst(string name, Expression expr) : Statement;
