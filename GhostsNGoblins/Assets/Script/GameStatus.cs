 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public int Puntos = 0;
    public int Vidas = 6;
    public int NivelActual = 1;
    public int NivelMasAlto = 3;
    public float TiempoRestante = 120;
    public bool TieneArmadura = true;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;

        if (gameStatusCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ReiniciarDatos()
    {
        Puntos = 0;
        Vidas = 6;
        NivelActual = 1;
        TiempoRestante = 120;
    }

    public static string TiempoRestanteFormateado()
    {
        // Usando el método FromSeconds de la clase TimeSpan damos formato
        // a los segundos que quedan para poder mostrarlos en el HUD.
        System.TimeSpan tiempo = System.TimeSpan.FromSeconds(FindObjectOfType<GameStatus>().TiempoRestante);
        return tiempo.ToString(@"mm\:ss");
    }

    private void Update()
    {
        TiempoRestante -= Time.deltaTime;

        if (TiempoRestante < 0)
            FindObjectOfType<GameController>().SendMessage("TerminarPartida");
    }
}
    
