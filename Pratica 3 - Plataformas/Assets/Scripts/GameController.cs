using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int puntos;
    private int vidas;
    private int itemsRestantes;
    private int nivelActual;
    [SerializeField] public TextMeshProUGUI textoGameOver;
    [SerializeField] public TextMeshProUGUI textoFin;
    [SerializeField] public TextMeshProUGUI textoTotalPuntos;
    [SerializeField] public TextMeshProUGUI textoPuntos;
    [SerializeField] public TextMeshProUGUI textoVidas;

    void Start()
    {
        itemsRestantes = FindObjectsOfType<Moneda>().Length;
        puntos = FindObjectOfType<GameStatus>().puntos;
        vidas = FindObjectOfType<GameStatus>().vidas;
        nivelActual = FindObjectOfType<GameStatus>().nivelActual;

        textoGameOver.enabled = false;
        textoFin.enabled = false;
        textoTotalPuntos.enabled = false;
        textoPuntos.text = "Puntos: " + puntos;
        textoVidas.text = "Vidas: " + vidas;
    }

    public void AnotarItemRecogido()
    {
        puntos += 10;
        FindObjectOfType<GameStatus>().puntos = puntos;
        textoPuntos.text = "Puntos: " + puntos;
        Debug.Log("Puntos: " + puntos);

        itemsRestantes--;
        Debug.Log("Items restantes: " + itemsRestantes);
        if (itemsRestantes <= 0 && nivelActual == 3)
        {
            TerminarPartida();
        }
    }

    public void PerderVida()
    {
        vidas--;
        FindObjectOfType<GameStatus>().vidas = vidas;
        FindObjectOfType<Player>().SendMessage("Recolocar");
        Debug.Log("Una vida menos. Quedan: " + vidas);
        textoVidas.text = "Vidas: " + vidas;
        if (vidas <= 0)
        {
            TerminarPartida();
        }
    }

    private void TerminarPartida()
    {
        if (vidas > 0)
        {
            textoFin.enabled = true;
            textoTotalPuntos.enabled = true;
            textoTotalPuntos.text += puntos;
        }
        else
        {
            textoGameOver.enabled = true;
        }
        Debug.Log("Partida terminada");
        FindObjectOfType<GameStatus>().ReiniciarDatos();
        StartCoroutine(VolverAlMenuPrincipal());
    }

    private IEnumerator VolverAlMenuPrincipal()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    
    public void AvanzarNivel()
    {
        nivelActual = FindObjectOfType<GameStatus>().nivelActual;
        nivelActual++;
        FindObjectOfType<GameStatus>().nivelActual = nivelActual;
        SceneManager.LoadScene("Nivel" + nivelActual);
        FindObjectOfType<Enemigo>().AumentarVelocidad();
    }
}