using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoZombi : MonoBehaviour
{
    [SerializeField] Transform prefabZombi;
    [SerializeField] float DistanciaMinima = 5;
    [SerializeField] float SegundosAparicion = 10;
    private AudioSource audioSource;
    private float proximaAparicion;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        proximaAparicion = SegundosAparicion;
    }

    // Update is called once per frame
    void Update()
    {
        proximaAparicion -= Time.deltaTime;
        if (proximaAparicion <= 0 && Mathf.Abs(FindObjectOfType<GameController>().DistanciaJugadorX(transform)) < DistanciaMinima)
        {
            GenerarZombi();
            proximaAparicion = SegundosAparicion;
        }
    }

    private void GenerarZombi()
    {
        Transform Zombi = Instantiate(prefabZombi, transform.position, Quaternion.identity);
        Direccion direccion = Zombi.GetComponent<Direccion>();
        direccion.MirarIzquierda = FindObjectOfType<GameController>().DistanciaJugadorX(transform) < 0;
        audioSource.Play();
    }
}
