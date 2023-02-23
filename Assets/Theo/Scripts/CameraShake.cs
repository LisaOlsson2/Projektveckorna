using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{   //Theo

    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Camera cam = GetComponent<Camera>();
        float size = cam.orthographicSize;

        Vector3 pos = transform.position;
        cam.orthographicSize = 4.5f;

        for (int i = 0; i < 6; i++)
        {
            transform.position += Vector3.right * Random.Range(-1f, 1f) + Vector3.up * Random.Range(-1f, 1f);
            yield return new WaitForSeconds(0.07f);
            transform.position = pos;
            yield return new WaitForSeconds(0.07f);
        }

        cam.orthographicSize = size;
    }
}