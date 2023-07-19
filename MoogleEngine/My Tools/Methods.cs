namespace MoogleEngine;
public class Methods
{
    public static int CLOSE(string a, string[] p) //metodo para hallar distancia entre palabras de titulo y texto
    {
        string[] t = Methods.StrConverter(a);
        int[][] pos = new int[p.Length][];
        pos = Methods.PosMaker(p, t, pos);
        int distance = Distance(pos, new int[pos.Length], int.MaxValue, 0);
        return distance;
    }
    public static int Distance(int[][] pos, int[] numbers, int distance, int count) //metodo para hallar la distancia2
    {
        foreach (int x in pos[count])
        {
            numbers[count] = x;
            if (count == pos.Length - 1)
            {
                int max = numbers.Max();
                int min = numbers.Min();
                if (distance > max - min)
                {
                    distance = max - min;
                }
            }
            else
            {
                distance = Distance(pos, numbers, distance, count + 1);
            }
        }
        return distance;
    }
    public static int[][] PosMaker(string[] p, string[] t, int[][] pos) //metodo que crea mi familia de posiciones de palabras
    {
        for (int i = 0; i < p.Length; i++)
        {
            pos[i] = CarTell(p[i], t);
        }
        return pos;
    }
    public static int[] CarTell(string a, string[] b)//metodo quedevuelve lasposisciones de las palabras en el texto
    {
        int[] car = new int[] { };
        for (int i = 0; i < b.Length; i++)
        {
            if (b[i] == a)
            {
                car=AddInt(i,car);
            }
        }
        return car;
    }
    static int[] AddInt(int s, int[] t)
    {
        int[] R = new int[t.Length + 1];
        int i = 0;
        foreach (int x in t)
        {
            R[i] = t[i];
            i++;
        }
        R[R.Length - 1] = s;
        return R;
    }
    public static bool ContainsText((TXT, float) a, string[] b) //metodo de pertenencia usado en e operador de cercania
    {
        int count = 0;
        foreach (string x in b)
        {
            if (a.Item1.text.Contains(x))
            {
                count++;
            }
        }
        if (count == b.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool ContainsTitle((TXT, float) a, string[] b) //metodo de pertenencia usado en e operador de cercania
    {
        int count = 0;
        foreach (string x in b)
        {
            if (a.Item1.title.ToLower().Contains(x))
            {
                count++;
            }
        }
        if (count == b.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static string NameFile(string name)
    {
        string[] Split = name.Split(".");
        string RealName = Split[0];
        return RealName;
    }
    public static TXT[] TXTListCreator(FileInfo[] A)
    {
        TXT[] TXTList = new TXT[A.Length];
        int i = 0;
        foreach (FileInfo x in A)
        {
            TXTList[i] = new TXT(x);
            i++;
        }
        return TXTList;
    }
    public static string[] StrConverter(string txt) //metodo para guardar array de palabras    (usado en Moogle / Query)
    {
        string[] SignBox = new string[] { "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "+", "-", "=", "`", "[", "]", "{", "}", ";", "|", ":", "'", "<", ">", ",", ".", "?", "/" };
        foreach (string a in SignBox) //elimino todos los signos
        {
            txt = txt.Replace(a, " ");
        }
        txt = txt.ToLower();//elimino las mayusculas
        string[] Separated = txt.Split(" "); //separo cada array de texto en array de palabras separadas
        Separated = EliminateRepeatsAndNulls(Separated);
        return Separated;
    }
    public static string[] StrConverterOperatorVersion(string txt) //metodo para guardar array de palabras que tienen operadores
    {
        string[] SignBox = new string[] { "@", "#", "$", "%", "&", "(", ")", "_", "+", "-", "=", "`", "[", "]", "{", "}", ";", "|", ":", "'", "<", ">", ",", ".", "?", "/" };
        foreach (string a in SignBox) //elimino todos los signos
        {
            txt = txt.Replace(a, " ");
        }
        txt = txt.ToLower();//elimino las mayusculas
        string[] Separated = txt.Split(" "); //separo cada array de texto en array de palabras separadas
        Separated = EliminateRepeatsAndNulls(Separated);
        return Separated;
    }
    public static string[] EliminateRepeatsAndNulls(string[] a)
    {
        string[] b = new string[] { };
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != "" && !b.Contains(a[i]))
            {
                string[] c = new string[b.Length + 1];
                for (int j = 0; j < b.Length; j++)
                {
                    c[j] = b[j];
                }
                c[c.Length - 1] = a[i];
                b = c;
            }
        }
        return b;
    }
    public static string[] AddString(string[] a, string b)
    {
        string[] newa = new string[a.Length + 1];
        for (int i = 0; i < a.Length; i++)
        {
            newa[i] = a[i];
        }
        newa[newa.Length - 1] = b;
        return newa;
    }
    public static List<float> Score(string s, TXT[] t) //metodo para obtener el score de las palabras  (usado en Data Type / Word)
    {   //Se declaran las variables a utilizar
        float tf;
        List<float> TF = new List<float>();
        float IDF;
        int WordFrecuency;
        int TotalWordArchives;
        //calcular el TF para cada archivo
        foreach (TXT x in t)
        {
            WordFrecuency = 0;
            string[] CompareWords = StrConverter(x.text);
            TotalWordArchives = CompareWords.Length;
            for (int i = 0; i < TotalWordArchives; i++)
            {
                if (s == CompareWords[i])
                {
                    WordFrecuency++;
                }
            }
            tf = ((float)WordFrecuency) / ((float)TotalWordArchives);
            TF.Add(tf);//agrupar todos los TF 
        }
        //calcular el IDF
        int WordFrecFiles = 0;
        foreach (TXT x in t)
        {
            string[] CompareWords = StrConverter(x.text);
            if (CompareWords.Contains(s))
            {
                WordFrecFiles++;
            }
        }
        IDF = (float)Math.Log(((float)t.Length) / ((float)WordFrecFiles));
        List<float> Score = new List<float>();
        foreach (float x in TF)      //calcular TF*IDF
        {
            if (x <= 0)
            {
                float LoneScore = 0;
                Score.Add(LoneScore);
            }
            else
            {
                float LoneScore = x * IDF;
                Score.Add(LoneScore);
            }
        }
        return Score;
    }
    public static List<float> IndividualScore(List<string> s, List<TXT> list)
    {
        List<float> Score = new List<float>();
        foreach (string x in s)
        {
            float tf;
            float IDF;
            int WordFrecuency;
            int TotalWordArchives;
            WordFrecuency = 0;
            string[] CompareWords = StrConverter(list[list.Count - 1].text);
            TotalWordArchives = CompareWords.Length;
            for (int i = 0; i < TotalWordArchives; i++)
            {
                if (x == CompareWords[i])
                {
                    WordFrecuency++;
                }
            }
            tf = ((float)WordFrecuency) / ((float)TotalWordArchives);

            int WordFrecFiles = 0;
            foreach (TXT y in list)
            {
                CompareWords = StrConverter(y.text);
                if (CompareWords.Contains(x))
                {
                    WordFrecFiles++;
                }
            }
            IDF = (float)Math.Log(((float)list.Count) / ((float)WordFrecFiles));
            if (tf <= 0)
            {
                Score.Add(0);
            }
            else
            {
                Score.Add((float)tf * IDF);
            }
        }
        return Score;
    }
    public static List<string> Intersection(string[] a, string[] b) //Metodo que devuelve la interseccion entre dos listas
    {
        List<string> intersection = new List<string>();
        foreach (string x in a)
        {
            if (b.Contains(x))
            {
                intersection.Add(x);
            }
        }
        return intersection;
    }
    public static string[] ClosestAnswer(string[] Q, string[] B)
    {
        List<string> Answer = new List<string>();
        foreach (string x in Q)
        {
            if (B.Contains(x))
            {
                Answer.Add(x);
            }
            else
            {
                int min = int.MaxValue;
                string res = "";     //creo un valor inicial 
                for (int i = 0; i < B.Length; i++)     //itero en la cadena de archivos Word y si encuentro una distancia más pequena con alguna palabra de la cadena, la sustituyo por el valor min
                {
                    if (min > Levenshtein(x, B[i], x.Length, B[i].Length))
                    {
                        min = Levenshtein(x, B[i], x.Length, B[i].Length);
                        res = B[i];
                    }
                }
                if (min > 5)
                {
                    Answer.Add("{" + x + "}");  //devuelvo nulo en caso que la palabra más parecida requiera más de 5 transformaciones
                }
                Answer.Add(res); //devuelvo la palabra con menor diferencia
            }
        }
        string[] Ret = new string[Answer.Count];
        for (int i = 0; i < Ret.Length; i++)
        {
            Ret[i] = Answer[i];
        }
        return Ret;
    }
    static int Levenshtein(string a, string b, int al, int bl) //el metodo para calcular la distancia entre la búsqueda y las palabra del documento
    {
        var aLenght = a.Length;
        var bLenght = b.Length;
        var matrix = new int[aLenght + 1, bLenght + 1];
        if (aLenght == 0)
        {
            return bLenght;
        }
        if (bLenght == 0)
        {
            return aLenght;
        }
        for (var i = 0; i <= aLenght; matrix[i, 0] = i++) { }
        for (var j = 0; j <= bLenght; matrix[0, j] = j++) { }
        for (var i = 1; i <= aLenght; i++)
        {
            for (var j = 1; j <= bLenght; j++)
            {
                var cost = (b[j - 1] == a[i - 1]) ? 0 : 1;
                matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + cost);
            }
        }
        return matrix[aLenght, bLenght];
    }
    public static List<TXT> TxtObtainer(string[] Q, TXT[] TXTvector)
    {
        List<TXT> Ret = new List<TXT>();
        foreach (string x in Q)
        {
            foreach (TXT y in TXTvector)
            {
                if (y.text.ToLower().Contains(x))
                {
                    Ret.Add(y);
                }
            }
        }
        return Ret;
    }
    public static List<(TXT, float)> CosSimilitude(string query, List<TXT> TXTList, float[,] ScoreMatrix, List<string> Intersection, Dictionary<string, int> DWord, Dictionary<string, int> DTxt)
    {
        List<(TXT, float)> Ret = new List<(TXT, float)>();
        List<TXT> AllFiles = new List<TXT>();
        foreach (TXT x in FilesProcessing.TXTvector)
        {
            AllFiles.Add(x);
        }
        AllFiles.Add(new TXT(query));
        List<float> QueryScore = IndividualScore(Intersection, AllFiles);
        foreach (TXT txt in TXTList)
        {
            List<float> DocumentScore = new List<float>();
            foreach (string word in Intersection)
            {
                DocumentScore.Add(ScoreMatrix[DTxt[txt.title], DWord[word]]);
            }
            float numerator = 0;
            double firstdenominator = 0;
            double seconddenominator = 0;
            for (int i = 0; i < DocumentScore.Count; i++)
            {
                numerator = numerator + (DocumentScore[i] * QueryScore[i]);
                firstdenominator = firstdenominator + (DocumentScore[i] * DocumentScore[i]);
                seconddenominator = seconddenominator + (QueryScore[i] * QueryScore[i]);
            }
            double denominator = Math.Sqrt(firstdenominator * seconddenominator);
            float result = (float)numerator / (float)denominator;
            Ret.Add((txt, result));
        }
        return Ret;
    }
    public static SearchItem[] ToSearchItem(List<(TXT, float)> List, float[,] Matrix, List<string> Intersection, Dictionary<string, int> DWord, Dictionary<string, int> DTxt)
    {
        List<SearchItem> Ret = new List<SearchItem>();
        foreach ((TXT, float) x in List)
        {
            string FavWord = "";
            float FavFloat = 0;
            foreach (string word in Intersection)
            {
                if (Matrix[DTxt[x.Item1.title], DWord[word]] >= FavFloat)
                {
                    FavFloat = Matrix[DTxt[x.Item1.title], DWord[word]];
                    FavWord = word;
                }
            }
            string snippet = Snippet(FavWord, x.Item1);
            Ret.Add(new SearchItem(x.Item1.title, snippet, x.Item2));
        }
        SearchItem[] Response = new SearchItem[Ret.Count];
        //Pasando la lista a array
        for (int i = 0; i < Response.Length; i++)
        {
            Response[i] = Ret[i];
        }
        return Response;
    }
    public static string Snippet(string a, TXT b) //metodo para obtener snippet y dar la respuesta (usado en Moogle / Query)
    {
        if (StrConverter(b.text).Contains(a))
        {
            int i = 0;
            foreach (string x in StrConverter(b.text))
            {
                if (x != a)
                {
                    i++;
                }
                else
                {
                    if (b.text.Length > 10)
                    {
                        if (i < 10)
                        {
                            string[] re = b.text.Split(" ", 11);
                            if(re.Length<10)
                            {
                                return String.Join(' ', re);
                            }
                            string[] re2 = new string[re.Length - 1];
                            for (int j = 0; j < re2.Length; j++)
                            {
                                re2[j] = re[j];
                            }
                            return String.Join(' ', re2);
                        }
                        if (i > b.text.Split(" ").Length - 10)
                        {
                            string[] re =
                            {
                               b.text.Split(" ")[b.text.Split(" ").Length-10],
                               b.text.Split(" ")[b.text.Split(" ").Length-9],
                               b.text.Split(" ")[b.text.Split(" ").Length-8],
                               b.text.Split(" ")[b.text.Split(" ").Length-7],
                               b.text.Split(" ")[b.text.Split(" ").Length-6],
                               b.text.Split(" ")[b.text.Split(" ").Length-5],
                               b.text.Split(" ")[b.text.Split(" ").Length-4],
                               b.text.Split(" ")[b.text.Split(" ").Length-3],
                               b.text.Split(" ")[b.text.Split(" ").Length-2],
                               b.text.Split(" ")[b.text.Split(" ").Length-1]
                           };
                            return String.Join(' ', re);
                        }
                        else
                        {
                            string[] re =
                            {
                               b.text.Split(" ")[i-5],
                               b.text.Split(" ")[i-4],
                               b.text.Split(" ")[i-3],
                               b.text.Split(" ")[i-2],
                               b.text.Split(" ")[i-1],
                               b.text.Split(" ")[i],
                               b.text.Split(" ")[i+1],
                               b.text.Split(" ")[i+2],
                               b.text.Split(" ")[i+3],
                               b.text.Split(" ")[i+4]
                           };
                            return String.Join(' ', re);
                        }

                    }
                    return b.text;
                }
            }
        }
        return "";
    }
    public static SearchItem[] EliminateDouble(SearchItem[] a)
    {
        SearchItem[] b = new SearchItem[] { };
        foreach (SearchItem x in a)
        {
            bool isin = false;
            foreach (SearchItem y in b)
            {
                if (y.Title == x.Title)
                {
                    isin = true;
                }
            }
            if (!isin)
            {
                SearchItem[] c = new SearchItem[b.Length + 1];
                for (int i = 0; i < b.Length; i++)
                {
                    c[i] = b[i];
                }
                c[c.Length - 1] = x;
                b = c;
            }
        }
        return b;
    }
}
