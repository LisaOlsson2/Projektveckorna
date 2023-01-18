using UnityEngine.Audio;
using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public MusicSound[] musicsound;


    // Start is called before the first frame update
    void Awake()
    {
        foreach (MusicSound s in musicsound)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Stop(string name)
    {
        MusicSound s = Array.Find(musicsound, musicsound => musicsound.name == name);
        s.source.Stop();
    }

    public void Play(string name)
    {
        MusicSound s = Array.Find(musicsound, musicsound => musicsound.name == name);
        s.source.Play();
    }
}
