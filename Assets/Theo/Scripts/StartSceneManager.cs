using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{   //Theo

    [SerializeField]
    GameObject settingsMenu;

    [SerializeField]
    GameObject startMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenu.activeSelf)    //n�r man trycker escape och settings menyn �r aktiv
        {
            settingsMenu.SetActive(false);  //st�nger settings menyn
            startMenu.SetActive(true);  //s�tter p� startmenyn
        }
    }
}
