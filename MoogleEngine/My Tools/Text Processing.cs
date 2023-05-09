namespace MoogleEngine;

public static class FilesProcessing
{
    static DirectoryInfo TxtDir = new DirectoryInfo(Path.Combine("..","Content"));
    static FileInfo[] TxtList = TxtDir.GetFiles("*.txt*");
    public static TXT[] TXTvector = Methods.TXTListCreator(TxtList);
}

public static class WordsProcessing
{
    static string[] WordsProcess(TXT[] texts)
    {
        string[] words = new string[] { };
        foreach (TXT x in texts)
        {
            string[] format = Methods.StrConverter(x.text);
            foreach (string y in format)
            {
                words = Methods.AddString(words, y);
            }
        }
        return Methods.EliminateRepeatsAndNulls(words);
    }
    public static string[] wordsvector = WordsProcess(FilesProcessing.TXTvector);
}

public static class Dictionary
{
    public static Dictionary<string, int> IntroduceWords(string[] wordsvector)
    {
        Dictionary<string, int> dicWords = new Dictionary<string, int>();
        for (int i = 0; i < wordsvector.Length; i++)
        {
            dicWords.Add(wordsvector[i], i);
        }
        return dicWords;
    }

    public static Dictionary<string, int> IntroduceTXT(TXT[] TXTvector)
    {
        Dictionary<string, int> dicTXT = new Dictionary<string, int>();
        for (int i = 0; i < TXTvector.Length; i++)
        {
            dicTXT.Add(TXTvector[i].title, i);
        }
        return dicTXT;
    }

    public static Dictionary<string, int> dicWords = IntroduceWords(WordsProcessing.wordsvector);
    public static Dictionary<string, int> dicTXT = IntroduceTXT(FilesProcessing.TXTvector);
}

public static class ScoreProcess
{
    static float[,] scorematrix = new float[Dictionary.dicTXT.Count(), Dictionary.dicWords.Count()];
    static float[,] ScoreMatrixMeth(string[] Words, TXT[] Files)
    {
        for(int j=0;j<scorematrix.GetLength(1);j++)
        {
            string actualword = Words[j];
            List<float> Values = Methods.Score(actualword,Files);
            for(int i = 0; i<scorematrix.GetLength(0);i++)
            {
                scorematrix[i,j] = Values[i];
            }
        }
        return scorematrix;
    }
    public static float[,] ScoreMatrix = ScoreMatrixMeth(WordsProcessing.wordsvector, FilesProcessing.TXTvector);
}