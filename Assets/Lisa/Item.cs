using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    ItKnows reference;

    bool interactable;

    GameObject item;

    readonly float distance = 150;

    static List<GameObject> inventory = new List<GameObject>();

    void Start()
    {
        reference = FindObjectOfType<ItKnows>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && interactable)
        {
            item = Instantiate(reference.emptyKinda, reference.canvas);
            item.GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
            item.GetComponent<RectTransform>().anchoredPosition += new Vector2(distance * inventory.Count, 0);
            inventory.Add(item);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = false;
    }
}