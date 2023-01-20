using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gråsuggspawner : MonoBehaviour
{
    //GJORT AV ELLIOT 
    //Variabler som gör att man ser i Unity och en Array som används i unity
    float timer;
    public Transform[] prefabs;
 
    void Start()
    {
        
    }

    // Update is called once per frame
    //Detta gör att objectet valt i arrayen kommer instantiate den genom random valda kordinater 
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            int rng = Random.Range(0, prefabs.Length);
            Instantiate(prefabs[rng], new Vector3(Random.Range(6, 14), -5, 0), prefabs[rng].transform.rotation);
            timer = 0;
        }
    }
}
