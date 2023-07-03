using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Nave : MonoBehaviour
{
    [SerializeField] float velocidad = 2;
    [SerializeField] Transform prefabDisparo;
    private float velocidadDisparo = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);
        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<AudioSource>().Play();
            //Instantiate(prefabDisparo, transform.position, Quaternion.identity);
            Transform disparo = Instantiate(prefabDisparo, transform.position, 
                Quaternion.identity);
            disparo.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector3(0, velocidadDisparo, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("Golpeado");
    }
}
