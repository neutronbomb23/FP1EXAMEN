using System;

namespace PideUnNumero // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isplaying = true;
            Console.WriteLine("Hello World!");
            Console.WriteLine("Como deseas jugar? \n [a] usuario \n [o] ordenador \n [s] salir");
            char c = char.Parse(Console.ReadLine());
            while(isplaying)
            {
                if(c == 'a')
                {
                    usuario();
                }
                else if(c == 'o')
                {
                    ordenador();
                }
                else if(c == 's')
                {
                    isplaying = false;
                }
            }
            //ordenador();
            //usuario();
        }

        #region Ordenador
        public static void ordenador()
        {
        Random random = new Random();
        int numeroGenerado = random.Next(0, 101); // Número entre 0 y 100
        int intentos = 0;
        bool adivinado = false;

        while (!adivinado) // Booleano de control
        {
            Console.Write("Introduce un número: ");
            int numero = int.Parse(Console.ReadLine()); // Pido número 
                intentos++;

                if (numero == numeroGenerado) // Ganas, booleano de control a true.
                {
                    Console.WriteLine("¡Felicidades! ¡Has adivinado el número en " + intentos + " intentos!");
                    adivinado = true;
                }
                else if (numero < numeroGenerado) 
                {
                    Console.WriteLine("El número generado es mayor que " + numero + ". Sigue intentándolo.");
                }
                else
                {
                    Console.WriteLine("El número generado es menor que " + numero + ". Sigue intentándolo.");
                }
          
            }
        }
        #endregion

        #region Usuario
        static void usuario()
        {
            int min = 0;
            int max = 100;
            bool seguirJugando = true;

            Console.WriteLine("Piensa un número entre 0 y 100");

            while (seguirJugando)
            {
                int candidato = (min + max) / 2;

                Console.Write("Tu número está por arriba, por debajo o es igual que " + candidato + "? [a/d/i]: ");
                string respuesta = Console.ReadLine();

                if (respuesta == "a")
                {
                    min = candidato + 1;
                }
                else if (respuesta == "d")
                {
                    max = candidato - 1;
                }
                else if (respuesta == "i")
                {
                    Console.WriteLine("¡He ganado!!");
                    seguirJugando = false;
                }
                else
                {
                    Console.WriteLine("Respuesta inválida. Por favor, responde con 'a' (arriba), 'd' (abajo) o 'i' (igual).");
                }

                if (min > max)
                {
                    Console.WriteLine("¡Has hecho trampa!"); // Comprobación que ha hecho trampa
                    seguirJugando = false;
                }
            }
        }
        #endregion

    }
}