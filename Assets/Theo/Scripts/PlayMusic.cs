using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    MusicSound musicSound;
    ButtonColorScript buttonColorScript;

    void Start()
    {
        musicSound = GetComponent<MusicSound>();
        buttonColorScript = GetComponent<ButtonColorScript>();
    }

    public void PLAYMusic()
    {
        if (buttonColorScript.musicButtonGreen)
        {
            musicSound.volume = 1;
        }
        else if (!buttonColorScript.musicButtonGreen)
        {
            musicSound.volume = 0;
        }
    }
}
