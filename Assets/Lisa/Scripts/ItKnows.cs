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
    Sprite[][] materials;
    int[][] materialAmounts;
    int waterChild;
    [SerializeField]
    GameObject water;

    // health
    public int health;

    // inventory
    public List<Sprite> inventorySprites = new List<Sprite>();
    public List<int> amounts = new List<int>();

    // things picked up
    [SerializeField]
    GameObject itemsParent;
    GameObject[] allItems;
    public bool[] itemsPickedUp;

    // position
    public Vector3 position;

    // endings
    public bool dead;
    public bool done;

    [SerializeField]
    Transform inventoryParent;
    
    [SerializeField]
    GameObject emptyKinda;

    public AudioController audioController;

    [SerializeField]
    GameObject cam;

    [SerializeField]
    Button exit;

    [SerializeField]
    Sprite death;

    [SerializeField]
    GameObject deathText;

    public readonly float distanceInventory = 575;

    void Start()
    {
        inEventOfTwo = FindObjectsOfType<ItKnows>();
        inventory = FindObjectOfType<Inventory>();
        audioController = FindObjectOfType<AudioController>();
        allItems = new GameObject[itemsParent.transform.childCount];

        for (int i = 0; i < itemsParent.transform.childCount; i++)
        {
            allItems[i] = itemsParent.transform.GetChild(i).gameObject;
        }

        if (inEventOfTwo.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            health = inventory.cheese.Length;
            itemsPickedUp = new bool[allItems.Length];
            itemsCrafted = new bool[allCraftingGhosts.Length];
            materials = new Sprite[allCraftingGhosts.Length][];
            materialAmounts = new int[allCraftingGhosts.Length][];
            waterChild = water.transform.childCount;
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
            inEventOfTwo[other].deathText = deathText;
            inEventOfTwo[other].water = water;

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
            else
            {
                allCraftingGhosts[i].materials = materials[i];
                allCraftingGhosts[i].amounts = materialAmounts[i];
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

        if (itemsCrafted[0])
        {
            if (waterChild < water.transform.childCount)
            {
                water.GetComponent<SpriteRenderer>().sprite = water.GetComponent<MoreCrafting>().other;
                water.transform.GetChild(waterChild).gameObject.SetActive(true);
            }
        }

        inventory.transform.position = position;

        if (dead)
        {
            deathText.SetActive(true);
            inventoryParent.parent.gameObject.SetActive(false);
            Destroy(inventory.GetComponent<PlayerBase>());
            Destroy(inventory.GetComponent<Animator>());
            inventory.GetComponent<SpriteRenderer>().sprite = death;
        }

        if (done)
        {
            TheEnd();
        }
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
                materials[i] = allCraftingGhosts[i].materials;
                materialAmounts[i] = allCraftingGhosts[i].amounts;
            }
        }

        inventorySprites = inventory.inventory;
        position = inventory.transform.position;
        
        if (itemsCrafted[0])
        {
            waterChild = Water();
        }

        SceneManager.LoadScene("Start");
    }

    int Water()
    {
        for (int i = 0; i < water.transform.childCount; i++)
        {
            if (water.transform.GetChild(i).gameObject.activeSelf)
            {
                return i;
            }
        }
        return water.transform.childCount;
    }

    public void TheEnd()
    {
        done = true;
    }

    public IEnumerator Death()
    {
        Destroy(inventory.GetComponent<Combat>());
        Animator animator = inventory.GetComponent<Animator>();
        animator.SetTrigger("Death");
        audioController.Play("Death");
        yield return new WaitForSeconds(1.5f);
        Destroy(animator);
        inventoryParent.parent.gameObject.SetActive(false);
        deathText.SetActive(true);
    }

}
