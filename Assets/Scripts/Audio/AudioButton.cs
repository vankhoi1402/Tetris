using UnityEngine;

public class AudioButton : MonoBehaviour
{
    private void OnEnable()
    {
        ButtonEvent.OnClick += ButtonClickAudio;
        ButtonEvent.OnHover += ButtonHoverAudio;
            
    }
    private void OnDisable()
    {
        ButtonEvent.OnClick -= ButtonClickAudio;
        ButtonEvent.OnHover -= ButtonHoverAudio;
    }
    private void ButtonClickAudio()
    {
        AudioManager.Instance.PlaySFX(SoundType.UIButton);
    }
    private void ButtonHoverAudio()
    {
        AudioManager.Instance.PlaySFX(SoundType.UIButton);
    }
}
