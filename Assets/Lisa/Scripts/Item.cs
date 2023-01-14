using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class Item : Interactive
{
    void Update()
    {
        if (Input.GetKeyDown(pickUp) && interactable)
        {
            if (inventory.inventory.Count == 0)
            {
                inventory.square.gameObject.SetActive(true);
            }

            int place = inventory.FindSprite(spriteRenderer.sprite);

            if (place == inventory.inventory.Count)
            {
                valueKeeper.amounts.Add(0);
                inventory.inventory.Add(spriteRenderer.sprite);
                valueKeeper.AddItem(place);
            }

            valueKeeper.amounts[place]++;
            inventory.inventoryUI[place].GetComponentInChildren<Text>().text = "" + valueKeeper.amounts[place];
            Destroy(gameObject);
        }
    }
}