using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class PlayerBase : BaseMostThings
{
    public readonly KeyCode left = KeyCode.A;
    public readonly KeyCode right = KeyCode.D;
    public readonly KeyCode sprint = KeyCode.LeftShift;

    public Rigidbody2D rb;
    public Animator animator;

    public static float staminaTimer;
    public static bool grounded;
    public static PolygonCollider2D[] colliders;
    public static int currentCollider;
    public static bool attacking;
    public static bool dont;

    public override void Start()
    {
        base.Start();
        grounded = false;
        attacking = false;
        dont = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void SetWalkOrIdleOrSprint()
    {
        if ((Input.GetKey(left) && !Input.GetKey(right)) || (Input.GetKey(right) && !Input.GetKey(left)))
        {
            if (Input.GetKey(sprint) && staminaTimer > 0)
            {
                ChangeAnimation("Run");
            }
            else
            {
                ChangeAnimation("Walk");
            }
        }
        else
        {
            ChangeAnimation("Idel");
        }
    }

    public void ChangeAnimation(string animation)
    {
        if ((grounded && !attacking) || animation == "Damage")
        {
            int old = currentCollider;
            animator.SetTrigger(animation);

            if (currentCollider == 0)
            {
                if (animation == "Run")
                {
                    currentCollider = 1;
                }
            }
            else if (animation != "Run")
            {
                if (animation == "Jump")
                {
                    dont = true;
                }
                currentCollider = 0;
            }

            if (old != currentCollider)
            {
                colliders[old].enabled = false;
                colliders[currentCollider].enabled = true;
            }
        }
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
