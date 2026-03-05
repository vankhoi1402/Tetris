using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TetrisQueue : MonoBehaviour
{
    [Header("Preview Render")]
    [SerializeField] private Tilemap previewTilemap;
    [SerializeField] private Tile[] tiles; // Gán 7 loại Tile màu vào đây

    [Header("Settings")]
    [SerializeField] private int spacing = 4; // Khoảng cách giữa các khối gạch trong Preview

    private Queue<int> _queue = new Queue<int>();
    private List<int> _bag = new List<int>();

    public void InitializeQueue(int size)
    {
        _queue.Clear();
        for (int i = 0; i < size; i++)
        {
            _queue.Enqueue(GenerateSmartRandom());
        }
        RenderPreview();
    }

    public int GetNextPiece()
    {
        int next = _queue.Dequeue();
        _queue.Enqueue(GenerateSmartRandom());
        RenderPreview();
        return next;
    }

    private void RenderPreview()
    {
        // 1. Xóa sạch Tile cũ trên bảng Preview
        previewTilemap.ClearAllTiles();

        // 2. Chuyển Queue thành mảng để duyệt vẽ
        int[] items = _queue.ToArray();

        for (int i = 0; i < items.Length; i++)
        {
            int pieceIndex = items[i];
            Vector2Int[] shapeData = TetrisData.Shapes[pieceIndex];
            Tile pieceTile = tiles[pieceIndex];

            // Tính toán vị trí Y để các khối xếp hàng dọc (khối đầu tiên ở trên cùng)
            // Ví dụ: khối 0 ở Y=0, khối 1 ở Y=-4, khối 2 ở Y=-8
            int yOffset = -i * spacing;

            foreach (var cell in shapeData)
            {
                Vector3Int tilePos = new Vector3Int(cell.x, cell.y + yOffset, 0);
                previewTilemap.SetTile(tilePos, pieceTile);
            }
        }
    }

    private int GenerateSmartRandom()
    {
        if (_bag.Count == 0)
        {
            for (int i = 0; i < 7; i++) _bag.Add(i);
            for (int i = 0; i < _bag.Count; i++)
            {
                int temp = _bag[i];
                int rand = Random.Range(i, _bag.Count);
                _bag[i] = _bag[rand];
                _bag[rand] = temp;
            }
        }
        int piece = _bag[0];
        _bag.RemoveAt(0);
        return piece;
    }
}