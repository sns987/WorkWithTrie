using System;
using System.Collections.Generic;
using BackEndTestApp.Extensions;

namespace BackEndTestApp.Helpers
{
    public static class ReadHelper
    {
        private const int MaxWordsCount = 100000;
        private const int MaxUserWordsCount = 15000;
        private const int MaxWordFrequency = 1000000;
        private const int MaxWordLength = 15;

        public static IEnumerable<KeyValuePair<string, int>> ReadWordsWithFrequency()
        {
            var wordsCount = ReadWordsCount(MaxWordsCount);

            var lineCount = 0;

            var words = new List<KeyValuePair<string, int>>(wordsCount);
            while (lineCount++ < wordsCount)
            {
                var lineArr = (Console.ReadLine() ?? "").Trim().Split(' ');
                if (lineArr.Length != 2)
                    throw new Exception("Incorrect line");

                var world = lineArr[0];
                var frequency = lineArr[1].ParseToInt();

                if (string.IsNullOrEmpty(world) || world.Length > MaxWordLength)
                    throw new Exception("Incorrect word");
                if (frequency < 1 || frequency > MaxWordFrequency)
                    throw new Exception("Incorrect frequency");

                words.Add(new KeyValuePair<string, int>(world, frequency));
            }
            return words;
        }

        public static IEnumerable<string> ReadUserWords()
        {
            var userWordsCount = ReadWordsCount(MaxUserWordsCount);

            var userWords = new List<string>(userWordsCount);
            var lineCount = 0;
            while (lineCount++ < userWordsCount)
            {
                var userWord = (Console.ReadLine() ?? string.Empty).Trim();

                if (string.IsNullOrEmpty(userWord) || userWord.Length > MaxWordLength)
                    throw new Exception("Incorrect word");

                userWords.Add(userWord);
            }
            return userWords;
        }

        private static int ReadWordsCount(int maxValue)
        {
            var wordsCount = (Console.ReadLine()).ParseToInt();
            if (wordsCount < 1 || wordsCount > maxValue)
                throw new Exception("Incorrect count of words");
            return wordsCount;
        }
    }
}