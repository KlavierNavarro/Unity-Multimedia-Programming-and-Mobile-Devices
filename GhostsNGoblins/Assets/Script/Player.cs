using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform prefabDisparo;
    [SerializeField] public float Velocidad = 1.5f;
    [SerializeField] public float FuerzaSalto = 500;
    [SerializeField] BoxCollider2D colliderPlataforma;
    public EstadoPlayer Estado = EstadoPlayer.Idle;
    public bool hayEscalera;
    public AudioClip AudioSaltar;
    public AudioClip AudioDisparar;
    public AudioClip AudioGolpeado;
    public AudioClip AudioMuerte;
    public AudioClip AudioEnemigoGolpeado;
    public AudioClip AudioJefeMuerto;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D colliderSalto;
    private PolygonCollider2D colliderPlayer;
    private AudioSource audioSource;
    private Direccion direccion;
    private List<Transform> disparos = new List<Transform>();
    private EstadoPlayer estadoAnterior;
    private float velocidadEscalera = 1;
    private float anteriorGravedad;
    private float xInicial;
    private float yInicial;
    private bool tieneArmadura = true;
    private bool saltando = false;
    private bool escalarArriba;

    public bool Disparando
    {
        get
        {
            return Estado == EstadoPlayer.Throw ||
                Estado == EstadoPlayer.CrouchThrow;
        }
    }

    public bool Escalando
    {
        get
        {
            return Estado == EstadoPlayer.Escalera;
        }
    }

    void Awake()
    {
        FindObjectOfType<GameController>().Player = this;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        colliderSalto = GetComponent<BoxCollider2D>();
        colliderPlayer = GetComponent<PolygonCollider2D>();
        direccion = GetComponent<Direccion>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        anteriorGravedad = rigidBody.gravityScale;
        xInicial = transform.position.x;
        yInicial = transform.position.y;
    }

    // No vamos a usar Input.GetAxis("Horizontal") para movernos ya que
    // necesitamos separar el movimiento hacia la izquierda y hacia la
    // derecha para poder evitar cambiar de dirección durante el salto.
    private void MoverDerecha()
    {
        rigidBody.velocity = new Vector2(Velocidad, rigidBody.velocity.y);
        direccion.MirarIzquierda = false;
        Estado = EstadoPlayer.Corre;
    }

    private void MoverIzquierda()
    {
        rigidBody.velocity = new Vector2(-Velocidad, rigidBody.velocity.y);
        direccion.MirarIzquierda = true;
        Estado = EstadoPlayer.Corre;
    }

    private void Parar()
    {
        Estado = EstadoPlayer.Idle;
    }

    private void Saltar()
    {
        // Utilizamos el método AddForce del Rigidbody ya que las físicas
        // de Unity hacen los saltos demasiado distintos a los de la época
        // de los juegos arcade.
        rigidBody.AddForce(new Vector2(0f, FuerzaSalto));
        audioSource.clip = AudioSaltar;
        audioSource.Play();
    }

    private void Escalar()
    {
        estadoAnterior = Estado;
        Estado = EstadoPlayer.Escalera;
        rigidBody.velocity = new Vector2(0,
            escalarArriba ? velocidadEscalera : -velocidadEscalera);
    }

    private void TerminarAnimacionEscalar()
    {
        Estado = estadoAnterior;
    }

    private void Agachar()
    {
        Estado = EstadoPlayer.Crouch;
    }

    private void Disparar()
    {
        if (disparos.Count < 3)
        {
            estadoAnterior = Estado;
            Vector3 offset;

            if (estadoAnterior == EstadoPlayer.Crouch)
            {
                Estado = EstadoPlayer.CrouchThrow;
                offset = new Vector3(0, 0.05f, 0);
            }
            else
            {
                Estado = EstadoPlayer.Throw;
                offset = new Vector3(0, 0.25f, 0);
            }

            Transform disparo = Instantiate(prefabDisparo, transform.position +
                offset, Quaternion.identity);
            Direccion direccion = disparo.GetComponent<Direccion>();
            direccion.MirarIzquierda = this.direccion.MirarIzquierda;
            disparos.Add(disparo);
            audioSource.clip = AudioDisparar;
            audioSource.Play();
        }
    }

    public void QuitarDisparo()
    {
        disparos.RemoveAt(0);
    }

    private void TerminarAnimacionDisparo()
    {
        Estado = estadoAnterior;
    }

    private void SerGolpeado()
    {
        if (tieneArmadura)
        {
            tieneArmadura = false;
            Estado = EstadoPlayer.Lose;
        }
        else
        {
            Estado = EstadoPlayer.Muerte;
            FindObjectOfType<GameController>().SendMessage("PerderVida");
        }
    }

    private void Recolocar()
    {
        transform.position = new Vector3(xInicial, yInicial, 0);
    }

    private void SonidoJefe()
    {
        audioSource.clip = AudioJefeMuerto;
        audioSource.Play();
    }

    // Cambios
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Suelo")
            escalarArriba = true;
        else if (other.tag == "Plataforma")
            escalarArriba = false;

        if (other.tag == "Enemigo")
        {
            audioSource.clip = AudioEnemigoGolpeado;
            audioSource.Play();
            Destroy(other.gameObject);
            SerGolpeado();
        }
        // Cambios
        else if (other.tag == "Ciclope")
        {
            audioSource.clip = AudioEnemigoGolpeado;
            audioSource.Play();
            SerGolpeado();
        }

        if (other.tag == "Finish")
        {
            FindObjectOfType<GameController>().SendMessage("AvanzarNivel");
        }

        if (other.tag == "Agua")
            FindObjectOfType<GameController>().SendMessage("PerderVida");
    }

    private void UpdateComprobarSalto()
    {
        Vector2 posicion = (Vector2)transform.position + colliderSalto.offset;
        Collider2D[] colisiones = Physics2D.OverlapBoxAll(posicion, colliderSalto.size, 0f);
        int contador = 0;

        saltando = true;

        while (colisiones.Length > contador && saltando)
        {
            if (colisiones[contador].tag == "Suelo" ||
                colisiones[contador].tag == "Plataforma")
                saltando = false;
            contador++;
        }
    }

    private void UpdateInput()
    {
        if (Disparando || Escalando)
            return;

        if (!saltando && Input.GetKeyDown(KeyCode.Space))
            Saltar();
        else if (Input.GetKeyDown(KeyCode.X))
            Disparar();
        else if (Input.GetKey(KeyCode.DownArrow) && !hayEscalera && !Escalando)
            Agachar();
        // No dejamos que el jugador cambie de dirección en mitad del
        // salto ya que no se puede hacer en el juego original.
        else if (Input.GetKey(KeyCode.RightArrow) && !saltando)
            MoverDerecha();
        else if (Input.GetKey(KeyCode.LeftArrow) && !saltando)
            MoverIzquierda();
        else if (hayEscalera &&
            (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
            Escalar();
        else if (!saltando)
            Parar();
    }

    private void UpdateAnimator()
    {
        string armadura = "A";
        string desnudo = "D";

        //Con este if damos prioridad a la animación del disparo y a la escalada.
        if (saltando && !Disparando && !Escalando)
        {
            if (rigidBody.velocity.x > 0.1f || rigidBody.velocity.x < -0.1f)
                animator.Play("SaltoRun" + (tieneArmadura ? armadura : desnudo));
            else
                animator.Play("Salto" + (tieneArmadura ? armadura : desnudo));
            return;
        }
        
        switch(Estado)
        {
            case EstadoPlayer.Corre:
                animator.Play("Corre" + (tieneArmadura ? armadura : desnudo));
                break;
            case EstadoPlayer.Crouch:
                animator.Play("Crouch" + (tieneArmadura ? armadura : desnudo));
                break;
            case EstadoPlayer.CrouchThrow:
                animator.Play("CrouchThrow" + (tieneArmadura ? armadura : desnudo));
                break;
            case EstadoPlayer.Escalera:
                animator.Play("Escalera" + (tieneArmadura ? armadura : desnudo));
                break;
            case EstadoPlayer.Idle:
                animator.Play("Idle" + (tieneArmadura ? armadura : desnudo));
                break;
            case EstadoPlayer.Lose:
                animator.Play("Lose");
                break;
            case EstadoPlayer.Muerte:
                animator.Play("Muerte");
                break;
            case EstadoPlayer.Throw:
                animator.Play("Throw" + (tieneArmadura ? armadura : desnudo));
                break;
            case EstadoPlayer.Win:
                animator.Play("Win");
                break;
        }
    }

    private void UpdateFisicas()
    {
        if (colliderPlataforma)
            Physics2D.IgnoreCollision(colliderPlayer, colliderPlataforma, false);
        switch (Estado)
        {
            case EstadoPlayer.Escalera:
                rigidBody.gravityScale = 0;
                if (colliderPlataforma)
                    Physics2D.IgnoreCollision(colliderPlayer, colliderPlataforma, true);
                break;
            default:
                rigidBody.gravityScale = anteriorGravedad;
                break;
        }
    }

    void Update()
    {
        UpdateComprobarSalto();
        UpdateInput();
        UpdateAnimator();
        UpdateFisicas();
    }

    private void LateUpdate()
    {
        spriteRenderer.flipX = direccion.MirarIzquierda;
    }
}
