using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class PlayerBase : BaseMostThings
{
    public readonly KeyCode left = KeyCode.A;
    public readonly KeyCode right = KeyCode.D;
    public readonly KeyCode sprint = KeyCode.LeftShift;

    public Rigidbody2D rb;
    public Animator animator;

    // these are static because they have to be the same in both combat and playermovement
    public static float staminaTimer;
    public static bool grounded;
    public static PolygonCollider2D[] colliders;
    public static int currentCollider;
    public static bool attacking;
    public static bool dont; // don't do ground stuff when you change to the jumping animation from the sprinting
    public static bool damageDelay;

    static string currentState;

    public override void Start()
    {
        base.Start();
        currentState = "Idel";
        grounded = false;
        attacking = false;
        dont = false;
        damageDelay = false;
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

    // all player animations except for death go through this function so that other things can happen as well
    public void ChangeAnimation(string animation)
    {
        if ((grounded && !attacking) || animation == "Damage") // so the animation doesn't change to something else while you're in the middle of jumping or attacking or taking damage, unless it gets told to change to the damage animation
        {
            int old = currentCollider; // the previous collider's place in the array of colliders
            animator.SetTrigger(animation);

            if (currentState == "Run" || currentState == "Walk") // if it changes from an animation where the sound loops
            {
                valueKeeper.audioController.Stop(currentState); // stop the sound
            }

            foreach (Sound sound in valueKeeper.audioController.sounds)
            {
                if (sound.name == animation) // if there's a sound for the animation
                {
                    valueKeeper.audioController.Play(animation); // play it
                    break;
                }
            }

            if (currentCollider == 0) // if the currently enabled collider isn't the running collider
            {
                if (animation == "Run") // if the new animation is the running animation
                {
                    currentCollider = 1; // change to the running collider (it gets changed for real further down)
                }
            }
            else if (animation != "Run") // if the current collider is the running collider and the new animation isn't the running animation
            {
                if (animation == "Jump" ||animation == "Damage") // if the new animation is the jumping or damage animation
                {
                    dont = true; // don't make the hit the ground stuff happen
                }
                currentCollider = 0; // change to the usual collider
            }

            if (old != currentCollider) // if the collider changes
            {
                colliders[old].enabled = false; // disable the old
                colliders[currentCollider].enabled = true; // enable the new
            }

            currentState = animation; // store the name of the current animation for when it changes next time
        }
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
