using UnityEngine.Audio;
using System;
using UnityEngine;
//Mickael
[System.Serializable]
public class AudioController : MusicAndSound
{
    public Sound[] sounds;

    // Start is called before the first frame update
    private void Start()
    {
        play = ItKnows.sound;
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
        if (play)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Play();
        }
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
