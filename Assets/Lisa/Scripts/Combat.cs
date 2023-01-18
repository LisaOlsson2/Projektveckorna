using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class Combat : PlayerBase
{
    Inventory inventory;

    [SerializeField]
    Sprite weapon;

    readonly float attackDelay = 0.25f;
    readonly float attackTime = 0.25f;
    readonly float knockback = 170;

    public override void Start()
    {
        base.Start();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (grounded && Input.GetKeyDown(inventory.use) && !attacking && staminaTimer > 0 && inventory.square.gameObject.activeSelf && inventory.CurrentSprite() == weapon)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        ChangeAnimation("Attack");
        staminaTimer -= 0.7f;
        attacking = true;
        yield return new WaitForSeconds(attackDelay);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        transform.GetChild(0).gameObject.SetActive(false);
        attacking = false;
        SetWalkOrIdleOrSprint();
    }


    void Knockback(float position)
    {
        rb.velocity = Vector3.zero;
        GetComponent<PlayerMovement>().enabled = false;
        rb.AddForce((Mathf.Abs(transform.position.x - position)/(transform.position.x - position) * Vector3.right + Vector3.up) * knockback);
        grounded = false;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Danger")
        {
            valueKeeper.health--;
            inventory.cheese[valueKeeper.health].SetActive(false);
            Knockback(collision.transform.position.x);
            if (valueKeeper.health <= 0)
            {
                /*
                foreach (GameObject cheese in inventory.cheese)
                {
                    cheese.SetActive(true);
                    valueKeeper.health++;
                }
                */
                valueKeeper.dead = true;
            }
        }
    }
}
