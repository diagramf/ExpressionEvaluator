using System.Collections.Generic;

namespace ExpressionEvalutor.Syntax
{
    internal sealed class UnaryExpressionSyntax : SyntaxNode
    {
        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;
        public SyntaxToken Operator { get; }
        public SyntaxNode Value { get; }

        public UnaryExpressionSyntax(SyntaxToken @operator, SyntaxNode value)
        {
            Operator = @operator;
            Value = value;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Operator;
            yield return Value;
        }
    }
}
