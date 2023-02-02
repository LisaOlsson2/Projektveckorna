using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
// things you can interact with, currently just things you can pick up and craft
public class Interactive : BaseMostThings
{
    public Inventory inventory;
    public SpriteRenderer spriteRenderer;
    public bool interactable;

    public int instructionChild;
    static int amount;
    public GameObject instructions;
    Text recipe;
    public string recipe2 = ":0";
    [SerializeField]
    Vector2 pixelDistance;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        inventory = FindObjectOfType<Inventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        instructions = inventory.instructions.GetChild(instructionChild).gameObject;
        recipe = instructions.GetComponent<Text>();
    }

    public virtual void Update()
    {
        if (instructions.activeSelf && interactable && amount < 2)
        {
            InactivateOthers(true);
        }
    }

    void InactivateOthers(bool mine)
    {
        for (int i = 0; i < inventory.instructions.childCount; i++)
        {
            if (inventory.instructions.GetChild(i).gameObject.activeSelf)
            {
                inventory.instructions.GetChild(i).gameObject.SetActive(false);
            }
        }
        instructions.SetActive(mine);

        if (mine)
        {
            inventory.instructions.anchoredPosition = new Vector2(transform.position.x - inventory.cam.position.x, transform.position.y - inventory.cam.position.y) * (Screen.width / 18.63f) + pixelDistance;
            if (instructionChild == 2)
            {
                recipe.text = recipe2;
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!interactable)
        {
            interactable = true;
            InactivateOthers(true);
            amount++;
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (interactable)
        {
            interactable = false;
            amount--;
            if (amount <= 0)
            {
                amount = 0;
                InactivateOthers(false);
            }
        }
    }
}