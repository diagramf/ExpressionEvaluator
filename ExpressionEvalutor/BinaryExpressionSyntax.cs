using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor
{
    internal sealed class BinaryExpressionSyntax : SyntaxNode
    {
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
        public SyntaxNode Left { get; }
        public SyntaxToken Operator { get; }
        public SyntaxNode Right { get; }

        public BinaryExpressionSyntax(SyntaxNode left, SyntaxToken @operator, SyntaxNode right)
        {
            Left = left;
            Operator = @operator;
            Right = right;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return Operator;
            yield return Right;
        }
    }
}
