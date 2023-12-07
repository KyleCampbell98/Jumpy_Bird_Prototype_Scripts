using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] gameSounds;
    private void Awake()
    {
        foreach(Sound sound in gameSounds)
        {
          sound.audioSourceRef = gameObject.AddComponent<AudioSource>(); // Adds an audio source to the audioManager for every sound in the array. We store this audio source as a variable so that we can reference it later on for playing the sound.
          sound.audioSourceRef.clip = sound.audioClip; // The audio source's clip will match that of the audio clip passed on to the sound element in the gameSOunds array.
          sound.audioSourceRef.volume = sound.volume;
          sound.audioSourceRef.pitch = sound.pitch;
          sound.audioSourceRef.loop = sound.isLooping;


        }
    }

    public void PlaySound (string nameOfSound)
    {
        Sound soundToPlay = Array.Find(gameSounds, sound => sound.SoundName == nameOfSound); // This line translates to, search the gameSounds array, and fins the sound, where sound.SoundName is equal to the name being passed into the method parameters. 
        if(soundToPlay == null) 
        { 
            Debug.LogError("AudioClip named: " + nameOfSound + ", could not be found, have you passed in the exact case sensitive name?"); 
            return; // Stops the method throwing a null reference error if an invalid name is passed into the string method parameters.
        } 
        soundToPlay.audioSourceRef.Play();
        
    }
}
