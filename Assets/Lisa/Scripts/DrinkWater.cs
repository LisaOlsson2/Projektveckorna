using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWater : ChangeInventorySprite
{
    PlayerMovement playerMovement;
    public override void Start()
    {
        base.Start();
        playerMovement = GetComponent<PlayerMovement>();
    }
    public override void Do()
    {
        interactable = false;
        playerMovement.StopFatigue();
    }
}
