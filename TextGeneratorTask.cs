using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            StringBuilder sb = new StringBuilder(phraseBeginning);
            var (lastWords, lastWordsCount) = GetLastWords(phraseBeginning);
            for (int i = 0; i < wordsCount; i++)
            {
                string key = String.Empty;
                if (lastWordsCount == 2 && nextWords.ContainsKey(lastWords))
                    key = lastWords;
                else
                {
                    string oneLastWord = lastWordsCount == 1 ? lastWords : lastWords.Split()[1];
                    if (nextWords.ContainsKey(oneLastWord))
                        key = oneLastWord;
                    else
                        break;
                }
                sb.Append(" ");
                sb.Append(nextWords[key]);
                (lastWords, lastWordsCount) = GetLastWords(sb.ToString());
            }
            return sb.ToString();
        }

        private static (string, int) GetLastWords(string phrase, char separator =  ' ')
        {
            var allWords = phrase.Split(separator);
            return allWords.Length >= 2 ? 
                    ($"{allWords[allWords.Length - 2]} {allWords[allWords.Length - 1]}", 2) :
                    (allWords[0], 1);
        }
    }
}