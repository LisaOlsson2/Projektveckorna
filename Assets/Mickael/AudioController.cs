using UnityEngine.Audio;
using System;
using UnityEngine;
//Mickael
[System.Serializable]
public class AudioController : MonoBehaviour
{
    public bool soundOn;
    public bool sfxOn;

    ButtonColorScript buttonColorScript;

    public Sound[] sounds;

    // Start is called before the first frame update
    private void Start()
    {
        buttonColorScript = GetComponent<ButtonColorScript>();

        soundOn = true;
        sfxOn = true;
    }

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    // Update is called once per frame
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    public void PlaySound()
    {
        if (buttonColorScript.sfxButtonGreen)
        {
            soundOn = true;
        }
        else if (!buttonColorScript.sfxButtonGreen)
        {
            soundOn = false;
        }
    }
}
