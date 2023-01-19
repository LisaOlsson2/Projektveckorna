using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class Crafting : Interactive
{
    // 0.2 = not close
    // 0.4 = close no item
    // 0.6 = close item
    // 0.8 = close item selected
    // 1 = built

    public Sprite[] materials;

    public int[] amounts;

    [SerializeField]
    bool more;

    [SerializeField]
    Sprite[] iconsIfMore;

    [SerializeField]
    Sprite otherIfMore;

    void Update()
    {
        if (interactable && inventory.square.gameObject.activeSelf)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                spriteRenderer.color = new Vector4(1, 1, 1, 0.6f);
                if (inventory.CurrentSprite() == materials[i])
                {
                    spriteRenderer.color = new Vector4(1, 1, 1, 0.8f);

                    if (Input.GetKeyDown(inventory.use))
                    {
                        if (materials[i] == inventory.water)
                        {
                            inventory.inventory[inventory.FindSprite(inventory.water)] = inventory.empty;
                            inventory.inventoryUI[inventory.FindSprite(inventory.empty)].GetComponent<Image>().sprite = inventory.empty;
                        }
                        else
                        {
                            inventory.UseItem(inventory.FindSprite(materials[i]));
                        }
                        amounts[i]--;

                        if (amounts[i] == 0)
                        {
                            Sprite[] sprites = new Sprite[materials.Length - 1];
                            int[] ints = new int[amounts.Length - 1];

                            int i3 = 0;
                            for (int i2 = 0; i2 < materials.Length; i2++)
                            {
                                if (i2 == i)
                                {
                                    continue;
                                }
                                sprites[i3] = materials[i2];
                                ints[i3] = amounts[i2];
                                i3++;
                            }

                            amounts = ints;
                            materials = sprites;
                        }

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
                    break;
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
        if (more)
        {
            MoreCrafting more;
            more = gameObject.AddComponent<MoreCrafting>();
            more.icons = iconsIfMore;
            more.other = otherIfMore;
        }
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
