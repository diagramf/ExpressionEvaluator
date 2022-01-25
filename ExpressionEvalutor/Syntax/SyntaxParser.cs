using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionEvalutor.Syntax
{
    public sealed class SyntaxParser
    {
        private readonly List<SyntaxToken> tokens;
        private int position;

        public SyntaxParser(string text)
        {
            tokens = new List<SyntaxToken>();
            int correctOffset = 0;

            Lexer lexer = new Lexer(text);
            SyntaxToken syntaxToken;
            SyntaxToken beforeToken = null;
            do
            {
                syntaxToken = lexer.NextToken();
                if (syntaxToken.Kind == SyntaxKind.WhiteSpaceToken ||
                    syntaxToken.Kind == SyntaxKind.BadToken)
                {
                    continue;
                }

                // 省略されている乗算を追加します。
                if (beforeToken != null)
                {
                    if (beforeToken.Kind == SyntaxKind.NumberToken &&
                        syntaxToken.Kind == SyntaxKind.OpenParenthesesToken)
                    {
                        tokens.Add(new SyntaxToken(SyntaxKind.MultiplyToken, syntaxToken.Position + correctOffset, "*", null));
                        correctOffset++;
                    }
                }

                tokens.Add(
                    new SyntaxToken(
                        syntaxToken.Kind,
                        syntaxToken.Position + correctOffset,
                        syntaxToken.Text,
                        syntaxToken.Value));

                beforeToken = syntaxToken;
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

        private SyntaxNode ParseExpression(int parentPrecedence = 0)
        {
            SyntaxNode left = ParsePrimaryExpression();

            while(true)
            {
                int precedence = Current.Kind.GetBinaryOperatorPrecedence();

                if (precedence == 0 || precedence <= parentPrecedence)
                {
                    break;
                }

                var @operator = NextToken();
                var right = ParseExpression(precedence);
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

            return new BadExpressionSyntax(Current);
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
