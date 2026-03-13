using UnityEngine;
using TMPro;
using System.Collections; // Cần thiết để chạy Coroutine

public class UIManagerScore : MonoBehaviour
{
    public static UIManagerScore Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private int displayedScore = 0; // Điểm số hiện tại đang hiển thị
    private Coroutine scoreAnimCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateUI(int targetScore, int lines, int level)
    {
        if (scoreText == null) return;

        // 1. Dừng hiệu ứng cũ nếu nó đang chạy để bắt đầu số mới
        if (scoreAnimCoroutine != null) StopCoroutine(scoreAnimCoroutine);

        // 2. Chạy hiệu ứng nhảy số
        scoreAnimCoroutine = StartCoroutine(AnimateScore(targetScore));

        // 3. Chạy hiệu ứng "Nảy" (Punch Scale)
        StartCoroutine(PunchScaleEffect());
    }

    // Hiệu ứng số chạy tăng dần
    private IEnumerator AnimateScore(int targetScore)
    {
        float duration = 0.4f; // Thời gian nhảy số (0.4 giây)
        float elapsed = 0f;
        int startScore = displayedScore;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;

            // Lerp giúp nội suy số từ cũ đến mới theo thời gian
            displayedScore = (int)Mathf.Lerp(startScore, targetScore, progress);
            scoreText.text = displayedScore.ToString("D6");
            yield return null;
        }

        displayedScore = targetScore;
        scoreText.text = displayedScore.ToString("D6");
    }

    // Hiệu ứng chữ giật nhẹ lên khi có điểm
    private IEnumerator PunchScaleEffect()
    {
        RectTransform rect = scoreText.rectTransform;
        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f); // Phóng to 20%

        // Phóng to nhanh (0.05 giây)
        float t = 0;
        while (t < 0.05f)
        {
            t += Time.deltaTime;
            rect.localScale = Vector3.Lerp(originalScale, targetScale, t / 0.05f);
            yield return null;
        }

        // Thu nhỏ lại từ từ (0.15 giây)
        t = 0;
        while (t < 0.15f)
        {
            t += Time.deltaTime;
            rect.localScale = Vector3.Lerp(targetScale, originalScale, t / 0.15f);
            yield return null;
        }
        rect.localScale = originalScale;
    }
}