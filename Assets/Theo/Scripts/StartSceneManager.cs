using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{   //Theo

    [SerializeField]
    GameObject settingsMenu;

    [SerializeField]
    GameObject startMenu;

    [SerializeField]
    Button clear;

    [SerializeField]
    GameObject load;

    private void Start()
    {
        if (GameObject.Find("The Allmighty") != null)
        {
            clear.interactable = true;
            GameObject.Find("New Save").SetActive(false);
            load.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenu.activeSelf)    //när man trycker escape och settings menyn är aktiv
        {
            settingsMenu.SetActive(false);  //stänger settings menyn
            startMenu.SetActive(true);  //sätter på startmenyn
        }
    }
}
