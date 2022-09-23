using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var sentences = text.Split(new char[] { '.', '!', '?', ';', ':', '(', ')' });
            foreach (var sentence in sentences)
            {
                var words = GetWordsFromSentence(sentence.Trim(new char[] { ' ', '\0' }));
                if (words.Count > 0)
                    sentencesList.Add(words);
            }
            return sentencesList;
        }

        private static List<string> GetWordsFromSentence(string sentence)
        {
            List<string> result = new List<string>();
            if (sentence.Length > 0)
            {
                List<char> wordsEndChar = new List<char>();
                foreach (var currChar in sentence)
                {
                    if (IsCharacterEndOfWord(currChar))
                        wordsEndChar.Add(currChar);
                }
                string[] words = sentence.Split(wordsEndChar.ToArray());
                result.AddRange(words.Where(x => x.Length > 0).Select(x => x.ToLower()));
            }
            return result;
        }

        private static bool IsCharacterEndOfWord(char c)
        {
            return !(char.IsLetter(c) || c.Equals('\'')) || c == null;
        }
    }
}
    
