using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeInventorySprite : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;

    public Inventory inventory;
    public bool interactable;

    public virtual void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(inventory.use) && interactable && inventory.square.gameObject.activeSelf && inventory.CurrentSprite() == sprite1)
        {
            inventory.inventory[inventory.FindSprite(sprite1)] = sprite2;
            inventory.inventoryUI[inventory.FindSprite(sprite2)].GetComponent<Image>().sprite = sprite2;
            Do();
        }
        
    }
    public virtual void Do()
    {

    }
}
