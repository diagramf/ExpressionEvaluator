using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor
{
    /// <summary>
    /// 構文ツリーのノード
    /// </summary>
    public abstract class SyntaxNode
    {
        /// <summary>
        /// 構文の種類
        /// </summary>
        public abstract SyntaxKind Kind { get; }

        /// <summary>
        /// このノードの子要素を取得します。
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}
