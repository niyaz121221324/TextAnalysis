namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var result = new Dictionary<string, string>();
        var nGramDic = GenerateNGram(text);

        foreach (var gram in  nGramDic)
        {
            result.Add(gram.Key, GetMostFrequentAddition(gram.Value));
        }

        return result;
    }

    private static Dictionary<string, Dictionary<string, int>> GenerateNGram(List<List<string>> text)
    {
        var nGramDic = new Dictionary<string, Dictionary<string, int>>();

        foreach (var sentence in text)
        {
            if (sentence.Count() == 0)
            {
                continue;
            }

            for (int i = 0; i < sentence.Count; i++)
            {
                //Обрабатываем как биграммы, так и триграммы для каждого предложения.
                AddOrUpdateNGram(nGramDic, sentence, i, 1);
                AddOrUpdateNGram(nGramDic, sentence, i, 2);
            }
        }

        return nGramDic;
    }

    private static string GetMostFrequentAddition(Dictionary<string, int> gram)
    {
        int maxValue = 0;
        string mostFrequentAddition = string.Empty;

        foreach (var word in gram.Keys)
        {
            if (gram[word] > maxValue)
            {
                mostFrequentAddition = word;
                maxValue = gram[word];
            }

            if (gram[word] == maxValue)
            {
                if (string.CompareOrdinal(word, mostFrequentAddition) < 0)
                {
                    mostFrequentAddition = word;
                }
            }
        }

        return mostFrequentAddition; 
    }

    private static void AddOrUpdateNGram(
        Dictionary<string, Dictionary<string, int>> nGramDic,
        List<string> sentence,
        int startIndex,
        int nGramSize)
    {
        if (startIndex + nGramSize >= sentence.Count)
        {
            return;
        }

        var nGramKey = GetNGramKey(sentence, startIndex, nGramSize);
        var nGramValue = GetNGramValue(sentence, startIndex, nGramSize);

        if (nGramDic.ContainsKey(nGramKey))
        {
            if (nGramDic[nGramKey].ContainsKey(nGramValue))
            {
                nGramDic[nGramKey][nGramValue]++;
                return;
            }

            nGramDic[nGramKey].Add(nGramValue, 1);
            return;
        }

        nGramDic.Add(nGramKey, new Dictionary<string, int> { { nGramValue, 1 } });
    }

    private static string GetNGramKey(List<string> sentence, int startIndex, int nGramSize)
    {
        return string.Join(" ", sentence.Skip(startIndex).Take(nGramSize));
    }

    private static string GetNGramValue(List<string> sentence, int startIndex, int nGramSize)
    {
        return sentence[startIndex + nGramSize];
    }
}