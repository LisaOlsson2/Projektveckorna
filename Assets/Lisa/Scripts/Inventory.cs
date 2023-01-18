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
    string[] numbers = new string[10];
    public List<Sprite> inventory = new List<Sprite>();
    public List<GameObject> inventoryUI = new List<GameObject>();

    public readonly Vector2 startPos = new Vector2(-2300, 0);

    public readonly KeyCode pickUp = KeyCode.Mouse1;
    public readonly KeyCode use = KeyCode.Mouse0;

    public override void Start()
    {
        base.Start();
        for (int i = 1; i < numbers.Length; i++)
        {
            numbers[i - 1] = i + "";
        }
        numbers[9] = 0 + "";

    }


    void Update()
    {

        if (square.gameObject.activeSelf)
        {
            if ((Input.mouseScrollDelta.y > 0 && square.transform.position.x < inventoryUI[inventoryUI.Count - 1].transform.position.x) || (Input.mouseScrollDelta.y < 0 && square.transform.position.x > inventoryUI[0].transform.position.x))
            {
                square.localPosition += Mathf.Abs(Input.mouseScrollDelta.y) /Input.mouseScrollDelta.y * Vector3.right * valueKeeper.distanceInventory;
            }

            foreach (string key in numbers)
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
                        square.transform.position = inventoryUI[place].transform.position;
                    }
                }
            }
        }

        if (valueKeeper.health < cheese.Length && Input.GetKeyDown(use) && square.gameObject.activeSelf && CurrentSprite() == food)
        {
            UseItem(FindSprite(food));
            GetComponent<PlayerMovement>().ChangeAnimation("Eat");
            cheese[valueKeeper.health].SetActive(true);
            valueKeeper.health++;
        }

    }

    public Sprite CurrentSprite()
    {
        return inventory[(int)((square.anchoredPosition.x - startPos.x) / valueKeeper.distanceInventory)];
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