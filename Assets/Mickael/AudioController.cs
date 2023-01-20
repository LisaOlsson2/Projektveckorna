using UnityEngine.Audio;
using System;
using UnityEngine;
//Mickael
[System.Serializable]
public class AudioController : MonoBehaviour
{
    public bool sfxOn;

    ButtonSpriteScript buttonSpriteScript;

    public Sound[] sounds;

    float[] volumes;

    // Start is called before the first frame update
    private void Start()
    {
        buttonSpriteScript = GetComponent<ButtonSpriteScript>();

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
        volumes = new float[sounds.Length];

        for (int i = 0; i < volumes.Length; i++)
        {
            volumes[i] = sounds[i].volume;
        }
    }

    public void NoSound()
    {
        foreach(Sound sound in sounds)
        {
            sound.volume = 0;
        }
    }

    public void Sound()
    {
        for (int i = 0; i < volumes.Length; i++)
        {
            sounds[i].volume = volumes[i];
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
    public void PlaySfx()
    {
        if (buttonSpriteScript.sfxButtonOn)
        {
            sfxOn = true;
        }
        else if (!buttonSpriteScript.sfxButtonOn)
        {
            sfxOn = false;
        }
    }
}
