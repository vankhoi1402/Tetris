using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }
    public int Lines { get; private set; }
    public int Level => (Lines / 10) + 1; // Cứ 10 hàng lên 1 Level

    private void OnEnable() => GameEvents.OnLinesClearedBatch += AddScore;
    private void OnDisable() => GameEvents.OnLinesClearedBatch-= AddScore;

    private void AddScore(List<int> rows)
    {
        int count = rows.Count;
        int multiplier = 0;

        // Công thức tính điểm chuẩn Tetris
        switch (count)
        {
            case 1: multiplier = 100; break; // Single
            case 2: multiplier = 300; break; // Double
            case 3: multiplier = 500; break; // Triple
            case 4: multiplier = 800; break; // Tetris!
        }

        Score += multiplier * Level;
        Lines += count;

        // Cập nhật UI (Nếu bạn đã có UIManager)
        if (UIManagerScore.Instance != null)
        {
            UIManagerScore.Instance.UpdateUI(Score, Lines, Level);
        }

        Debug.Log($"Score: {Score} | Lines: {Lines} | Level: {Level}");
    }
}