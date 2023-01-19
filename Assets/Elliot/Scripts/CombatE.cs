using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class CombatE : BaseMostThings
{
    //Animator animator;
    PolygonCollider2D[] colliders;
    // [0] Idel
    // [1] Roll

    readonly float distance = 1;
    readonly float knockback = 170;
    readonly float deathDuration = 1.5f;

    int health = 2;
    float speed = 0.7f;
    float startPos;
    bool defaultState = true;
    public Animator animator;

    public override void Start()
    {
        //animator = GetComponent<Animator>();
        colliders = GetComponents<PolygonCollider2D>();
        startPos = transform.position.x;
        animator = GetComponentInParent<Animator>();
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

    IEnumerator DeathE()
    {
        gameObject.tag = "Untagged";
        yield return new WaitForSeconds(deathDuration);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.DetachChildren();
        animator.SetBool("Died", true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Danger" && health > 0)
        {
            animator.SetBool("TookDamage", true);
            collision.gameObject.SetActive(false);
            health--;
            GetComponent<Rigidbody2D>().AddForce(new Vector3(Mathf.Abs(transform.position.x - collision.transform.position.x) / (transform.position.x - collision.transform.position.x), 1, 0) * knockback);
            animator.SetBool("TookDamage", false);
            if (health <= 0)
            {
                StartCoroutine(DeathE());
            }
        }
    }
}
