using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class Combat : PlayerBase
{
    Inventory inventory;

    [SerializeField]
    Sprite weapon; // stick inventory icon

    CameraShake cameraShake;

    readonly float attackDelay = 0.25f;
    readonly float attackTime = 0.25f;
    readonly float knockback = 170;

    public override void Start()
    {
        base.Start();
        inventory = GetComponent<Inventory>();
        cameraShake = FindObjectOfType<CameraShake>();
    }

    void Update()
    {
        // a buncha conditions that need to be true in order to attack
        if (grounded && Input.GetKeyDown(inventory.use) && !attacking && staminaTimer > 0 && inventory.square.gameObject.activeSelf && inventory.CurrentSprite() == weapon)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        ChangeAnimation("Attack");
        staminaTimer -= 0.7f; // drain some stamina
        attacking = true;
        yield return new WaitForSeconds(attackDelay);
        transform.GetChild(0).gameObject.SetActive(true); // set your dangerous child active
        yield return new WaitForSeconds(attackTime);
        transform.GetChild(0).gameObject.SetActive(false);
        attacking = false;
        SetWalkOrIdleOrSprint();
    }

    void Knockback(float position)
    {
        // the damage animation starts in ondisable in the movement script
        rb.velocity = Vector3.zero;
        GetComponent<PlayerMovement>().enabled = false; // disables the movement script, it becomes enabled again when you hit the ground
        rb.AddForce((Mathf.Abs(transform.position.x - position)/(transform.position.x - position) * Vector3.right + Vector3.up) * knockback); // knockback away from the enemy
        grounded = false;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Danger" && valueKeeper.health > 0)
        {
            cameraShake.StartShake();
            valueKeeper.health--;
            inventory.cheese[valueKeeper.health].SetActive(false);
            Knockback(collision.transform.position.x);
            if (valueKeeper.health <= 0)
            {
                valueKeeper.dead = true;
            }
        }
    }
}
