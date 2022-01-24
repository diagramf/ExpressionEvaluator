using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor
{
    internal sealed class ParenthesesExpressionSyntax : SyntaxNode
    {
        public override SyntaxKind Kind => SyntaxKind.ParenthesesExpression;

        public SyntaxNode OpenParentheses { get; }
        public SyntaxNode Expression { get; }
        public SyntaxNode CloseParentheses { get; }

        public ParenthesesExpressionSyntax(SyntaxNode openParentheses, SyntaxNode expression, SyntaxNode closeParentheses)
        {
            OpenParentheses = openParentheses;
            Expression = expression;
            CloseParentheses = closeParentheses;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParentheses;
            yield return Expression;
            yield return CloseParentheses;
        }
    }
}
