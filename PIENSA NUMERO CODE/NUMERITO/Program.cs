using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    
    {
        const int N = 4;
        static Random rnd = new Random();
        static void Main(string[] args)
        {
       
            int maxMuertos;
            int intentos = 0;
            int MAX_INTENTOS = 10;
            int muertos = 0, heridos = 0;
            int[] combJug;
            int[] combSec;
            Console.WriteLine("Hello World!");
            //GeneraComb(combSec);
            Console.WriteLine("Prefieres 4 dígitos o 6 dígitos? Escribe q para 4 dígitos o w para 6 dígitos");
            char c = char.Parse(Console.ReadLine());
            if(c == 'q')
            { 
                combJug = new int[4];
                combSec = new int[4];
                maxMuertos = 4;
                SeleccionaNum("Numeros.txt", combSec);
            }
            else
            {
                combJug = new int[6];
                combSec = new int[6];
                maxMuertos = 6;
                SeleccionaNum("Numeros100.txt", combSec);
            }
            while(intentos < MAX_INTENTOS && muertos != maxMuertos)
            {
                 LeeCombinacion(combJug);
                 Evalua(combSec, combJug, out muertos, out heridos);
                 intentos++;                 
                 Console.WriteLine("Muertos: {0}  Heridos: {1} Intentos: {2}", muertos, heridos, intentos);
            }          
            if(muertos >= maxMuertos)
            {         
                Console.WriteLine("¡HAS GANADO!");             
            }
            else if(intentos >= MAX_INTENTOS)
            {
                Console.WriteLine("Has perdido! LA COMBINACIÓN ERA: ");
                for(int i = 0; i < combSec.Length; i++)
                {
                    Console.Write(combSec[i]);
                } 
            }          
        }

      /*  public static void SeleccionaNum(string file,  int[] combSec)
        {
            int numNumeros;
            int[] posiblesCombinaciones;

            using(StreamReader reader = new StreamReader(file))
            {
                numNumeros = int.Parse(reader.ReadLine());
                posiblesCombinaciones = new int[numNumeros];
                for(int i = 0; i < numNumeros; i++)
                {
                    posiblesCombinaciones[i] = int.Parse(reader.ReadLine());
                }
                Random rnd = new Random();
                int numero = rnd.Next(numNumeros);
            }
            
        } */

        public static void SeleccionaNum(string file, int[] combSec)
        {
             // Leer todas las líneas del archivo y guardarlas en el arreglo 'lineas'
             string[] lineas = File.ReadAllLines(file);
             // Obtener la cantidad de secuencias del primer elemento del arreglo 'lineas'
             int numSecuencias = int.Parse(lineas[0]);
             // Seleccionar una línea aleatoria, excluyendo la primera línea que contiene la cantidad de secuencias
             int numeroLinea = rnd.Next(1, lineas.Length);
             string lineaSeleccionada = lineas[numeroLinea];
             Console.WriteLine(numeroLinea);
             // Verificar si la longitud de la línea seleccionada coincide con la longitud esperada del arreglo 'combSec'
             if (lineaSeleccionada.Length != combSec.Length)
             {
                   Console.WriteLine("Error: La línea seleccionada no tiene la longitud esperada.");
                   return;
             }
             // Recorrer cada dígito de la línea seleccionada y asignarlo al arreglo 'combSec'
             for (int i = 0; i < combSec.Length; i++)
             {
                 // Intentar convertir el carácter en un dígito numérico y asignarlo a 'combSec[i]'
                 if (!int.TryParse(lineaSeleccionada[i].ToString(), out combSec[i]))
                 {
                     Console.WriteLine("Error: La línea seleccionada contiene caracteres no numéricos.");
                     return;
                 }
             }
        }

        public static void GeneraComb(int[] combSec)
        {
            int[] v = {5,3,1,9,6,0,2,8,4,7};
            int i = rnd.Next(0,7);
            Console.WriteLine(i);
            for(int j = 0; j < N; j++)
            {
                combSec[j] = v[i+j];
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
            for(int i = 0; i < combSec.Length; i++)
            {
                if(Muerto(combSec, combJug, i))
                {
                    m++;
                }
                else if(Herido(combSec, combJug, i))
                {
                    h++;
                }
            }
        }

        public static void LeeCombinacion(int[] combJug)
        {
            Console.WriteLine("Cual es tu combinación?");
            string input = Console.ReadLine();
            // Convertir cada carácter del input a un número entero y almacenarlo en el array
            for (int i = 0; i < combJug.Length; i++)
            {
                combJug[i] = int.Parse(input[i].ToString());
            }
        }
    }
}