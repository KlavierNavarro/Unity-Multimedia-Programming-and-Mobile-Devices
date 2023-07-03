using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi : MonoBehaviour
{
    [SerializeField] float Velocidad = 0.5f;
    [SerializeField] float TiempoTotalVida = 6;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private Direccion direccion;
    private EstadoZombi Estado = EstadoZombi.Entrando;
    private float tiempoLlevaVida;
    private float gravedad;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direccion = GetComponent<Direccion>();
    }

    private void Start()
    {
        // Deshabilitamos el collider para que no pueda matarnos mientras esté
        // entrando a la escena, más fiel al juego de arcade. Lo habilitaremos
        // en la función usada para terminar la animación de entrada a escena.
        boxCollider.enabled = false;
        tiempoLlevaVida = 0;
        gravedad = rigidBody.gravityScale;
    }

    void Update()
    {
        tiempoLlevaVida += Time.deltaTime;
        if (tiempoLlevaVida > TiempoTotalVida)
        {
            Estado = EstadoZombi.Saliendo;
        }

        if (!spriteRenderer.isVisible)
        {
            Destroy(gameObject);
        }

        switch (Estado)
        {
            case EstadoZombi.Andando:
                animator.Play("ZombiAnda");
                rigidBody.velocity = new Vector2((direccion.MirarIzquierda ? -1 : 1) * Velocidad, rigidBody.velocity.y);
                rigidBody.gravityScale = gravedad;
                break;
            case EstadoZombi.Entrando:
                animator.Play("ZombiEntra");
                rigidBody.velocity = new Vector2(0, 0);
                rigidBody.gravityScale = 0;
                break;
            case EstadoZombi.Saliendo:
                animator.Play("ZombiSale");
                rigidBody.velocity = new Vector2(0, 0);
                rigidBody.gravityScale = 0;
                break;
        }
    }

    public void TerminarZombiEntra()
    {
        Estado = EstadoZombi.Andando;
        boxCollider.enabled = true;
    }

    private void LateUpdate()
    {
        spriteRenderer.flipX = !direccion.MirarIzquierda;
    }
}
