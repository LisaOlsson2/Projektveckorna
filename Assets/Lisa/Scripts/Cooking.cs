using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
// Almost the same as crafting, i'll fix that later
public class Cooking : Interactive
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    int[] ints;

    public Sprite[] materials;

    public int[] amounts;

    [SerializeField]
    Sprite sprite;

    public override void Start()
    {
        base.Start();
        instructionChild = 2;
    }

    public override void Update()
    {
        if (interactable) // if you're close enough and you have at least one of the materials in your inventory
        {
            for (int i = 0; i < materials.Length; i++) // go through the materials needed and check if one is selected
            {
                if (inventory.CurrentSprite() == materials[i]) // if selected
                {

                    if (Input.GetKeyDown(inventory.use)) // if you press use
                    {
                        if (materials[i] == inventory.water) // if it's water
                        {
                            inventory.ChangeSprite(inventory.FindSprite(inventory.water), inventory.empty);
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
                        foreach (int amount in amounts)
                        {
                            if (amount > 0) // if there are more materials left of this thing
                            {
                                craft = false;
                                break;
                            }
                        }

                        if (craft)
                        {
                            valueKeeper.waiting[transform.GetSiblingIndex() - 3] = true;
                            amounts = ints;
                            materials = sprites;
                            transform.parent.GetComponent<MoreCrafting>().StartCooking(transform.GetSiblingIndex(), transform.GetSiblingIndex());
                            Cook();
                        }
                        valueKeeper.ingredients[transform.GetSiblingIndex() - 3] = materials;
                        valueKeeper.ingredientAmounts[transform.GetSiblingIndex() - 3] = amounts;

                    }
                    break;
                }
            }
        }
    }

    public void Cook()
    {
        spriteRenderer.sprite = sprite;
        GetComponent<Item>().enabled = true;
        this.enabled = false;
    }
}
