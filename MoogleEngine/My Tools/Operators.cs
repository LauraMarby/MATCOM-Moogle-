namespace MoogleEngine;


public class Operators
{
    public static string[] NoSign(string[] a) //Metodo para crear un array de palabras modificadas por el operador NO //arreglado
    {
        string[] answer = new string[] { };
        int j;
        string s;
        foreach (string x in a)
        {
            j = 0;
            for (int k = 0; k < x.Length; k++)
            {
                if (x[k] == '!')
                {
                    j++;
                }
            }
            if (j > 0 && j != x.Length)
            {
                char[] c = new char[x.Length - 1];
                for (int i = 0; i < c.Length; i++)
                {
                    c[i] = x[j];
                    j++;
                }
                s = String.Concat(c);
                answer = Methods.AddString(answer, s);
            }
        }
        return answer;
    }
    public static string[] YesSign(string[] a) //Metodo para crear un array de palabras modificadas por el operador YES //arreglado
    {
        string[] answer = new string[] { };
        int j;
        string s;
        foreach (string x in a)
        {
            j = 0;
            for (int k = 0; k < x.Length; k++)
            {
                if (x[k] == '^')
                {
                    j++;
                }
            }
            if (j > 0 && x.Length != j)
            {
                char[] c = new char[x.Length - 1];
                for (int i = 0; i < c.Length; i++)
                {
                    c[i] = x[j];
                    j++;
                }
                s = String.Concat(c);
                answer = Methods.AddString(answer, s);
            }
        }
        return answer;
    }
    public static string[] CloseSign(string[] a) //Metodo para crear un array de palabras modificadas por el operador CLOSE //arreglado
    {
        string[] answer = new string[] { };
        int count;
        foreach (string x in a)
        {
            count = 0;
            if (x[x.Length - 1] != '~' && x[0] != '~')
            {
                foreach (char y in x)
                {
                    if (y == '~')
                    {
                        count++;
                    }
                }
                if (count != x.Length)
                {
                    answer = Methods.AddString(answer, x);
                }
            }
            /*foreach (char y in x)
            {
                if (y == '~')
                {
                    if (x.Length != 1)
                    {
                        answer = Methods.AddString(answer, x);
                    }
                }
            }*/
        }
        return answer;
    }
    public static string[] CareSign(string[] a) //Metodo para crear un array de palabras modificadas por el operador CARE //arreglado
    {
        string[] answer = new string[] { };
        int count;
        foreach (string x in a)
        {
            count = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == '*')
                {
                    count++;
                }
            }
            if (count != x.Length && count != 0)
            {
                answer = Methods.AddString(answer, x);
            }
            /*if (x[0] == '*')
            {
                if (x.Length != 1)
                {
                    answer = Methods.AddString(answer, x);
                }
            }*/
        }
        return answer;
    }
    public static List<(TXT, float)> OpNo(List<(TXT, float)> a, string[] b) //arreglado
    {
        if (b.Length != 0)
        {
            int count;
            List<(TXT, float)> answer = new List<(TXT, float)>();
            for (int i = 0; i < a.Count; i++)
            {
                count = 0;
                foreach (string x in b)
                {
                    if (!a[i].Item1.text.ToLower().Contains(x) && !a[i].Item1.title.ToLower().Contains(x))
                    {
                        count++;
                    }
                    if (count == b.Length)
                    {
                        answer.Add(a[i]);
                    }
                }
            }
            return answer;
        }
        return a;
    }
    public static List<(TXT, float)> OpYes(List<(TXT, float)> a, string[] b) //arreglado
    {
        if (b.Length != 0)
        {
            int count;
            List<(TXT, float)> answer = new List<(TXT, float)>();
            for (int i = 0; i < a.Count; i++)
            {
                count = 0;
                foreach (string x in b)
                {
                    if (a[i].Item1.text.ToLower().Contains(x) || a[i].Item1.title.ToLower().Contains(x))
                    {
                        count++;
                    }
                    if (count == b.Length)
                    {
                        answer.Add(a[i]);
                    }
                }
            }
            return answer;
        }
        return a;
    }
    public static List<(TXT, float)> OpCare(List<(TXT, float)> a, string[] b)//arreglado
    {
        if (b.Length != 0)
        {
            int pointcount;
            int j;
            string word;
            float score;
            foreach (string x in b)
            {
                pointcount = 1;
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] == '*')
                    {
                        pointcount++;
                    }
                }
                char[] c = new char[x.Length - pointcount + 1];
                j = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != '*')
                    {
                        c[j] = x[i];
                        j++;
                    }
                }
                word = String.Concat(c);
                for (int i = 0; i < a.Count; i++)
                {
                    score = a[i].Item2;
                    if (a[i].Item1.text.ToLower().Contains(word))
                    {
                        score = a[i].Item2 * pointcount;
                    }
                    a[i] = (a[i].Item1, score);
                }
                /*int pointcount = 1;
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
                }*/
            }
        }
        return a;
    }
    public static List<(TXT, float)> OpClose(List<(TXT, float)> a, string[] b) //arreglado
    {
        if (b.Length == 0)
        {
            return a;
        }
        else
        {
            foreach (string x in b)
            {
                string[] p1 = x.Split('~');
                string[] p = Methods.EliminateRepeatsAndNulls(p1);
                if (p.Length == 1)
                {
                    break;
                }
                for (int k = 0; k < a.Count; k++)
                {
                    if (Methods.ContainsTitle(a[k], p) || Methods.ContainsText(a[k], p))
                    {
                        int distancetitle = int.MaxValue;
                        int distancetext = int.MaxValue;
                        if (Methods.ContainsTitle(a[k], p))
                        {
                            distancetitle = Methods.CLOSE(a[k].Item1.title, p);
                        }
                        if (Methods.ContainsText(a[k], p))
                        {
                            distancetext = Methods.CLOSE(a[k].Item1.text,p);
                        }
                        int distance = Math.Min(distancetitle, distancetext);
                        a[k] = (a[k].Item1, (float)a[k].Item2 * (float)a.Count() * ((float)1 / (float)distance));
                    }

                    /*if (Methods.Contains2(a[k],p))
                    {
                        string[] t = Methods.StrConverter(a[k].Item1.text);
                        int[][] pos = new int[p.Length][];

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
                    }*/
                }
            }
            return a;
        }
    }
}