using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteController : MonoBehaviour
{
    public bool muteMusic;
    public bool muteSound;

    private AudioSource _musicSource;
    public AudioSource[] _soundSources;

    public void Start()
    {
        _musicSource = GetComponent<AudioSource>();
    }
    

    public void ToggleMusic()
    {
        muteMusic = !muteMusic;
        _musicSource.mute = muteMusic;
    }

    public void ToggleSound()
    {
        muteSound = !muteSound;

        foreach (var soundSource in _soundSources)
        {
            soundSource.mute = muteSound;
        }
    }
}
