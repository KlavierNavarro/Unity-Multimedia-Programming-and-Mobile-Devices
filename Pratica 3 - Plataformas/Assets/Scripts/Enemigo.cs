using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float velocidad = 1;
    private Vector3 siguientePosicion;
    private byte numeroSiguientePosicion = 0;
    private float distanciaCambio = 0.2f;

    private void Start()
    {
        siguientePosicion = wayPoints[0].position;
        
        if (FindObjectOfType<GameStatus>().nivelActual > 1)
            AumentarVelocidad();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            siguientePosicion,
            velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, siguientePosicion) < distanciaCambio)
        {
            numeroSiguientePosicion++;
            if (numeroSiguientePosicion >= wayPoints.Count)
                numeroSiguientePosicion = 0;
            siguientePosicion = wayPoints[numeroSiguientePosicion].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        FindObjectOfType<GameController>().SendMessage("PerderVida");
    }

    public void AumentarVelocidad()
    {
        velocidad += 0.5f;
    }
}
