using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecifikSpawner : MonoBehaviour
{
    //Elliot
    [SerializeField]
    private GameObject GrosuggaSpawner;
    float timer;
    float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Den här gör så att unity enemyn spawnar en ny av sig själv på en timer
        timer += Time.deltaTime;
        if (timer > 2)
        {
            Instantiate(GrosuggaSpawner, transform.position, GrosuggaSpawner.transform.rotation);
            timer = 0;
        }

    }
}


   