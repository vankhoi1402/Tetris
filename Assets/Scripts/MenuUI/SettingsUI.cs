using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("UI References")]
    public Slider musicSlider;
    public Slider sfxSlider;
    public Button closeButton;

    void Start()
    {
        // 1. Cập nhật giá trị ban đầu cho Slider từ SettingsManager (Singleton)
        if (SettingsManager.Instance != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

            // 2. Lắng nghe sự kiện khi kéo Slider
            musicSlider.onValueChanged.AddListener(SettingsManager.Instance.SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SettingsManager.Instance.SetSFXVolume);
        }

        // 3. Nút đóng Panel
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));

        // Mặc định ẩn Panel khi mới vào Scene
       // gameObject.SetActive(false);
    }
}