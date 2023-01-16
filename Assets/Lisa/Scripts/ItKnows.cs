using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Lisa
public class ItKnows : MonoBehaviour
{
    ItKnows[] inEventOfTwo;
    Inventory inventory;

    // crafted items
    public Crafting[] allCraftingGhosts;
    public bool[] itemsCrafted;

    // health
    public int health;

    // inventory
    public List<Sprite> inventorySprites = new List<Sprite>();
    public List<int> amounts = new List<int>();

    // things picked up
    public GameObject[] allItems;
    public bool[] itemsPickedUp;

    // position
    public Vector3 position;

    public Transform canvas;
    public GameObject emptyKinda;
    public AudioController audioController;

    [SerializeField]
    Button exit;

    public readonly int distanceInventory = 100;

    void Start()
    {
        inEventOfTwo = FindObjectsOfType<ItKnows>();
        inventory = FindObjectOfType<Inventory>();

        if (inEventOfTwo.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            health = inventory.cheese.Length;
            itemsPickedUp = new bool[allItems.Length];
            itemsCrafted = new bool[allCraftingGhosts.Length];
        }
        else if (inEventOfTwo.Length == 2)
        {
            int other;
            if (inEventOfTwo[0] == this)
            {
                other = 1;
            }
            else
            {
                other = 0;
            }

            inEventOfTwo[other].SetValues(allItems, canvas, exit, inventory, allCraftingGhosts);
            Destroy(gameObject);
        }
        else
        {
            print("that's not supposed to happen");
        }
    }

    void SetValues(GameObject[] items, Transform oldCanvas, Button button, Inventory player, Crafting[] craftingGhosts)
    {
        inventory = player;

        allItems = items;
        allCraftingGhosts = craftingGhosts;
        canvas = oldCanvas;

        button.onClick.AddListener(Exit);

        for (int i = inventory.cheese.Length - 1; i > health - 1; i--)
        {
            inventory.cheese[i].SetActive(false);
        }

        for (int i = 0; i < allItems.Length; i++)
        {
            if (itemsPickedUp[i])
            {
                Destroy(allItems[i]);
            }
        }
        for (int i = 0; i < allCraftingGhosts.Length; i++)
        {
            if (itemsCrafted[i])
            {
                allCraftingGhosts[i].spriteRenderer = allCraftingGhosts[i].GetComponent<SpriteRenderer>();
                allCraftingGhosts[i].Craft();
            }
        }

        inventory.inventory = inventorySprites;

        if (inventory.inventory.Count > 0)
        {
            inventory.square.gameObject.SetActive(true);

            for (int i = 0; i < inventorySprites.Count; i++)
            {
                AddItem(i);
            }

        }

        inventory.transform.position = position;
    }

    public void AddItem(int place)
    {
        GameObject item = Instantiate(emptyKinda, canvas);

        item.GetComponent<Image>().sprite = inventory.inventory[place];
        item.GetComponentInChildren<Text>().text = amounts[place] + "";
        item.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, distanceInventory * place);
        inventory.inventoryUI.Add(item);
    }

    public void Exit()
    {
        for (int i = 0; i < allItems.Length; i++)
        {
            if (allItems[i] == null)
            {
                itemsPickedUp[i] = true;
            }
            else
            {
                itemsPickedUp[i] = false;
            }
        }
        for (int i = 0; i < allCraftingGhosts.Length; i++)
        {
            if (allCraftingGhosts[i] == null)
            {
                itemsCrafted[i] = true;
            }
            else
            {
                itemsCrafted[i] = false;
            }
        }

        inventorySprites = inventory.inventory;

        position = inventory.transform.position;

        SceneManager.LoadScene("Start");
    }
}
