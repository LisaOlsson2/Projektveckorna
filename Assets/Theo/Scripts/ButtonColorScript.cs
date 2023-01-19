using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorScript : MonoBehaviour
{   //Theo
    public Color NormalColor;
    public Color HighlightedColor;
    public Color PressedColor;

    public Color clickedNormalColor;
    public Color clickedHighlightedColor;
    public Color clickedPressedColor;

    public bool musicButtonGreen;
    public bool sfxButtonGreen;

    public Button musicButton;
    public Button sfxButton;

    private void Start()
    {
        musicButtonGreen = true;
        sfxButtonGreen = true;
    }

    public void ChangeMusicColor()
    {
        ColorBlock musicCB = musicButton.colors;
        if (musicCB.normalColor == NormalColor)     //ändrar färg på musik knappen ifall den är grön (start färgen)
        {
            musicCB.normalColor = clickedNormalColor;
            musicCB.highlightedColor = clickedHighlightedColor;
            musicCB.pressedColor = clickedPressedColor;
            musicButton.colors = musicCB;
            musicButtonGreen = false;
        }
        else    //ändrar färg på kanppen musik ifall den är röd (klickad färg)
        {
            musicCB.normalColor = NormalColor;
            musicCB.highlightedColor = HighlightedColor;
            musicCB.pressedColor = PressedColor;
            musicButton.colors = musicCB;
            musicButtonGreen = true;
        }
        
    }
    public void ChangeSFXColor()       //ändrar färg på sfx knappen ifall den är grön (start färgen)
    {
        ColorBlock sfxCB = sfxButton.colors;
        if (sfxCB.normalColor == NormalColor)
        {
            sfxCB.normalColor = clickedNormalColor;
            sfxCB.highlightedColor = clickedHighlightedColor;
            sfxCB.pressedColor = clickedPressedColor;
            sfxButton.colors = sfxCB;
            sfxButtonGreen = false;
        }
        else    //ändrar färg på sfx knappen ifall den är röd (klickad färg)
        {
            sfxCB.normalColor = NormalColor;
            sfxCB.highlightedColor = HighlightedColor;
            sfxCB.pressedColor = PressedColor;
            sfxButton.colors = sfxCB;
            sfxButtonGreen = true;
        }
        
    }
}
