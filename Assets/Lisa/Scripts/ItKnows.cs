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

    Transform inventoryParent;
    GameObject emptyKinda;
    public AudioController audioController;

    [SerializeField]
    GameObject cam;

    [SerializeField]
    Button exit;

    public readonly float distanceInventory = 575;

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

            inEventOfTwo[other].audioController = audioController;
            inEventOfTwo[other].allItems = allItems;
            inEventOfTwo[other].inventoryParent = inventoryParent;
            inEventOfTwo[other].exit = exit;
            inEventOfTwo[other].inventory = inventory;
            inEventOfTwo[other].allCraftingGhosts = allCraftingGhosts;
            inEventOfTwo[other].cam = cam;

            inEventOfTwo[other].SetValues();

            Destroy(gameObject);
        }
        else
        {
            print("that's not supposed to happen");
        }
    }

    void SetValues()
    {
        exit.onClick.AddListener(Exit);

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
        cam.transform.position = new Vector3(inventory.transform.position.x, cam.transform.position.y, cam.transform.position.z);
    }

    public void AddItem(int place)
    {
        GameObject item = Instantiate(emptyKinda, inventoryParent);
        item.GetComponent<Image>().sprite = inventory.inventory[place];
        item.GetComponentInChildren<Text>().text = amounts[place] + "";
        item.GetComponent<RectTransform>().localPosition += new Vector3(distanceInventory * place, 0, 0);
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

    public void TheEnd()
    {
        SceneManager.LoadScene("The End");
    }
}
