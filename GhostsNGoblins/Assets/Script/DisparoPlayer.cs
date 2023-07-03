using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPlayer : MonoBehaviour
{
    [SerializeField] float Velocidad = 5f;
    [SerializeField] Transform prefabExplosion;
    [SerializeField] Transform prefabDesaparecer;
    private Rigidbody2D rigidBody;
    private Direccion direccion;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        direccion = GetComponent<Direccion>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = new Vector2((direccion.MirarIzquierda ? -1 : 1) * Velocidad, rigidBody.velocity.y);

        // El método isVisible nos indica si el disparo se ve en cámara para
        // destruirlo en caso de que se salga de la pantalla
        if (!spriteRenderer.isVisible)
        {
            Destroy(gameObject);
            FindObjectOfType<Player>().SendMessage("QuitarDisparo");
        }
    }

    // Cambios
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemigo")
        {
            Transform explosion = Instantiate(prefabExplosion, transform.position
                + new Vector3(direccion.MirarIzquierda ? -0.3f : 0.3f, 0, 0), Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
            FindObjectOfType<Player>().SendMessage("QuitarDisparo");
            FindObjectOfType<GameController>().SendMessage("AnotarPuntos", 200);
        }
        // Aquí comprobamos si las lanzas chocan con las tumbas, pero
        // las tumbas ya tienen un tag de suelo para comprobar si el player
        // está saltando o no.
        else if (other.tag == "Suelo")
        {
            FindObjectOfType<Tumba>().SendMessage("ReproducirAudio");
            Transform desaparecer = Instantiate(prefabDesaparecer, other.transform.position +
                new Vector3(direccion.MirarIzquierda ? 0.2f : -0.2f, 0, 0), Quaternion.identity);
            Destroy(gameObject);
            FindObjectOfType<Player>().SendMessage("QuitarDisparo");
        }
        // Cambios
        else if (other.tag == "Ciclope")
        {
            Transform explosion = Instantiate(prefabExplosion, transform.position
                + new Vector3(direccion.MirarIzquierda ? -0.3f : 0.3f, 0, 0), Quaternion.identity);
            Destroy(gameObject);
            FindObjectOfType<Player>().SendMessage("QuitarDisparo");
        }
    }
}
