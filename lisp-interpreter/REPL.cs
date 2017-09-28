using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using LispInterpreter.Parsing;
using LispInterpreter.Parsing.Tokens;
using LispInterpreter.Parsing.AST;

namespace LispInterpreter
{
    class REPL
    {
        TextReader Input;
        bool ShouldRun = true;
        Lexer Lex;
        Parser Parse;
        List<Token> Toks;
        ASTNode LastRoot;
        public REPL(TextReader Input) {
            this.Input = Input;
            Lex = new Lexer();
            Parse = new Parser();
            Toks = new List<Token>();
        }

        public void Read() {
            int parenCount = 0;
            while (true) {
                string Line = Input.ReadLine();
                if (Line.Length == 0)
                    return;
                Lex.AddText(Line);
                while (Lex.TokensRemain == true) {
                    Token t = Lex.NextToken();
                    switch (t) {
                        case LeftParenToken l:
                            parenCount++;
                            goto default;
                        case RightParenToken r:
                            parenCount--;
                            goto default;
                        default:
                            Toks.Add(t);
                            break;
                    }
                    if (parenCount == 0)
                        return;
                }
            }
        }

        public void Eval() {
            Parse.AddTokens(Toks);
            LastRoot = Parse.Parse();
        }

        public void Print() {
            foreach (Token t in Toks) {
                //Console.WriteLine(t.ToString());
            }
            if (LastRoot != null)
                Console.WriteLine(LastRoot.Print(0));
        }

        private void Clear() {
            Toks.Clear();
            LastRoot = null;
        }

        public void Run() {
            while (ShouldRun == true)
            {
                Console.Write("==>");
                Read();
                Eval();
                Print();
                Clear();
            }
        }
    }
}
