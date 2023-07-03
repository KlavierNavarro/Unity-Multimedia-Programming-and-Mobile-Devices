using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ciclope : MonoBehaviour
{
    [SerializeField] public int DisparosParaMorir = 5;
    [SerializeField] float Velocidad = 0.6f;
    [SerializeField] float FuerzaSalto = 700;
    public EstadoCiclope Estado = EstadoCiclope.Quieto;
    public static AudioSource audioSource;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private Direccion direccion;
    private float yInicial;
    public float tiempoEnEstado;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direccion = GetComponent<Direccion>();
    }

    // Cambios
    private void Start()
    {
        yInicial = transform.position.y;
        DisparosParaMorir = 15;
        // Cambios
        tiempoEnEstado = 0;
    }

    private void MirarAlJugador()
    {
        direccion.MirarIzquierda = FindObjectOfType<GameController>().DistanciaJugadorX(transform) < 0;
    }

    private void Andar()
    {
        tiempoEnEstado = 0;
        Estado = EstadoCiclope.Andando;
    }

    private void Parar()
    {
        tiempoEnEstado = 0;
        Estado = EstadoCiclope.Quieto;
    }

    private void Saltar()
    {
        tiempoEnEstado = 0;
        Estado = EstadoCiclope.Saltando;
        rigidBody.AddForce(new Vector2(0, FuerzaSalto));
    }

    private void UpdateEstado()
    {
        System.Random random = new System.Random();
        tiempoEnEstado += Time.deltaTime;
        int numero = random.Next(2);

        if (tiempoEnEstado > 2)
        {
            switch (Estado)
            {
                case EstadoCiclope.Andando:
                    if (numero == 0)
                        Parar();
                    else
                        Saltar();
                    break;
                case EstadoCiclope.Quieto:
                    if (numero == 0)
                        Andar();
                    else
                        Saltar();
                    break;
                case EstadoCiclope.Saltando:
                    if (transform.position.y < yInicial + 0.1)
                        Parar();
                    break;
            }
        }
    }

    private void UpdateAnimator()
    {
        switch (Estado)
        {
            case EstadoCiclope.Andando:
                animator.Play("CiclopeAndando");
                break;
            case EstadoCiclope.Quieto:
                animator.Play("CiclopeQuieto");
                break;
            case EstadoCiclope.Saltando:
                animator.Play("CiclopeSaltando");
                break;
        }
    }

    private void UpdateVelocidad()
    {
        switch (Estado)
        {
            case EstadoCiclope.Andando:
                rigidBody.velocity = new Vector2((direccion.MirarIzquierda ? -1 : 1) * Velocidad, 0);
                break;
            case EstadoCiclope.Quieto:
                rigidBody.velocity = Vector2.zero;
                break;
            case EstadoCiclope.Saltando:
                rigidBody.velocity = new Vector2((direccion.MirarIzquierda ? -1 : 1) * Velocidad, rigidBody.velocity.y);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DisparoPlayer")
        {
            if (DisparosParaMorir > 1)
            {
                DisparosParaMorir--;
                audioSource.Play();
            }
            else if (DisparosParaMorir <= 1)
            {
                FindObjectOfType<Player>().SendMessage("SonidoJefe");
                FindObjectOfType<GameController>().SendMessage("AnotarPuntos", 1000);
                Destroy(gameObject);
                FindObjectOfType<GameController>().SendMessage("TerminarJuego");
            }
        }
    }

    private void Update()
    {
        // Con esto evitamos que haga los update mientras no sea visible.
        if (spriteRenderer.isVisible)
        {
            MirarAlJugador();
            UpdateEstado();
            UpdateAnimator();
            UpdateVelocidad();
        }
    }

    private void LateUpdate()
    {
        spriteRenderer.flipX = !direccion.MirarIzquierda;
    }
}
