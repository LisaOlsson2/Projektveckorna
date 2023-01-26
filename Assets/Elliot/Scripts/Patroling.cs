using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
   public float startPostiton;
    public bool rollingD;


    Rigidbody2D rb;
    public Animator animator;

    private void Start() //Hämtar och startar data här - Elliot
    {
        startPostiton = transform.position.x;
        moving = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() //Den här gör så att gubben rör sig genom "speed" - Elliot
    {
        if (moving)
        {
            rollingD = true; 
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
        //Använder raycast för att kolla om den kollidar med något under sig, om detta händer vänder objectet och raycasten till höger eller vänster - Elliot
     

            if (transform.position.x  >= startPostiton + 8)
            {
                Debug.Log("flip");
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
              
            }
            else if (transform.position.x <= startPostiton - 8)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                
            }
        

        if (startRollTimer)
        {
            animator.SetBool("Detect", true);
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
            animator.SetBool("Roll", true);
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
                animator.SetBool("Roll", false);

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
                animator.SetBool("Detect", false);
                rollingD = true;

            }
        }
    }
  
}
    

