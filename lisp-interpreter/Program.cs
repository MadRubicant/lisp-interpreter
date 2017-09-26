using System;
using LispInterpreter.Parser;
using LispInterpreter.Parser.Tokens;

using System.Text.RegularExpressions;

namespace LispInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Lexer L = new Lexer(args[0]);
            while (L.TokensRemain)
                Console.WriteLine(L.NextToken());
            Console.Read();
        }
    }
}