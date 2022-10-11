using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    [SerializeField] Transform prefabExplosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Finish")
            Destroy(gameObject);
        else if (other.tag == "Player")
        {
            Transform explosion = Instantiate(prefabExplosion,
                other.transform.position, Quaternion.identity);
            explosion.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            Destroy(explosion.gameObject, 1f);
            Destroy(other.gameObject);
        }
    }
}
