using ExpressionEvalutor.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor.Evaluation
{
    public sealed class Evalutor
    {
        private readonly SyntaxNode root;
        public Evalutor(SyntaxNode root)
        {
            this.root = root;
        }

        public float Evalute()
        {
            return EvaluteExpression(root);
        }

        public string ToStringExpression()
        {
            return ToStringExpressionRecursively(root);
        }

        private float EvaluteExpression(SyntaxNode node)
        {
            if (node is NumberExpressionSyntax n)
            {
                return Convert.ToSingle(n.NumberSyntax.Value);
            }

            if (node is UnaryExpressionSyntax u)
            {
                var @operator = u.Operator;
                var value = EvaluteExpression(u.Value);
                if (@operator.Kind == SyntaxKind.SubtractToken)
                {
                    return -value;
                }
                else if (@operator.Kind == SyntaxKind.AdditionToken)
                {
                    return value;
                }
                else
                {
                    throw new Exception($"想定しない単項演算子 {@operator.Kind} が渡されました");
                }
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
                else if (@operator.Kind == SyntaxKind.ModuloToken)
                {
                    return left % right;
                }
                else
                {
                    throw new Exception($"想定しない二項演算子 {@operator.Kind} が渡されました。");
                }
            }

            if (node is ParenthesesExpressionSyntax p)
            {
                return EvaluteExpression(p.Expression);
            }

            throw new Exception($"想定しないノード {node.Kind} が渡されました。");
        }

        private string ToStringExpressionRecursively(SyntaxNode node)
        {
            if (node is NumberExpressionSyntax n)
            {
                return n.NumberSyntax.Value.ToString();
            }

            if (node is UnaryExpressionSyntax u)
            {
                var @operator = u.Operator;
                var value = ToStringExpressionRecursively(u.Value);
                if (@operator.Kind == SyntaxKind.SubtractToken)
                {
                    return "-" + value;
                }
                else if (@operator.Kind == SyntaxKind.AdditionToken)
                {
                    return value;
                }
                else
                {
                    throw new Exception($"想定しない単項演算子 {@operator.Kind} が渡されました");
                }
            }

            if (node is BinaryExpressionSyntax b)
            {
                var left = ToStringExpressionRecursively(b.Left);
                var right = ToStringExpressionRecursively(b.Right);
                var @operator = b.Operator;

                if (@operator.Kind == SyntaxKind.AdditionToken)
                {
                    return left + " + " + right;
                }
                else if (@operator.Kind == SyntaxKind.SubtractToken)
                {
                    return left + " - " + right;
                }
                else if (@operator.Kind == SyntaxKind.MultiplyToken)
                {
                    return left + " * " + right;
                }
                else if (@operator.Kind == SyntaxKind.DivideToken)
                {
                    return left + " / " + right;
                }
                else if (@operator.Kind == SyntaxKind.ModuloToken)
                {
                    return left + " % " + right;
                }
                else
                {
                    throw new Exception($"想定しない二項演算子 {@operator.Kind} が渡されました。");
                }
            }

            if (node is ParenthesesExpressionSyntax p)
            {
                return "(" + ToStringExpressionRecursively(p.Expression) + ")";
            }

            throw new Exception($"想定しないノード {node.Kind} が渡されました。");
        }
    }
}
