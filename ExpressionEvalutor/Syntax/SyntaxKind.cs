
namespace ExpressionEvalutor.Syntax
{
    /// <summary>
    /// 構文の種類
    /// </summary>
    public enum SyntaxKind
    {
        BadToken,

        NumberToken,

        AdditionToken,
        SubtractToken,
        MultiplyToken,
        DivideToken,
        ModuloToken,
        ExponentiationToken,

        EndOfFileToken,
        WhiteSpaceToken,
        OpenParenthesesToken,
        CloseParenthesesToken,

        UnaryExpression,
        BinaryExpression,
        ParenthesesExpression,
        NumberExpression,
        BadExpression,
    }
}
