using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundM : MonoBehaviour
{
    public static SoundM _Sounds { get; private set; }
    private AudioSource audioSrc;

    public void Play(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }

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
