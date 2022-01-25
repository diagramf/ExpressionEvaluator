using System.Collections.Generic;
using System.Linq;

namespace ExpressionEvalutor.Syntax
{
    internal sealed class BadExpressionSyntax : SyntaxNode
    {
        public override SyntaxKind Kind => SyntaxKind.BadExpression;
        public SyntaxNode Syntax { get; }

        public BadExpressionSyntax(SyntaxNode syntax)
        {
            Syntax = syntax;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}
