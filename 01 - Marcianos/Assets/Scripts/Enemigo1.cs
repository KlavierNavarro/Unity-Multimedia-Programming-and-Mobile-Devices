using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemigo1 : MonoBehaviour
{
    [SerializeField] float velocidadX = 2;
    [SerializeField] private float velocidadY = -1.1f;
    [SerializeField] Transform prefabDisparoEnemigo;
    private float velocidadDisparo = -4;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disparar());
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

    IEnumerator Disparar()
    {
        float pausa = UnityEngine.Random.Range(5.0f, 11.0f);
        yield return new WaitForSeconds(pausa);

        Transform disparo = Instantiate(prefabDisparoEnemigo,
            transform.position, Quaternion.identity);

        disparo.gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector3(0, velocidadDisparo, 0);

        StartCoroutine(Disparar());
    }

}
