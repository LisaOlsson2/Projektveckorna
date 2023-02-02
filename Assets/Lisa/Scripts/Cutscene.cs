using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    readonly float duration = (8 + 15)/12f;

    void Start()
    {
        StartCoroutine(Wait());
    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("Save1");
    }
}
