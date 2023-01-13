using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class Crafting : Interactive
{
    // 0.2 = not close
    // 0.4 = close no item
    // 0.6 = close item
    // 0.8 = close item selected
    // 1 = built

    int place;

    void Update()
    {
        if (interactable)
        {
            if (inventory.square.transform.position.y == inventory.inventoryUI[place].transform.position.y)
            {
                spriteRenderer.color = new Vector4(1, 1, 1, 0.8f);

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    inventory.UseItem(place);
                    Craft();
                }
            }
            else
            {
                spriteRenderer.color = new Vector4(1, 1, 1, 0.6f);
            }
        }
    }

    public void Craft()
    {
        spriteRenderer.color = Vector4.one;
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = Vector2.one;
        boxCollider.isTrigger = false;
        Destroy(this);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (inventory.FindSprite(spriteRenderer.sprite) < inventory.inventory.Count)
        {
            place = inventory.FindSprite(spriteRenderer.sprite);
        }
        else
        {
            spriteRenderer.color = new Vector4(1, 1, 1, 0.4f);
            interactable = false;
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        spriteRenderer.color = new Vector4(1, 1, 1, 0.2f);
    }
}
