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

        private SyntaxToken Peek(int offset)
        {
            int index = position + offset;

            if (index >= tokens.Count)
            {
                return tokens.LastOrDefault();
            }

            return tokens[index];
        }

        private SyntaxToken Current => Peek(0);
        private SyntaxToken Lookahead => Peek(1);

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
            }

            return null;
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
