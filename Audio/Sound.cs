using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string SoundName;

    public AudioClip audioClip; // The actual clip that will be played

    [Range(0, 1)] public float volume;
    [Range(0.1f, 3)] public float pitch;
    public bool isLooping;

    [HideInInspector] // We hide this object in the inspector as we are already populating this variable in the AudioManager script. Having this variable public and visable could lead to accidentally populating the field manually and breaking the code. 
    public AudioSource audioSourceRef;
}
