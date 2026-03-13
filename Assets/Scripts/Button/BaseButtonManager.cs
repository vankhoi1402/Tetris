using UnityEngine;

public abstract class BaseButtonManager : MonoBehaviour
{
    public virtual void OnButtonClick(ButtonType type)
    {
        // Các nút dùng chung ở mọi nơi
        if (type == ButtonType.Quit) Application.Quit();
        if (type == ButtonType.Settings)
        {
            GameEvents.OnOpenSettings?.Invoke();
            GameManager.Instance.ShowSetting(); 
        }
    }

}