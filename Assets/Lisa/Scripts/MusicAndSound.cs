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

    //Button button;
    
    public void Change()
    {
        /*
        if (button == null)
        {
            button = image.GetComponent<Button>();
        }
        */

        if (image.sprite == enabledSprite)
        {
            play = false;
            image.sprite = disabledSprite;
            //SpriteState spriteState = new SpriteState();
            //spriteState.highlightedSprite = enabledSprite;
            //button.spriteState = spriteState;
        }
        else
        {
            play = true;
            image.sprite = enabledSprite;
            //SpriteState spriteState = new SpriteState();
            //spriteState.highlightedSprite = disabledSprite;
            //button.spriteState = spriteState;
        }
    }
}
