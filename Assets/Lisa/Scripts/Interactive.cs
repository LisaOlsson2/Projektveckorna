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
    public GameObject instructions;
    [SerializeField]
    Vector2 pixelDistance;

    static string currentName;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        inventory = FindObjectOfType<Inventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        instructions = inventory.instructions.GetChild(instructionChild).gameObject;
    }

    public virtual void Update()
    {
        if (interactable && currentName != gameObject.name)
        {
            InactivateOthers(true);
        }
    }

    public void InactivateOthers(bool mine)
    {
        if ((instructionChild == 0 && Backgrounds.currentArea == 0) || instructionChild > 0)
        {
            for (int i = 0; i < inventory.instructions.childCount; i++)
            {
                if (inventory.instructions.GetChild(i).gameObject.activeSelf)
                {
                    if (i == 2)
                    {
                        for (int i2 = 0; i2 < inventory.instructions.GetChild(i).childCount; i2++)
                        {
                            if (inventory.instructions.GetChild(i).GetChild(i2).gameObject.activeSelf)
                            {
                                inventory.instructions.GetChild(i).GetChild(i2).gameObject.SetActive(false);
                            }
                        }
                    }
                    inventory.instructions.GetChild(i).gameObject.SetActive(false);
                }
            }
            instructions.SetActive(mine);


            if (mine)
            {
                currentName = gameObject.name;
                inventory.instructions.anchoredPosition = new Vector2(transform.position.x - inventory.cam.transform.position.x, transform.position.y - inventory.cam.transform.position.y) * (Screen.width / 18.63f) + pixelDistance;
                if (instructionChild == 2)
                {
                    GetComponent<Materials>().UpdateText(true);
                }
            }
        }
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!interactable)
        {
            interactable = true;
            InactivateOthers(true);

            if (this.GetType() == typeof(Item) && transform.childCount > 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (interactable)
        {
            interactable = false;
            if (currentName == gameObject.name)
            {
                InactivateOthers(false);
            }
            if (this.GetType() == typeof(Item) && transform.childCount > 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}