using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class Crafting : Materials
{
    // 0.2 = not close
    // 0.4 = close no item
    // 0.6 = close item
    // 0.8 = close item selected
    // 1 = built

    public int[] amounts; // how many of the material at the same place in the array above that are needed

    [SerializeField]
    bool more; // true if you can add more to this after it's been crafted

    [SerializeField]
    Sprite[] iconsIfMore; // materials that can be added later

    [SerializeField]
    Sprite otherIfMore; // the change of sprite for when it's been crafted further

    public override void Start()
    {
        instructionChild = 2;
        base.Start();
    }

    public override void Update()
    {
        if (interactable) // if you're close enough and you have at least one of the materials in your inventory
        {
            for (int i = 0; i < materials.Length; i++) // go through the materials needed and check if one is selected
            {
                spriteRenderer.color = new Vector4(1, 1, 1, 0.6f); // not selected
                if (inventory.CurrentSprite() == materials[i]) // if selected
                {
                    spriteRenderer.color = new Vector4(1, 1, 1, 0.8f); // selected

                    if (Input.GetKeyDown(inventory.use)) // if you press use
                    {
                        if (materials[i] == inventory.water) // if it's water
                        {
                            inventory.inventory[inventory.FindSprite(inventory.water)] = inventory.empty; // change to the empty bottle cap sprite
                            inventory.inventoryUI[inventory.FindSprite(inventory.empty)].GetComponent<Image>().sprite = inventory.empty; // change the sprite of the object too
                        }
                        else
                        {
                            inventory.UseItem(inventory.FindSprite(materials[i]));
                        }
                        amounts[i]--;

                        if (amounts[i] == 0) // if it's the last needed of a material
                        {
                            Sprite[] sprites = new Sprite[materials.Length - 1]; // make a new array with one less spot than the previous
                            int[] ints = new int[amounts.Length - 1];

                            int i3 = 0;
                            for (int i2 = 0; i2 < materials.Length; i2++)
                            {
                                if (i2 == i)
                                {
                                    continue;
                                }

                                // if the material wasn't the one used up
                                sprites[i3] = materials[i2]; // add it to the new array
                                ints[i3] = amounts[i2];
                                i3++; // the next that will be added to is the next place in the new array
                            }

                            amounts = ints; // save the new array as the usual array
                            materials = sprites;
                        }
                        bool craft = true;
                        foreach(int amount in amounts)
                        {
                            if (amount > 0) // if there are more materials left of this thing
                            {
                                craft = false;
                                inventory.PlayCraftingAnimation(1); // play the animation once
                                OnTriggerExit2D(null);
                                OnTriggerEnter2D(null);
                                break;
                            }
                        }

                        if (craft)
                        {
                            inventory.PlayCraftingAnimation(3); // play the animation thrice
                            Craft1(); // craft the item
                        }
                        UpdateText(craft);
                    }
                    break;
                }
            }
        }
    }

    void Craft1() // thingy before craft to check if it's the last craftable item
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
        if (more)
        {
            MoreCrafting more;
            more = gameObject.AddComponent<MoreCrafting>();
            more.icons = iconsIfMore;
            more.other = otherIfMore;
            more.usual = spriteRenderer.sprite;
        }
        spriteRenderer.color = Vector4.one;
        Destroy(this);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = new Vector4(1, 1, 1, 0.4f);
        for (int i = 0; i < materials.Length; i++)
        {
            if (inventory.FindSprite(materials[i]) < inventory.inventory.Count) // if you have at least one of the materials in your inventory
            {
                base.OnTriggerEnter2D(collision); // makes interactable true
                return;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        spriteRenderer.color = new Vector4(1, 1, 1, 0.2f);
    }
}
