using System;

class JuegoDel15
{
    public void Genera(int[,] tab)
    {
        int contador = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                tab[i, j] = contador++;
            }
        }
    }

    public void Desordena(int[,] tab)
    {
        Random random = new Random();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int x = random.Next(4);
                int y = random.Next(4);

                int temp = tab[i, j];
                tab[i, j] = tab[x, y];
                tab[x, y] = temp;
            }
        }
    }

    public void Muestra(int[,] tab)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Console.Write($"{tab[i, j],2} ");
            }
            Console.WriteLine();
        }
    }

    public bool Resuelto(int[,] tab)
    {
        int contador = 1;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (tab[i, j] != contador && contador < 16)
                {
                    return false;
                }
                contador++;
            }
        }
        return true;
    }

    public bool Busca(int[,] tab, int n, out int f, out int c)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (tab[i, j] == n)
                {
                    f = i;
                    c = j;
                    return true;
                }
            }
        }
        f = -1;
        c = -1;
        return false;
    }

    public bool BuscaLibre(int[,] tab, int f, int c, out int lf, out int lc)
    {
        int[] dx = { -1, 0, 1, 0 };
        int[] dy = { 0, 1, 0, -1 };

        for (int i = 0; i < 4; i++)
        {
            lf = f + dx[i];
            lc = c + dy[i];

            if (lf >= 0 && lf < 4 && lc >= 0 && lc < 4 && tab[lf, lc] == 0)
            {
                return true;
            }
        }
        lf = -1;
        lc = -1;
        return false;
    }

    public bool Mueve(int[,] tab, int n)
    {
        int f, c;
        if (Busca(tab, n, out f, out c))
        {
            int lf, lc;
            if (BuscaLibre(tab, f, c, out lf, out lc))
            {
                tab[f, c] = 0;
                tab[lf, lc] = n;
                return true;
            }
        }
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        JuegoDel15 juego = new JuegoDel15();
        int[,] tablero = new int[4, 4];

        juego.Genera(tablero);
        juego.Desordena(tablero);

        while (!juego.Resuelto(tablero))
        {
            Console.Clear();
            juego.Muestra(tablero);
            Console.WriteLine("Ingrese un número entre 1 y 15 para mover, o 0 para salir:");
            int numero = int.Parse(Console.ReadLine());

            if (numero == 0)
            {
                Console.WriteLine("Has salido del juego.");
                break;
            }

            if (juego.Mueve(tablero, numero))
            {
                Console.WriteLine("Movimiento realizado.");
            }
            else
            {
                Console.WriteLine("Movimiento inválido.");
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        if (juego.Resuelto(tablero))
        {
            Console.Clear();
            juego.Muestra(tablero);
            Console.WriteLine("¡Felicidades! Has resuelto el juego del 15.");
        }
    }
}

