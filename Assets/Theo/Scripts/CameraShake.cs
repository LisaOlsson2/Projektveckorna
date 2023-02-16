using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{   //Theo
    public Vector2 amplitude;
    public Vector2 frequency;
    Vector2 time = Vector2.zero;
    static bool shouldShake;
    public static float shakeTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (false) //take damage
        {
            shakeTime = 0.25f;
        }
        Vector3 shakePos = transform.localPosition;     //shakePos �r kamerans position
        if (shakeTime > 0)
        {
            shouldShake = true;
            shakeTime -= Time.deltaTime;    //skak tiden blir mindre medans shaketime �r mer �n 0
        }
        else
        {
            shouldShake = false;
        }
        if (shouldShake == true)
        {
            time.x += frequency.x * Time.deltaTime;     //dest� mer tid som g�r dest� mer skakar det
            shakePos.x = Mathf.Sin(time.x) * amplitude.x;   //skakar med hj�lp av sinus kurvan skapad med time.x
        }
        transform.localPosition = shakePos;
    }
}