using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackEndTestApp.Helpers;
using NUnit.Framework;

namespace Tests.Helpers
{
    [TestFixture]
    public class ReadHelperTests
    {
        private static IEnumerable ReadWordsWithFrequencyTestCases
        {
            get
            {
                yield return
                    new TestCaseData(new object[]
                    {new[] {"5", "kare 10 ", "kanojo 20", "karetachi 1", "korosu 7", "sakura 3"}})
                        .Returns(new[]
                        {
                            new KeyValuePair<string, int>("kare", 10),
                            new KeyValuePair<string, int>("kanojo", 20),
                            new KeyValuePair<string, int>("karetachi", 1),
                            new KeyValuePair<string, int>("korosu", 7),
                            new KeyValuePair<string, int>("sakura", 3)
                        });
                yield return
                    new TestCaseData(new object[]
                    {new[] {"1000000", "kare 10 ", "kanojo 20", "karetachi 1", "korosu 7", "sakura 3"}})
                        .Throws(typeof (Exception))
                        .SetName("Incorrect count of words");
                yield return new TestCaseData(new object[] {new[] {"100000", "kareyyyyutiuyoip 10 "}})
                    .Throws(typeof (Exception))
                    .SetName("Incorrect word");
                yield return new TestCaseData(new object[] {new[] {"1", "kar 1000001"}})
                    .Throws(typeof (Exception))
                    .SetName("Incorrect frequency");
                yield return new TestCaseData(new object[] {new[] {"1", "kar 104 error"}})
                    .Throws(typeof (Exception))
                    .SetName("Incorrect line");
            }
        }

        private static IEnumerable ReadUserWordsTestCases
        {
            get
            {
                yield return
                    new TestCaseData(new object[] {new[] {"3", "k", "ka", "kar"}})
                        .Returns(new[] {"k", "ka", "kar"});
                yield return new TestCaseData(new object[] {new[] {"1000001", "k", "ka", "kar"}})
                    .Throws(typeof (Exception))
                    .SetName("Incorrect count of words");
                yield return new TestCaseData(new object[] {new[] {"1", "kareyyyyutiuyoip"}})
                    .Throws(typeof (Exception))
                    .SetName("Incorrect word");
            }
        }

        [Test, TestCaseSource("ReadWordsWithFrequencyTestCases")]
        public KeyValuePair<string, int>[] ReadWordsWithFrequencyTest(string[] lines)
        {
            var input = new StringReader(String.Join(Environment.NewLine, lines));
            Console.SetIn(input);
            return ReadHelper.ReadWordsWithFrequency().ToArray();
        }

        [Test, TestCaseSource("ReadUserWordsTestCases")]
        public string[] ReadUserWordsTest(string[] lines)
        {
            var input = new StringReader(String.Join(Environment.NewLine, lines));
            Console.SetIn(input);
            return ReadHelper.ReadUserWords().ToArray();
        }
    }
}