using System.Collections;
using System.Collections.Generic;
using BackEndTestApp.Helpers;
using NUnit.Framework;

namespace Tests.Helpers
{
    [TestFixture]
    public class BinarySearchTests
    {
        private static IEnumerable BinarySearchTestCases
        {
            get
            {
                yield return new TestCaseData(new object[]
                {
                    new[]
                    {
                        new KeyValuePair<string, int>("kare", 20),
                        new KeyValuePair<string, int>("kanojo", 20),
                        new KeyValuePair<string, int>("karetachi", 10),
                        new KeyValuePair<string, int>("korosu", 10),
                        new KeyValuePair<string, int>("korosuas", 10),
                        new KeyValuePair<string, int>("korosuasa", 10),
                        new KeyValuePair<string, int>("korosuasc", 10),
                        new KeyValuePair<string, int>("korosuasf", 10)
                    },
                    new KeyValuePair<string, int>("korosuasb", 10)
                })
                    .Returns(6)
                    .SetName("Check sorting when need MoveRightWhileEqualFrequency");

                yield return new TestCaseData(new object[]
                {
                    new[]
                    {
                        new KeyValuePair<string, int>("karet", 7),
                        new KeyValuePair<string, int>("kareta", 7),
                        new KeyValuePair<string, int>("karetac", 7),
                        new KeyValuePair<string, int>("karetach", 7),
                        new KeyValuePair<string, int>("karetachi", 7),
                        new KeyValuePair<string, int>("karetb", 7),
                        new KeyValuePair<string, int>("korosu", 7),
                        new KeyValuePair<string, int>("korosuba", 7),
                        new KeyValuePair<string, int>("korosubaa", 7),
                        new KeyValuePair<string, int>("korosubab", 7)
                    },
                    new KeyValuePair<string, int>("korosubaba", 7)
                })
                    .Returns(10)
                    .SetName("Check sorting when need MoveRightWhileEqualFrequency and will be max posistion");


                yield return new TestCaseData(new object[]
                {
                    new[]
                    {
                        new KeyValuePair<string, int>("karet", 7),
                        new KeyValuePair<string, int>("kareta", 7),
                        new KeyValuePair<string, int>("karetac", 7),
                        new KeyValuePair<string, int>("karetach", 7),
                        new KeyValuePair<string, int>("karetachi", 7),
                        new KeyValuePair<string, int>("karetb", 7),
                        new KeyValuePair<string, int>("korosu", 7),
                        new KeyValuePair<string, int>("korosuba", 7),
                        new KeyValuePair<string, int>("korosubaa", 7),
                        new KeyValuePair<string, int>("korosubab", 7)
                    },
                    new KeyValuePair<string, int>("karetaa", 7)
                })
                    .Returns(2)
                    .SetName("Check sorting when need MoveLeftUntilMeetBigger");
            }
        }

        [Test, TestCaseSource("BinarySearchTestCases")]
        public int BinarySearchTest(IReadOnlyList<KeyValuePair<string, int>> wordsWithFrequency,
            KeyValuePair<string, int> item)
        {
            return BinarySearch.BinarySearchForStringWithFrequency(wordsWithFrequency, item);
        }
    }
}