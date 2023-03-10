using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lisa
public class Water : Interactive
{
    [SerializeField]
    Sprite filled;

    public Sprite[] sprites;

    public int current;

    public override void Start()
    {
        instructionChild = 3;
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(inventory.pickUp) && interactable && inventory.square.gameObject.activeSelf && inventory.CurrentSprite() == inventory.empty)
        {
            inventory.ChangeSprite(inventory.FindSprite(inventory.empty), filled);

            if (sprites.Length > 0)
            {
                spriteRenderer.sprite = sprites[current];
                current++;
                valueKeeper.blueberrys = current;

                if (current == sprites.Length)
                {
                    Destroy(this);
                }
            }
        }
    }
}
