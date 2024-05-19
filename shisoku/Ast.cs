
using System.Reflection;

namespace shisoku;
public abstract record Expression(Typeinfo Type);
public abstract record Typeinfo();
public record Unchecked(): Typeinfo();
public  record Checked(Type Type): Typeinfo();
public record NumberExpression(int Number) : Expression(new Checked(new IntType()));
public record VariableExpression(string Name, Typeinfo TypeInfo) : Expression(TypeInfo);
public record BoolExpression(bool Bool) : Expression(new Checked(new BoolType()));
public record AddExpression(Expression Left, Expression Right, Typeinfo TypeInfo) : Expression(TypeInfo);
public record SubExpression(Expression Left, Expression Right, Typeinfo TypeInfo) : Expression(TypeInfo);
public record MulExpression(Expression Left, Expression Right, Typeinfo TypeInfo) : Expression(TypeInfo);
public record DivExpression(Expression Left, Expression Right, Typeinfo TypeInfo) : Expression(TypeInfo);
public record ModExpression(Expression Left, Expression Right, Typeinfo TypeInfo) : Expression(TypeInfo);
public record EqualExpression(Expression Left, Expression Right) : Expression(new Checked(new BoolType()));
public record FunctionExpression(List<(string,Type)> ArgumentNames, Statement[] Body, Type ReturnType) : Expression(new Checked(new FunctionType(ArgumentNames, ReturnType)));
public record RecursiveFunctionExpression(List<(string,Type)> ArgumentNames, Statement[] Body, string FuncName, Type ReturnType) : Expression(new Checked(new FunctionType(ArgumentNames, ReturnType)));
public record CallExpression((string, Expression)[] Arguments, Expression FunctionBody, Typeinfo TypeInfo) : Expression(TypeInfo);
// TODO 型の実装が終わったらFunctionの定義もそれに合わせて変更する。


public abstract record Statement();
public record StatementExpression(Expression Expr) : Statement;
public record StatementConst(string Name, Type type, Expression Expr) : Statement;
public record StatementReturn(Expression Expr) : Statement;
public record StatementSwitch(Expression TargetExpression, (Expression, Statement[])[] Cases) : Statement;

