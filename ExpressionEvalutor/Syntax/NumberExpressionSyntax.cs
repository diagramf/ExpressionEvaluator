using System.Collections.Generic;

namespace ExpressionEvalutor.Syntax
{
    internal sealed class NumberExpressionSyntax : SyntaxNode
    {
        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        public SyntaxToken NumberSyntax { get; }

        public NumberExpressionSyntax(SyntaxToken numberSyntax)
        {
            NumberSyntax = numberSyntax;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberSyntax;
        }
    }
}
