using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class Inventory : BaseMostThings
{
    public GameObject[] cheese;

    [SerializeField]
    Sprite food;

    public RectTransform square;

    public List<Sprite> inventory = new List<Sprite>();
    public List<GameObject> inventoryUI = new List<GameObject>();

    public readonly Vector2 startPos = new Vector2(100, 100);

    public readonly KeyCode pickUp = KeyCode.Mouse1;
    public readonly KeyCode use = KeyCode.Mouse0;

    void Update()
    {
        if (square.gameObject.activeSelf && ((Input.mouseScrollDelta.y > 0 && square.transform.position.y < inventoryUI[inventoryUI.Count - 1].transform.position.y) || (Input.mouseScrollDelta.y < 0 && square.transform.position.y > inventoryUI[0].transform.position.y)))
        {
            square.anchoredPosition += Input.mouseScrollDelta * Vector2.up * valueKeeper.distanceInventory;
        }

        if (valueKeeper.health < cheese.Length && Input.GetKeyDown(use) && square.gameObject.activeSelf && CurrentSprite() == food)
        {
            UseItem(FindSprite(food));
            cheese[valueKeeper.health].SetActive(true);
            valueKeeper.health++;
        }

    }

    public Sprite CurrentSprite()
    {
        return inventory[(int)(square.anchoredPosition.y - startPos.y) / valueKeeper.distanceInventory];
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

    public void UseItem(int place)
    {
        Text text = inventoryUI[place].transform.GetChild(0).GetComponent<Text>();

        text.text = "" + (int.Parse(text.text) - 1);
        valueKeeper.amounts[place] = int.Parse(text.text);
        if (int.Parse(text.text) == 0)
        {
            inventory.Remove(inventory[place]);
            Destroy(inventoryUI[place]);
            inventoryUI.Remove(inventoryUI[place]);
            valueKeeper.amounts.Remove(0);

            if (inventory.Count == 0)
            {
                square.anchoredPosition = startPos;
                square.gameObject.SetActive(false);
            }
            else
            {
                if (place == inventory.Count)
                {
                    square.transform.position = inventoryUI[place - 1].transform.position;
                }
                else
                {
                    for (int i = place + 1; i < inventory.Count; i++)
                    {
                        inventoryUI[i].transform.position = inventoryUI[i - 1].transform.position;
                    }
                    inventoryUI[place].transform.position = square.transform.position;
                }
            }
        }
    }
}