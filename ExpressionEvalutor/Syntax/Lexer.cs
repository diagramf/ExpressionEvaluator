
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
        private void Next()
        {
            position++;
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
            SyntaxToken syntaxToken = null;


            switch (Current)
            {
                case '\0':
                    syntaxToken = new SyntaxToken(SyntaxKind.EndOfFileToken, position++, "\0", null);
                    break;
                case '+':
                    syntaxToken = new SyntaxToken(SyntaxKind.AdditionToken, position++, "+", null);
                    break;
                case '-':
                    syntaxToken = new SyntaxToken(SyntaxKind.SubtractToken, position++, "-", null);
                    break;
                case '*':
                    syntaxToken = new SyntaxToken(SyntaxKind.MultiplyToken, position++, "*", null);
                    break;
                case '/':
                    syntaxToken = new SyntaxToken(SyntaxKind.DivideToken, position++, "/", null);
                    break;
                case '%':
                    syntaxToken = new SyntaxToken(SyntaxKind.ModuloToken, position++, "%", null);
                    break;
                case '(':
                    syntaxToken = new SyntaxToken(SyntaxKind.OpenParenthesesToken, position++, "(", null);
                    break;
                case ')':
                    syntaxToken = new SyntaxToken(SyntaxKind.CloseParenthesesToken, position++, ")", null);
                    break;
                case ' ':
                    syntaxToken = ReadWhiteSpace();
                    break;

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
                    syntaxToken = ReadNumber();
                    break;
            }

            if (syntaxToken == null)
            {
                return new SyntaxToken(SyntaxKind.BadToken, position++, text.Substring(position - 1, 1), null);
            }

            return syntaxToken;
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
