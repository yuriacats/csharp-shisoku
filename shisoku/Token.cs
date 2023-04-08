namespace shisoku;
public abstract record Token();
public abstract record TokenSymbol() : Token;
public record TokenNumber(int Number) : Token;
public record TokenPlus() : TokenSymbol;
public record TokenSlash() : TokenSymbol;
public record TokenMinus() : TokenSymbol;
public record TokenAsterisk() : TokenSymbol;
public record TokenStartSection() : Token;
public record TokenEndSection() : Token;
// 変数追加機能で加えたToken
public record TokenConst() : Token;
public record TokenVariable() : Token;
public record TokenEqual() : Token;
public record TokenIdentifier(string Name) : Token;

