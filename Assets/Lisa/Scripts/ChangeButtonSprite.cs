using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonSprite : MonoBehaviour
{
    [SerializeField]
    Sprite sprite1;
    [SerializeField]
    Sprite sprite2;

    Image image;
    Button button;

    private void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void OnClickSound()
    {
        if (image.sprite == sprite1)
        {
            FindObjectOfType<AudioController>().NoSound();
            image.sprite = sprite2;

            SpriteState spriteState = new SpriteState();
            spriteState.highlightedSprite = sprite1;
            button.spriteState = spriteState;
            
        }
        else
        {
            FindObjectOfType<AudioController>().Sound();
            image.sprite = sprite1;

            SpriteState spriteState = new SpriteState();
            spriteState.highlightedSprite = sprite2;
            button.spriteState = spriteState;
        }
    }

    public void OnClickMusic()
    {
        if (image.sprite == sprite1)
        {
            FindObjectOfType<MusicManager>().NoSound();
            image.sprite = sprite2;

            SpriteState spriteState = new SpriteState();
            spriteState.highlightedSprite = sprite1;
            button.spriteState = spriteState;
        }
        else
        {
            FindObjectOfType<MusicManager>().Sound();
            image.sprite = sprite1;

            SpriteState spriteState = new SpriteState();
            spriteState.highlightedSprite = sprite2;
            button.spriteState = spriteState;
        }
    }
}
