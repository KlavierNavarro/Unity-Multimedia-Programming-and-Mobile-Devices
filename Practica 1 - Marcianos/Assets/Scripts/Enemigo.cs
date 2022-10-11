using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float velocidadY = -1.5f;
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
        switch (transform.position.y)
        {
            case > 3.4f:
                velocidadY = -velocidadY;
                transform.Translate(-0.15f, -0.1f, 0);
                break;
            case < -3.4f:
                velocidadY = -velocidadY;
                transform.Translate(-0.15f, 0.1f, 0);
                break;
            default:
                transform.Translate(0, velocidadY * Time.deltaTime, 0);
                break;
        }
    }

    IEnumerator Disparar()
    {
        float pausa = Random.Range(5.0f, 11.0f);
        yield return new WaitForSeconds(pausa);

        Transform disparo = Instantiate(prefabDisparoEnemigo,
            transform.position, Quaternion.identity);
        disparo.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector3(velocidadDisparo, 0, 0);
        StartCoroutine(Disparar());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Finish")
            Destroy(gameObject);
        else if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
