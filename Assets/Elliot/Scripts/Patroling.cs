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

    public GameObject Player;
    public bool flip;
    public float rollSpeed;


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
        Vector3 scale = transform.localScale;
        if (Player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            transform.Translate(x: rollSpeed * Time.deltaTime, y: 0, z: 0);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            transform.Translate(x: rollSpeed * Time.deltaTime * -1, y: 0, z: 0);
        }
            transform.localScale = scale;
            
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
    

