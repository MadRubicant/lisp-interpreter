using System;
using System.Collections.Generic;
using System.Text;

using LispInterpreter.Parsing.Tokens;

namespace LispInterpreter.Parsing.AST
{
    class FunctionNode : ASTNode {
        private static int Plus(int left, int right) {
            return left + right;
        }

        private static int Minus(int left, int right) {
            return left - right;
        }

        private static int Times(int left, int right) {
            return left * right;
        }

        private static int Divide(int left, int right) {
            return left / right;
        }

        Func<int, int, int> Function;
        ASTNode Left, Right;
        public override object Eval() {
            throw new NotImplementedException();
        }

        public override string Print(int depth) {
            string cur = GetType().ToString();
            cur = cur.PadLeft(cur.Length + depth, '|');
            cur += '\n' + Left.Print(depth + 1) + Right.Print(depth + 1);
            return cur;
        }

        public FunctionNode(OpToken op, ASTNode left, ASTNode right) {
            Left = left;
            Right = right;
            switch (op.Capture) {
                case "+":
                    Function = Plus;
                    break;
                case "-":
                    Function = Minus;
                    break;
                case "*":
                    Function = Times;
                    break;
                case "/":
                    Function = Divide;
                    break;
                default:
                    throw new ParserException();
            }
        }
    }
}
