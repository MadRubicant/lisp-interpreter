using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

using LispInterpreter.Parsing.Tokens;
using LispInterpreter.Parsing.AST;

namespace LispInterpreter.Parsing {
    class Parser {
        IList<Token> Tokens;
        int StartPos;
        public Parser() {
            this.Tokens = new List<Token>();
        }
        public Parser(IList<Token> Tokens) {
            this.Tokens = Tokens;
        }

        public void AddTokens(IList<Token> Tokens) {
            this.Tokens = this.Tokens.Concat(Tokens).ToList();
        }

        public ASTNode Parse() {
            ASTNode ret;
            int Pos = 0;
            if (Tokens.Count == 0)
                return null;
            (ret, Pos) = Parse(Pos);
            //Console.WriteLine("Ate {0} tokens", Pos + 1);
            Tokens = Tokens.Skip(Pos + 1).ToList();
            return ret;
        }

        private (ASTNode, int) Parse(int Pos) {
            ASTNode ret = null;
            int ParenCount = 0;
            //Console.WriteLine("At token {0}", Pos);
            while (Pos < Tokens.Count) {
                switch (Tokens[Pos]) {
                    case LeftParenToken L:
                        ParenCount++;
                        (ret, Pos) = ParseExpression(Pos + 1);
                        Pos++;
                        continue;
                    case RightParenToken R:
                        ParenCount--;
                        Pos++;
                        if (ParenCount == 0)
                            return (ret, Pos);
                        else if (ParenCount < 0)
                            throw new ParserException("Too many closeparens");
                        else continue;
                    case IntToken I:
                        return (new IntNode(I.Data), Pos);
                }
                
            }
            if (ret != null)
                return (ret, Pos);
            throw new ParserException("Reached end of token stream before finished parsing");
        }

        private (ASTNode, int) ParseExpression(int Pos) {
            //Console.WriteLine("At token {0}", Pos);
            switch (Tokens[Pos]) {
                case OpToken Op:
                    ASTNode left, right;
                    (left, Pos) = Parse(Pos + 1);
                    (right, Pos) = Parse(Pos + 1);
                    return (new FunctionNode(Op, left, right), Pos);
            }
            throw new ParserException("Attempted to parse non-expression as an expression");
        }
    }
}