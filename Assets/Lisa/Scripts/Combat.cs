using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class Combat : PlayerBase
{
    Inventory inventory;

    [SerializeField]
    Sprite weapon;

    readonly float slashCooldown = 0.15f;
    readonly float attackDelay = 0.25f;
    readonly float attackTime = 0.25f;

    bool attacking;

    public override void Start()
    {
        base.Start();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !attacking && staminaTimer > 0 && inventory.square.gameObject.activeSelf && inventory.inventory[(int)(inventory.square.anchoredPosition.y - inventory.startPos.y) / valueKeeper.distanceInventory] == weapon)
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
        SetWalkOrIdleOrSprint();
        yield return new WaitForSeconds(slashCooldown);
        attacking = false;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Danger")
        {
            valueKeeper.health--;
            inventory.cheese[valueKeeper.health].SetActive(false);
            if (valueKeeper.health <= 0)
            {
                // change this later
                foreach (GameObject cheese in inventory.cheese)
                {
                    cheese.SetActive(true);
                    valueKeeper.health++;
                }
            }
        }
    }
}
