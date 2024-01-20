namespace shisoku;
public abstract record Type();
//public record ListType(Type ElementType) : Type;
public record IntType() : Type;
public record BoolType() : Type;
//public record StringType() : Type;
public record FunctionType(List<(string ,Type)> ArgumentTypes, Type ReturnType) : Type;