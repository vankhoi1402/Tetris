using UnityEngine;

public class UIManagerPanels : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject hudPanel; // Chứa Score/Lines khi đang chơi

    private void OnEnable()
    {
        GameEvents.OnGamePaused += ShowPause;
        GameEvents.OnGameResumed += HideAll;
        GameEvents.OnGameOver += ShowGameOver;
        GameEvents.OnOpenSettings += ShowSettings;
    }

    private void OnDisable()
    {
        GameEvents.OnGamePaused -= ShowPause;
        GameEvents.OnGameResumed -= HideAll;
        GameEvents.OnGameOver -= ShowGameOver;
        GameEvents.OnOpenSettings -= ShowSettings;
    }

    private void HideAll()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
        if (hudPanel != null) hudPanel.SetActive(true);
    }

    private void ShowPause()
    {
       // if (hudPanel != null)
       //     hudPanel.SetActive(false);

        pausePanel.SetActive(true);
       // pausePanel.transform.SetAsLastSibling();
    }

    private void ShowGameOver()
    {
        Debug.Log("show panel");
        HideAll();
        gameOverPanel.SetActive(true);
        if (hudPanel != null) hudPanel.SetActive(false); // Thua thì ẩn điểm số chính đi cho gọn
    }

    private void ShowSettings()
    {
        Debug.Log("Open Settings");
        HideAll();
        settingsPanel.SetActive(true);

        Debug.Log("Self: " + settingsPanel.activeSelf);
        Debug.Log("Hierarchy: " + settingsPanel.activeInHierarchy);
    }
}