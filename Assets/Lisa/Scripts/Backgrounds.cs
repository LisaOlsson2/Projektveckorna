using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrounds : BaseMostThings
{
    [SerializeField]
    PlayerMovement player;

    GameObject[] backgrounds;

    readonly float borderDistance = 18.63f;
    static int currentArea;

    readonly float distanceToChange = 1;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        backgrounds = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
        }

        backgrounds[currentArea].SetActive(true);
        transform.position += borderDistance * currentArea * Vector3.right;
    }


    private void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) > borderDistance/2 + distanceToChange)
        {
            NewArea((int)(Mathf.Abs(player.transform.position.x - transform.position.x)/(player.transform.position.x - transform.position.x)));
        }
    }

    void NewArea(int direction)
    {
        backgrounds[currentArea].SetActive(false);
        currentArea += direction;
        backgrounds[currentArea].SetActive(true);
        transform.position += direction * borderDistance * Vector3.right;
    }
}