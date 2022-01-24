using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor
{
    public sealed class SyntaxParser
    {
        private readonly List<SyntaxToken> tokens;
        private int position;

        public SyntaxParser(string text)
        {
            tokens = new List<SyntaxToken>();

            Lexer lexer = new Lexer(text);
            SyntaxToken syntaxToken;
            do
            {
                syntaxToken = lexer.NextToken();

                if (syntaxToken.Kind != SyntaxKind.WhiteSpaceToken &&
                    syntaxToken.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(syntaxToken);
                }

            } while (syntaxToken.Kind != SyntaxKind.EndOfFileToken);
        }

        /// <summary>
        /// 現在の位置からオフセットだけ進んだ位置のトークンを取得します。
        /// </summary>
        /// <remarks>
        /// 取得する位置がトークンリストの長さを超える場合は、最後の要素を取得します。
        /// </remarks>
        /// <param name="offset"></param>
        /// <returns></returns>
        private SyntaxToken Peek(int offset)
        {
            int index = position + offset;

            if (index >= tokens.Count)
            {
                return tokens.LastOrDefault();
            }

            return tokens[index];
        }

        /// <summary>
        /// 現在の位置のトークン
        /// </summary>
        private SyntaxToken Current => Peek(0);
        /// <summary>
        /// 一つ先の位置のトークン
        /// </summary>
        private SyntaxToken Lookahead => Peek(1);

        /// <summary>
        /// トークンを取得して、位置を進めます。
        /// </summary>
        /// <returns></returns>
        private SyntaxToken NextToken()
        {
            SyntaxToken nextToken = Current;
            position++;
            return nextToken;
        }

        private SyntaxToken MatchToken(SyntaxKind kind)
        {
            if (Current.Kind == kind)
            {
                return NextToken();
            }

            return new SyntaxToken(Current.Kind, position, null, null);
        }

        public SyntaxTree Parse()
        {
            var root = ParseExpression();
            var endOfFile = MatchToken(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(root, endOfFile);
        }

        private SyntaxNode ParseExpression()
        {
            return ParseTerm();
        }

        private SyntaxNode ParseTerm()
        {
            var left = ParseFactor();

            while(Current.Kind == SyntaxKind.AdditionToken ||
                  Current.Kind == SyntaxKind.SubtractToken)
            {
                var @operator = NextToken();
                var right = ParseFactor();
                left = new BinaryExpressionSyntax(left, @operator, right);
            }

            return left;
        }

        private SyntaxNode ParseFactor()
        {
            var left = ParsePrimaryExpression();

            while (Current.Kind == SyntaxKind.MultiplyToken ||
                  Current.Kind == SyntaxKind.DivideToken)
            {
                var @operator = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, @operator, right);
            }

            return left;
        }

        private SyntaxNode ParsePrimaryExpression()
        {
            switch (Current.Kind)
            {
                case SyntaxKind.OpenParenthesesToken:
                    return ParseParenthesesExpression();

                case SyntaxKind.NumberToken:
                    return ParseNumberExpression();

                case SyntaxKind.AdditionToken:
                case SyntaxKind.SubtractToken:
                    return ParseUnaryExpression();
            }

            return null;
        }

        private SyntaxNode ParseUnaryExpression()
        {
            SyntaxToken @operator = NextToken();
            SyntaxNode value = ParsePrimaryExpression();

            return new UnaryExpressionSyntax(@operator, value);
        }

        private SyntaxNode ParseParenthesesExpression()
        {
            var openParentheses = MatchToken(SyntaxKind.OpenParenthesesToken);
            var expression = ParseExpression();
            var closeParentheses = MatchToken(SyntaxKind.CloseParenthesesToken);
            return new ParenthesesExpressionSyntax(openParentheses, expression, closeParentheses);
        }

        private SyntaxNode ParseNumberExpression()
        {
            SyntaxToken numberToken = MatchToken(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }
    }
}
