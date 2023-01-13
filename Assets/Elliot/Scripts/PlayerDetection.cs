using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
   
    public float distance;

    private bool movingRight = true;

    public Transform pDetection;

    BoxCollider2D boxCollider2D;
    Rigidbody2D rb;
    public bool startTimer;
    bool isGrounded;
    public LayerMask mask;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        // transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size - new Vector3(0.1f, 0, 0), 0, -transform.up, 0.1f, mask);
        Debug.DrawRay(transform.position, -transform.up, Color.red, 5f); 
        if (hit.transform != null)
        {
           
        }
        else
        {
       
        }
    }
}
