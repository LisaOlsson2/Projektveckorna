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
        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenu.activeSelf)    //när man trycker escape och settings menyn är aktiv
        {
            settingsMenu.SetActive(false);  //stänger settings menyn
            startMenu.SetActive(true);  //sätter på startmenyn
        }
    }
}
