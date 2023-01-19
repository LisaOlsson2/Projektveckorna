using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class PlayerMovement : PlayerBase
{
    readonly KeyCode jump = KeyCode.W;
    readonly KeyCode dash = KeyCode.Space;

    readonly float dashDuration = 0.3f;
    readonly float dashForce = 500;
    readonly float jumpForce = 200;
    readonly float baseSpeed = 2;
    readonly float staminaFull = 4;
    readonly int sprintSpeed = 2;

    readonly float rightWorldBorder = 65.205f;
    readonly float leftWorldBorder = -9.315f;

    [SerializeField]
    Slider slider;

    Image staminaImageToChangeColor;
    ChangeInventorySprite water;

    int speedMultiplier = 2;

    public override void Start()
    {
        base.Start();
        water = GetComponent<ChangeInventorySprite>();

        staminaImageToChangeColor = slider.fillRect.GetComponent<Image>();
        slider.maxValue = staminaFull;
        staminaTimer = staminaFull;

        colliders = GetComponents<PolygonCollider2D>();
        // [0] idle walk jump attack eat damage
        // [1] sprint
    }

    private void OnEnable()
    {
        if (animator != null)
        {
            if (staminaTimer > 0 && Input.GetKey(sprint))
            {
                speedMultiplier *= sprintSpeed;
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
    private void OnDisable()
    {
        if (animator != null && audioController != null)
        {
            if (staminaTimer > 0 && Input.GetKey(sprint) && Mathf.Abs(speedMultiplier) > sprintSpeed)
            {
                speedMultiplier /= sprintSpeed;
            }

            ChangeAnimation("Damage");
        }

    }

    void Update()
    {
        slider.value = Mathf.Abs(staminaTimer);

        if (staminaTimer > 0)
        {
            if (Input.GetKey(sprint) && ((Input.GetKey(right) && !Input.GetKey(left)) || (Input.GetKey(left) && !Input.GetKey(right))))
            {
                staminaTimer -= Time.deltaTime;
                if (staminaTimer <= 0)
                {
                    StartFatigue();
                }
            }
            else if (staminaTimer < staminaFull)
            {
                staminaTimer += Time.deltaTime;
            }

            if (Input.GetKeyDown(sprint))
            {
                speedMultiplier *= sprintSpeed;
                if ((Input.GetKey(left) && !Input.GetKey(right)) || (Input.GetKey(right) && !Input.GetKey(left)))
                {
                    ChangeAnimation("Run");
                }
            }
            if (Input.GetKeyUp(sprint) && Mathf.Abs(speedMultiplier) > sprintSpeed)
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

        if ((Input.GetKeyUp(right) && !Input.GetKey(left)) || (Input.GetKeyUp(left) && !Input.GetKey(right)) || (Input.GetKeyDown(right) && Input.GetKey(left)) || (Input.GetKeyDown(left) && Input.GetKey(right)))
        {
            ChangeAnimation("Idel");
        }


        if (Input.GetKeyDown(jump) && grounded)
        {
            ChangeAnimation("Jump");
            rb.AddForce(transform.up * jumpForce);

            grounded = false;
        }

        if ((transform.position.x > leftWorldBorder && Input.GetKey(left) && !Input.GetKey(right)) || (transform.position.x < rightWorldBorder && Input.GetKey(right) && !Input.GetKey(left)))
        {
            transform.position += new Vector3(baseSpeed * speedMultiplier, 0, 0) * Time.deltaTime;

            if (Input.GetKeyDown(dash) && staminaTimer > 0)
            {
                StartCoroutine(Dash(speedMultiplier));
            }
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    IEnumerator Dash(int direction)
    {
        staminaTimer -= 1.5f;

        if (staminaTimer < 0)
        {
            StartFatigue();
        }

        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector3(dashForce * (Mathf.Abs(direction) / direction), 0, 0));
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = 1;
        rb.velocity = Vector3.zero;
    }

    public void StopFatigue()
    {
        staminaTimer = staminaFull;
        staminaImageToChangeColor.color = Color.green;
        speedMultiplier *= 2;
    }

    void StartFatigue()
    {
        SetWalkOrIdleOrSprint();
        staminaImageToChangeColor.color = Color.red;
        speedMultiplier = Mathf.Abs(speedMultiplier) / speedMultiplier;
        staminaTimer = -staminaFull;
        water.interactable = true;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (dont)
        {
            dont = false;
        }
        else if (collision.gameObject.tag == "Ground" && !grounded)
        {
            grounded = true;
            audioController.Play("Landing");

            if (this.enabled)
            {
                SetWalkOrIdleOrSprint();
            }
            else if (rb != null)
            {
                rb.velocity = Vector3.zero;
                if (valueKeeper.dead)
                {
                    StartCoroutine(valueKeeper.Death());
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
        animator.ResetTrigger("Idel");

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
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            speedMultiplier = -Mathf.Abs(speedMultiplier);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            speedMultiplier = Mathf.Abs(speedMultiplier);
        }
    }

}