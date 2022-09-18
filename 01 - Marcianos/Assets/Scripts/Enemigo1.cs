using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1 : MonoBehaviour
{
    [SerializeField] float velocidadX = 2;
    [SerializeField] private float velocidadY = -1.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocidadX * Time.deltaTime,
            velocidadY * Time.deltaTime, 0);
        if ((transform.position.x < -4.5) || (transform.position.x > 4.5))
            velocidadX = -velocidadX;

        if ((transform.position.y < -2.7) || (transform.position.y > 2.7))
            velocidadY = -velocidadY;
    }
}
