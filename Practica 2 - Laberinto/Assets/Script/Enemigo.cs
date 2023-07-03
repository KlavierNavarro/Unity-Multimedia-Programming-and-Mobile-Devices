using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    Vector3 siguientePosicion;
    float velocidad;
    byte numeroSiguientePosicion = 0;
    float distanciaCambio = 0.2f;

    private void Start()
    {
        velocidad = 2;
        if (SceneManager.GetActiveScene().name == "Nivel2")
            velocidad += 2f;
        siguientePosicion = wayPoints[0].position;
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
            if (numeroSiguientePosicion >= wayPoints.Length)
                numeroSiguientePosicion = 0;
            siguientePosicion = wayPoints[numeroSiguientePosicion].position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.SendMessage("PerderVida");
    }

    public void IncrementarVelocidad()
    {
        velocidad += 2f;
    }
}