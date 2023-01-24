using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
// Damage for the enemy
public class TempEnemy : MonoBehaviour
{
    readonly float knockback = 170;
    readonly float deathDuration = 1.5f;

    int health = 2;
    Animator animator;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    IEnumerator Death()
    {
        animator.SetBool("Died", true); // Elliot added this
        gameObject.tag = "Untagged"; // it has the tag danger otherwise, but it changes here so it won't make the player take damage on colission
        yield return new WaitForSeconds(deathDuration);
        transform.GetChild(0).gameObject.SetActive(true); // activate the cheese
        transform.DetachChildren(); // unparent the cheese
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Danger" && health > 0)
        {
            animator.SetBool("TookDamage", true); // Elliot added this
            collision.gameObject.SetActive(false); // a square with the tag danger is set active when the player attacks, this row inactivates it
            health--;
            GetComponent<Rigidbody2D>().AddForce(new Vector3(Mathf.Abs(transform.position.x - collision.transform.position.x) / (transform.position.x - collision.transform.position.x), 1, 0) * knockback); // knockback
            animator.SetBool("TookDamage", false); // Elliot added this
            if (health <= 0)
            {
                StartCoroutine(Death());
            }
        }
    }
}