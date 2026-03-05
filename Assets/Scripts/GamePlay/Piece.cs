using UnityEngine;
using UnityEngine.Tilemaps;

public class Piece : MonoBehaviour
{
    public Vector3Int position;
    public Vector3Int[] cells;
    public Tile tile;
    public Tetromino type; // Biến này cần được cập nhật khi Initialize

    public void Initialize(Vector3Int spawnPos, Tetromino type, Tile tile)
    {
        this.position = spawnPos;
        this.tile = tile;
        this.type = type; // CHỖ THIẾU QUAN TRỌNG: Cập nhật loại gạch đang rơi

        // Lấy dữ liệu hình dạng từ TetrisData dựa trên type truyền vào
        Vector2Int[] data = TetrisData.Shapes[(int)type];

        this.cells = new Vector3Int[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            // Ép kiểu từ Vector2Int của Data sang Vector3Int của Piece
            this.cells[i] = (Vector3Int)data[i];
        }
    }
}