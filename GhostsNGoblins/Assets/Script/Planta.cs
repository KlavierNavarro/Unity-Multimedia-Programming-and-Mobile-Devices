using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planta : MonoBehaviour
{
    [SerializeField] float TiempoEntreDisparos = 5;
    [SerializeField] GameObject prefabDisparo;
    private float proximoDisparo;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        proximoDisparo = TiempoEntreDisparos;
    }

    private void Disparar()
    {
        animator.Play("PlantaDisparando");
        // He tenido que crear los disparos como GameObject en vez de
        // Transform porque al intentar instanciarlos me daba un error
        // que solo aparece dos veces en Google.
        GameObject disparo = Instantiate(prefabDisparo, transform.position +
            new Vector3(0, 0.2f, 0), Quaternion.identity);
        DisparoPlanta disparoPlanta = disparo.GetComponent<DisparoPlanta>();
        // Creamos un vector para dar dirección al disparo y lo normalizamos
        // para que sea un vector unitario, sin magnitud.
        disparoPlanta.Direccion = (FindObjectOfType<GameController>().Player.transform.position +
            new Vector3(0, 0.2f, 0) -transform.position);
    }

    private void TerminarAnimacionDisparo()
    {
        animator.Play("PlantaQuieta");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            Destroy(gameObject);
    }

    void Update()
    {
        if (spriteRenderer.isVisible)
        {
            proximoDisparo -= Time.deltaTime;

            if (proximoDisparo <= 0)
            {
                Disparar();
                proximoDisparo = TiempoEntreDisparos;
            }
        }
    }
}
