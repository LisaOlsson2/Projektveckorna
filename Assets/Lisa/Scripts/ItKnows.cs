using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Lisa
// this is the valuekeeper
public class ItKnows : MonoBehaviour
{
    ItKnows[] inEventOfTwo; // every time you enter the scene after the first time
    Inventory inventory; // the inventory script attached to the player

    // saves crafted items
    public Crafting[] allCraftingGhosts; // array of everything that can be crafted, the crafting scripts get destroyed when something gets crafted
    public bool[] itemsCrafted; // true if the craftingghost in the same place is null
    Sprite[][] materials; // each craftingghost has a sprite array of materials
    int[][] materialAmounts; // and an int array of amounts
    int waterChild; // active child in the fireplace. if none, this is the count of children in the fireplace
    [SerializeField]
    GameObject water; // the fireplace

    // saves health
    public int health;

    // saves inventory
    public List<Sprite> inventorySprites = new List<Sprite>(); // things in the inventory
    public List<int> amounts = new List<int>(); // how many of each of the things in the inventory

    // saves things picked up
    [SerializeField]
    GameObject itemsParent; // the parent of all things that can be picked up
    GameObject[] allItems; // stores the children of the itemsparent
    public bool[] itemsPickedUp; // true if the same place in allitems is null (has been picked up)

    // saves position
    public Vector3 position;

    // saves endings
    public bool dead;
    public bool done;


    public AudioController audioController;

    [SerializeField]
    Transform inventoryParent;
    
    [SerializeField]
    GameObject emptyKinda; // gameobject for the inventory, it gets a sprite when it gets instantiated

    [SerializeField]
    Button[] exitButtons;

    [SerializeField]
    Sprite death;

    [SerializeField]
    GameObject deathText; // deathscreen

    [SerializeField]
    GameObject theEnd; // congratilationscreen

    public readonly float distanceInventory = 575;

    public PlayerMovement player; // used by other scripts to make things happen in the player script

    void Start()
    {
        inEventOfTwo = FindObjectsOfType<ItKnows>();
        inventory = FindObjectOfType<Inventory>();
        audioController = FindObjectOfType<AudioController>();
        allItems = new GameObject[itemsParent.transform.childCount];
        player = inventory.GetComponent<PlayerMovement>();

        for (int i = 0; i < itemsParent.transform.childCount; i++) // add the children to the array
        {
            allItems[i] = itemsParent.transform.GetChild(i).gameObject;
        }

        if (inEventOfTwo.Length == 1) // if this is a new save
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
            inEventOfTwo[other].exitButtons = exitButtons;
            inEventOfTwo[other].inventory = inventory;
            inEventOfTwo[other].allCraftingGhosts = allCraftingGhosts;
            inEventOfTwo[other].deathText = deathText;
            inEventOfTwo[other].theEnd = theEnd;
            inEventOfTwo[other].water = water;
            inEventOfTwo[other].player = player;

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
        foreach(Button exit in exitButtons)
        {
            exit.onClick.AddListener(Exit);
        }

        for (int i = inventory.cheese.Length - 1; i > health - 1; i--)
        {
            // if this cheese has been lost, inactivate it
            inventory.cheese[i].SetActive(false);
        }

        for (int i = 0; i < allItems.Length; i++)
        {
            if (itemsPickedUp[i]) // if this item has been picked up
            {
                Destroy(allItems[i]); // destroy it
            }
        }
        for (int i = 0; i < allCraftingGhosts.Length; i++)
        {
            if (itemsCrafted[i]) // if this thing has been crafted
            {
                allCraftingGhosts[i].spriteRenderer = allCraftingGhosts[i].GetComponent<SpriteRenderer>(); // so it can craft before its start has been called
                allCraftingGhosts[i].Craft(); // crafts the thing
            }
            else // if it hasn't been crafted
            {
                allCraftingGhosts[i].materials = materials[i]; // make the materials left the same as when the scene was exited
                allCraftingGhosts[i].amounts = materialAmounts[i]; // same, but with the amount of materials left
            }
        }

        inventory.inventory = inventorySprites; // gives the player its old inventory back

        if (inventory.inventory.Count > 0) // if the inventory isn't empty
        {
            inventory.square.gameObject.SetActive(true);

            for (int i = 0; i < inventorySprites.Count; i++)
            {
                AddItem(i);
            }

        }

        if (itemsCrafted[0]) // if the fire place has been crafted
        {
            if (waterChild < water.transform.childCount) // if there's a pot in it
            {
                water.GetComponent<SpriteRenderer>().sprite = water.GetComponent<MoreCrafting>().other;
                water.transform.GetChild(waterChild).gameObject.SetActive(true);
            }
        }

        inventory.transform.position = position; // places the player where it was

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
        GameObject item = Instantiate(emptyKinda, inventoryParent); // instantiates an object
        item.GetComponent<Image>().sprite = inventory.inventory[place]; // changes the sprite of the object to the sprite in its place in the list of sprites
        item.GetComponentInChildren<Text>().text = amounts[place] + ""; // changes the text of the child of the object to the amount ni the inventory of this material
        item.GetComponent<RectTransform>().localPosition += new Vector3(distanceInventory * place, 0, 0); // moves it to its place in the inventory
        inventory.inventoryUI.Add(item); // adds the object to the inventory gameobject list
    }
    
    public void Exit() // saves progress before exiting
    {
        for (int i = 0; i < allItems.Length; i++)
        {
            if (allItems[i] == null) // if the gameobject has been destroyed (picked up)
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
        
        if (itemsCrafted[0]) // if the fireplace has been crafted
        {
            waterChild = Water(); // check if any children of the fireplace are active, if they are, waterchild becomes the active one, otherwise it's the amount of children
        }

        SceneManager.LoadScene("StartScene");
    }

    int Water() // retruns the index of the active child of the fireplace
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
        inventoryParent.parent.gameObject.SetActive(false); // inactivate the usual canvas
        theEnd.SetActive(true); // activate the canvas with the congratulationscreen
    }

    public IEnumerator Death()
    {
        Destroy(inventory.GetComponent<Combat>()); // destroys the combat script, the movement script will already be disabled
        Animator animator = inventory.GetComponent<Animator>(); // reference to the players animator
        animator.SetTrigger("Death"); // starts the death animation
        audioController.Play("Death"); // plays the death sound
        yield return new WaitForSeconds(18/12f); // the death animation is 18 frames and plays at a framerate of 12 fps, so it just waits until it's done
        Destroy(animator);
        inventoryParent.parent.gameObject.SetActive(false); // inactivates the usual canvas
        deathText.SetActive(true); // activates the deathscreen canvas
    }

}
