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

    public Button musicButton;
    public Button sfxButton;
    
    public void ChangeMusicColor()
    {
        ColorBlock musicCB = musicButton.colors;
        if (musicCB.normalColor == NormalColor)
        {
            musicCB.normalColor = clickedNormalColor;
            musicCB.highlightedColor = clickedHighlightedColor;
            musicCB.pressedColor = clickedPressedColor;
            musicButton.colors = musicCB;
        }
        else
        {
            musicCB.normalColor = NormalColor;
            musicCB.highlightedColor = HighlightedColor;
            musicCB.pressedColor = PressedColor;
            musicButton.colors = musicCB;
        }
        
    }
    public void ChangeSFXColor()
    {
        ColorBlock sfxCB = sfxButton.colors;
        if (sfxCB.normalColor == NormalColor)
        {
            sfxCB.normalColor = clickedNormalColor;
            sfxCB.highlightedColor = clickedHighlightedColor;
            sfxCB.pressedColor = clickedPressedColor;
            sfxButton.colors = sfxCB;
        }
        else
        {
            sfxCB.normalColor = NormalColor;
            sfxCB.highlightedColor = HighlightedColor;
            sfxCB.pressedColor = PressedColor;
            sfxButton.colors = sfxCB;
        }
        
    }
}
