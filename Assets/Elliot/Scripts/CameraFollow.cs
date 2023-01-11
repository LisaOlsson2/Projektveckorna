using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;// kan välja kamera offset från 1 - 10 
    [Range(1, 10)]
    public float smoothFactor;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        Follow();
    }

    // Update is called once per frame
    void Follow()

    {
        Vector3 targetPosision = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosision, smoothFactor * Time.fixedDeltaTime);
        transform.position = targetPosision;
    }
}
