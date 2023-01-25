using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class Inventory : BaseMostThings
{
    public GameObject[] cheese; // health

    [SerializeField]
    Sprite food; // cheese inventory icon

    public Sprite water; // clean water icon
    public Sprite dirty; // dirty water icon
    public Sprite empty; // empty water icon

    public RectTransform square; // thingy behind the icons to show which one you're at
    string[] numbers = new string[10]; // keys
    public List<Sprite> inventory = new List<Sprite>(); // keeper of icons
    public List<GameObject> inventoryUI = new List<GameObject>(); // keeper of objects with icons

    public readonly Vector2 startPos = new Vector2(-2300, 0); // local position of the first place in the inventory

    public readonly KeyCode pickUp = KeyCode.Mouse1; // right click
    public readonly KeyCode use = KeyCode.Mouse0; // left click

    public override void Start()
    {
        base.Start();
        for (int i = 1; i < numbers.Length; i++) // assigns the numbers so i wouldn't have to write them all down. starts from 1 because the 0 key is to the right of 9
        {
            numbers[i - 1] = i + "";
        }
        numbers[9] = "0";

    }

    void Update()
    {
        if (square.gameObject.activeSelf) // if the inventory isn't empty
        {
            if ((Input.mouseScrollDelta.y > 0 && square.transform.position.x < inventoryUI[inventoryUI.Count - 1].transform.position.x) || (Input.mouseScrollDelta.y < 0 && square.transform.position.x > inventoryUI[0].transform.position.x)) // if you scroll the wheel within how much you have in your inventory
            {
                square.localPosition += Mathf.Abs(Input.mouseScrollDelta.y) /Input.mouseScrollDelta.y * Vector3.right * valueKeeper.distanceInventory; // move the square
            }

            foreach (string key in numbers) // goes through the keys and checks if one gets pressed
            {
                if (Input.GetKeyDown(key))
                {
                    int place;
                    if (int.Parse(key) > 0)
                    {
                        place = int.Parse(key) - 1;
                    }
                    else
                    {
                        place = 9;
                    }

                    if (place < inventory.Count)
                    {
                        square.transform.position = inventoryUI[place].transform.position; // moves the square
                    }
                }
            }

            if (Input.GetKeyDown(use))
            {
                if (valueKeeper.health < cheese.Length && CurrentSprite() == food) // if you press use while you have the cheese selected in the inventory and you aren't at full health
                {
                    UseItem(FindSprite(food));
                    GetComponent<PlayerMovement>().ChangeAnimation("Eat");
                    cheese[valueKeeper.health].SetActive(true);
                    valueKeeper.health++;
                }

                if (PlayerBase.staminaTimer < 0 && CurrentSprite() == water)
                {
                    ChangeSprite(FindSprite(water), empty);
                    valueKeeper.player.StopFatigue();
                }
            }

        }
    }

    public Sprite CurrentSprite() // returns the sprite of the current item in the inventory
    {
        return inventory[(int)((Mathf.RoundToInt(square.anchoredPosition.x) - startPos.x) / (valueKeeper.distanceInventory))];
    }

    public int FindSprite(Sprite sprite) // returns the place in the inventory in which a specific sprite is
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

        text.text = "" + (int.Parse(text.text) - 1); // subtracts 1 from the text in the child of the object
        valueKeeper.amounts[place] = int.Parse(text.text); // updates the amount
        if (int.Parse(text.text) == 0) // if it's the last of it's kind in the inventory
        {
            inventory.Remove(inventory[place]); // remove its sprite from the list of sprites
            Destroy(inventoryUI[place]); // destroy the object showing you have it
            inventoryUI.Remove(inventoryUI[place]); // remove the now empty spot in the list of gameobjects
            valueKeeper.amounts.Remove(0); // remove its 0 from the list of amounts

            if (inventory.Count == 0) // if it was the last in the inventory
            {
                square.gameObject.SetActive(false);
            }
            else
            {
                if (place == inventory.Count) // if it had the last spot of objects in the inventory
                {
                    square.transform.position = inventoryUI[place - 1].transform.position; // move the square to the new last object
                }
                else
                {
                    // move all objects to the right of it one place to the left
                    for (int i = inventory.Count - 1; i > place; i--)
                    {
                        inventoryUI[i].transform.position = inventoryUI[i - 1].transform.position;
                    }
                    inventoryUI[place].transform.position = square.transform.position;
                }
            }
        }
    }

    public void ChangeSprite(int place, Sprite newSprite)
    {
        inventory[place] = newSprite;
        inventoryUI[place].GetComponent<Image>().sprite = newSprite;
    }
}