using System;

public static class UIEvents
{
    public static event Action OnPlayClicked;
    public static event Action OnQuitClicked;

    public static void RaisePlay()
    {
        OnPlayClicked?.Invoke();
    }

    public static void RaiseQuit()
    {
        OnQuitClicked?.Invoke();
    }
}