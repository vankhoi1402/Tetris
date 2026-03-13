using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [Header("Visual Effects")]
    public GameObject clearEffectPrefab;


    private void OnEnable()
    {
        // Đăng ký lắng nghe sự kiện
        GameEvents.OnLinesClearedBatch += PlayEffects;
    }

    private void OnDisable()
    {
        // Hủy đăng ký khi Object bị tắt để tránh lỗi bộ nhớ
        GameEvents.OnLinesClearedBatch -= PlayEffects;
    }

    private void PlayEffects(List<int> rows)
    {
        foreach (int row in rows)
        {
            if (clearEffectPrefab != null)
            {
                // Đặt Z = -1f để hiệu ứng luôn nằm trên Tilemap
                Vector3 spawnPos = new Vector3(1, row, -1f);
                GameObject vfx = Instantiate(clearEffectPrefab, spawnPos, Quaternion.identity);

                

                Destroy(vfx, 4.0f);
            }
        }

        // Thêm logic âm thanh dựa trên số hàng
        if (rows.Count == 4)
        {
            Debug.Log("TETRIS! Phát âm thanh đặc biệt!");
        }
    }
}