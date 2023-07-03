using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LanzarJuego()
    {
        SceneManager.LoadScene("Nivel1");
    }
}
