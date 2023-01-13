using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{   //Theo
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //laddar scenen efter den som är aktiv
    }
    public void QuitGame()
    {
        Application.Quit();     //stänger spelet
        Debug.Log("Quit!");
    }
}
