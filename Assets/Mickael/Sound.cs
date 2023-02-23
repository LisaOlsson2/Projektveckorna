using UnityEngine.Audio;
using UnityEngine;
// Mickael
// den här koden får listan att synas i unity
[System.Serializable]
public class Sound
// Den här koden är själva listan.
{
    // Namn på ljudet
    public string name;
    // Ljud clipet
    public AudioClip clip;
    // den här koden ändrar volymen från 0 till 1
    [Range(0f, 1f)]
    public float volume;
    // den här koden ändrar pitchen från 1 till 3
    [Range(.1f, 3f)]
    public float pitch;
    // den här koden loopar ljudet
    public bool loop;
    
    [HideInInspector]
    public AudioSource source;
}
