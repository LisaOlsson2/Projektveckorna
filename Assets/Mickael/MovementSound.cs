using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class MovementSound : BaseMostThings
{
    readonly KeyCode left = KeyCode.A;
    readonly KeyCode right = KeyCode.D;
    readonly KeyCode jump = KeyCode.W;
    readonly KeyCode dash = KeyCode.Space;
    readonly KeyCode sprint = KeyCode.LeftShift;

    readonly float dashDuration = 0.3f;
    readonly float dashForce = 500;
    readonly float jumpForce = 200;
    readonly float baseSpeed = 2;
    readonly float staminaFull = 4;
    readonly int sprintSpeed = 3;

    readonly float rightBorder = 10;
    readonly float leftBorder = -20;

    readonly float halfCamWorldspace = 8;

    Rigidbody2D rb;
    Animator animator;

    Image staminaImageToChangeColor;

    [SerializeField]
    GameObject cam;

    [SerializeField]
    Slider slider;

    bool grounded;
    float camDistanceX;
    float staminaTimer;
    int speedMultiplier = 2;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        staminaImageToChangeColor = slider.fillRect.GetComponent<Image>();
        slider.maxValue = staminaFull;
    }

    void Update()
    {
        slider.value = Mathf.Abs(staminaTimer);

        if (Input.GetKey(sprint) && ((Input.GetKey(right) && !Input.GetKey(left)) || (Input.GetKey(left) && !Input.GetKey(right))) && staminaTimer > 0)
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
            if (staminaTimer > 0 && staminaImageToChangeColor.color == Color.red)
            {
                staminaImageToChangeColor.color = Color.green;
                speedMultiplier *= 2;
            }
        }

        if (staminaTimer > 0)
        {
            if (Input.GetKeyDown(sprint))
            {
                speedMultiplier *= sprintSpeed;
            }
            if (Input.GetKeyUp(sprint) && Mathf.Abs(speedMultiplier) > sprintSpeed)
            {
                speedMultiplier /= sprintSpeed;
            }
        }

        if (Input.GetKeyDown(left) || (Input.GetKeyUp(right) && Input.GetKey(left)))
        {
            FindObjectOfType<AudioController>().Play("footsteps");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            speedMultiplier = -Mathf.Abs(speedMultiplier);
        }
        if (Input.GetKeyDown(right) || (Input.GetKeyUp(left) && Input.GetKey(right)))
        {
            FindObjectOfType<AudioController>().Play("footsteps");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            speedMultiplier = Mathf.Abs(speedMultiplier);
        }


        if (Input.GetKeyDown(jump) && grounded)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(transform.up * jumpForce);

            FindObjectOfType<AudioController>().Play("Jump");
        }

        if ((transform.position.x > leftBorder && Input.GetKey(left) && !Input.GetKey(right)) || (transform.position.x < rightBorder && Input.GetKey(right) && !Input.GetKey(left)))
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

        camDistanceX = transform.position.x - cam.transform.position.x;
        cam.transform.position += new Vector3(0, transform.position.y - cam.transform.position.y, 0) * baseSpeed * Time.deltaTime;
        if ((cam.transform.position.x < rightBorder - halfCamWorldspace && cam.transform.position.x > leftBorder + halfCamWorldspace) || (transform.position.x > leftBorder + halfCamWorldspace && transform.position.x < rightBorder - halfCamWorldspace))
        {
            cam.transform.position += new Vector3(camDistanceX, 0, 0) * baseSpeed * Time.deltaTime;
        }
    }

    IEnumerator Dash(int direction)
    {
        staminaTimer -= 1;

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

    void StartFatigue()
    {
        staminaImageToChangeColor.color = Color.red;
        speedMultiplier = Mathf.Abs(speedMultiplier) / speedMultiplier;
        staminaTimer = -staminaFull;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (Input.GetKey(left) || Input.GetKey(right))
            {
                animator.SetTrigger("Walk");
                
            }
            else
            {
                animator.SetTrigger("Walk"); // there's no idle animation at the moment
            }

            grounded = true;

            FindObjectOfType<AudioController>().Play("Landing");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}