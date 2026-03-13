using UnityEngine;
using TMPro; // Đảm bảo đã có namespace này

public class GameOverScreen : MonoBehaviour
{
    [Header("Data Reference")]
    [SerializeField] private ScoreData scoreData;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText; // Thêm Text cho điểm cao
    private void Start()
    {
        this.Show();
    }
    

    public void Show()
    {
        Debug.Log("show score");
        // Hiển thị điểm hiện tại của ván chơi
        finalScoreText.text = "Final Score: " + scoreData.currentScore.ToString();

        // Hiển thị kỷ lục điểm cao nhất từ ScriptableObject
        highScoreText.text = "Best Score: " + scoreData.highScore.ToString();

        // (Tùy chọn) Thêm thông báo nếu người chơi vừa phá kỷ lục
        if (scoreData.currentScore >= scoreData.highScore && scoreData.currentScore > 0)
        {
            highScoreText.text = "New Record: " + scoreData.highScore.ToString();
            highScoreText.color = Color.yellow; // Làm nổi bật nếu là kỷ lục mới
        }
    }
}