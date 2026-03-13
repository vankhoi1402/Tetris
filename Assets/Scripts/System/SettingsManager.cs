using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public AudioMixer mainMixer; // Kéo file Audio Mixer vào đây

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicVolume(float value)
    {
        // Mixer dùng thang đo Decibel (từ -80 đến 20), nên cần công thức Log10
        float dbValue = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
        mainMixer.SetFloat("MusicVol", dbValue);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        float dbValue = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
        mainMixer.SetFloat("SFXVol", dbValue);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    void LoadSettings()
    {
        float mVol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        // Đợi 1 nhịp để Mixer kịp khởi tạo rồi set giá trị
        SetMusicVolume(mVol);
        SetSFXVolume(sVol);
    }
}