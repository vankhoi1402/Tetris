using UnityEngine;
using TMPro; // Bắt buộc phải có để điều khiển TextMeshPro

public class UIManagerScore : MonoBehaviour
{
    // Singleton: Giúp gọi UIManager.Instance từ bất cứ đâu
    public static UIManagerScore Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    

    private void Awake()
    {
        // Khởi tạo Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Hàm cập nhật tất cả thông số
    public void UpdateUI(int score, int lines, int level)
    {
        if (scoreText != null) scoreText.text = score.ToString("D6"); // Định dạng 6 chữ số (000120)
      
    }
}