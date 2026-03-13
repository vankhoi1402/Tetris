using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CurrentState != GameState.GameOver)
        {
            if (CurrentState == GameState.Paused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
        GameEvents.OnGamePaused?.Invoke(); // Phát sự kiện
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
        GameEvents.OnGameResumed?.Invoke(); // Phát sự kiện
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        Time.timeScale = 0f;
        GameEvents.OnGameOver?.Invoke(); // Phát sự kiện
    }

    // Các hàm bổ trợ vẫn giữ nguyên
    public void RestartGame() 
    { 
        Time.timeScale = 1f; SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void GoToMainMenu()
    {
        // QUAN TRỌNG: Trả thời gian về bình thường trước khi chuyển Scene
        Time.timeScale = 1f;

        // Bạn có thể dùng tên Scene hoặc Index trong Build Settings
        SceneManager.LoadScene("MainMenu");
    }
    public void ShowSetting()
    {
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
    }
}