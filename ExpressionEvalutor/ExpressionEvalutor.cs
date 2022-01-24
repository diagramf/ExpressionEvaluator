using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor
{
    public sealed class ExpressionEvalutor
    {
        private readonly SyntaxNode root;
        public ExpressionEvalutor(SyntaxNode root)
        {
            this.root = root;
        }

        public float Evalute()
        {
            return EvaluteExpression(root);
        }

        private float EvaluteExpression(SyntaxNode node)
        {
            if (node is NumberExpressionSyntax n)
            {
                return Convert.ToSingle(n.NumberSyntax.Value);
            }

            if (node is BinaryExpressionSyntax b)
            {
                var left = EvaluteExpression(b.Left);
                var right = EvaluteExpression(b.Right);
                var @operator = b.Operator;

                if (@operator.Kind == SyntaxKind.AdditionToken)
                {
                    return left + right;
                }
                else if (@operator.Kind == SyntaxKind.SubtractToken)
                {
                    return left - right;
                }
                else if (@operator.Kind == SyntaxKind.MultiplyToken)
                {
                    return left * right;
                }
                else if (@operator.Kind == SyntaxKind.DivideToken)
                {
                    return left / right;
                }
                else
                {
                    throw new Exception($"想定しないオペレータ {@operator.Kind} が渡されました。");
                }
            }

            if (node is ParenthesesExpressionSyntax p)
            {
                return EvaluteExpression(p.Expression);
            }

            throw new Exception($"想定しないノード {node.Kind} が渡されました。");
        }
    }
}
