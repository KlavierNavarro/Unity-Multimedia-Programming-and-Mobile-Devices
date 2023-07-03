using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salida : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("Recolocar");
            FindObjectOfType<Enemigo>().SendMessage("IncrementarVelocidad");
        }
    }
}
