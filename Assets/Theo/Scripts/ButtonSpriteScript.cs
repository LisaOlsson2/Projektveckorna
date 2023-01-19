using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteScript : MonoBehaviour
{
    public Sprite OnMusicSprite;
    public Sprite OffMusicSprite;
    public Sprite OnSFXSprite;
    public Sprite OffSFXSprite;
    public Button MusicButton;
    public Button SFXButton;

    public bool musicButtonOn;
    public bool sfxButtonOn;


    void Start()
    {
        musicButtonOn = true;
        sfxButtonOn = true;
    }

    void Update()
    {
        
    }

    public void ChanegeMusicButtonImage()
    {
        if (MusicButton.image.sprite == OnMusicSprite)
        {
            MusicButton.image.sprite = OffMusicSprite;
            musicButtonOn = false;
        }
        else
        {
            MusicButton.image.sprite = OnMusicSprite;
            musicButtonOn = true;
        }
    }
    public void ChangeSFXButtonImage()
    {
        if (SFXButton.image.sprite == OnSFXSprite)
        {
            SFXButton.image.sprite = OffSFXSprite;
            sfxButtonOn = false;
        }
        else
        {
            SFXButton.image.sprite = OnSFXSprite;
            sfxButtonOn = true;
        }
    }
}
