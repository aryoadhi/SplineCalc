using System;
using System.Collections.Generic;

namespace SplineCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            List<float> x = new List<float>();
            List<float> y = new List<float>();
            Console.WriteLine("This program will calculate cubic spline function.");
            Console.WriteLine("Input data set.");

            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine("x" + i + ":");
                x.Add(Convert.ToSingle(Console.ReadLine()));
                Console.WriteLine("y" + i + ":");
                y.Add(Convert.ToSingle(Console.ReadLine()));
            }


            int n = x.Count;

            List<float> d_vec = new List<float>();
            List<float> b_vec = new List<float>();
            List<float> a_vec = new List<float>();
            for (int i = 1; i <= (n - 1); i++)
            {
                d_vec.Add(0);
                b_vec.Add(0);
                a_vec.Add(0);
            }
            List<float> vec = new List<float>();
            for (int i = 1; i <= n; i++)
                vec.Add(0);
            List<float> delta_x = new List<float>();
            List<float> delta_y = new List<float>();
            for (int i = 1; i <= (n - 1); i++)
            {
                delta_x.Add(0);
                delta_y.Add(0);
            }

            for (int i = 0; i <= (n - 2); i++)
            {
                a_vec[i] = y[i];
                delta_x[i] = x[i + 1] - x[i];
                delta_y[i] = y[i + 1] - y[i];
            }

            List<float> Au = new List<float>
            {
                0
            };
            List<float> Am = new List<float>
            {
                1
            };
            List<float> Al = new List<float>();
            for (int i = 1; i <= (n - 2); i++)
            {
                Au.Add(delta_x[i]);
                Am.Add(2 * (delta_x[i - 1] + delta_x[i]));
                Al.Add(delta_x[i - 1]);
            }
            Am.Add(1);
            Al.Add(0);
            vec[0] = 0;
            vec[n - 1] = 0;
            for (int i = 1; i <= (n - 2); i++)
                vec[i] = 3 * ((delta_y[i] / delta_x[i])
                    - (delta_y[i - 1] / delta_x[i - 1]));

            Al.Insert(0, Single.NaN);

            Au[0] = Au[0] / Am[0];
            vec[0] = vec[0] / Am[0];
            for (int i = 1; i <= (n - 2); i++)
            {
                Au[i] = Au[i] / (Am[i] - (Al[i] * Au[i - 1]));
                vec[i] = (vec[i] - (Al[i] * vec[i - 1]))
                    / (Am[i] - (Al[i] * Au[i - 1]));
            }
            vec[n - 1] = (vec[n - 1] - (Al[n - 1] * vec[n - 2]))
                    / (Am[n - 1] - (Al[n - 1] * Au[n - 2]));

            List<float> c_vec = new List<float>();
            for (int i = 1; i <= n; i++)
                c_vec.Add(0);
            c_vec[n - 1] = vec[n - 1];
            for (int i = (n - 2); i >= 0; i--)
                c_vec[i] = vec[i] - (Au[i] * c_vec[i + 1]);

            for (int i = 0; i <= (n - 2); i++)
            {
                b_vec[i] = (delta_y[i] / delta_x[i])
                    - (delta_x[i] / 3) * (2 * c_vec[i] + c_vec[i + 1]);
                d_vec[i] = (c_vec[i + 1] - c_vec[i])
                    / (3 * delta_x[i]);
            }

            Console.WriteLine("Coefficients of a:");
            foreach (float element in a_vec)
                Console.WriteLine(Convert.ToString(element));
            Console.WriteLine("Coefficients of b:");
            foreach (float element in b_vec)
                Console.WriteLine(Convert.ToString(element));
            Console.WriteLine("Coefficients of c:");
            foreach (float element in c_vec)
                Console.WriteLine(Convert.ToString(element));
            Console.WriteLine("Coefficients of d:");
            foreach (float element in d_vec)
                Console.WriteLine(Convert.ToString(element));

        }
    }
}
