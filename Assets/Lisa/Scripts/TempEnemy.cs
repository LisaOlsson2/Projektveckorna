using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class TempEnemy : BaseMostThings
{
    [SerializeField]
    Transform player;

    //Animator animator;
    PolygonCollider2D[] colliders;
    // [0] Idel
    // [1] Roll

    readonly float distance = 1;
    readonly float knockback = 170;

    int health = 2;
    float speed = 0.7f;
    float startPos;
    bool defaultState = true;

    public override void Start()
    {
        //animator = GetComponent<Animator>();
        colliders = GetComponents<PolygonCollider2D>();
        startPos = transform.position.x;
    }

    private void Update()
    {
        if (defaultState)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (Mathf.Abs(startPos - transform.position.x) > distance)
            {
                speed = -speed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Danger")
        {
            collision.gameObject.SetActive(false);
            health--;
            GetComponent<Rigidbody2D>().AddForce(Mathf.Abs(transform.position.x - collision.transform.position.x) / (transform.position.x - collision.transform.position.x) * Vector3.right * knockback);
            if (health <= 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.DetachChildren();
                Destroy(gameObject);
            }
        }
    }
}
