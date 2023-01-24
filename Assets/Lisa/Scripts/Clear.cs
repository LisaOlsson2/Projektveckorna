using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lisa, might change this script later
public class Clear : MonoBehaviour
{
    [SerializeField]
    GameObject newGame;

    public void ClearSave()
    {
        Destroy(GameObject.Find("The Allmighty"));
        GameObject.Find("Load Save").SetActive(false);
        newGame.SetActive(true);
        GetComponent<Button>().interactable = false;
    }
}
