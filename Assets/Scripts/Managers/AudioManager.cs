using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Serializable]
    class SoundIDClipPair
    {
        public SoundID SoundID;
        public AudioClip AudioClip;
    }

    [SerializeField] private SoundIDClipPair[] sounds;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;

    private readonly Dictionary<SoundID, AudioClip> clips = new();

    private void Start()
    {
        foreach (var sound in sounds)
        {
            clips.Add(sound.SoundID, sound.AudioClip);
        }
    }

    private void PlayMusic(AudioClip audioClip, bool looping = true)
    {
        if (musicSource.isPlaying) return;

        musicSource.clip = audioClip;
        musicSource.loop = looping;
        musicSource.Play();
    }
    private void PlayEffect(AudioClip audioClip)
    {
        effectSource.PlayOneShot(audioClip);
    }

    public void PlayEffect(SoundID soundID)
    {
        if(soundID == SoundID.None) return;

        PlayEffect(clips[soundID]);
    }
}