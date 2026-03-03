using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Vector2Int boardSize = new Vector2Int(10, 20);

    public void Render(Piece piece, bool visible)
    {
        foreach (var cell in piece.cells)
        {
            tilemap.SetTile(cell + piece.position, visible ? piece.tile : null);
        }
    }

    public bool IsValidMove(Vector3Int[] cells, Vector3Int position)
    {
        foreach (var cell in cells)
        {
            Vector3Int actualPos = cell + position;
            // Kiểm tra biên
            if (actualPos.x < -boardSize.x / 2 || actualPos.x >= boardSize.x / 2) return false;
            if (actualPos.y < -boardSize.y / 2) return false;
            // Kiểm tra va chạm tile đã có sẵn
            if (tilemap.HasTile(actualPos)) return false;
        }
        return true;
    }
    public void ClearFullLines()
    {
        // Giả sử board cao từ -10 đến 9 (20 hàng)
        // Quét từ hàng dưới cùng lên trên
        for (int row = -10; row < 10; row++)
        {
            if (IsLineFull(row))
            {
                DeleteLine(row);
                row--; // Kiểm tra lại hàng này sau khi hàng trên sụp xuống
            }
        }
    }

    private bool IsLineFull(int row)
    {
        // Kiểm tra chiều rộng từ -5 đến 4 (10 ô)
        for (int col = -5; col < 5; col++)
        {
            if (!tilemap.HasTile(new Vector3Int(col, row, 0)))
            {
                return false; // Chỉ cần 1 ô trống là hàng chưa đầy
            }
        }
        return true;
    }

    private void DeleteLine(int row)
    {
        // 1. Phát tín hiệu (Observer Pattern)
        GameEvents.RaiseLineCleared(row);
        // 1. Xóa hàng hiện tại
        for (int col = -5; col < 5; col++)
        {
            tilemap.SetTile(new Vector3Int(col, row, 0), null);
        }

        // 2. Di chuyển tất cả các hàng phía trên xuống 1 đơn vị
        // Quét từ hàng vừa xóa lên đỉnh board
        for (int y = row + 1; y < 10; y++)
        {
            for (int x = -5; x < 5; x++)
            {
                Vector3Int currentPos = new Vector3Int(x, y, 0);
                TileBase tileAbove = tilemap.GetTile(currentPos);

                Vector3Int targetPos = new Vector3Int(x, y - 1, 0);
                tilemap.SetTile(targetPos, tileAbove);
                tilemap.SetTile(currentPos, null); // Xóa ô ở trên sau khi dời xuống
            }
        }
    }
}