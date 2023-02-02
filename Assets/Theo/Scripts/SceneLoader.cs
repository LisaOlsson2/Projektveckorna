using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{   //Theo
    public void LoadGame()
    {
        SceneManager.LoadScene("Save1");   //laddar scenen efter den som �r aktiv
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Cutscene");   //laddar scenen efter den som �r aktiv
    }
    public void QuitGame()
    {
        Application.Quit();     //st�nger spelet
        Debug.Log("Quit!");
    }
}
