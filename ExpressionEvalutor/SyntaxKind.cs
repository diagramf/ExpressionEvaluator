using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor
{
    public enum SyntaxKind
    {
        BadToken,

        NumberToken,

        AdditionToken,
        SubtractToken,
        MultiplyToken,
        DivideToken,

        EndOfFileToken,
        WhiteSpaceToken,
        OpenParenthesesToken,
        CloseParenthesesToken,

        NumberExpression,
        BinaryExpression,
        ParenthesesExpression,
    }
}
