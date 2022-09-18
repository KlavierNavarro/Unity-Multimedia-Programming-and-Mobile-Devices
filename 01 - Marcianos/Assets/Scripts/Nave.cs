using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Nave : MonoBehaviour
{
    [SerializeField] float velocidad = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);
    }
}
