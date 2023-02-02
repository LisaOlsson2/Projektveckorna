using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
// things you can interact with, currently just things you can pick up and craft
public class Interactive : BaseMostThings
{
    public Inventory inventory;
    public SpriteRenderer spriteRenderer;
    public bool interactable;

    public int instructionChild;

    static int amount;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        inventory = FindObjectOfType<Inventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        if (inventory.instructions.GetChild(instructionChild).gameObject.activeSelf && interactable && amount < 2)
        {
            for (int i = 0; i < inventory.instructions.childCount; i++)
            {
                if (inventory.instructions.GetChild(i).gameObject.activeSelf)
                {
                    inventory.instructions.GetChild(i).gameObject.SetActive(false);
                }
            }
            inventory.instructions.GetChild(instructionChild).gameObject.SetActive(true);
        }
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!interactable)
        {
            interactable = true;
            for (int i = 0; i < inventory.instructions.childCount; i++)
            {
                if (inventory.instructions.GetChild(i).gameObject.activeSelf)
                {
                    inventory.instructions.GetChild(i).gameObject.SetActive(false);
                }
            }
            inventory.instructions.GetChild(instructionChild).gameObject.SetActive(true);
            inventory.instructions.anchoredPosition = (transform.position - inventory.cam.position) * (Screen.width / 18.63f);
            amount++;
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (interactable)
        {
            interactable = false;
            amount--;
            if (amount <= 0)
            {
                amount = 0;
                inventory.instructions.GetChild(instructionChild).gameObject.SetActive(false);
            }
        }
    }
}