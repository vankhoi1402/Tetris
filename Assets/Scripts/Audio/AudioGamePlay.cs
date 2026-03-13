using UnityEngine;

public class AudioGamePlay : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnMove += MoveAudio;
        GameEvents.OnLock += LockAudio;
        GameEvents.OnLineClear += ClearAudio;

    }
    private void OnDisable()
    {
        GameEvents.OnMove -= MoveAudio;
        GameEvents.OnLock -= LockAudio;
        GameEvents.OnLineClear -= ClearAudio;
    }
    private void MoveAudio()
    {
        AudioManager.Instance.PlaySFX(SoundType.Move);
    }
    private void LockAudio()
    {
        AudioManager.Instance.PlaySFX(SoundType.Drop);
    }
    private void ClearAudio()
    {
        AudioManager.Instance.PlaySFX(SoundType.ClearLine);
    }
}
