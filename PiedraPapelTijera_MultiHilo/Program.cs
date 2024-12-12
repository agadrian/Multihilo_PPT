using System;
using System.Threading;

public class PiedraPapelTijera_MultiHilo
{
    // Guadar las elecciones
    private static string[] elecciones = new string[16];
    private static Random random = new Random();
    
    private static object bloqueo = new object();

    
    
    public static void Main()
    {
        // Crear los 16 hilos
        Thread[] hilos = new Thread[16];

        
        // Crear los 16 jugadores, 1 en cada hilo
        for (int i = 0; i < 16; i++)
        {
            
            int jugador = i;
            hilos[i] = new Thread((() => Jugador(jugador)));
            hilos[i].Start();
        }
        
        // Esperamos a que todos los hilos terminen su ejecución
        for (int i = 0; i < 16; i++)
        {
            hilos[i].Join();
        }


        IniciarPartidas();
       

    }
    
    static void Jugador(int jugador)
    {
        // Cada jugador elige aleatoriamente Piedra, Papel o Tijera
        elecciones[jugador] = GenerarEleccion();
        Console.WriteLine($"Jugador {jugador + 1} elige: {elecciones[jugador]}");
    }
    
    
    private static string GenerarEleccion()
    {
        // Eleccion aleatoria
        string[] opciones = { "Piedra", "Papel", "Tijera" };
        return opciones[random.Next(0, opciones.Length)]; // 0-2
    }



    private static void IniciarPartidas()
    {
        int[] ganadores = new int[16];

        // Meter todos los indices al empezar en los ganadores
        for (int i = 0; i < 16; i++)
        {
            ganadores[i] = i;
        }

        
        while (ganadores.Length > 1)
        {
            // La mitad de los jugadores que quedan despues de la primera ronda. Se igauala a la lista ganadores una vez acabe el turno de partidas.
            int[] siguienteRonda = new int[ganadores.Length / 2];

            
            for (int i = 0; i < ganadores.Length / 2; i++)
            {
                // Vamos haciendo pasrtidas de dos en dos. El primero con el segundo, tercero coin el cuarto, y asi sucesivamente
                int jugador1 = ganadores[i * 2];
                int jugador2 = ganadores[i * 2 + 1];
                Console.WriteLine($"Jugador {jugador1 + 1} contra {jugador2 + 1}");

                // Vamos añadiendo el ganador a la lista de siguienteronda
                siguienteRonda[i] = CheckVictoria(jugador1, jugador2);
            }
            
            
            // Movemos los nuevos finalistas a la lista de ganadores inicial.
            ganadores = siguienteRonda;

            if (ganadores.Length > 1)
            {
                Console.WriteLine($"\nSiguiente ronda Comienza... Jugadores restantes: {string.Join(", ", ganadores.Select(j => j + 1))}");
            }
            
        }
        
        Console.WriteLine($"\nEl jugador final y ganador es: {ganadores[0] + 1}");
       
    }
    
    private static int CheckVictoria(int jug1, int jug2)
    {
        //Console.WriteLine("Resultados de la partida: \n");
        if (elecciones[jug1] == elecciones[jug2])
        {
            Console.WriteLine($"Empate! Pasa el juagador: {jug1+1}");
            return jug1; // Seria empate, pero devuelvo el jug1 por ejemplo
        }
        else if (
            (elecciones[jug1] == "Piedra" && elecciones[jug2] == "Tijera") ||
            (elecciones[jug1] == "Papel" && elecciones[jug2] == "Piedra") ||
            (elecciones[jug1] == "Tijera" && elecciones[jug2] == "Papel")
        )
        {
            Console.WriteLine($"Jugador {jug1+1} gana!");
            return jug1;
            
        }
        else
        {
            Console.WriteLine($"Jugador {jug2+1} gana!");
            return jug2;
           
        }
    }
}