using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPlanta : MonoBehaviour
{
    [SerializeField] float Velocidad = 0.5f;
    public Vector2 Direccion;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = Direccion * Velocidad;

        if (!spriteRenderer.isVisible)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            Destroy(gameObject);
    }
}
