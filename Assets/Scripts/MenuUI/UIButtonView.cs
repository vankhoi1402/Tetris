using UnityEngine;
using UnityEngine.UI;

public class UIButtonView : MonoBehaviour
{
    [SerializeField] private ButtonType buttonType;
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        switch (buttonType)
        {
            case ButtonType.Play:
                UIEvents.RaisePlay();
                break;

            case ButtonType.Quit:
                UIEvents.RaiseQuit();
                break;
        }
    }
}

public enum ButtonType
{
    Play,
    Quit
}