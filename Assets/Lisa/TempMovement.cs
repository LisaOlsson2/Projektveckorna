using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMovement : MonoBehaviour
{
    [SerializeField]
    GameObject cam;

    readonly float dashDuration = 0.3f;
    readonly float dashForce = 500;
    readonly float jumpForce = 250;
    readonly float speed = 4;

    readonly float rightBorder = 10;
    readonly float leftBorder = -20;

    readonly float halfCamWorldspace = 8;

    bool grounded;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(1));
            }
            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(-1));
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }

        if (Input.GetKey(KeyCode.D) && transform.position.x < rightBorder)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) && transform.position.x > leftBorder)
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }

        cam.transform.position = new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z);
        if (transform.position.x < rightBorder - halfCamWorldspace && transform.position.x > leftBorder + halfCamWorldspace)
        {
            cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z);
        }
    }

    IEnumerator Dash(int direction)
    {
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector3(dashForce * direction, 0, 0));
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