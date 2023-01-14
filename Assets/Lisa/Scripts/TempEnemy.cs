using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class TempEnemy : BaseMostThings
{
    int health = 2;

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
}
