using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [Header("Visual Effects")]
    public GameObject clearEffectPrefab;

    [Header("Audio Effects")]
    public AudioSource audioSource;
    public AudioClip clearSound;

    private void OnEnable()
    {
        // Đăng ký lắng nghe sự kiện
        GameEvents.OnLineCleared += PlayEffects;
    }

    private void OnDisable()
    {
        // Hủy đăng ký khi Object bị tắt để tránh lỗi bộ nhớ
        GameEvents.OnLineCleared -= PlayEffects;
    }

    private void PlayEffects(int row)
    {
        // 1. Tạo hiệu ứng hạt (VFX)
        if (clearEffectPrefab != null)
        {
            Vector3 spawnPos = new Vector3(-1, row, 0);
            GameObject vfx = Instantiate(clearEffectPrefab, spawnPos, Quaternion.identity);
            Debug.Log("phat hieu ung ");
            Destroy(vfx, 1.0f);
        }

        // 2. Phát âm thanh (SFX)
        //if (audioSource != null && clearSound != null)
        //{
        //    audioSource.PlayOneShot(clearSound);
        //}
    }
}