using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEnterAreaDetector : MonoBehaviour
{
    [field: SerializeField]

    public bool playerInArea { get; private set; }

    public Transform Player { get; private set; }

    [SerializeField]
    private string detectionTag = "Player";

    Patroling patroling;


    private void Start()
    {
        GetComponent<Patroling>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            patroling.moving = false;
            playerInArea = true;
            Player = collision.gameObject.transform;
            Debug.Log("enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            patroling.moving = true;
            playerInArea = false;
            Player = null;
            Debug.Log("Exit");
        }
    }
}
