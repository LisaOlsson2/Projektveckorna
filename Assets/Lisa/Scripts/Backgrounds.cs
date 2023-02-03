using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lisa
public class Backgrounds : BaseMostThings
{
    [SerializeField]
    GameObject player;

    MusicManager musicManager;

    readonly float borderDistance = 18.63f; // distance between areas, pretty much the same as the width of the background, this is also added to the cams position when the player moves to a new area
    public static int currentArea; // this is static so that the cam can be positioned where it was before the game scene was left

    readonly float distanceToChange = 1; // how far the player has to walk outside the frame to change area

    public override void Start()
    {
        base.Start(); // makes a reference to the valueKeeper

        musicManager = FindObjectOfType<MusicManager>(); // reference to Enricos musicManager

        musicManager.Play("music" + (currentArea + 1)); // plays the music for the current area
        transform.position += borderDistance * currentArea * Vector3.right; // positions the cam in the area it was in last time
    }

    private void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) > borderDistance/2 + distanceToChange) // if the player goes further than a predetermined distance beyond half of about the width of the cam
        {
            NewArea((int)(Mathf.Abs(player.transform.position.x - transform.position.x)/(player.transform.position.x - transform.position.x))); // divides the absolute value of the distance with the actual distance to use either 1 or -1
        }
    }

    private void NewArea(int direction) // the direction is either 1 or -1 and determines if the cam should move left or right, it's also added to the current area
    {
        musicManager.Stop("music" + (currentArea + 1)); // stops the current music
        currentArea += direction; // updates the current area
        transform.position += direction * borderDistance * Vector3.right; // moves the cam
        musicManager.Play("music" + (currentArea + 1)); // plays the new current music
    }
}