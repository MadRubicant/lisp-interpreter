using System;
using System.Collections.Generic;
using System.Text;

namespace LispInterpreter.Parsing.AST
{
    abstract class ASTNode
    {
        public abstract object Eval();

        public abstract string Print(int depth);

        public override string ToString() {
            return Print(0);
        }
    }
}
