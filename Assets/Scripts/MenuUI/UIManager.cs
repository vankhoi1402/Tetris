using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GamePlay";

    private void OnEnable()
    {
        UIEvents.OnPlayClicked += LoadGame;
        UIEvents.OnQuitClicked += QuitGame;
    }

    private void OnDisable()
    {
        UIEvents.OnPlayClicked -= LoadGame;
        UIEvents.OnQuitClicked -= QuitGame;
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    private void QuitGame()
    {
        Debug.Log("Quit Game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}