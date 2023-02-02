using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicAndSound : MonoBehaviour
{
    public bool play;

    [SerializeField]
    Sprite enabledSprite;
    [SerializeField]
    Sprite disabledSprite;

    [SerializeField]
    Image image;

    public void Change()
    {
        if (image.sprite == enabledSprite)
        {
            play = false;
            image.sprite = disabledSprite;
        }
        else
        {
            play = true;
            image.sprite = enabledSprite;
        }
    }
}
