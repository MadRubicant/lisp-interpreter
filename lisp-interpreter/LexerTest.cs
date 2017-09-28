using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using System.Reflection;

using LispInterpreter.Parsing;
using LispInterpreter.Parsing.Tokens;

namespace LispInterpreter
{
    [TestFixture]
    class LexerTest
    {
        [Test]
        public void LeftParen() {
            Lexer lexer = new Lexer("(");
            Assert.AreEqual(new LeftParenToken() { Capture = "(" }, lexer.NextToken());
        }

        [Test]
        public void TokenPatternsExist() {
            var TokSubclasses = typeof(Token).GetTypeInfo().Assembly.DefinedTypes.Where(t => t.IsSubclassOf(typeof(Token)));

        }
    }
}
