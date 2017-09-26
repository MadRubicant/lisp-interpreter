using System;
using System.Collections.Generic;
using System.Text;

namespace LispInterpreter.Parser
{

    public class LexerException : Exception {
        int Line;
        public LexerException() { }
        public LexerException(string message) : base(message) { }
        public LexerException(string message, Exception inner) : base(message, inner) { }
        public LexerException(string message, int Line) : base(message) {
            this.Line = Line;
        }
    }
}
