using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Uses Unity's Audio Source to play first an introductory audio track, then a looping song track afterwards.
/// </summary>
public class AudioLooper : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip introClip;
    public AudioClip loopClip;

    public bool playOnAwake;

    void Start()
    {
        // Force an audio source if one has not been assigned nor is even attached to this GameObject
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
            if (!audioSource)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.spatialBlend = 0f;
            }
        }

        if (playOnAwake)
        {
            Play();
        }
    }

    public void Play()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = loopClip;
            audioSource.loop = true;
            if (introClip)
            {
                double duration = (double)introClip.samples / introClip.frequency;
                audioSource.PlayOneShot(introClip);
                audioSource.PlayScheduled(AudioSettings.dspTime + duration);
            }
            else
            {
                audioSource.Play();
            }
        }
    }
}
