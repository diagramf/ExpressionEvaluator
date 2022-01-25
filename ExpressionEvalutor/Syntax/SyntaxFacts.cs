using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor.Syntax
{
    public static class SyntaxFacts
    {
        /// <summary>
        /// 単項演算子の優先順位を返します。
        /// </summary>
        /// <remarks>
        /// 優先順位が高いほど値は大きくなります。
        /// 単項演算子でない場合は、すべて0となります。
        /// </remarks>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch(kind)
            {
                case SyntaxKind.AdditionToken:
                case SyntaxKind.SubtractToken:
                    return 1;
            }

            return 0;
        }

        /// <summary>
        /// 二項演算子の優先順位を返します。
        /// </summary>
        /// <remarks>
        /// 優先順位が高いほど値は大きくなります。
        /// 二項演算子でない場合は、すべて0となります。
        /// </remarks>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch(kind)
            {
                case SyntaxKind.ExponentiationToken:
                    return 3;

                case SyntaxKind.MultiplyToken:
                case SyntaxKind.DivideToken:
                case SyntaxKind.ModuloToken:
                    return 2;

                case SyntaxKind.AdditionToken:
                case SyntaxKind.SubtractToken:
                    return 1;
            }

            return 0;
        }
    }
}
