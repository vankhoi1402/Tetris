using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardRenderer : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    // Vẽ hoặc Xóa một khối Piece
    public void DrawPiece(Piece piece, bool visible)
    {
        foreach (var cell in piece.cells)
        {
            Vector3Int tilePos = cell + piece.position;
            tilemap.SetTile(tilePos, visible ? piece.tile : null);
        }
    }

    // Xóa một ô cụ thể (Dùng khi xóa hàng)
    public void SetTile(Vector3Int position, TileBase tile)
    {
        tilemap.SetTile(position, tile);
    }

    // Lấy Tile tại một vị trí (Dùng để copy gạch khi dời hàng xuống)
    public TileBase GetTile(Vector3Int position)
    {
        return tilemap.GetTile(position);
    }

    public void ClearAll()
    {
        tilemap.ClearAllTiles();
    }
    // Thêm vào trong class BoardRenderer
    public void ClearTile(Vector3Int position)
    {
        tilemap.SetTile(position, null);
    }

    // Hàm này giúp di chuyển tile từ vị trí cũ sang vị trí mới
    public void MoveTile(Vector3Int from, Vector3Int to)
    {
        TileBase tile = GetTile(from);
        SetTile(to, tile);
        ClearTile(from);
    }
}