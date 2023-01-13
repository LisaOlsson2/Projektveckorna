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

    private void Start()
    {
        moving = true;
    }
    private void Update()
    {
        if(moving == true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
            
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                print("moving right");
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                print("moving left");
            }
        }
    }
}
    

