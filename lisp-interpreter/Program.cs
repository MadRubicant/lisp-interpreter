using System;
using LispInterpreter.Parsing;
using LispInterpreter.Parsing.Tokens;

using System.Text.RegularExpressions;

namespace LispInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            REPL MainLoop = new REPL(Console.In);
            MainLoop.Run();
        }
    }
}