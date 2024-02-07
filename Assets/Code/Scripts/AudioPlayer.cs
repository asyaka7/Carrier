using Assets.Code.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    AudioSource audioSource;

    public static AudioPlayer Instance { get; private set; }

    public bool IsPlaying { get => audioSource.isPlaying; }

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
        Instance = this;
    }

    public void Play(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource?.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Audio clip is empty");
        }
    }
}
