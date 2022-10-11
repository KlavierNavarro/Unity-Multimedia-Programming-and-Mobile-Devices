using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class Bola : MonoBehaviour
{
    float velocidadAvance = 600f;
    float velocidadRotac = 200f;
    float xInicial, zInicial;
    Quaternion rotacionInicial;
    int vidas = 3;
    public TMP_Text TextoIntentos;
    public TMP_Text GameOver;
    public TMP_Text TextoNivel;

    // Start is called before the first frame update
    void Start()
    {
        xInicial = transform.position.x;
        zInicial = transform.position.z;
        rotacionInicial = transform.rotation;
        TextoIntentos.text = "Intentos: " + vidas;
    }

    // Update is called once per frame
    void Update()
    {
        if (vidas > 0)
        {
            float avance = Input.GetAxis("Vertical") * velocidadAvance * Time.deltaTime;
            float rotacion = Input.GetAxis("Horizontal") * velocidadRotac * Time.deltaTime;

            transform.Rotate(Vector3.up, rotacion);

            transform.position += transform.forward * Time.deltaTime * avance;
        }

        if (SceneManager.GetActiveScene().name == "Nivel1")
        {
            TextoNivel.text = "Nivel 1";
        }
        else if (SceneManager.GetActiveScene().name == "Nivel2")
        {
            TextoNivel.text = "Nivel 2";
        }
    }

    public void PerderVida()
    {
        Recolocar();
        vidas--;
        TextoIntentos.text = "Intentos: " + vidas;

        if (vidas <= 0)
        {
            GameOver.text = "Game Over";
            StartCoroutine(Cerrar());
        }
    }

    IEnumerator Cerrar()
    {
        yield return new WaitForSeconds(3);
        UnityEngine.Application.Quit();
    }

    public void Recolocar()
    {
        transform.position = new Vector3(xInicial, transform.position.y, zInicial);
        transform.rotation = rotacionInicial;
    }
}