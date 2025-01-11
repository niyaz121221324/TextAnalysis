using System.Text;

namespace TextAnalysis;

static class TextGeneratorTask
{
    public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        if (wordsCount <= 0 || string.IsNullOrEmpty(phraseBeginning))
        {
            return phraseBeginning;
        }

        StringBuilder phrase = new StringBuilder(phraseBeginning);

        for (int i = 0; i < wordsCount; i++)
        {
            var nextWord = GetNextWord(nextWords, phrase.ToString());

            if (string.IsNullOrEmpty(nextWord))
            {
                break;
            }

            phrase.Append($" {nextWord}");
        }

        return phrase.ToString();
    }

    private static string GetNextWord(
        Dictionary<string, string> nextWords,
        string phrase)
    {
        if (string.IsNullOrEmpty(phrase))
        {
            return string.Empty;
        }

        string[] words = phrase.Split(' ');

        string currentWord = words[words.Length - 1];
        string previousWord = words.Length > 1 ? words[(words.Length - 1) - 1] : string.Empty;
        string combinedPhrase = $"{previousWord} {currentWord}";

        if (nextWords.TryGetValue(combinedPhrase, out var nextWord))
        {
            return nextWord;
        }
        else if (nextWords.TryGetValue(currentWord, out nextWord))
        {
            return nextWord;
        }

        return string.Empty;
    }
}