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

        if (Player.transform.position.x > transform.position.x)     //om spelarens position �r mer �n fiendens position s� r�r sig fienden h�ger, Theo
        {
            playerPositionRight = true;
        }
        else        //annars s� r�r den sig v�nster, Theo
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
            if (rollTimer >= 5)     //om det har g�tt 5 sekunder eller mer, Theo
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
                if (playerPositionRight)        //fienden r�r sig �t h�ger i 1 sekund ifall playerPositionRight �r sant, Theo
                {
                    rb.AddForce(new Vector2(rollSpeed*Time.deltaTime, 0), ForceMode2D.Impulse);
                    Debug.Log("rullar h�ger");
                }
                else        //annars r�r fienden sig �t v�nster, Theo
                {
                    rb.AddForce(new Vector2(-rollSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
                    Debug.Log("rullar v�nster");
                }
            }
            else if (rollingTimer > 1)      //efter 1 sekund s� blir fineden "stunned" och stannar, Theo
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
            if (stunTimer >= 3)     //fienden st�r still i 3 sekunder, Theo
            {
                Debug.Log("not stunned");
                moving = true;
                stunTimer = 0;
                startStunTimer = false;
            }
        }
    }
}
    

