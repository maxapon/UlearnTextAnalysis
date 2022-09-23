using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var allNGramms = GetFrequentsyDict(text);
            var result = GetTheMostFrequent(ref allNGramms);
            return result;
        }

        private static List<(string, string, int)> GetFrequentsyDict(List<List<string>> text)
        {
            var result = new List<(string, string, int)>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (var sentence in text)
            {
                GetNGramm(2, sentence, ref result);
                GetNGramm(3, sentence, ref result);
            }
            sw.Stop();
            string r = sw.Elapsed.ToString();
            return result;
        }

        private static void GetNGramm(int N, List<string> sentence, ref List<(string, string, int)> freqList) 
        {
            for (int i = 0; i < sentence.Count - N + 1; i++)
            {
                var slice = sentence.GetRange(i, N);
                var key = string.Join(" ", slice.ToArray(), 0, N - 1);
                string val = slice[slice.Count - 1];
                var item = freqList.Where(x => x.Item1 == key && x.Item2 == val).ToArray();
                if (item.Length > 0)
                {
                    item[0].Item3++;
                }
                else
                {
                    freqList.Add((key, val, 1));
                }
            }
        }

        private static Dictionary<string, string> GetTheMostFrequent(ref List<(string, string, int)> allNGramm)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var nGramm in allNGramm)
            {
                if (!result.ContainsKey(nGramm.Item1))
                {
                    var items = allNGramm.Where(x => x.Item1 == nGramm.Item1).ToArray();
                    int maxFreq = items.Max(x => x.Item3);
                    List<(string, string, int)> itemsWithOneFreq = items.Where(x => x.Item3 == maxFreq).ToList();

                    var maxLex = itemsWithOneFreq[0];
                    if (itemsWithOneFreq.Count > 1)
                    {
                        foreach (var item in itemsWithOneFreq)
                        {
                            if (string.CompareOrdinal(item.Item2, maxLex.Item2) < 0)
                                maxLex = item;
                        }
                    }
                    result.Add(maxLex.Item1, maxLex.Item2);
                }
            }
            return result;
        }
   }
}