using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrounds : MonoBehaviour
{
    [SerializeField]
    PlayerMovement player;

    GameObject[] backgrounds;

    float currentRightBorder;
    float currentLeftBorder;
    static int currentArea;
    float distance;

    //[{area}, borders]
    readonly float[,] borders = { { -27.945f, 27.945f}, { 27.945f, 50} };

    readonly float distanceToChange = 1;
    readonly float halfCamWorldspace = 9.315f;

    // Start is called before the first frame update
    void Start()
    {
        backgrounds = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
        }

        backgrounds[currentArea].SetActive(true);
        currentLeftBorder = borders[currentArea, 0];
        currentRightBorder = borders[currentArea, 1];
    }


    private void Update()
    {
        distance = player.transform.position.x - transform.position.x;

        if ((transform.position.x < currentRightBorder - halfCamWorldspace && transform.position.x > currentLeftBorder + halfCamWorldspace) || (player.transform.position.x > currentLeftBorder + halfCamWorldspace && player.transform.position.x < currentRightBorder - halfCamWorldspace))
        {
            transform.position += new Vector3(distance, 0, 0) * player.baseSpeed * Time.deltaTime;
        }
        else
        {
            if (player.transform.position.x > currentRightBorder + distanceToChange)
            {
                NewArea(1);
            }
            if (player.transform.position.x < currentLeftBorder - distanceToChange)
            {
                NewArea(-1);
            }
        }

    }

    void NewArea(int direction)
    {
        backgrounds[currentArea].SetActive(false);
        currentArea += direction;
        backgrounds[currentArea].SetActive(true);
        currentLeftBorder = borders[currentArea, 0];
        currentRightBorder = borders[currentArea, 1];
        transform.position += Vector3.right * halfCamWorldspace * 2 * direction;
    }
}
