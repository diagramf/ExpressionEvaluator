using System.Collections.Generic;
using System.Linq;

namespace ExpressionEvalutor.Syntax
{
    /// <summary>
    /// 構文のトークンを表します。
    /// 数字やオペレータなどの構文での一つの塊を表します。
    /// </summary>
    public class SyntaxToken : SyntaxNode
    {
        /// <summary>
        /// トークンが存在する位置
        /// </summary>
        public int Position { get; }
        /// <summary>
        /// トークンのテキスト
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// トークンの値
        /// </summary>
        public object Value { get; }

        public override SyntaxKind Kind { get; }

        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}
