using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grosugga : MonoBehaviour
{
    public bool startTimer;
    bool isGrounded;
    public LayerMask mask;

    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //referens till spelarens rigidbody
        boxCollider2D = transform.GetComponent<BoxCollider2D>(); //referens till spelarens boxcollider2D
        sprite = GetComponent<SpriteRenderer>(); //referens till spelarens spriterenderer
    }

    // Update is called once per frame
    void Update()
    {
        //Om spelaren ror sig mot ett speciellt holl kommer "spriten/gubben" att vanda sin texture mot "ratt holl"
        float horiz = Input.GetAxis("Horizontal");
        if (horiz < 0)
        {
            sprite.flipX = true;
        }
        else if (horiz > 0)
        {
            sprite.flipX = false;
        }


        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size - new Vector3(0.1f, 0, 0), 0, -transform.up, 0.1f, mask); 
        //skapar en boxcollider som ar lite under spelaren och kan bara traffa plattformar

        if (hit.transform != null) //om boxcollidern traffar nagot so startas timern och isGrounded blir true
        {
            startTimer = true;
            isGrounded = true;
     
        }
        else //om den inte traffar nagot so ar startTimer false och isGrounded true
        {
            startTimer = false;
            isGrounded = false;
      
        }
    }
}
