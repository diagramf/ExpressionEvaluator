
namespace ExpressionEvalutor.Syntax
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
