using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class TempEnemy : BaseMostThings
{
    int health = 2;
    bool rolling;

    private void Update()
    {
        if (rolling)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Danger")
        {
            collision.gameObject.SetActive(false);
            health--;
            if (health <= 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.DetachChildren();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            rolling = true;
            collision.transform.position += new Vector3(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y, 0)/2;
        }
    }
}
