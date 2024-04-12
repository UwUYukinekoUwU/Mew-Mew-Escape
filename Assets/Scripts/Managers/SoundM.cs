using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager responsible for playing sound. Doesn't get destroyed on scene load.
/// </summary>
public class SoundM : MonoBehaviour
{
    public static SoundM _Sounds { get; private set; }
    private AudioSource audioSrc;

    /// <summary>
    /// Plays the clip passed to it.
    /// </summary>
    public void Play(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }

    /// <summary>
    /// Resets all values of this manager to default.
    /// </summary>
    public static void ResetManager()
    {     

    }

    private SoundM() {}
   
    public void Awake()
    {
        if (_Sounds != null)
            return;

        audioSrc = GetComponent<AudioSource>();
        _Sounds = this;
    }


}
