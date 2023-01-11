using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class Inventory : MonoBehaviour
{
    public List<Sprite> inventory = new List<Sprite>();
    public List<GameObject> inventoryUI = new List<GameObject>();

    public void UseItem(Sprite item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == item)
            {
                inventory.Remove(inventory[i]);
                Destroy(inventoryUI[i]);
                break;
            }
        }
    }
}
