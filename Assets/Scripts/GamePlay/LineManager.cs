using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    private Board _board;

    public void Initialize(Board board)
    {
        _board = board;
    }

    public void ClearFullLines()
    {
        // Bước 1: Thu thập danh sách các hàng đầy dựa trên Logic của Board
        List<int> fullRows = GetFullRows();

        if (fullRows.Count > 0)
        {
            // Bước 2: Báo cho hệ thống Effect và Score (Observer Pattern)
            // Gửi danh sách Y gốc trước khi bị dời để Effect nổ đúng chỗ
            GameEvents.RaiseMultipleLinesCleared(fullRows);

            // Bước 3: Thực hiện xóa vật lý và dồn hàng (Physical Action)
            PerformClearing();
        }
    }

    private List<int> GetFullRows()
    {
        List<int> rows = new List<int>();
        int halfHeight = _board.Size.y / 2;

        // Quét từ dưới đáy lên đỉnh
        for (int y = -halfHeight; y < halfHeight; y++)
        {
            if (_board.IsLineFull(y))
            {
                rows.Add(y);
            }
        }
        return rows;
    }

    private void PerformClearing()
    {
        int halfHeight = _board.Size.y / 2;

        // Quét lại và thực hiện xóa dồn (để tránh lỗi index khi dời hàng)
        for (int y = -halfHeight; y < halfHeight; y++)
        {
            if (_board.IsLineFull(y))
            {
                DeleteRowAndShiftDown(y);
                // Sau khi dồn hàng trên xuống, phải kiểm tra lại chính hàng này (y--)
                y--;
            }
        }
    }

    private void DeleteRowAndShiftDown(int rowY)
    {
        int halfWidth = _board.Size.x / 2;
        int halfHeight = _board.Size.y / 2;

        // 1. Xóa hàng đầy (Yêu cầu Renderer thực hiện)
        for (int x = -halfWidth; x < halfWidth; x++)
        {
            _board.Renderer.ClearTile(new Vector3Int(x, rowY, 0));
            GameEvents.OnLineClear.Invoke(); // Gọi sự kiện để phát âm thanh và hiệu ứng nổ
        }

        // 2. Dời toàn bộ các hàng phía trên xuống 1 đơn vị
        // Bắt đầu từ hàng ngay trên hàng vừa xóa cho đến đỉnh bàn cờ
        for (int y = rowY + 1; y < halfHeight; y++)
        {
            for (int x = -halfWidth; x < halfWidth; x++)
            {
                Vector3Int sourcePos = new Vector3Int(x, y, 0);
                Vector3Int targetPos = new Vector3Int(x, y - 1, 0);

                // Ra lệnh cho Renderer di chuyển Tile
                _board.Renderer.MoveTile(sourcePos, targetPos);
            }
        }
    }
}