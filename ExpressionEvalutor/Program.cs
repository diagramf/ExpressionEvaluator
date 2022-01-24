using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvalutor
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> ");
                string read = Console.ReadLine();
                SyntaxTree syntaxTree = SyntaxTree.Parse(read);

                ExpressionEvalutor evalutor = new ExpressionEvalutor(syntaxTree.Root);
                PrettyPrint(syntaxTree.Root);
                Console.WriteLine($"{evalutor.Evalute()}");
            }

        }

        static void PrettyPrint(SyntaxNode node, int depth=0)
        {
            string space = "";
            for (int i = 0; i < depth; ++i)
            {
                space += "  ";
            }
            if (node.Kind == SyntaxKind.NumberToken)
            {
                Console.WriteLine($"{space} {node.Kind} {(node as SyntaxToken).Value}");
            }
            else
            {
                Console.WriteLine($"{space} {node.Kind}");
            }

            depth += 1;
            foreach (var children in node.GetChildren())
            {
                PrettyPrint(children, depth);
            }
        }
    }
}
