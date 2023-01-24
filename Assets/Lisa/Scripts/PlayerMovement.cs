using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa, fix the water
public class PlayerMovement : PlayerBase
{
    readonly KeyCode jump = KeyCode.W;
    readonly KeyCode dash = KeyCode.P; // the dash used to be on space, but there wasn't an animation for it right before the vernissage, so I put it on P and made it so that jump was on both space and W
    readonly KeyCode jump2 = KeyCode.Space;

    readonly float dashDuration = 0.3f;
    readonly float dashForce = 500;
    readonly float jumpForce = 200;
    readonly float baseSpeed = 2; // this gets multiplied with the speedmultiplier
    readonly float staminaFull = 4;
    readonly int sprintSpeed = 2; // the speedmultiplier gets multiplied with this when you start sprinting
    readonly int tiredSpeed = 2; // added because mickael didn't want the player to be slow when it's tired

    readonly float rightWorldBorder = 18.63f * 3 + 9.315f;
    readonly float leftWorldBorder = -9.315f;

    [SerializeField]
    Slider slider; // indicator for the stamina

    Image staminaImageToChangeColor;
    ChangeInventorySprite water;

    int speedMultiplier = 2;

    public override void Start()
    {
        base.Start();
        water = GetComponent<ChangeInventorySprite>();

        staminaImageToChangeColor = slider.fillRect.GetComponent<Image>();
        slider.maxValue = staminaFull;
        staminaTimer = staminaFull; // start with full stamina

        colliders = GetComponents<PolygonCollider2D>();
        // [0] idle walk jump attack eat damage
        // [1] sprint
    }
    private void OnDisable() // this script gets disabled through the combat script when you take damage
    {
        if (animator != null && valueKeeper.audioController != null)
        {
            if (staminaTimer > 0 && Input.GetKey(sprint) && Mathf.Abs(speedMultiplier) > sprintSpeed) // if you're sprinting
            {
                speedMultiplier /= sprintSpeed; // stop sprinting
            }

            ChangeAnimation("Damage");
        }

    }
    private void OnEnable() // it gets enabled again when the player hits the ground
    {
        if (animator != null)
        {
            if (staminaTimer > 0 && Input.GetKey(sprint)) // if you're pressing the sprint key and have enough stamina
            {
                speedMultiplier *= sprintSpeed; // start sprinting
            }

            if (Input.GetKey(left) && !Input.GetKey(right))
            {
                StartLeftOrRight("left");
            }
            if (Input.GetKey(right) && !Input.GetKey(left))
            {
                StartLeftOrRight("right");
            }

            SetWalkOrIdleOrSprint();
        }
    }

    void Update()
    {
        slider.value = Mathf.Abs(staminaTimer); // makes the slider show the stamina

        if (staminaTimer > 0) // if you have stamina
        {
            if (Input.GetKey(sprint) && ((Input.GetKey(right) && !Input.GetKey(left)) || (Input.GetKey(left) && !Input.GetKey(right)))) // if you're sprinting either left or right
            {
                staminaTimer -= Time.deltaTime; // drain the stamina
                if (staminaTimer <= 0) // if your stamina runs out
                {
                    StartFatigue(); // get tired
                }
            }
            else if (staminaTimer < staminaFull) // if you aren't sprinting to either the left or right and your stamina isn't full, but still above 0
            {
                staminaTimer += Time.deltaTime; // refill the stamina
            }

            if (Input.GetKeyDown(sprint))
            {
                speedMultiplier *= sprintSpeed;
                if ((Input.GetKey(left) && !Input.GetKey(right)) || (Input.GetKey(right) && !Input.GetKey(left)))
                {
                    ChangeAnimation("Run");
                }
            }
            if (Input.GetKeyUp(sprint) && Mathf.Abs(speedMultiplier) > sprintSpeed) // if you're sprinting and you stop
            {
                SetWalkOrIdleOrSprint();
                speedMultiplier /= sprintSpeed;
            }
        }

        if ((Input.GetKeyDown(left) && !Input.GetKey(right)) || (Input.GetKeyUp(right) && Input.GetKey(left)))
        {
            StartLeftOrRight("left");
        }
        if ((Input.GetKeyDown(right) && !Input.GetKey(left))|| (Input.GetKeyUp(left) && Input.GetKey(right)))
        {
            StartLeftOrRight("right");
        }

        if ((Input.GetKeyUp(right) && !Input.GetKey(left)) || (Input.GetKeyUp(left) && !Input.GetKey(right)) || (Input.GetKeyDown(right) && Input.GetKey(left)) || (Input.GetKeyDown(left) && Input.GetKey(right))) // if you press down right or left while holding the other or stop while not holding the other
        {
            ChangeAnimation("Idel");
        }

        if ((Input.GetKeyDown(jump) || Input.GetKeyDown(jump2)) && grounded)
        {
            ChangeAnimation("Jump");
            rb.AddForce(transform.up * jumpForce);

            grounded = false;
        }

        if ((transform.position.x > leftWorldBorder && Input.GetKey(left) && !Input.GetKey(right)) || (transform.position.x < rightWorldBorder && Input.GetKey(right) && !Input.GetKey(left))) // if you're inside the left world border and move left or inside the right and move right
        {
            transform.position += new Vector3(baseSpeed * speedMultiplier, 0, 0) * Time.deltaTime;

            if (Input.GetKeyDown(dash) && staminaTimer > 0)
            {
                StartCoroutine(Dash(speedMultiplier));
            }
        }
        else // so you can't dash out of the borders
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    IEnumerator Dash(int direction)
    {
        staminaTimer -= 1.5f; // drain a bit of stamina

        if (staminaTimer < 0)
        {
            StartFatigue();
        }

        rb.gravityScale = 0; // no gravity, so that you stay at the same height until the dash stops
        rb.velocity = Vector3.zero; // incase you're jumping at the same time or already in a previous dash
        rb.AddForce(new Vector3(dashForce * (Mathf.Abs(direction) / direction), 0, 0)); // divide the absolute value of the direction by the direcion to get 1 or -1
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = 1; // gravity again!
        rb.velocity = Vector3.zero; // no velocity, the dash is over
    }

    // i gotta change this
    public void StopFatigue()
    {
        staminaTimer = staminaFull;
        staminaImageToChangeColor.color = Color.green;
        speedMultiplier *= 2;
    }

    // 
    void StartFatigue()
    {
        SetWalkOrIdleOrSprint();
        staminaImageToChangeColor.color = Color.red;
        speedMultiplier = (Mathf.Abs(speedMultiplier) / speedMultiplier) * tiredSpeed;
        staminaTimer = -staminaFull;
        print(water);
        water.interactable = true; // you can now drink water, but i'll probably remove it
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (dont) // this bool exists for when you jump while you're sprinting, because the collider is switched then
        {
            dont = false;
        }
        else if (collision.gameObject.tag == "Ground" && !grounded)
        {
            grounded = true;
            valueKeeper.audioController.Play("Landing");

            if (this.enabled) // if you hit the ground after jumping
            {
                SetWalkOrIdleOrSprint();
            }
            else if (rb != null) // if you hit the ground after taking damage
            {
                rb.velocity = Vector3.zero;
                if (valueKeeper.dead)
                {
                    StartCoroutine(valueKeeper.Death()); // die
                }
                else
                {
                    this.enabled = true;
                }
            }
        }
    }

    void StartLeftOrRight(string direction)
    {
        animator.ResetTrigger("Idel"); // the jumping animation to the idel animation has an exit time, and if you start walking after the trigger has been set, but before the idel animation has started playing, you need to reset it so that it doesn't start playing the idel animation while you're walking

        if ((!Input.GetKey(sprint) || staminaTimer < 0))
        {
            ChangeAnimation("Walk");
        }
        else if (Input.GetKey(sprint) && staminaTimer > 0)
        {
            ChangeAnimation("Run");
        }

        if (direction == "left")
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0); // flips
            speedMultiplier = -Mathf.Abs(speedMultiplier); // speed multiplier becomes negative, even if it already was
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0); // flips
            speedMultiplier = Mathf.Abs(speedMultiplier); // positive
        }
    }

}