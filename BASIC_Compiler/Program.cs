using BASIC_Compiler.AnaliseLexica;
using System;

namespace BASIC_Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            AnalisadorLexico analisadorLexico = new AnalisadorLexico(true);

            analisadorLexico.ExtrairTokensLexicos("P1", "C://Files/a.txt", null);

            Console.ReadKey();
        }
    }
}
