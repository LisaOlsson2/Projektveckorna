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
        if (musicCB.normalColor == NormalColor)     //�ndrar f�rg p� musik knappen ifall den �r gr�n (start f�rgen)
        {
            musicCB.normalColor = clickedNormalColor;
            musicCB.highlightedColor = clickedHighlightedColor;
            musicCB.pressedColor = clickedPressedColor;
            musicButton.colors = musicCB;
        }
        else    //�ndrar f�rg p� kanppen musik ifall den �r r�d (klickad f�rg)
        {
            musicCB.normalColor = NormalColor;
            musicCB.highlightedColor = HighlightedColor;
            musicCB.pressedColor = PressedColor;
            musicButton.colors = musicCB;
        }
        
    }
    public void ChangeSFXColor()       //�ndrar f�rg p� sfx knappen ifall den �r gr�n (start f�rgen)
    {
        ColorBlock sfxCB = sfxButton.colors;
        if (sfxCB.normalColor == NormalColor)
        {
            sfxCB.normalColor = clickedNormalColor;
            sfxCB.highlightedColor = clickedHighlightedColor;
            sfxCB.pressedColor = clickedPressedColor;
            sfxButton.colors = sfxCB;
        }
        else    //�ndrar f�rg p� sfx knappen ifall den �r r�d (klickad f�rg)
        {
            sfxCB.normalColor = NormalColor;
            sfxCB.highlightedColor = HighlightedColor;
            sfxCB.pressedColor = PressedColor;
            sfxButton.colors = sfxCB;
        }
        
    }
}
