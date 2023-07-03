using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Player Player;
    private int puntos;
    private int vidas;
    private int nivelActual;
    private bool tieneArmadura;

    private void Start()
    {
        puntos = FindObjectOfType<GameStatus>().Puntos;
        vidas = FindObjectOfType<GameStatus>().Vidas;
        nivelActual = FindObjectOfType<GameStatus>().NivelActual;
        tieneArmadura = FindObjectOfType<GameStatus>().TieneArmadura;
        FindObjectOfType<HUD>().SendMessage("CambiarTextoFin", false);
        FindObjectOfType<HUD>().SendMessage("CambiarGameOver", false);
    }

    public void AnotarPuntos(int numPuntos)
    {
        puntos += numPuntos;
        FindObjectOfType<GameStatus>().Puntos = puntos;
    }

    // Cambios
    public void PerderVida()
    {
        vidas--;
        FindObjectOfType<GameStatus>().Vidas = vidas;
        // Cambios
        FindObjectOfType<Player>().SendMessage("Recolocar");
        // Cambios
        FindObjectOfType<GameStatus>().TieneArmadura = false;

        if (vidas < 1)
        {
            FindObjectOfType<HUD>().SendMessage("CambiarGameOver", true);
            TerminarPartida();
        }
    }

    public void TerminarJuego()
    {
        FindObjectOfType<HUD>().SendMessage("CambiarTextoFin", true);
        TerminarPartida();
    }

    public void TerminarPartida()
    {
        FindObjectOfType<GameStatus>().SendMessage("ReiniciarDatos");
        StartCoroutine(VolverAlMenuPrincipal());
    }

    private IEnumerator VolverAlMenuPrincipal()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.5f); // 3 segundos de tiempo real
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    // Cambios
    public void AvanzarNivel()
    {
        // Cambios
        nivelActual++;
        if (nivelActual > FindObjectOfType<GameStatus>().NivelMasAlto)
            nivelActual = 1;
        FindObjectOfType<GameStatus>().NivelActual = nivelActual;
        SceneManager.LoadScene("Nivel" + nivelActual);
    }

    // Cambios
    public float DistanciaJugadorX(Transform transform)
    {
        return FindObjectOfType<Player>().transform.position.x - transform.position.x;
    }
}
