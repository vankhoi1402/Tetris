public class MenuButtonManager : BaseButtonManager
{
    public override void OnButtonClick(ButtonType type)
    {
        base.OnButtonClick(type); // Chạy các nút chung trước

        if (type == ButtonType.StartGame)
            UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
}