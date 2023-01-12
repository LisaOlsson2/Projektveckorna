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
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !background.activeSelf && !pauseMenu.activeSelf)
        {
            PauseGame();
            background.SetActive(true);
            pauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && background.activeSelf && pauseMenu.activeSelf)
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
            Time.timeScale = 0;     
            Debug.Log("time scale 0");
        }
        else
        {
            Time.timeScale = 1;
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
