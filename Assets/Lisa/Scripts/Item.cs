using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class Item : MonoBehaviour
{
    Inventory inventory;
    ItKnows reference;

    bool interactable;

    GameObject item;

    readonly float distance = 150;

    void Start()
    {
        reference = FindObjectOfType<ItKnows>();
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && interactable)
        {
            if (inventory.inventory.Count == 0)
            {
                Instantiate(reference.thingy, reference.canvas);
            }
            item = Instantiate(reference.emptyKinda, reference.canvas);
            inventory.inventory.Add(GetComponent<SpriteRenderer>().sprite);
            item.GetComponent<Image>().sprite = inventory.inventory[inventory.inventory.Count - 1];
            item.GetComponent<RectTransform>().anchoredPosition += new Vector2(distance * inventory.inventoryUI.Count, 0);
            inventory.inventoryUI.Add(item);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = false;
    }
}