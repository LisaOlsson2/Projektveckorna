using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : BaseMostThings
{
    public Inventory inventory;
    public SpriteRenderer spriteRenderer;
    public bool interactable;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        inventory = FindObjectOfType<Inventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = true;
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        interactable = false;
    }
}
