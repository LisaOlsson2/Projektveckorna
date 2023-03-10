using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Lisa
public class Item : Interactive
{
    [SerializeField]
    Sprite icon;

    [SerializeField]
    Sprite water;

    public override void Start()
    {
        instructionChild = 0;
        base.Start();
    }

    private void OnEnable()
    {
        Start();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(inventory.pickUp) && interactable) // if you pick it up while you're close enough
        {
            Vector3 raycastStartPos = new Vector3(Input.mousePosition.x - Screen.width / 2f, Input.mousePosition.y - Screen.height / 2f) * (18.63f / Screen.width);
            Ray ray = new Ray(raycastStartPos + inventory.cam.transform.position, Vector3.forward);
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);
            if (hitInfo.transform != null && hitInfo.transform.parent == transform)
            {
                PickUp();
            }
        }
    }


    void PickUp()
    {

        if (inventory.inventory.Count == 0) // if the inventory is empty
        {
            inventory.square.gameObject.SetActive(true); // make the lil thingy that shows where you are appear
        }

        inventory.AddItem(icon);

        if (transform.parent.name != "Water") // if you pick the item up from anything that isn't the fire place
        {
            Destroy(gameObject);
        }
        else // if you pick the item up from the fireplace
        {
            transform.parent.GetComponent<MoreCrafting>().ChangeSprite(); // changes the sprite of the fireplace to the one it has when it's empty
            gameObject.SetActive(false);

            if (transform.GetSiblingIndex() > 2)
            {
                valueKeeper.waiting[transform.GetSiblingIndex() - 3] = false;
                spriteRenderer.sprite = water;
                GetComponent<Cooking>().enabled = true;
                this.enabled = false;
            }
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