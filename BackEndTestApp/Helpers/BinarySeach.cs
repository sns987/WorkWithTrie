using System.Collections.Generic;

namespace BackEndTestApp.Helpers
{
    public static class BinarySearch
    {
        public static int BinarySearchForStringWithFrequency(
            IReadOnlyList<KeyValuePair<string, int>> wordsWithFrequency,
            KeyValuePair<string, int> item)
        {
            var count = wordsWithFrequency.Count;
            if (count == 0 || wordsWithFrequency[count - 1].Value > item.Value)
            {
                return count;
            }

            var i = 0;
            var j = count;
            while (i + 1 < j)
            {
                var middle = i + (j - i)/2;
                if (wordsWithFrequency[middle].Value < item.Value)
                {
                    j = middle;
                }
                else if (wordsWithFrequency[middle].Value == item.Value)
                {
                    middle = string.CompareOrdinal(item.Key, wordsWithFrequency[middle].Key) < 0
                        ? MoveLeftUntilMeetBigger(wordsWithFrequency, item, middle, 0)
                        : MoveRightWhileEqualFrequency(wordsWithFrequency, item, middle, j);

                    i = middle;
                    j = middle + 1;
                    break;
                }
                else
                {
                    i = middle;
                }
            }

            return GetPosition(wordsWithFrequency, item, i, j);
        }

        private static int GetPosition(IReadOnlyList<KeyValuePair<string, int>> wordsWithFrequency,
            KeyValuePair<string, int> item, int i, int j)
        {
            if (wordsWithFrequency[i].Value == item.Value)
                return string.CompareOrdinal(item.Key, wordsWithFrequency[i].Key) < 0 ? i : j;

            return wordsWithFrequency[i].Value < item.Value ? i : j;
        }

        private static int MoveLeftUntilMeetBigger(IReadOnlyList<KeyValuePair<string, int>> wordsWithFrequency,
            KeyValuePair<string, int> item, int middle, int minIndexValue)
        {
            while (true)
            {
                if (wordsWithFrequency[middle].Value > item.Value
                    || string.CompareOrdinal(item.Key, wordsWithFrequency[middle].Key) >= 0)
                {
                    middle++;
                    break;
                }

                if (middle == minIndexValue)
                    break;
                middle--;
            }
            return middle;
        }

        private static int MoveRightWhileEqualFrequency(IReadOnlyList<KeyValuePair<string, int>> wordsWithFrequency,
            KeyValuePair<string, int> item, int middle, int maxIndexValue)
        {
            while (true)
            {
                if (wordsWithFrequency[middle].Value == item.Value
                    && string.CompareOrdinal(item.Key, wordsWithFrequency[middle].Key) >= 0)
                {
                    if (middle + 1 == maxIndexValue)
                        break;

                    middle++;
                }
                else
                {
                    break;
                }
            }
            return middle;
        }
    }
}