using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using LispInterpreter.Parser.Tokens;

namespace LispInterpreter.Parser
{

    class Lexer {
        Dictionary<Regex, Type> Patterns;
        readonly Regex BadToken = new Regex(@"^\S+");
        Char[] Input;
        int Pos;
        int Line;
        public bool TokensRemain { get; private set; }

        public Lexer(string InputString) {
            Input = InputString.ToCharArray();
            Pos = 0;
            Line = 0;
            TokensRemain = true;
            Patterns = new Dictionary<Regex, Type>();
            foreach (var type in Token.GetTokenPatterns())
                RegisterTokenDef(type.Item2, type.Item1);
        }

        public void RegisterTokenDef(string Expression, Type TokenType) {
            Patterns.Add(new Regex(String.Format("^({0})", Expression)), TokenType);
        }

        public Token NextToken() {
            while (Char.IsWhiteSpace(Input[Pos])) {
                Pos++;
                if (Input[Pos] == '\n')
                    Line++;
            }
            string substr = new string(Input, Pos, Input.Length - Pos);
            var toks = Patterns.Select(kv => new Tuple<Regex, Match>(kv.Key, kv.Key.Match(substr))).Where(t => t.Item2.Success);
            if (toks.Count() == 0) {
                throw new LexerException(String.Format("Unexpected token '{1}' at line {0}", Line, BadToken.Match(substr).Value), Line);
            }
            var token = toks.Aggregate((left, right) => left.Item2.Length > right.Item2.Length ? left : right);
            Pos += token.Item2.Length;
            if (Pos == Input.Length)
                TokensRemain = false;
            return Token.CreateToken(Patterns[token.Item1], token.Item2.Value);
        }
    }
}
