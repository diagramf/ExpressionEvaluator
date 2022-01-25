
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

        EndOfFileToken,
        WhiteSpaceToken,
        OpenParenthesesToken,
        CloseParenthesesToken,

        NumberExpression,
        BinaryExpression,
        ParenthesesExpression,
        UnaryExpression,
        BadExpression,
    }
}
