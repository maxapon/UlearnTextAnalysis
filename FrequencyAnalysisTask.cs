using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var allNGrams = GetFrequentsyDict(text);
            var result = GetTheMostFrequent(allNGrams);
            return result;
        }

        private static Dictionary<(string, string), int> GetFrequentsyDict(List<List<string>> text)
        { 
            Dictionary<(string, string), int> result = new Dictionary<(string, string), int>();
            foreach (var sentence in text)
            {
                for (int i = 0; i < sentence.Count - 1; i++)
                { 
                    var biGramKey = (sentence[i], sentence[i+1]);
                    AddNGramIfNeed(biGramKey, ref result);
                    if (i < sentence.Count - 2)
                    {
                        var triGramKey = ($"{sentence[i]} {sentence[i+1]}", sentence[i+2]);
                        AddNGramIfNeed(triGramKey, ref result);
                    }
                }
            }
            return result;
        }

        //На странице с заданием https://ulearn.me/course/basicprogramming/Praktika_Chastotnost_N_gramm__eb894d4d-5854-4684-898b-5480895685e5
        //Я оставил коментарий по поводу замечания "Разрешено передавать через ref только примитивные типы."
        //посмотри позже, может ответил кто-нибудь
        private static void AddNGramIfNeed((string, string) nGramKey, ref Dictionary<(string, string), int> result)
        {
            if (result.ContainsKey(nGramKey))
                result[nGramKey]++;
            else
                result.Add(nGramKey, 1);
        }

        private static Dictionary<string, string> GetTheMostFrequent(Dictionary<(string, string), int> allNGram)
        { 
            Dictionary<string, string> result = new Dictionary<string, string>();
            var groups = allNGram.GroupBy(x => x.Key.Item1).ToArray();
            foreach (var group in groups)
            {
                int maxValue = group.Max( m => m.Value );
                var mostFrequentNGrams = group.Where( w => w.Value == maxValue ).ToArray();
                int resIndex = 0;
                if (mostFrequentNGrams.Length > 1)
                {
                    for (int i = 0; i < mostFrequentNGrams.Length; i++)
                    {
                        string currNGramValue = mostFrequentNGrams[i].Key.Item2;
                        if (string.CompareOrdinal(currNGramValue, mostFrequentNGrams[resIndex].Key.Item2) < 0)
                            resIndex = i;
                    }
                }

                result.Add(mostFrequentNGrams[resIndex].Key.Item1, mostFrequentNGrams[resIndex].Key.Item2);
            }

            return result;
        }
    }
}