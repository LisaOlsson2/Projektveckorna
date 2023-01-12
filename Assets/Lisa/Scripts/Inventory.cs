using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class Inventory : BaseMostThings
{
    public RectTransform square;

    public List<Sprite> inventory = new List<Sprite>();
    public List<GameObject> inventoryUI = new List<GameObject>();

    void Update()
    {
        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0)
        {
            square.anchoredPosition += Input.mouseScrollDelta * reference.distanceInventory;
        }
    }

    public int FindSprite(Sprite sprite)
    {
        for (int i = inventory.Count - 1; i > -1; i--)
        {
            if (inventory[i] ==sprite)
            {
                return i;
            }
        }
        return inventory.Count;
    }

    public void UseItem(Sprite item, int place)
    {
        if (int.Parse(inventoryUI[place].transform.GetChild(0).GetComponent<Text>().text) == 1)
        {
            inventory.Remove(inventory[place]);
            Destroy(inventoryUI[place]);
            inventoryUI.Remove(inventoryUI[place]);
        }
        else
        {
            inventoryUI[place].transform.GetChild(0).GetComponent<Text>().text = "" + (int.Parse(inventoryUI[place].transform.GetChild(0).GetComponent<Text>().text) - 1);
        }

        if (inventory.Count == 0)
        {
            square.gameObject.SetActive(false);
        }
    }
}
