using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    // Sự kiện khi một hàng bị xóa, truyền vào vị trí Y của hàng đó
    // Sự kiện cũ (nếu bạn vẫn muốn dùng)
    public static System.Action<int> OnLineCleared;

    // Sự kiện mới cho phép gửi nhiều hàng cùng lúc
    public static System.Action<List<int>> OnLinesClearedBatch;
    public static void RaiseMultipleLinesCleared(List<int> rows)
    {
        OnLinesClearedBatch?.Invoke(rows);
    }
    // Các sự kiện trạng thái Game mới
    public static System.Action OnGamePaused;
    public static System.Action OnGameResumed;
    public static System.Action OnGameOver;
    public static Action OnOpenSettings;
    //event game play 
    public static Action OnMove;
    public static Action OnLock;
    public static Action OnLineClear;
}