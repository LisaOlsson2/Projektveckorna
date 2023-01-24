using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class MoreCrafting : Interactive
{
    public Sprite[] icons;
    public Sprite other;
    public Sprite usual;
    Animator animator;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (interactable && Input.GetKeyDown(inventory.use)) // if you're close enough and click use
        {
            for (int i = 0; i < icons.Length - 1; i++)
            {
                if (inventory.CurrentSprite() == icons[i]) // if the selected item in the inventory is one that can be used here
                {
                    transform.GetChild(i).gameObject.SetActive(true); // set the child with the same index as the icon active
                    inventory.UseItem(inventory.FindSprite(icons[i])); // use the object in the inventory
                    spriteRenderer.sprite = other;
                }
            }

            // the fireplace has a stone in its last icon spot that can be used to light the dirty water child on fire if it's active, its sibling index is 1
            if (inventory.CurrentSprite() == icons[icons.Length - 1] && transform.GetChild(1).gameObject.activeSelf) // if the stone is selected in the inventory and the dirty water child is active
            {
                StartCoroutine(BoilWater());
            }
        }
    }

    IEnumerator BoilWater()
    {
        valueKeeper.audioController.Play("Fire");
        transform.GetChild(1).gameObject.SetActive(false);
        animator.enabled = true; // enable the animator so it can play the boiling animation
        yield return new WaitForSeconds(8/12f * 4);
        valueKeeper.audioController.Stop("Fire");
        animator.enabled = false; // disables the animator so it stops playing the boiling animation
        spriteRenderer.sprite = other; // sets the sprite so it isn't one with fire
        transform.GetChild(2).gameObject.SetActive(true); // activates the clean water
    }

    public void ChangeSprite()
    {
        spriteRenderer.sprite = usual;
    }
}
