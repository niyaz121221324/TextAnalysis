namespace TextAnalysis;

static class SentencesParserTask
{
    private readonly static char[] sentenceDelimiters = new char[] { '.', '!', '?', ';', ':', '(', ')' };

    private readonly static char[] wordDelimiters = new char[]
    {
        ' ', ',', '/', '—', '“', '”',
        '‘', '…', '^', '#', '$', '-', '+',
        '=' , '\n', '\r', '\"',
    };

    public static List<List<string>> ParseSentences(string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException("text");
        }

        return text.Split(sentenceDelimiters, StringSplitOptions.RemoveEmptyEntries)
            .Select(sentence => SplitSentenceToWords(sentence))
            .Where(words => words.Count > 0)
            .ToList();
    }

    private static string PreprocessSentence(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
        {
            throw new ArgumentNullException("sentence");
        }

        return new string(sentence
            .Select(symbol => char.IsLetter(symbol) || symbol == '\'' ? char.ToLower(symbol) : ' ')
            .ToArray());
    }

    private static List<string> SplitSentenceToWords(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
        {
            throw new ArgumentNullException("sentence");
        }

        return PreprocessSentence(sentence).Split(wordDelimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}