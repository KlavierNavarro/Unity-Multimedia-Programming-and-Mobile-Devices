using System;
using System.Collections.Generic;
using UnityEngine;

public class Direccion : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool mirandoIzq = false;

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public bool MirarIzquierda
    {
        get { return mirandoIzq; }
        set
        {
            mirandoIzq = value;
            spriteRenderer.flipX = mirandoIzq;
        }
    }

}
