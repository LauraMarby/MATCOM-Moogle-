namespace MoogleEngine;
public class MyMatrix
{
    //multiplicar dos matrices
    double[,] MatrixMult(double[,] a, double[,] b)
    {
        if (a.GetLength(1) != b.GetLength(0))
        {
            throw new Exception();
        }
        double[,] result = new double[a.GetLength(0), b.GetLength(1)];
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < b.GetLength(1); j++)
            {
                for (int k = 0; k < a.GetLength(1); k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        return result;
    }
    //mutliplicar matriz por escalar
    double[,] MatrixMultNum(double[,] a, double alf)
    {
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                a[i, j] = a[i, j] * alf;
            }
        }
        return a;
    }

    //traspuesta
    double[,] TrasposedMatrix(double[,] a)
    {
        double[,] trasposed = new double[a.GetLength(1), a.GetLength(0)];
        int k = 0;
        int l = 0;
        for (int i = 0; i < a.GetLength(0); i++)
        {
            k = 0;
            for (int j = 0; j < a.GetLength(1); j++)
            {
                trasposed[k, l] = a[i, j];
                k++;
            }
            l++;
        }
        return trasposed;
    }

    //suma de matrices
    int[,] MatrixSum(int[,] a, int[,] b)
    {
        if (a.GetLength(0) == b.GetLength(0) && a.GetLength(1) == b.GetLength(1))
        {
            int[,] sum = new int[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    sum[i, j] = a[i, j] + b[i, j];
                }
            }
            return sum;
        }
        throw new Exception();
    }

    //gauss
    double[,] Gauss(double[,] a, bool x)
    {
        double[,] Order(double[,] a, bool x)
        {
            if (x == false)
            {
                return a;
            }
            int countcero = a.GetLength(1);
            int[] order = new int[a.GetLength(0)];
            int n = order.Length - 1;
            int[] Order2(int countcero, int[] order, int n)
            {
                int count;
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    count = 0;
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        if (a[i, j] != 0)
                        {
                            break;
                        }
                        else if (a[i, j] == 0)
                        {
                            count++;
                        }
                    }
                    if (count == countcero)
                    {
                        order[n] = i;
                        n--;
                    }
                }
                if (countcero >= 0)
                {
                    order = Order2(countcero - 1, order, n);
                }
                return order;
            }
            order = Order2(countcero, order, n);
            double[,] newa = new double[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < order.Length; i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    newa[i, j] = a[order[i], j];
                }
            }
            return newa;
        }

        double[,] Recursiv(double[,] a, int n, int m)
        {
            double k;
            double l = a[n - 1, m];
            for (int i = n; i < a.GetLength(0); i++)
            {
                k = a[i, m];
                for (int j = m; j < a.GetLength(1); j++)
                {
                    if (a[i, j] == 0)
                    {
                        break;
                    }
                    else
                    {
                        a[i, j] = k * (-1 * a[n - 1, j]) + l * a[i, j];
                    }
                }
            }
            if (n < a.GetLength(0) - 1 && m < a.GetLength(1) - 1)
            {
                a = Recursiv(a, n + 1, m + 1);
            }
            return a;
        }
        a = Order(a, x);
        a = Recursiv(a, 1, 0);
        return a;
    }

    //determinante
    double Det(double[,] a)
    {
        if (a.GetLength(0) != a.GetLength(1))
        {
            throw new Exception();
        }
        if (a.Length == 1)
        {
            return a[0, 0];
        }
        double RecursivMult(double[,] a, int f, int c, int count, double result)
        {
            if (c == a.GetLength(1))
            {
                c = 0;
            }
            result = a[f, c] * result;
            count++;
            if (count < a.GetLength(1) && f < a.GetLength(0) - 1)
            {
                result = RecursivMult(a, f + 1, c + 1, count, result);
            }
            return result;
        }

        int i = 0;
        double sum1 = 0;
        while (i < a.GetLength(0))
        {
            sum1 += RecursivMult(a, 0, i, 0, 1);
            if (a.GetLength(0) == 2)
            {
                break;
            }
            i++;
        }

        double RecursivMult2(double[,] a, int f, int c, int count, double result)
        {
            if (c == a.GetLength(1))
            {
                c = 0;
            }
            result *= a[f, c];
            count++;
            if (count < a.GetLength(1) && f >= 0)
            {
                result = RecursivMult2(a, f - 1, c + 1, count, result);
            }
            return result;
        }

        i = 0;
        double sum2 = 0;
        while (i < a.GetLength(0))
        {
            sum2 += RecursivMult2(a, a.GetLength(0) - 1, i, 0, 1);
            if (a.GetLength(0) == 2)
            {
                break;
            }
            i++;
        }

        double result = sum1 - sum2;
        return result;
    }

    //inversa
    double[,] InverseMatrix(double[,] a)////////////////
    {
        double det = Det(a);
        if (det != 0)
        {
            a = TrasposedMatrix(a);
            double[,] inverse = new double[a.GetLength(0), a.GetLength(1)];
            double[,] dets = new double[a.GetLength(0) - 1, a.GetLength(1) - 1];
            int n;
            int x = 0;
            int y = 0;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        n = 1;
                    }
                    else
                    {
                        n = -1;
                    }
                    for (int k = 0; k < a.GetLength(0); k++)
                    {
                        for (int l = 0; l < a.GetLength(1); l++)
                        {
                            if (k != i && j != l)
                            {
                                dets[x, y] = a[k, l];
                                if (y == dets.GetLength(1) - 1)
                                {
                                    if (x != dets.GetLength(0) - 1)
                                    {
                                        x++;
                                    }
                                    y = 0;
                                }
                                else
                                {
                                    y++;
                                }
                            }
                        }
                    }
                    inverse[i, j] = n * Det(dets);
                }
            }
            inverse = MatrixMultNum(inverse, 1 / det);
            return inverse;
        }
        throw new Exception();
    }

    //identidad
    double[,] Identity(int n)
    {
        double[,] Identity = new double[n, n];
        int count = 0;
        for (int i = 0; i < Identity.GetLength(0); i++)
        {
            for (int j = 0; j < Identity.GetLength(1); j++)
            {
                if (j == count)
                {
                    Identity[i, j] = 1;
                }
                else
                {
                    Identity[i, j] = 0;
                }
            }
            count++;
        }
        return Identity;
    }
}