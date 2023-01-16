using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
   /* public AudioSource footstepsSound, sprintSound;

    void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            footstepsSound.enabled = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                footstepsSound.enabled = false;
                sprintSound.enabled = true;
            }
            else
            {
                footstepsSound.enabled = true;
                sprintSound.enabled = false;
            }
            
        }
        else
        {
            footstepsSound.enabled = false;
            sprintSound.enabled = false;
        }
       
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !grounded)
        {
            grounded = true;
            FindObjectOfType<AudioController>().Play("Landing");
            footstepsSound.enabled = true;
            sprintSound.enabled = true;
        }
        else
        {
            footstepsSound.enabled = false;
            sprintSound.enabled = false;
        }
    }*/
}
