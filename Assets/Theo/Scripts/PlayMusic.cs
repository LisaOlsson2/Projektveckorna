using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    MusicSound musicSound;
    ButtonSpriteScript buttonSpriteScript;

    void Start()
    {
        musicSound = GetComponent<MusicSound>();
        buttonSpriteScript = GetComponent<ButtonSpriteScript>();
    }

    public void PLAYMusic()
    {
        if (buttonSpriteScript.musicButtonOn)
        {
            musicSound.volume = 1;
        }
        else if (!buttonSpriteScript.musicButtonOn)
        {
            musicSound.volume = 0;
        }
    }
}
