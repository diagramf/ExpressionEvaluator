using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor
{
    public sealed class SyntaxTree
    {
        public SyntaxNode Root {get; set;}
        public SyntaxToken EndOfFileToken { get; set; }
        public SyntaxTree(SyntaxNode root, SyntaxToken endOfFileToken)
        {
            Root = root;
            EndOfFileToken = endOfFileToken;
        }

        public static SyntaxTree Parse(string text)
        {
            SyntaxParser parser = new SyntaxParser(text);
            return parser.Parse();
        }
    }
}
