using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<HUD>().SendMessage("CambiarGameOver", false);
        FindObjectOfType<HUD>().SendMessage("CambiarTextoFin", false);
    }

    void Update()
    {
        //Pasamos al primer nivel al presionar enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Nivel1");
        }
    }
}
