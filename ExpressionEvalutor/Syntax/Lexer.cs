
namespace ExpressionEvalutor.Syntax
{
    /// <summary>
    /// テキストから構文を解析するクラス
    /// </summary>
    public sealed class Lexer
    {
        private readonly string text;
        private int position;

        public Lexer(string text)
        {
            this.text = text;
        }

        private char Current => Peek(0);

        private int Next()
        {
            return position++;
        }

        /// <summary>
        /// 現在の位置からオフセットだけ進んだ位置の文字を取得します。
        /// </summary>
        /// <remarks>
        /// 取得する位置がテキストの範囲を超える場合、null文字を返します。
        /// </remarks>
        /// <param name="offset">オフセット</param>
        /// <returns></returns>
        private char Peek(int offset)
        {
            int index = position + offset;

            if (index >= text.Length)
            {
                return '\0';
            }

            return text[index];
        }

        /// <summary>
        /// 次のトークンを取得し、現在の位置を次のトークンの位置まで移動します。
        /// </summary>
        /// <returns></returns>
        public SyntaxToken NextToken()
        {
            switch (Current)
            {
                case '\0':
                    return new SyntaxToken(SyntaxKind.EndOfFileToken, position++, "\0", null);
                case '+':
                    return new SyntaxToken(SyntaxKind.AdditionToken, position++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.SubtractToken, position++, "-", null);
                case '*':
                    return new SyntaxToken(SyntaxKind.MultiplyToken, position++, "*", null);
                case '/':
                    return new SyntaxToken(SyntaxKind.DivideToken, position++, "/", null);
                case '%':
                    return new SyntaxToken(SyntaxKind.ModuloToken, position++, "%", null);
                case '^':
                    return new SyntaxToken(SyntaxKind.ExponentiationToken, position++, "^", null);
                case '(':
                    return new SyntaxToken(SyntaxKind.OpenParenthesesToken, position++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenthesesToken, position++, ")", null);
                case ' ':
                    return ReadWhiteSpace();

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return ReadNumber();
            }

            return new SyntaxToken(SyntaxKind.BadToken, position++, Peek(-1).ToString(), null);
        }

        private SyntaxToken ReadWhiteSpace()
        {
            int start = position;
            while (char.IsWhiteSpace(Current))
            {
                Next();
            }

            int tokenLength = position - start;
            string tokenText = text.Substring(start, tokenLength);
            return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, tokenText, null);
        }

        private SyntaxToken ReadNumber()
        {
            int start = position;
            while (char.IsDigit(Current) || Current == '.')
            {
                Next();
            }

            int tokenLength = position - start;
            string tokenText = text.Substring(start, tokenLength);
            float.TryParse(tokenText, out float value);

            return new SyntaxToken(SyntaxKind.NumberToken, start, tokenText, value);
        }
    }
}
