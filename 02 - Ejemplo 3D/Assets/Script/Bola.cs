using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class Bola : MonoBehaviour
{
    float velocidadAvance = 1000f;
    float velocidadRotac = 200f;
    float xInicial, zInicial, rotacionY;
    int vidas = 3;

    // Start is called before the first frame update
    void Start()
    {
        xInicial = transform.position.x;
        zInicial = transform.position.z;
        rotacionY = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        float avance = Input.GetAxis("Vertical") * velocidadAvance * Time.deltaTime;
        float rotacion = Input.GetAxis("Horizontal") * velocidadRotac * Time.deltaTime;

        transform.Rotate(Vector3.up, rotacion);

        transform.position += transform.forward * Time.deltaTime * avance;

    }

    public void PerderVida()
    {
        UnityEngine.Debug.Log("Una vida menos");
        Recolocar();
        vidas--;

        if (vidas <= 0)
        {
            UnityEngine.Debug.Log("Partida terminada");
            UnityEngine.Application.Quit();
        }
    }

    public void Recolocar()
    {
        transform.position = new Vector3(xInicial, transform.position.y, zInicial);
    }
}