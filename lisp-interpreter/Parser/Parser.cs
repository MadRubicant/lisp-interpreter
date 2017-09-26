using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using LispInterpreter.Parser.Tokens;
using LispInterpreter.Parser.AST;

namespace LispInterpreter.Parser
{
    class Parser {
        IList<Token> Tokens;
        public Parser(IList<Token> Tokens) {
            this.Tokens = Tokens;
        }


    }
}
