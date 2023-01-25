using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Interactive
{
    private void Update()
    {
        if (Input.GetKeyDown(inventory.pickUp) && interactable && inventory.square.gameObject.activeSelf && inventory.CurrentSprite() == inventory.empty)
        {
            inventory.ChangeSprite(inventory.FindSprite(inventory.empty), inventory.dirty);
        }
    }
}
