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
        if (Input.GetKeyDown(inventory.pickUp) && interactable)
        {
            if (inventory.inventory.Count == 0)
            {
                inventory.square.gameObject.SetActive(true);
            }

            int place = inventory.FindSprite(icon);

            if (place == inventory.inventory.Count)
            {

                valueKeeper.amounts.Add(0);
                inventory.inventory.Add(icon);
                valueKeeper.AddItem(inventory.FindSprite(icon));
            }

            valueKeeper.amounts[place]++;
            inventory.inventoryUI[place].GetComponentInChildren<Text>().text = "" + valueKeeper.amounts[place];
            if (transform.parent == null || transform.parent.name != "Water")
            {
                Destroy(gameObject);
            }
            else
            {
                transform.parent.GetComponent<MoreCrafting>().ChangeSprite();
                gameObject.SetActive(false);
            }

            foreach (Sound sound in audioController.sounds)
            {
                if (sound.name == gameObject.tag)
                {
                    audioController.Play(gameObject.tag);
                }
            }
        }
    }
}