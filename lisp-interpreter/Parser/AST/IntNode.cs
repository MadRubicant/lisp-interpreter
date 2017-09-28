using System;
using System.Collections.Generic;
using System.Text;

namespace LispInterpreter.Parsing.AST
{
    class IntNode : ASTNode {
        int Value;
        public override object Eval() {
            throw new NotImplementedException();
        }

        public override string Print(int depth) {
            string val = Value.ToString() + '\n';
            val = val.PadLeft(val.Length + depth, '|');
            return val;
        }

        public IntNode(int value) {
            Value = value;
        }
    }
}
