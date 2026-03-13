public class GameplayButtonManager : BaseButtonManager
{
    public override void OnButtonClick(ButtonType type)
    {
        base.OnButtonClick(type); // Chạy các nút chung trước

        switch (type)
        {
            case ButtonType.Pause: GameManager.Instance.PauseGame(); break;
            case ButtonType.Resume: GameManager.Instance.ResumeGame(); break;
            case ButtonType.Restart: GameManager.Instance.RestartGame(); break;
            case ButtonType.BackToMenu: GameManager.Instance.GoToMainMenu(); break;
        }
    }
}