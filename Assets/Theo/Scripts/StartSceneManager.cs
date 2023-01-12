using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{

    [SerializeField]
    GameObject settingsMenu;

    [SerializeField]
    GameObject startMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            startMenu.SetActive(true);
        }
    }
}
