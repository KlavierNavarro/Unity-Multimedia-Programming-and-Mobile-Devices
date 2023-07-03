using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrozoDeTierra : MonoBehaviour
{
    [SerializeField] float Velocidad = 0.3f;
    private bool haciaIzquierda = false;
    private Rigidbody2D rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.velocity = new Vector2((haciaIzquierda ? -1 : 1) * Velocidad, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Suelo")
            haciaIzquierda = !haciaIzquierda;
    }
}
