using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace LispInterpreter.Parsing.Tokens
{
    public abstract class Token : IEquatable<Token>
    {
        public string Capture;
        private static Dictionary<Type, ConstructorInfo> Constructors;

        public static Token CreateToken(Type TokenType, string CapturedString) {
            if (!TokenType.GetTypeInfo().IsSubclassOf(typeof(Token)))
                throw new ArgumentException("Attempted to create a token with a type not derived from Token", "TokenType");

            var Tok = (Token)Constructors[TokenType].Invoke(new object[0]);
            Tok.Capture = CapturedString;
            return Tok;
        }

        public static IEnumerable<Tuple<Type, string>> GetTokenPatterns() {
            var subclasses = typeof(Token).GetTypeInfo().Assembly.GetTypes().Where(t => t.GetTypeInfo().IsSubclassOf(typeof(Token)));
            return subclasses.Select(t => new Tuple<Type, string>(t, (string)t.GetTypeInfo().GetField("Pattern").GetValue(null)));
        }
        static Token() {
            var subclasses = typeof(Token).GetTypeInfo().Assembly.GetTypes().Where(t => t.GetTypeInfo().IsSubclassOf(typeof(Token)));
            Constructors = subclasses.ToDictionary(t => t, t => t.GetConstructor(new Type[0]));
        }

        public override string ToString() {
            return String.Format("{1}: '{0}'", Capture, GetType().Name);
        }

        public override bool Equals(object obj) {
            return obj is Token ? Equals((Token)obj) : false;
        }

        public bool Equals(Token other) {
            return GetType() == other.GetType() && Capture == other.Capture;
        }

        public override int GetHashCode() {
            return Capture.GetHashCode();
        }
    }

    public class LeftParenToken : Token {
        public static string Pattern = @"\(";
    }

    public class RightParenToken : Token {
        public static string Pattern = @"\)";
    }

    public class IntToken : Token {
        public static string Pattern = @"-?[0-9]+";
        public int Data { get {
                return int.Parse(Capture);
            } }
    }

    public class OpToken : Token {
        public static string Pattern = @"\+|-|\*|\/";
    }
}
