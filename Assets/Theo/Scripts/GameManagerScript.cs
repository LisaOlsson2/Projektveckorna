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

    [SerializeField]
    GameObject gameUICanvas;

    public bool paused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !background.activeSelf && !pauseMenu.activeSelf)    //om man trycker på escape och ifall bakgrunden och pausmenyn är inaktiv 
        {
            PauseGame();
            background.SetActive(true);     //sätter på bakgrunden
            pauseMenu.SetActive(true);      //sätter på pausmenyn
            gameUICanvas.SetActive(false);  //stänger av game UI canvas

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && background.activeSelf && pauseMenu.activeSelf)     //om man trycker på escape och ifall bakgrunden och pausmenyn är aktiv
        {
            UnPauseGame();
            background.SetActive(false);
            pauseMenu.SetActive(false);
            gameUICanvas.SetActive(true);
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
