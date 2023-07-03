using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI TextoFin;
    [SerializeField] public TextMeshProUGUI GameOver;
    [SerializeField] public TextMeshProUGUI Puntuacion;
    [SerializeField] public TextMeshProUGUI Tiempo;
    [SerializeField] public TextMeshProUGUI MaxPuntuacion;
    [SerializeField] public Image Vida1;
    [SerializeField] public Image Vida2;
    [SerializeField] public Image Vida3;
    [SerializeField] public Image Vida4;
    [SerializeField] public Image Vida5;
    [SerializeField] public Image Vida6;
    private int topScore = 0;

    public void CambiarTextoFin(bool activado)
    {
        if (activado)
            TextoFin.enabled = true;
        else
            TextoFin.enabled = false;
    }

    public void CambiarGameOver(bool activado)
    {
        if (activado)
            GameOver.enabled = true;
        else
            GameOver.enabled = false;
    }

    private void Update()
    {
        Puntuacion.text = FindObjectOfType<GameStatus>().Puntos.ToString();
        Tiempo.text = GameStatus.TiempoRestanteFormateado();

        Vida1.enabled = FindObjectOfType<GameStatus>().Vidas >= 6;
        Vida2.enabled = FindObjectOfType<GameStatus>().Vidas >= 5;
        Vida3.enabled = FindObjectOfType<GameStatus>().Vidas >= 4;
        Vida4.enabled = FindObjectOfType<GameStatus>().Vidas >= 3;
        Vida5.enabled = FindObjectOfType<GameStatus>().Vidas >= 2;
        Vida6.enabled = FindObjectOfType<GameStatus>().Vidas >= 1;

        if (FindObjectOfType<GameStatus>().Puntos > topScore)
        {
            topScore = FindObjectOfType<GameStatus>().Puntos;
            MaxPuntuacion.text = topScore.ToString();
        }
    }
}
