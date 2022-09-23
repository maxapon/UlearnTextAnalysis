using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Security.Authentication.ExtendedProtection.Configuration;

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
                for (int i = 0; i < sentence.Count - 1; i++)
                {
                    string biGramKey = sentence[i];
                    string value = sentence[i + 1];
                    
                    //Кортеж является ссылочным типом, поэтому Find вернет ссылку на кортеж и мы сможем поменять его значение
                    var biGram = result.Find(x => x.Item1 == biGramKey && x.Item2 == value);
                    //BiGram
                    if (biGram != default((string, string, int)))
                    {
                        biGram.Item3++;
                    }
                    else
                    {
                        result.Add((biGramKey, value, 1));
                    }
                    //TriGram
                    if (i < sentence.Count - 2)
                    {
                        string triGramKey = sentence[i] + " " + sentence[i + 1];
                        value = sentence[i + 2];
                        var triGram = result.Find(x => x.Item1 == triGramKey && x.Item2 == value);
                        if (triGram != default((string, string, int)))
                        {
                            triGram.Item3++;
                        }
                        else
                        {
                            result.Add((triGramKey, value, 1));
                        }
                    }
                }
            }
            sw.Stop();
            string r = sw.Elapsed.ToString();
            return result;
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