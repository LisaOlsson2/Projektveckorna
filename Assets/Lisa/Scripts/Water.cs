using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : ChangeInventorySprite
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = false;
    }
}
