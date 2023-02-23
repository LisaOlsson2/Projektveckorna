using UnityEngine.Audio;
using System;
using UnityEngine;

public class MusicManager : MusicAndSound
{
    public MusicSound[] musicsound;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (MusicSound s in musicsound)//This is a music controller, voume, pitch and if it should loop
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        play = ItKnows.music;
        if (!play)
        {
            Change();
        }
    }

    public void Stop(string name)//this is to stop the music
    {
        MusicSound s = Array.Find(musicsound, musicsound => musicsound.name == name);
        if (s != null)
        {
            s.source.Stop();
        }
    }

    public void Play(string name)//this is to start playing one of the musics
    {
        if (play)
        {
            MusicSound s = Array.Find(musicsound, musicsound => musicsound.name == name);
            if (s != null)
            {
                s.source.Play();
            }
        }
    }
}
