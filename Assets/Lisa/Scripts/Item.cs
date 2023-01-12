using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class Item : Interactive
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && interactable)
        {
            if (inventory.inventory.Count == 0)
            {
                inventory.square.gameObject.SetActive(true);
            }

            int place = inventory.FindSprite(spriteRenderer.sprite);

            if (place == inventory.inventory.Count)
            {
                GameObject item = Instantiate(reference.emptyKinda, reference.canvas);
                inventory.inventory.Add(spriteRenderer.sprite);
                item.GetComponent<Image>().sprite = inventory.inventory[inventory.inventory.Count - 1];
                item.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, reference.distanceInventory * inventory.inventoryUI.Count);
                inventory.inventoryUI.Add(item);
            }
            else
            {
                Text text = inventory.inventoryUI[place].transform.GetChild(0).GetComponent<Text>();
                text.text = "" + (int.Parse(text.text) + 1);
            }

            Destroy(gameObject);
        }
    }
}