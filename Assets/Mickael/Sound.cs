using UnityEngine.Audio;
using UnityEngine;
// Mickael
// den h�r koden f�r listan att synas i unity
[System.Serializable]
public class Sound
// Den h�r koden �r sj�lva listan.
{
    // Namn p� ljudet
    public string name;
    // Ljud clipet
    public AudioClip clip;
    // den h�r koden �ndrar volymen fr�n 0 till 1
    [Range(0f, 1f)]
    public float volume;
    // den h�r koden �ndrar pitchen fr�n 1 till 3
    [Range(.1f, 3f)]
    public float pitch;
    // den h�r koden loopar ljudet
    public bool loop;
    
    [HideInInspector]
    public AudioSource source;
}
