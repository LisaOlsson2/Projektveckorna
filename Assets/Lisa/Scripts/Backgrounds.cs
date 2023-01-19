using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrounds : BaseMostThings
{
    [SerializeField]
    GameObject player;

    MusicManager musicManager;

    readonly float borderDistance = 18.63f;
    static int currentArea;

    readonly float distanceToChange = 1;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        musicManager = FindObjectOfType<MusicManager>();

        musicManager.Play("music" + (currentArea + 1));
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
        musicManager.Stop("music" + (currentArea + 1));
        currentArea += direction;
        transform.position += direction * borderDistance * Vector3.right;
        musicManager.Play("music" + (currentArea + 1));
    }
}
