using System;
using System.Threading;

public class PiedraPapelTijera_MultiHilo
{
    private static string j1Eleccion;
    private static string j2Eleccion;
    private static object bloqueo = new object();

    private static Random random = new Random();
    
    public static void Main()
    {
        
        // Creamos los dos hilos, uno para cada jugador
        Thread jugador1 = new Thread(Jugador1);
        Thread jugador2 = new Thread(Jugador2);
        
        // Inicia los hilos de ambos jugadores
        jugador1.Start();
        jugador2.Start();
        
        // Permite que el hilo principal, espere a que estos dos acaben antes de seguir,, para que cuando vaya a comprobar la victoria ambos hayn elegido ya.
        jugador1.Join();
        jugador2.Join();
        
        
        // Comprueba los resultados
        CheckVictoria();

    }
    
    
    static void Jugador1()
    {
        // Simulamos la elección del jugador 1
        j1Eleccion = GenerarEleccion();
        Console.WriteLine("Jugador 1 elige: " + j1Eleccion);
    }

    private static void Jugador2()
    {
        
        // Simulamos la elección del jugador 2
        j2Eleccion = GenerarEleccion();
        Console.WriteLine("Jugador 2 elige: " + j2Eleccion);
    }
    
    
    private static string GenerarEleccion()
    {
        // Eleccion aleatoria
        string[] opciones = { "Piedra", "Papel", "Tijera" };
        return opciones[random.Next(0, opciones.Length)]; // 0-2
    }
    
    private static void CheckVictoria()
    {
        Console.WriteLine("Resultados de la partida: \n");
        if (j1Eleccion == j2Eleccion)
        {
            Console.WriteLine("Empate!");
        }
        else if (
            (j1Eleccion == "Piedra" && j2Eleccion == "Tijera") ||
            (j1Eleccion == "Papel" && j2Eleccion == "Piedra") ||
            (j1Eleccion == "Tijera" && j2Eleccion == "Papel")
        )
        {
            Console.WriteLine("Jugador 1 gana!");
        }
        else
        {
            Console.WriteLine("Jugador 2 gana!");
        }
    }
}