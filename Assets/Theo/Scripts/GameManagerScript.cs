using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{   //Theo

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject settingsMenuIG;

    [SerializeField]
    GameObject background;

    public bool paused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !background.activeSelf && !pauseMenu.activeSelf)    //om man trycker p� escape och ifall bakgrunden och pausmenyn �r inaktiv 
        {
            PauseGame();
            background.SetActive(true);     //s�tter p� bakgrunden
            pauseMenu.SetActive(true);      //s�tter p� pausmenyn
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && background.activeSelf && pauseMenu.activeSelf)     //om man trycker p� escape och ifall bakgrunden och pausmenyn �r aktiv
        {
            UnPauseGame();
            background.SetActive(false);
            pauseMenu.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && background.activeSelf && settingsMenuIG.activeSelf)
        {
            settingsMenuIG.SetActive(false);
            pauseMenu.SetActive(true);
        }

    }

    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        paused = false;
        Time.timeScale = 1;
    }
}
