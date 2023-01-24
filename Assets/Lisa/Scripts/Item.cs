using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa
public class Item : Interactive
{
    [SerializeField]
    Sprite icon;

    void Update()
    {
        if (Input.GetKeyDown(inventory.pickUp) && interactable) // if you pick it up while you're close enough
        {
            if (inventory.inventory.Count == 0) // if the inventory is empty
            {
                inventory.square.gameObject.SetActive(true); // make the lil thingy that shows where you are appear
            }

            int place = inventory.FindSprite(icon); // the place of this item in the inventory list

            if (place == inventory.inventory.Count) // if it doesn't exist yet
            {
                valueKeeper.amounts.Add(0); // add a new int to the list with amounts in the valuekeeper, it gets increased later
                inventory.inventory.Add(icon); // add it to the inventory
                valueKeeper.AddItem(inventory.FindSprite(icon)); // add it in the UI
            }

            valueKeeper.amounts[place]++; // increase the amount of this item in the inventory
            inventory.inventoryUI[place].GetComponentInChildren<Text>().text = "" + valueKeeper.amounts[place]; // change the number to the new amount
            if (transform.parent.name != "Water") // if you pick the item up from anything that isn't the fire place
            {
                Destroy(gameObject);
            }
            else // if you pick the item up from the fireplace
            {
                transform.parent.GetComponent<MoreCrafting>().ChangeSprite(); // changes the sprite of the fireplace to the one it has when it's empty
                gameObject.SetActive(false);
            }

            foreach (Sound sound in valueKeeper.audioController.sounds) // goes through mickaels audiocontroller and checks if there's a sound that matches the tag of this item
            {
                if (sound.name == gameObject.tag)
                {
                    valueKeeper.audioController.Play(gameObject.tag);
                    break;
                }
            }
        }
    }
}