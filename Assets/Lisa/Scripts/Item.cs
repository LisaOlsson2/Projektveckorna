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
                inventory.square.gameObject.SetActive(true);
            }

            if (inventory.FindSprite(GetComponent<SpriteRenderer>().sprite) == inventory.inventory.Count)
            {
                item = Instantiate(reference.emptyKinda, reference.canvas);
                inventory.inventory.Add(GetComponent<SpriteRenderer>().sprite);
                item.GetComponent<Image>().sprite = inventory.inventory[inventory.inventory.Count - 1];
                item.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, reference.distanceInventory * inventory.inventoryUI.Count);
                inventory.inventoryUI.Add(item);
            }
            else
            {
                inventory.inventoryUI[inventory.FindSprite(GetComponent<SpriteRenderer>().sprite)].transform.GetChild(0).GetComponent<Text>().text = "" + (int.Parse(inventory.inventoryUI[inventory.FindSprite(GetComponent<SpriteRenderer>().sprite)].transform.GetChild(0).GetComponent<Text>().text) + 1);
            }

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