using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor
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
    }
}
