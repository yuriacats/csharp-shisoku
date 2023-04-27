namespace shisoku;
public abstract record Token();
public abstract record TokenSymbol() : Token;
public record TokenNumber(int number) : Token;
public record TokenPlus() : TokenSymbol;
public record TokenSlash() : TokenSymbol;
public record TokenMinus() : TokenSymbol;
public record TokenAsterisk() : TokenSymbol;
public record TokenBracketOpen() : Token;
public record TokenBracketClose() : Token;
// 変数追加機能で加えたToken
public record TokenConst() : Token;
public record TokenVariable() : Token;
public record TokenEqual() : Token;
public record TokenTrue() : Token;
public record TokenFalse() : Token;
public record TokenIdentifier(string Name) : Token;
public record TokenSemicolon() : Token;
public record TokenColon() : Token;
public record TokenComma() : Token;
public record TokenPipe() : Token;
public record TokenArrow() : Token;
public record TokenCurlyBracketOpen() : Token;
public record TokenCurlyBracketClose() : Token;
// TODO SquareBracketの実装ができたら、テストを改変する
public record TokenSquareBracketOpen() : Token;
public record TokenSquareBracketClose() : Token;
// TODO AngleBracketの実装ができたら、テストを改変する
public record TokenAngleBracketOpen() : Token;
public record TokenAngleBracketClose() : Token;
public record TokenQuestion() : Token;
public record TokenEqualEqual() : Token;

