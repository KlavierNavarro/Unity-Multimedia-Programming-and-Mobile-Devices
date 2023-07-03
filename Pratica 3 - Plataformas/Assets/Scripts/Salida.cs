using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Salida : MonoBehaviour
{
    private AudioSource sonido;

    private void Start()
    {
        sonido = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sonido.Play();
            StartCoroutine(Iterador());
        } 
    }

    private IEnumerator Iterador()
    {
        //Mientras el sonido suena no avanza de nivel
        while (sonido.isPlaying)
        {
            yield return null;
        }
        FindObjectOfType<GameController>().SendMessage("AvanzarNivel");
    }
}
