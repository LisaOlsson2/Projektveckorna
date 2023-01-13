using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroling : MonoBehaviour
{

    public bool moving;
    public float speed;
    public float distance;

    private bool movingRight = true;

    public Transform groundDetection;

    public float rollTimer;

    public bool startRollTimer;

    public float stunTimer;

    public bool startStunTimer;


    private void Start()
    {
        moving = true;
    }
    private void Update()
    {
        if (moving)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            startRollTimer = false;
            rollTimer = 0;
        }
            
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
           
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                
            }
        }
        if (startRollTimer)
        {
            rollTimer += Time.deltaTime;
            if (rollTimer >= 5)
            {
                Roll();
            }
        }

        void Roll()
        {
            //rulla (add force) (transform.position, mot spelaren)
            startRollTimer = false;
            rollTimer = 0;
            Debug.Log("rullar");
            startStunTimer = true;
            Debug.Log("stunTimer start");
            moving = false;
            Debug.Log("stunned");

        }
        if (startStunTimer)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= 3)
            {
                Debug.Log("not stunned");
                moving = true;
                stunTimer = 0;
                startStunTimer = false;
            }
        }
    }
}
    

