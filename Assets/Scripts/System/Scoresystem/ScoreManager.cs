using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScoreData scoreData; // Kéo file GameScore vào đây

    private void Awake()
    {
        scoreData.Reset();
        scoreData.LoadHighScore();
    }

    private void OnEnable() => GameEvents.OnLinesClearedBatch += AddScore;
    private void OnDisable() => GameEvents.OnLinesClearedBatch -= AddScore;

    private void AddScore(List<int> rows)
    {
        int count = rows.Count;
        int multiplier = 0;

        switch (count)
        {
            case 1: multiplier = 100; break;
            case 2: multiplier = 300; break;
            case 3: multiplier = 500; break;
            case 4: multiplier = 1200; break;
        }

        scoreData.currentScore += multiplier * scoreData.currentLevel;
        scoreData.linesCleared += count;
        scoreData.currentLevel = (scoreData.linesCleared / 10) + 1;
        AudioManager.Instance.PlaySFX(SoundType.ScoreUp);

        scoreData.UpdateHighScore();

        // Gửi sự kiện để UI tự cập nhật (Nếu bạn dùng hệ thống Event)
        // GameEvents.OnScoreChanged?.Invoke(); 

        // Hoặc gọi trực tiếp UIManager nếu vẫn dùng Singleton cho UI
        if (UIManagerScore.Instance != null)
        {
            UIManagerScore.Instance.UpdateUI(
                scoreData.currentScore,
                scoreData.linesCleared,
                scoreData.currentLevel
                
            );
        }
    }
}