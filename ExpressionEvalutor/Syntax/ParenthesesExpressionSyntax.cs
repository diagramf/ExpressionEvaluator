using System.Collections.Generic;

namespace ExpressionEvalutor.Syntax
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
