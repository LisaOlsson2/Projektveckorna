using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEnterAreaDetector : MonoBehaviour
{       //Theo
    [field: SerializeField]

    public bool playerInArea { get; private set; }

    public Transform Player { get; private set; }

    [SerializeField]
    private string detectionTag = "Player";

    [SerializeField]
    Patroling patroling;


    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag(detectionTag))     //om triggern kolliderar med objectet med detectionTag så slutar fienden röra på sig
        {
            playerInArea = true;
            Player = collision.gameObject.transform;
            Debug.Log("enter");
            patroling.moving = false;
            Debug.Log("NotMoving");
            animator.SetBool("Detect", true);
            patroling.startRollTimer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))     //om triggern slutar kollidera med objectet med detectionTag så börjar fienden röra på sig igen
        {
            playerInArea = false;
            Player = null;
            Debug.Log("Exit");
            patroling.moving = true;
            Debug.Log("Moving");
            animator.SetBool("Detect", false);

        }
    }
}
