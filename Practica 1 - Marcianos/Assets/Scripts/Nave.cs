using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Nave : MonoBehaviour
{
    [SerializeField] float velocidad = 10;
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
        float vertical = Input.GetAxis("Vertical");
        
        switch (transform.position.x)
        {
            case >= 4.5f:
                transform.Translate(-0.01f, 0, 0);
                break;
            case <= -4.5f:
                transform.Translate(+0.01f, 0, 0);
                break;
            default:
                transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);
                break;
        }
        switch (transform.position.y)
        {
            case >= 3.4f:
                transform.Translate(0, -0.01f, 0);
                break;
            case <= -3.4f:
                transform.Translate(0, +0.01f, 0);
                break;
            default:
                transform.Translate(0, vertical * velocidad * Time.deltaTime, 0);
                break;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<AudioSource>().Play();
            Transform disparo = Instantiate(prefabDisparo, transform.position,
                Quaternion.identity);
            disparo.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector3(velocidadDisparo, 0, 0);
        }
    }
}
