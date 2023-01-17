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

    [SerializeField]
    Sprite[] materials;

    [SerializeField]
    int[] amounts;

    void Update()
    {
        if (interactable)
        {

            for (int i = 0; i < materials.Length; i++)
            {
                if (inventory.CurrentSprite() == materials[i])
                {
                    spriteRenderer.color = new Vector4(1, 1, 1, 0.8f);

                    if (Input.GetKeyDown(inventory.use))
                    {
                        inventory.UseItem(inventory.FindSprite(materials[i]));
                        amounts[i]--;

                        bool craft = true;
                        foreach(int amount in amounts)
                        {
                            if (amount > 0)
                            {
                                craft = false;
                                break;
                            }
                        }

                        if (craft)
                        {
                            Craft1();
                        }
                    }
                }

            }
        }
    }

    void Craft1()
    {
        Craft();
        foreach (Crafting craftingGhost in valueKeeper.allCraftingGhosts)
        {
            if (craftingGhost != null && craftingGhost != this)
            {
                return;
            }
        }
        valueKeeper.TheEnd();
    }
    
    public void Craft()
    {
        spriteRenderer.color = Vector4.one;
        //BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        //boxCollider.size = Vector2.one;
        //boxCollider.isTrigger = false;
        Destroy(this);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        for (int i = 0; i < materials.Length; i++)
        {
            if (inventory.FindSprite(materials[i]) < inventory.inventory.Count)
            {
                return;
            }
        }

        spriteRenderer.color = new Vector4(1, 1, 1, 0.4f);
        interactable = false;
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        spriteRenderer.color = new Vector4(1, 1, 1, 0.2f);
    }
}
