using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalera : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Player player;
    private float gravedadAnterior;

    private void Start()
    {
        player = GetComponent<Player>();
        rigidBody = player.GetComponent<Rigidbody2D>();
        gravedadAnterior = rigidBody.gravityScale;
    }

    private void Update()
    {
        if (!player.hayEscalera && player.Escalando)
        {
            rigidBody.gravityScale = gravedadAnterior;
            player.SendMessage("TerminarAnimacionEscalar");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Escalera")
            player.hayEscalera = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Escalera")
            player.hayEscalera = false;
    }
}
