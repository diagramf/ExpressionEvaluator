using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor
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
