using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    Inventory inventory;
    SpriteRenderer spriteRenderer;

    readonly float distanceToCraft = 2;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Mathf.Abs(inventory.transform.position.x - transform.position.x) < distanceToCraft)
        {
            spriteRenderer.color = new Vector4(1, 1, 1, 0.7f);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                inventory.UseItem(spriteRenderer.sprite);
                spriteRenderer.color = new Vector4(1, 1, 1, 1);
                gameObject.AddComponent<BoxCollider2D>();
                Destroy(this);
            }
        }
        else
        {
            spriteRenderer.color = new Vector4(1, 1, 1, 0.4f);
        }
    }
}
