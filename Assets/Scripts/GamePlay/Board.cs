using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private BoardRenderer render; // Tham chiếu tới View
    [SerializeField] private Vector2Int boardSize = new Vector2Int(10, 20);

    // Render bây giờ chỉ là một lệnh chuyển tiếp (Proxy)
    public void Render(Piece piece, bool visible)
    {
        render.DrawPiece(piece, visible);
    }

    // Kiểm tra va chạm vẫn nằm ở đây vì nó là Logic
    public bool IsValidMove(Vector3Int[] cells, Vector3Int position)
    {
        foreach (var cell in cells)
        {
            Vector3Int actualPos = cell + position;
            if (IsOutOfBounds(actualPos)) return false;

            // Hỏi Renderer xem tại vị trí đó có gạch chưa
            if (render.GetTile(actualPos) != null) return false;
        }
        return true;
    }

    private bool IsOutOfBounds(Vector3Int pos)
    {
        return pos.x < -boardSize.x / 2 || pos.x >= boardSize.x / 2 || pos.y < -boardSize.y / 2;
    }
    // THÊM HÀM NÀY VÀO:
    public bool IsLineFull(int row)
    {
        int halfWidth = boardSize.x / 2;

        // Quét từ trái sang phải của hàng 'row'
        for (int x = -halfWidth; x < halfWidth; x++)
        {
            // Hỏi Renderer xem ô này có gạch không
            if (render.GetTile(new Vector3Int(x, row, 0)) == null)
            {
                return false; // Chỉ cần 1 ô trống => Hàng chưa đầy
            }
        }
        return true; // Không có ô nào trống => Hàng đầy!
    }

    // Getter để các class khác như LineManager lấy Renderer ra dùng
    public BoardRenderer Renderer => render;
    public Vector2Int Size => boardSize;
}