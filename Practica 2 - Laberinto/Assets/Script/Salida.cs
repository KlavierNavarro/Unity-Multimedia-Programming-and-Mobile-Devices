using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Salida : MonoBehaviour
{
    public TMP_Text TextoFin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "Nivel1")
            {
                SceneManager.LoadScene("Nivel2");
                FindObjectOfType<Enemigo>().SendMessage("IncrementarVelocidad");
            }
            else if (SceneManager.GetActiveScene().name == "Nivel2")
            {
                TextoFin.text = "Fin";
                StartCoroutine(Fin());
            }
        }
    }

    IEnumerator Fin()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Inicio");
    }
}