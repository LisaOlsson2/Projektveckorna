using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class TempEnemy : BaseMostThings
{
    readonly float knockback = 170;
    readonly float deathDuration = 1.5f;

    int health = 2;
    Animator animator;
    public override void Start()
    {
        base.Start();
        animator = GetComponentInParent<Animator>();
    }

    IEnumerator Death()
    {
        animator.SetBool("Died", true);
        gameObject.tag = "Untagged";
        yield return new WaitForSeconds(deathDuration);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.DetachChildren();
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
                StartCoroutine(Death());
            }
        }
    }
}