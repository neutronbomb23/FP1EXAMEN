using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        const int N = 4;
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            int[] combJug = new int[N];
            int[] combSec = new int[N];
            int muertos, heridos;
            GeneraComb(combSec);
            for (int i = 0; i < combSec.Length; i++)
            {
                Console.Write(combSec[i]);
            }
            LeeCombinacion(combJug);
            for (int i = 0; i < combJug.Length; i++)
            {
                Console.WriteLine(combJug[i]);
            }
        }

        public static void GeneraComb(int[] combSec)
        {
            int[] v = { 5, 3, 1, 9, 6, 0, 2, 8, 4, 7 };
            int i = rnd.Next(0, 7);
            Console.WriteLine(i);
            for (int j = 0; j < N; j++)
            {
                combSec[j] = v[i + j];
            }
        }

        public static bool Muerto(int[] combSec, int[] combJug, int pos)
        {
            return combSec[pos] == combJug[pos];
        }

        public static bool Herido(int[] combSec, int[] combJug, int pos)
        {
            // Obtener el valor objetivo en la posición especificada del combinación secreta
            int target = combSec[pos];
            // Recorrer la combinación del jugador
            for (int i = 0; i < combJug.Length; i++)
            {
                // Verificar si el valor en la posición actual no es la misma posición de comparación
                // y si coincide con el valor objetivo
                if (i != pos && combJug[i] == target)
                {
                    // El jugador ha acertado un número en una posición incorrecta
                    return true;
                }
            }
            // No se ha encontrado ninguna coincidencia en posiciones incorrectas
            return false;
        }

        public static void Evalua(int[] combSec, int[] combJug, out int m, out int h)
        {
            m = 0;
            h = 0;
            for (int i = 0; i < combSec.Length; i++)
            {
                if (Muerto(combSec, combJug, i))
                {
                    m++;
                }
                else if (Herido(combSec, combJug, i))
                {
                    h++;
                }
            }
        }

        public static void LeeCombinacion(int[] combJug)
        {
            Console.WriteLine("Prueba Combinación: ");
            for (int i = 0; i < N; i++)
            {
                combJug[i] = int.Parse(Console.ReadLine());
            }
        }
    }
}