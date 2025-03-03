﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_of_triangular_matrix
{
    class Operations
    {
//-------------- Функция для получения индекса к в сжатой форме--------------
// Предупреждение: функция корректно работает, если M[i, j] является значимым
// Принимает на вход i, j - номер строки и столбца в матрице, саму матрицу
// Возвращает индекс элемента в сжатой форме матрицы

        public static int getIndexK(int i, int j, Matrix M)
        {
            if (M.Type == Category.bot_left)
            {
                return M.N * i - i * (i + 1) / 2 + j;
            }
            else if (M.Type == Category.top_right)
            {
                return i * (i + 1) / 2 + j;
            }
            else if (M.Type == Category.bot_right)
            {
                return M.N * i - i * (i - 1) / 2 + j;
            }
            else if (M.Type == Category.top_left)
            {
                return (i + 1) * i / 2 + j - (M.N - i - 1);
            }
            else
            {
                return -1;
            }
        }

//-------------- Функция для проверки элемента на значимость--------------
// Принимает на вход i, j - номер строки и столбца в матрице, саму матрицу
// Возвращает true, если элемент M[i, j] должен быть НЕЗНАЧИМЫМ
// Возвращает false, если элемент M[i, j] должен быть ЗНАЧИМЫМ

        public static bool isV(int i, int j, Matrix M)
        {
            if (M.Type == Category.bot_left)
            {
                return (j < i);
            }
            else if (M.Type == Category.top_right)
            {
                return !(j < i + 1);
            }
            else if (M.Type == Category.bot_right)
            {
                return !(j < M.N - i);
            }
            else if (M.Type == Category.top_left)
            {
                return (j < M.N - i - 1);
            }
            else
            {
                return false;
            }
        }

//-------------- Функция для возвращения значения элемента матрицы--------------
// Принимает на вход i, j - номер строки и столбца в матрице, саму матрицу
// Возвращает значение элемента из сжатой формы матрицы М

        public static double getElement(int i, int j, Matrix M)
        {
            if (M.Type == Category.bot_left)
            {
                if (j < i)
                {
                    return M.V;
                }
                else
                {
                    int k = M.N * i - i * (i + 1) / 2 + j; // выведенная формула
                    return M.Packed_form[k];
                }
            }
            else if (M.Type == Category.top_right)
            {
                if (j < i + 1)
                {
                    int k = i * (i + 1) / 2 + j; // выведенная формула
                    return M.Packed_form[k];
                }
                else
                {
                    return M.V;
                }
            }
            else if (M.Type == Category.bot_right)
            {
                if (j < M.N - i)
                {
                    int k = M.N * i - i * (i - 1) / 2 + j;// выведенная формула
                    return M.Packed_form[k];
                }
                else
                {
                    return M.V;
                }
            }
            else if (M.Type == Category.top_left)
            {
                if (j < M.N - i - 1)
                {
                    return M.V;
                }
                else
                {
                    int k = (i + 1) * i / 2 + j - (M.N - i - 1); // выведенная формула
                    return M.Packed_form[k];
                }
            }
            else
            {
                return Double.NaN;
            }
        }

//-------------- Функция суммирования матриц А и В--------------
// Предупреждение: История сообщений передается по ссылке, так как она изменяется
// Принимает на вход матрицы A, B, C, историю сообщений
// Возвращает результат сложения двух матриц, если равны размерности и типы матриц A, B
// Возвращает матрицу С, если не совпадают размерности или типы матриц A, B
       
        public static Matrix Summ(Matrix A, Matrix B, Matrix C, ref History_message history)
        {
            if (A.Type == B.Type && A.N == B.N)
            {
                int sizePackedForm = A.N * (A.N + 1) / 2;
                Matrix Result = new Matrix('C', A.N, A.V + B.V, A.Type, new double[sizePackedForm]);
            
                for (int i = 0; i < sizePackedForm; i++)
                {
                    Result.Packed_form[i] = A.Packed_form[i] + B.Packed_form[i];
                }

                history = history.Add("Операция успешно выполнена");
                return Result;
            }
            else
            {
                history = history.Add("Не совпадают типы или размерности матриц");
                return C;
            }
        }

//-------------- Функция разности матриц А и В--------------
// Предупреждение: История сообщений передается по ссылке, так как она изменяется
// Принимает на вход матрицы A, B, C, историю сообщений
// Возвращает результат вычитания двух матриц, если равны размерности и типы матриц A, B
// Возвращает матрицу С, если не совпадают размерности или типы матриц A, B

        public static Matrix Subtraction(Matrix A, Matrix B, Matrix C, ref History_message history)
        {

            if (A.Type == B.Type && A.N == B.N)
            {
                int sizePackedForm = A.N * (A.N + 1) / 2;
                Matrix Result = new Matrix('C', A.N, A.V-B.V, A.Type, new double[sizePackedForm]);

                for (int i = 0; i < sizePackedForm; i++)
                {
                    Result.Packed_form[i] = A.Packed_form[i] - B.Packed_form[i];
                }
                history = history.Add("Операция успешно выполнена");
                return Result;
            }
            else
            {
                history = history.Add("Не совпадают типы или размерности матриц");
                return C;
            }
        }

//-------------- Функция умножения матриц А и В--------------
// Предупреждение: История сообщений передается по ссылке, так как она изменяется
// Принимает на вход матрицы A, B, C, историю сообщений
// Возвращает результат умножения двух матриц, если равны размерности и типы матриц A, B и при этом значения V равны 0
// Возвращает матрицу С, если не совпадают размерности или типы матриц A, B        

         public static Matrix Multiply(Matrix A, Matrix B, Matrix C, ref History_message history)
         {
            if (A.N == B.N)
	        {
                Category ourType = Category.none;
                Matrix Result = new Matrix('C', A.N, A.V, Category.none, null);

                for(Category type = Category.top_left; type <= Category.bot_right && ourType == Category.none; type++)
                {
                    Result.Type = type;
                    ourType = TypeCorrect(A, B, Result);
                } 
                if(ourType == Category.none)
                {
                    history = history.Add("Операция прервана. Матрица не будет треугольной формы");
                    return C;
                }
                int sizePackedForm = A.N * (A.N + 1) / 2;
                Result.Packed_form = new double[sizePackedForm];
                for (int i = 0; i < Result.N; i++)
                    for (int j = 0; j < Result.N; j++)
                    {
                        if(!isV(i, j, Result))
                        {
                            Result.Packed_form[getIndexK(i, j, Result)] = 0;
                            for (int k = 0; k < Result.N; k++)
                            {
                                double aik = 0, bkj = 0;
                                if (!isV(i, k, A))
                                {
                                    aik = getElement(i, k, A);
                                }
                                if (!isV(k, j, B))
                                {
                                    bkj = getElement(k, j, B);
                                }
                                Result.Packed_form[getIndexK(i, j, Result)] += aik * bkj;
                            }
                        }
                    }
                history = history.Add("Операция успешно выполнена");
                return Result;
            }
            else
            {
                history = history.Add("Не совпадают размерности матриц");
                return C;
            }
         }


//-------------------Подфункция для проверки матрицы на тип------------
        public static Category TypeCorrect(Matrix A, Matrix B, Matrix Result)
        {
            bool isfirst = true;
            double a0 = 0, a1;
            for (int i = 0; i < A.N; i++)
                for (int j = 0; j < A.N; j++)
                {
                    if (isV(i, j, Result))
                    {
                        a1 = 0;
                        for (int k = 0; k < Result.N; k++)
                        {
                            double aik = A.V, bkj = B.V;
                            if (!isV(i, k, A))
                                aik = getElement(i, k, A);
                            if (!isV(k, j, B))
                                bkj = getElement(k, j, B);

                            if (isfirst)
                                a0 += aik * bkj;
                            else
                                a1 += aik * bkj;
                        }
                        if (isfirst)
                            isfirst = false;
                        else
                        {
                            if (Math.Abs(a0 - a1) > 1E-15)
                                return Category.none;
                        }
                    }
                }
            Result.V = a0;
            return Result.Type;
        }


//-------------- Функция перестановки матриц --------------
// Предупреждение: История сообщений и матрицы передаются по ссылке, так как они изменяются
// Принимает на вход 2 матрицы, историю сообщений
// Возвращает определитель матрицы, если тип не nonе и V равно 0
// Возвращает Nan, если не известен тип или V не равно 0     
        public static void Replace_A_B(ref Matrix A,ref Matrix B, ref History_message history)
        {
            Matrix C = A;
            A = new Matrix(A.Name, B.N, B.V, B.Type, B.Packed_form);
            B = new Matrix(B.Name, C.N, C.V, C.Type, C.Packed_form);
            history = history.Add("Операция успешно выполнена");
        }


//-------------- Функция нахождения определителя матрицы --------------
// Предупреждение: История сообщений передается по ссылке, так как она изменяется
// Принимает на вход матрицы A, историю сообщений
// Возвращает определитель матрицы, если тип не nonе и V равно 0
// Возвращает Nan, если не известен тип или V не равно 0        

        public static double DeterminantReverseMatrix (Matrix A, ref History_message history)
        {
            double Det = 1;
            if (A.V != 0)
            {
                history = history.Add("Операция прервана. Значение V должно быть равно 0");
                return Double.NaN;
            }
            else
            {
                if (A.Type == Category.bot_left || A.Type == Category.top_right)
                {
                    for (int i = 0; i < A.N; i++)
                        Det *= getElement(i, i, A);
                }
                else
                {
                    for (int i = 0; i < A.N; i++)
                        Det *= getElement(A.N - 1 - i, i, A);
                    Det = -Det;
                }
                history = history.Add("Определитель равен " + Det.ToString());
                return Det;
            }    
        }

//-------------- Функция нахождения обратной матрицы --------------
// Предупреждение: История сообщений передается по ссылке, так как она изменяется
// Принимает на вход 2 матрицы, историю сообщений   
        public static Matrix Reverse(Matrix A, Matrix C, ref History_message history, double detA)
        {
            if (Math.Abs(detA) == 0) 
            {
                history = history.Add("Операция прервана. Матрица невырожденная!");
                return C;
            }
            else
            {
                double[, ] TempMatrica = getObrMatrica(getMatricaFromMatrix(A), A.N);
                Matrix TempMatrix = new Matrix('C', A.N, A.V, A.Type, null);
                history = history.Add("Операция успешно выполнена");
                if (A.Type == Category.top_left)
                    TempMatrix.Type = Category.bot_right;
                else if (A.Type == Category.bot_right)
                    TempMatrix.Type = Category.top_left;
                TempMatrix.Packed_form = PackMatrica(TempMatrix, TempMatrica);
                return TempMatrix;
            }
              
        }

//------------- Подфункция нахождения обратной матрицы --------------
// Принимает на вход двумерный массив - данная матрица и размерность
// Возвращает двумерный массив - матрицу, обратную данной
        public static double[, ] getObrMatrica(double[, ] A, int n)
        {
            double temp;
            double[,] E = getEMatrix(n);

            if(A[0, 0] == 0)
            {
                E = SwapRowsInMatrix(E, n);
                A = SwapRowsInMatrix(A, n);
            }

            for (int k = 0; k < n; k++)
            {
                temp = A[k, k];

                for (int j = 0; j < n; j++)
                {
                    A[k, j] /= temp;
                    E[k, j] /= temp;
                }

                for (int i = k + 1; i < n; i++)
                {
                    temp = A[i, k];

                    for (int j = 0; j < n; j++)
                    {
                        A[i, j] -= A[k, j] * temp;
                        E[i, j] -= E[k, j] * temp;
                    }
                }
            }
            for (int k = n - 1; k > 0; k--)
            {
                for (int i = k - 1; i >= 0; i--)
                {
                    temp = A[i, k];

                    for (int j = 0; j < n; j++)
                    {
                        A[i, j] -= A[k, j] * temp;
                        E[i, j] -= E[k, j] * temp;
                    }
                }
            }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    A[i, j] = E[i, j];
            E = null;
            return A;
        }
//------------- Подфункция задания единичной матрицы --------------
// Принимает на вход размерность
// Возвращает двумерный массив - единичну матрицу
        public static double[,] getEMatrix(int n)
        {
            double[,] E = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        E[i, j] = 1.0;
                    else
                        E[i, j] = 0.0;
                }
            return E;
        }
//---------- Подфункция получения думерного массива из объекта класса Matrix --------------
// Принимает на вход объект класса Matrix
// Возвращает двумерный массив - полную матрицу данного объекта
        public static double[,] getMatricaFromMatrix(Matrix A)
        {
            double[,] matrix = new double[A.N, A.N];
            for (int tempi = 0; tempi < A.N; tempi++)
                for (int tempj = 0; tempj < A.N; tempj++)
                    matrix[tempi, tempj] = getElement(tempi, tempj, A);
            return matrix;
        }
//------------- Подфункция упаковки двумерного массива --------------
// Принимает на вход объект класса Matrix и двумерный массив - обратная матрица 
// Возвращает упакованную форму
        public static double[] PackMatrica(Matrix M, double[,] ResultMatrica)
        {
            double[] packedForm = new double[M.N * (M.N + 1) / 2];
            int k = 0;
            for (int i = 0; i < M.N; i++)
                for (int j = 0; j < M.N; j++)
                {
                    if (isV(i, j, M))
                    {
                        // пропустить
                    }
                    else
                    {
                        packedForm[k] = ResultMatrica[i, j];
                        k++;
                    }
                }
            return packedForm;
        }

        public static double[,] SwapRowsInMatrix(double[,] A, int n)
        {
            double tempelement;
            for(int i = 0; i < n/2; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    tempelement = A[i, j];
                    A[i, j] = A[n - i - 1, j];
                    A[n - i - 1, j] = tempelement;
                }
            }
            return A;
        }
    }

}
