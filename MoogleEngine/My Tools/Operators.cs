namespace MoogleEngine;


public class Operators
{
    public static string[] NoSign(string[] a) //Metodo para crear un array de palabras modificadas por el operador NO
    {
        string[] answer = new string[] { };
        foreach (string x in a)
        {
            if (x[0] == '!')
            {
                answer = Methods.AddString(answer, x);
            }
        }
        string[] RealAnswer = new string[answer.Length];
        for (int i = 0; i < RealAnswer.Length; i++)
        {
            char[] b = answer[i].ToCharArray();
            char[] c = new char[b.Length - 1];

            for (int j = 0; j < c.Length; j++)
            {
                c[j] = b[j + 1];
            }
            RealAnswer[i] = String.Join("", c);
        }
        return RealAnswer;
    }
    public static string[] YesSign(string[] a) //Metodo para crear un array de palabras modificadas por el operador YES
    {
        string[] answer = new string[] { };
        foreach (string x in a)
        {
            if (x[0] == '^')
            {
                answer = Methods.AddString(answer, x);
            }
        }
        string[] RealAnswer = new string[answer.Length];
        for (int i = 0; i < RealAnswer.Length; i++)
        {
            char[] b = answer[i].ToCharArray();
            char[] c = new char[b.Length - 1];

            for (int j = 0; j < c.Length; j++)
            {
                c[j] = b[j + 1];
            }
            RealAnswer[i] = String.Join("", c);
        }
        return RealAnswer;
    }
    public static string[] CloseSign(string[] a) //Metodo para crear un array de palabras modificadas por el operador CLOSE
    {
        string[] answer = new string[] { };
        foreach (string x in a)
        {
            foreach (char y in x)
            {
                if (y == '~')
                {
                    answer = Methods.AddString(answer, x);
                }
            }
        }
        return answer;
    }
    public static string[] CareSign(string[] a) //Metodo para crear un array de palabras modificadas por el operador CARE
    {
        string[] answer = new string[] { };
        foreach (string x in a)
        {
            if (x[0] == '*')
            {
                answer = Methods.AddString(answer, x);
            }
        }
        return answer;
    }
    public static List<(TXT, float)> OpNo(List<(TXT, float)> a, string[] b)
    {
        if (b.Length != 0)
        {
            List<(TXT, float)> answer = new List<(TXT, float)>();
            for (int i = 0; i < a.Count; i++)
            {
                foreach (string x in b)
                {
                    if (!a[i].Item1.text.Contains(x) && !a[i].Item1.title.Contains(x))
                    {
                        answer.Add(a[i]);
                    }
                }
            }
            return answer;
        }
        return a;
    }
    public static List<(TXT, float)> OpYes(List<(TXT, float)> a, string[] b)
    {
        if (b.Length != 0)
        {
            List<(TXT, float)> answer = new List<(TXT, float)>();
            for (int i = 0; i < a.Count; i++)
            {
                foreach (string x in b)
                {
                    if (a[i].Item1.text.Contains(x) || a[i].Item1.title.Contains(x))
                    {
                        answer.Add(a[i]);
                    }
                }
            }
            return answer;
        }
        return a;
    }
    public static List<(TXT, float)> OpCare(List<(TXT, float)> a, string[] b)
    {
        if (b.Length != 0)
        {
            foreach (string x in b)
            {
                int pointcount = 1;
                char[] c = x.ToCharArray();
                foreach (char y in c)
                {
                    if (y == '*')
                    {
                        pointcount++;
                    }
                }
                int reversepointcount = pointcount;
                while (reversepointcount != 0)
                {
                    char[] d = new char[c.Length - 1];
                    for (int i = 0; i < d.Length; i++)
                    {
                        d[i] = c[i + 1];
                    }
                    c = d;
                    reversepointcount--;
                }
                string e = String.Join("", c);
                for (int i = 0; i < a.Count; i++)
                {
                    float score = a[i].Item2;
                    if (a[i].Item1.text.Contains(e))
                    {
                        score = a[i].Item2 * pointcount;
                    }
                    a[i] = (a[i].Item1, score);
                }
            }
        }
        return a;
    }
    public static List<(TXT, float)> OpClose(List<(TXT, float)> a, string[] b)
    {
        if (b.Length == 0)
        {
            return a;
        }
        else
        {
            foreach (string x in b)
            {
                string[] p = x.Split('~');
                if (p[0] == p[1])
                {
                    break;
                }
                for (int k = 0; k < a.Count; k++)
                {
                    if (a[k].Item1.text.Contains(p[0]) && a[k].Item1.text.Contains(p[1]))
                    {
                        string[] t = Methods.StrConverter(a[k].Item1.text);
                        int[] posarray1 = new int[] { };
                        int[] posarray2 = new int[] { };
                        for (int i = 0; i < t.Length; i++)
                        {
                            if (p[0] == t[i])
                            {
                                posarray1 = AddInt(i, posarray1);
                            }
                            if (p[1] == t[i])
                            {
                                posarray2 = AddInt(i, posarray2);
                            }
                        }
                        int distance = int.MaxValue;
                        foreach (int i in posarray1)
                        {
                            foreach (int j in posarray2)
                            {
                                int temporaldistance = Math.Max(i, j) - Math.Min(i, j);
                                if (temporaldistance < distance)
                                {
                                    distance = temporaldistance;
                                }
                            }
                        }
                        a[k] = (a[k].Item1, (float)a[k].Item2 * (float)20 * ((float)1 / (float)distance));
                    }
                }
            }
            return a;
        }
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
}