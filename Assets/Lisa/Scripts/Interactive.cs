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

    static int amount;

    public int instructionChild;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        inventory = FindObjectOfType<Inventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        inventory.instructions.gameObject.SetActive(true);
        inventory.instructions.anchoredPosition = (transform.position - inventory.cam.position) * (Screen.width / 18.63f);
        amount++;
        interactable = true;
        inventory.instructions.GetChild(instructionChild).gameObject.SetActive(true);
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        interactable = false;
        amount--;
        if (amount <= 0)
        {
            amount = 0;
            inventory.instructions.GetChild(instructionChild).gameObject.SetActive(false);
            inventory.instructions.gameObject.SetActive(false);
        }
    }
}