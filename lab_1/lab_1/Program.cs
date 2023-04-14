using System;
using System.Threading;

namespace lab_1
{
    class Program
    {
        static double[] x = { 1, 2, 3, 4, 5 }; //ініціалізація масиву значень
        static double[] y = { 7.1, 27.8, 62.1, 110, 161 };
        static int n = 0;

        static double a1, b1, a2, b2;
        static double d1, d2;

        public static void Main(string[] args)
        {
            if (x.Length == y.Length)
            {
                n = x.Length;
            }
            for(int i=0; i<n; i++)//нормалізація даних
            {
                x[i] = Math.Log(x[i]);
            }

            Thread thread1 = new Thread(ThreadFunction1); //створ перший потік
            Thread thread2 = new Thread(ThreadFunction2); //створ другий потік
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();


            if (d1 < d2)
            {
                Console.WriteLine("Result Point Vector:");
                Console.WriteLine("y=" + a1 + "*lnx+" + b1);
            }
            else
            {
                Console.WriteLine("Result Point Vector:");
                Console.WriteLine("y=" + Math.Pow(Math.E,a2) + "*x^" + b2);
            }
        }
        public static void ThreadFunction1()
        {
            double Xi = 0;
            double Xi2 = 0;
            double XiYi = 0;
            double Yi = 0;

            for(int i = 0; i < n; i++) { 
                Xi += x[i]; 
                Xi2 += x[i]*x[i];
                XiYi += x[i]*y[i];
                Yi += y[i];
            }
            //знайдемо підсумкові коефіцієнти і похибку
            a1 = (Yi * Xi2 * n - XiYi * n * Xi) / (Xi2 * n * n - n * Xi * Xi);
            b1 = (XiYi * n - Yi * Xi) / (Xi2 * n - Xi * Xi);
            d1 = Math.Sqrt(((Yi - a1 * Xi - b1) * (Yi - a1 * Xi - b1)) / (Yi * Yi));
            Console.WriteLine("d1=" + d1);
        }
        public static void ThreadFunction2()
        {
            double Xi = 0;
            double Xi2 = 0;
            double XiYi = 0;
            double Yi = 0;
            for(int i = 0; i < n; i++)
            {
                y[i] = Math.Log(y[i]);
            }

            for (int i = 0; i < n; i++) 
            {
                Xi += x[i];
                Xi2 += x[i] * x[i];
                XiYi += x[i] * y[i];
                Yi += y[i];
            }
            //знайдемо підсумкові коефіцієнти і похибку
            a2 = (Yi * Xi2 * n - XiYi * n * Xi) / (Xi2 * n * n - n * Xi * Xi);
            b2 = (XiYi * n - Yi * Xi) / (Xi2 * n - Xi * Xi);
            d2 = Math.Sqrt(((Yi - a2 * Xi - b2) * (Yi - a2 * Xi - b2)) / (Yi * Yi));
            Console.WriteLine("d2=" + d2);
        }
    }
}
