using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreCrafting : Interactive
{
    public Sprite[] icons;
    public Sprite other;

    Sprite usual;
    Animator animator;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        usual = spriteRenderer.sprite;
    }

    private void Update()
    {
        if (interactable && Input.GetKeyDown(inventory.use))
        {
            for (int i = 0; i < icons.Length - 1; i++)
            {
                if (inventory.CurrentSprite() == icons[i])
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    inventory.UseItem(inventory.FindSprite(icons[i]));
                    spriteRenderer.sprite = other;
                }
            }

            if (inventory.CurrentSprite() == icons[icons.Length - 1] && transform.GetChild(1).gameObject.activeSelf)
            {
                StartCoroutine(BoilWater());
            }
        }
    }

    IEnumerator BoilWater()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        animator.enabled = true;
        yield return new WaitForSeconds(1);
        animator.enabled = false;
        spriteRenderer.sprite = usual;
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void ChangeSprite()
    {
        spriteRenderer.sprite = usual;
    }
}
