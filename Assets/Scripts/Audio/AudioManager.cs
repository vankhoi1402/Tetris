using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public SoundData[] sounds;

    Dictionary<SoundType, AudioClip> soundDict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        soundDict = new Dictionary<SoundType, AudioClip>();

        foreach (SoundData s in sounds)
        {
            soundDict[s.type] = s.clip;
        }
    }

    public void PlaySFX(SoundType type)
    {
        if (soundDict.ContainsKey(type))
        {
            sfxSource.PlayOneShot(soundDict[type]);
            Debug.Log("sound name"+ type);
        }
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}