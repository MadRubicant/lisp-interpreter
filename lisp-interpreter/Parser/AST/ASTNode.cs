using System;
using System.Collections.Generic;
using System.Text;

namespace LispInterpreter.Parser.AST
{
    abstract class ASTNode
    {
        public abstract object Eval();
    }
}
