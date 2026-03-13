using UnityEngine;

[CreateAssetMenu(fileName = "NewScoreData", menuName = "Game/Score Data")]
public class ScoreData : ScriptableObject
{
    public int currentScore;
    public int highScore;
    public int linesCleared;
    public int currentLevel;

    public void Reset()
    {
        currentScore = 0;
        linesCleared = 0;
        currentLevel = 1;
        // Không reset highScore ở đây để giữ kỷ lục
    }

    public void UpdateHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}