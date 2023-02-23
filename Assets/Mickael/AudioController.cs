using UnityEngine.Audio;
using System;
using UnityEngine;
//Mickael
/* Den här koden funkar som en lista med ljud. */
[System.Serializable]
public class AudioController : MusicAndSound
{
    public Sound[] sounds;

    void Awake()
    {
        // den här koden låter mig döpa, välja ljud clip, ändra volym, ändra pitch eller bestämma om ljudet loopar.
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        play = ItKnows.sound;
        if (!play)
        {
            Change();
        }
    }

    // Update is called once per frame
    // Den här koden om ljudet ska spelas letar den efter namnet på ljudet i listan och spelar den.
    public void Play (string name)
    {
        if (play)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Play();
        }
    }
    // Den här koden om ljudet ska spelas så letar den efter namnet på ljudet i listan och stoppar den.
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
