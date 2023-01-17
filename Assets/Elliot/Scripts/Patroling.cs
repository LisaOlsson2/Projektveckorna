using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroling : MonoBehaviour
{

    public bool moving;

    public float speed;

    public float distance;

    public float rollSpeed;

    private bool movingRight = true;

    public Transform groundDetection;

    public float rollTimer;

    public bool startRollTimer;

    public float stunTimer;

    public bool startStunTimer;

    public GameObject Player;

    public float rollingTimer;

    public bool startRollingTimer;

    bool playerPositionRight;

    Rigidbody2D rb;

    private void Start()
    {
        moving = true;
        rb = GetComponent<Rigidbody2D>();
        rollSpeed = 15;
    }
    private void Update()
    {
        if (moving)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            startRollTimer = false;
            rollTimer = 0;
        }

        if (Player.transform.position.x > transform.position.x)     //om spelarens position är mer än fiendens position så rör sig fienden höger, Theo
        {
            playerPositionRight = true;
        }
        else        //annars så rör den sig vänster, Theo
        {
            playerPositionRight = false;
        }
            
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight)
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
            if (rollTimer >= 5)     //om det har gått 5 sekunder eller mer, Theo
            {
                startRollTimer = false;
                rollTimer = 0;
                startRollingTimer = true;
            }
        }

        if (startRollingTimer)
        {
            rollingTimer += Time.deltaTime;
            if (rollingTimer < 1)
            {
                if (playerPositionRight)        //fienden rör sig åt höger i 1 sekund ifall playerPositionRight är sant, Theo
                {
                    rb.AddForce(new Vector2(rollSpeed*Time.deltaTime, 0), ForceMode2D.Impulse);
                    Debug.Log("rullar höger");
                }
                else        //annars rör fienden sig åt vänster, Theo
                {
                    rb.AddForce(new Vector2(-rollSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
                    Debug.Log("rullar vänster");
                }
            }
            else if (rollingTimer > 1)      //efter 1 sekund så blir fineden "stunned" och stannar, Theo
            {
                startStunTimer = true;
                Debug.Log("stunTimer start");
                moving = false;
                Debug.Log("stunned");
                rollingTimer = 0;
                startRollingTimer = false;
            }
        }

        if (startStunTimer)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= 3)     //fienden står still i 3 sekunder, Theo
            {
                Debug.Log("not stunned");
                moving = true;
                stunTimer = 0;
                startStunTimer = false;
            }
        }
    }
}
    

