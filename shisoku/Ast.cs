
namespace shisoku;
public abstract record Expression();
public record NumberExpression(int number) : Expression;
public record VariableExpression(string name) : Expression;
public record BoolExpression(bool Bool) : Expression;
public record AddExpression(Expression left, Expression right) : Expression;
public record SubExpression(Expression left, Expression right) : Expression;
public record MulExpression(Expression left, Expression right) : Expression;
public record DivExpression(Expression left, Expression right) : Expression;
public record ModExpression(Expression left, Expression right) : Expression;
public record EqualExpression(Expression left, Expression right) : Expression;
public record FunctionExpression(List<String> argumentNames, Statement[] body) : Expression;
public record CallExpression((string, Expression)[] arguments, Expression functionBody) : Expression;
// TODO 型の実装が終わったらFunctionの定義もそれに合わせて変更する。


public abstract record Statement();
public record StatementExpression(Expression expr) : Statement;
public record StatementConst(string name, Expression expr) : Statement;
public record StatementReturn(Expression expr) : Statement;
public record StatementSwitch(Expression targetExpression, (Expression, Statement[])[] cases) : Statement;

