using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class TempMovement : MonoBehaviour
{
    [SerializeField]
    GameObject cam;

    readonly float dashDuration = 0.3f;
    readonly float dashForce = 500;
    readonly float jumpForce = 250;
    readonly float speed = 2;
    readonly float stamina = 4;
    readonly float sprintInputDelay = 0.2f;
    readonly int sprint = 7;

    readonly float rightBorder = 10;
    readonly float leftBorder = -20;

    readonly float halfCamWorldspace = 8;

    bool grounded;
    float camDistanceX;
    float staminaTimer;
    int speedMultiplier = 1;
    float sprintTimer;

    Rigidbody2D rb;

    [SerializeField]
    Slider slider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        slider.maxValue = stamina;
    }

    void Update()
    {
        print(speedMultiplier);

        slider.value = Mathf.Abs(staminaTimer);

        if (Mathf.Abs(speedMultiplier) == sprint)
        {
            staminaTimer -= Time.deltaTime;

            if (staminaTimer < 0)
            {
                slider.fillRect.GetComponent<Image>().color = Color.red;
                sprintTimer = 0;
                speedMultiplier = Mathf.Abs(speedMultiplier)/speedMultiplier;
                staminaTimer = -4;
            }
        }
        else if (staminaTimer < stamina)
        {
            staminaTimer += Time.deltaTime;
            if (staminaTimer > 0)
            {
                slider.fillRect.GetComponent<Image>().color = Color.green;
            }
        }

        if (staminaTimer > 0)
        {
            if (Mathf.Abs(speedMultiplier) == 2 && sprintTimer < sprintInputDelay)
            {
                sprintTimer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.A))
                {
                    speedMultiplier = -sprint;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    speedMultiplier = sprint;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    speedMultiplier = 2;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    speedMultiplier = -2;
                }
                if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
                {
                    if (sprintTimer >= sprintInputDelay || speedMultiplier == sprint)
                    {
                        sprintTimer = 0;
                    }
                    speedMultiplier = Mathf.Abs(speedMultiplier)/speedMultiplier;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }


        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }

        if ((transform.position.x > leftBorder && Input.GetKey(KeyCode.A)) || (transform.position.x < rightBorder && Input.GetKey(KeyCode.D)))
        {
            transform.position += new Vector3(speed * speedMultiplier, 0, 0) * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space) && staminaTimer > 0)
            {
                StartCoroutine(Dash(speedMultiplier));
            }
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        camDistanceX = transform.position.x - cam.transform.position.x;
        cam.transform.position += new Vector3(0, transform.position.y - cam.transform.position.y, 0) * speed * Time.deltaTime;
        if ((cam.transform.position.x < rightBorder - halfCamWorldspace && cam.transform.position.x > leftBorder + halfCamWorldspace) || (transform.position.x > leftBorder + halfCamWorldspace && transform.position.x < rightBorder - halfCamWorldspace))
        {
            cam.transform.position += new Vector3(camDistanceX, 0, 0) * speed * Time.deltaTime;
        }
    }

    IEnumerator Dash(int direction)
    {
        staminaTimer -= 1;
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector3(dashForce * (Mathf.Abs(direction) / direction), 0, 0));
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = 1;
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }
}