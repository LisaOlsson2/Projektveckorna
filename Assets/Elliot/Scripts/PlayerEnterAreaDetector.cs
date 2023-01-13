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

    [SerializeField]
    Patroling patroling;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            playerInArea = true;
            Player = collision.gameObject.transform;
            Debug.Log("enter");
            patroling.moving = false;
            Debug.Log("NotMoving");
            patroling.startRollTimer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            playerInArea = false;
            Player = null;
            Debug.Log("Exit");
            patroling.moving = true;
            Debug.Log("Moving");
        }
    }
}
