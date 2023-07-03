using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuervo : MonoBehaviour
{
    [SerializeField] float Velocidad = 1.5f;
    [SerializeField] float DistanciaMinima = 3;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private Direccion direccion;
    private bool volando;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direccion = GetComponent<Direccion>();
    }

    void Update()
    {
        if (volando)
        {
            animator.Play("CuervoVolando");
            // Utilizamos una ecuación calculando el seno del tiempo que transcurre
            // en el juego, lo multiplicamos por 2 y le damos una amplitud de 1.6
            // para que los cuervos no vuelen en línea recta sino que puedan ir
            // subiendo y bajando.
            rigidBody.velocity = new Vector2((direccion.MirarIzquierda ? -1 : 1) * Velocidad,
                Mathf.Sin(Time.time * 2f) * 1.6f);
            if (!spriteRenderer.isVisible)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            animator.Play("CuervoQuieto");
            rigidBody.velocity = Vector2.zero;
            direccion.MirarIzquierda = FindObjectOfType<GameController>().DistanciaJugadorX(transform) < 0;
            volando = Mathf.Abs(FindObjectOfType<GameController>().DistanciaJugadorX(transform)) < DistanciaMinima;
        }
    }

    private void LateUpdate()
    {
        spriteRenderer.flipX = !direccion.MirarIzquierda;
    }
}
