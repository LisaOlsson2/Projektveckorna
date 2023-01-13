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
            pauseMenu.SetActive(true);      //s�tter op pausmenyn
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

        if (paused)
        {
            Time.timeScale = 0;     //pausar tiden
            Debug.Log("time scale 0");
        }
        else
        {
            Time.timeScale = 1;     //startar tiden
            Debug.Log("time scale 1");
        }
    }

    public void PauseGame()
    {
        paused = true;
    }
    public void UnPauseGame()
    {
        paused = false;
    }
}
