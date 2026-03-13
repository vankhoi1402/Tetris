using UnityEngine;

public class OpenSetting : MonoBehaviour
{
    public GameObject settingsPrefab; // Kéo prefab vào đây

    public void OpenPanel()
    {
        Debug.Log("show setting ");
        settingsPrefab.SetActive(true);
        
    }
}
