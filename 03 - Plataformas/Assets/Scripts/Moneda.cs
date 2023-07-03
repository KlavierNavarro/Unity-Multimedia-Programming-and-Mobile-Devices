using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    private AudioSource sonido;

    private void Start()
    {
        sonido = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameController>().SendMessage("AnotarItemRecogido");
        AudioSource.PlayClipAtPoint(sonido.clip, Camera.main.transform.position);
        Destroy(gameObject);
    }
}