using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteScript : MonoBehaviour
{
    public Button MusicButton;
    public Sprite OnMusicSprite;
    public Sprite OffMusicSprite;

    public Button SFXButton;
    public Sprite OnSFXSprite;
    public Sprite OffSFXSprite;

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
        if (MusicButton.image.sprite == OnMusicSprite)  //change sprite on music button if the active sprite is "OnMusicSprite"
        {
            MusicButton.image.sprite = OffMusicSprite;
            musicButtonOn = false;
        }
        else                                            //change sprite on music button if the active sprite isn't "OnMusicSprite"
        {
            MusicButton.image.sprite = OnMusicSprite;
            musicButtonOn = true;
        }
    }
    public void ChangeSFXButtonImage()
    {
        if (SFXButton.image.sprite == OnSFXSprite)       //change sprite on sfx button if the active sprite is "OnSFXSprite"
        {
            SFXButton.image.sprite = OffSFXSprite;
            sfxButtonOn = false;
        }
        else                                            //change sprite on sfx button if the active sprite isn't "OnSFXSprite"
        {
            SFXButton.image.sprite = OnSFXSprite;
            sfxButtonOn = true;
        }
    }
}
