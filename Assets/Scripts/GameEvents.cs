using System;
using UnityEngine;

public static class GameEvents
{
    // Sự kiện khi một hàng bị xóa, truyền vào vị trí Y của hàng đó
    public static event Action<int> OnLineCleared;

    public static void RaiseLineCleared(int row)
    {
        OnLineCleared?.Invoke(row);
    }
}