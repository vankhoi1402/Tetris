using UnityEngine;
using UnityEngine.Tilemaps;

public class TetrisHold : MonoBehaviour
{
    [Header("Hold UI")]
    [SerializeField] private Tilemap holdTilemap;
    [SerializeField] private Tile[] tiles;

    // Biến lưu trữ khối đang được giữ
    // Dùng Tetromino? (nullable) để biết lúc đầu ô Hold đang trống
    public Tetromino? HeldTetromino { get; private set; } = null;

    // Vẽ khối gạch lên ô Hold
    public void SetHoldPiece(Tetromino type)
    {
        HeldTetromino = type;
        RenderHold();
    }

    private void RenderHold()
    {
        holdTilemap.ClearAllTiles();

        if (HeldTetromino == null) return;

        int pieceIndex = (int)HeldTetromino.Value;
        Vector2Int[] shapeData = TetrisData.Shapes[(int)HeldTetromino.Value];
        Tile pieceTile = tiles[pieceIndex];

        // Vẽ khối gạch vào giữa ô Hold
        foreach (var cell in shapeData)
        {
            // Có thể cộng thêm offset để căn giữa tùy vị trí Tilemap của bạn
            Vector3Int tilePos = new Vector3Int(cell.x, cell.y, 0);
            holdTilemap.SetTile(tilePos, pieceTile);
        }
    }

    // Reset ô Hold khi bắt đầu game mới
    public void Clear()
    {
        HeldTetromino = null;
        holdTilemap.ClearAllTiles();
    }
}