using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class Crafting : MonoBehaviour
{
    Inventory inventory;
    SpriteRenderer spriteRenderer;

    readonly float distanceToCraft = 2; // i'll make it an ontrigger box collider later so it won't be checking every frame
    int checkedInventory; // 0 = not checked, 1 = doesn't have the item, 2 = has the item
    int place;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Mathf.Abs(inventory.transform.position.x - transform.position.x) < distanceToCraft)
        {
            if (checkedInventory == 0)
            {
                if (inventory.FindSprite(spriteRenderer.sprite) < inventory.inventory.Count)
                {
                    place = inventory.FindSprite(spriteRenderer.sprite);
                    checkedInventory = 2;
                }
                else
                {
                    checkedInventory = 1;
                }
            }
            
            if (checkedInventory == 2)
            {
                if (inventory.square.transform.position.y == inventory.inventoryUI[place].transform.position.y)
                {
                    spriteRenderer.color = new Vector4(1, 1, 1, 0.7f);
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        inventory.UseItem(spriteRenderer.sprite, place);
                        spriteRenderer.color = new Vector4(1, 1, 1, 1);
                        gameObject.AddComponent<BoxCollider2D>();
                        Destroy(this);
                    }
                }
                else
                {
                    spriteRenderer.color = new Vector4(1, 1, 1, 0.5f);
                }
            }
        }
        else
        {
            checkedInventory = 0;
            spriteRenderer.color = new Vector4(1, 1, 1, 0.2f);
        }
    }
}
