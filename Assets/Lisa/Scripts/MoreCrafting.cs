using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class MoreCrafting : Materials
{
    public Sprite[] icons;
    public Sprite other;
    public Sprite usual;
    Animator animator;

    public override void Start()
    {
        instructionChild = 2;
        base.Start();
        animator = GetComponent<Animator>();

        materials = new Sprite[3];
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = icons[i];
        }
    }

    public override void Update()
    {
        base.Update();
        if (interactable && Input.GetKeyDown(inventory.use)) // if you're close enough and click use
        {
            for (int i = 0; i < icons.Length - 1; i++)
            {
                if (inventory.CurrentSprite() == icons[i])
                {
                    if (transform.GetChild(0).gameObject.activeSelf && (i == 1 || i == 2))
                    {
                        inventory.ChangeSprite(inventory.FindSprite(icons[i]), inventory.empty);
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else if (transform.GetChild(2).gameObject.activeSelf && i > 2)
                    {
                        inventory.UseItem(inventory.FindSprite(icons[i]));
                        transform.GetChild(2).gameObject.SetActive(false);
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else if (i < 3)
                    {
                        CheckChildren(i);
                    }
                }
            }

            // the fireplace has a stone in its last icon spot that can be used to light the dirty water child on fire if it's active, its sibling index is 1
            if (inventory.CurrentSprite() == icons[icons.Length - 1] && transform.GetChild(1).gameObject.activeSelf) // if the stone is selected in the inventory and the dirty water child is active
            {
                StartCoroutine(Cook(1, 2));
            }
        }
    }

    void CheckChildren(int i)
    {
        for (int i2 = 0; i2 < transform.childCount; i2++)
        {
            if (transform.GetChild(i2).gameObject.activeSelf)
            {
                return;
            }
        }

        transform.GetChild(i).gameObject.SetActive(true); // set the child with the same index as the icon active
        inventory.UseItem(inventory.FindSprite(icons[i])); // use the object in the inventory
        spriteRenderer.sprite = other;

        if (i == 0)
        {
            materials = new Sprite[2];
            materials[0] = icons[1];
            materials[1] = icons[2];
        }
        else if (i == 1)
        {
            materials = new Sprite[1];
            materials[0] = icons[icons.Length - 1];
        }
        else if (i == 2)
        {
            materials = new Sprite[3];
            materials[0] = icons[4];
            materials[1] = icons[5];
            materials[2] = icons[6];
        }
    }

    public void StartCooking(int a, int b)
    {
        StartCoroutine(Cook(a, b));
    }

    IEnumerator Cook(int oldChild, int newChild)
    {
        valueKeeper.audioController.Play("Fire");
        transform.GetChild(oldChild).gameObject.SetActive(false);
        animator.enabled = true;
        if (newChild > 3)
        {
            animator.SetTrigger((newChild - 1) + "");
        }
        else
        {
            animator.SetTrigger(newChild + "");
        }
        yield return new WaitForSeconds(8 / 12f * 4);
        valueKeeper.audioController.Stop("Fire");
        animator.enabled = false; // disables the animator so it stops playing the boiling animation
        spriteRenderer.sprite = other; // sets the sprite so it isn't one with fire
        transform.GetChild(newChild).gameObject.SetActive(true); // activates the clean water
    }

    public void ChangeSprite()
    {
        spriteRenderer.sprite = usual;
    }
}
