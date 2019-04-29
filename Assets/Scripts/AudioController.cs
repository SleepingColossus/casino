using UnityEngine;

public class AudioController : MonoBehaviour
{
    public bool muteMusic;
    public bool muteSound;

    public AudioSource musicSource;
    public AudioSource[] soundSources;

    public AudioClip basicMatchSound;
    public AudioClip jackpotMatchSound;
    public AudioClip gameOverSound;
    private AudioSource _audioSource;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = basicMatchSound;
    }

    public void ToggleMusic()
    {
        muteMusic = !muteMusic;
        musicSource.mute = muteMusic;
    }

    public void ToggleSound()
    {
        muteSound = !muteSound;

        foreach (var soundSource in soundSources)
        {
            soundSource.mute = muteSound;
        }
    }

    public void PlayMatchSound()
    {
        _audioSource.Play();
    }

    public void SetMatchSound(SymbolType s)
    {
        _audioSource.clip = s == SymbolType.Dollar ? jackpotMatchSound : basicMatchSound;
    }

    public void PlayGameOverSound()
    {
        _audioSource.clip = gameOverSound;
        _audioSource.Play();
    }
}
