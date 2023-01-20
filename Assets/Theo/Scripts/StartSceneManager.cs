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
        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenu.activeSelf)    //n�r man trycker escape och settings menyn �r aktiv
        {
            settingsMenu.SetActive(false);  //st�nger settings menyn
            startMenu.SetActive(true);  //s�tter p� startmenyn
        }
    }
}
