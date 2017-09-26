using System;
using System.Collections.Generic;
using System.Text;

namespace LispInterpreter.Parser.AST
{
    class IntNode : ASTNode {
        int Value;
        public override object Eval() {
            throw new NotImplementedException();
        }

        public IntNode(int value) {
            Value = value;
        }
    }
}
