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
}