using UnityEngine;
using UnityEngine.Tilemaps;

public class TetrisController : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private Piece activePiece;
    [SerializeField] private TetrisQueue tetrisQueue;
    [SerializeField] private TetrisHold tetrisHold;
    [SerializeField] private Tile[] tiles; // Gán 7 loại Tile màu trong Inspector

   
    private float dropTime = 0.5f; // 1 giây rơi 1 lần
    private float timer;
    private bool canHold = true; // Mỗi lượt rơi chỉ được Hold 1 lần
    private void Start()
    {
        // 1. Yêu cầu Queue chuẩn bị sẵn gạch (ví dụ hiện trước 3 khối)
        tetrisQueue.InitializeQueue(3);

        // 2. Bắt đầu trận đấu
        SpawnNewPiece();
    }

    private void Update()
    {
        // 1. Đồng hồ đếm ngược để rơi tự động
        timer += Time.deltaTime;
        if (timer >= dropTime)
        {
            MovePiece(Vector3Int.down);
            timer = 0;
        }
    }

    private void OnEnable()
    {
        TetrisInputHandler.OnMovePerformed += HandleMove;
        TetrisInputHandler.OnRotatePerformed += HandleRotate;
        TetrisInputHandler.OnHardDropPerformed += HandleHardDrop;
        TetrisInputHandler.OnHoldPerformed += Hold;
    }

    private void OnDisable()
    {
        TetrisInputHandler.OnMovePerformed -= HandleMove;
        TetrisInputHandler.OnRotatePerformed -= HandleRotate;
        TetrisInputHandler.OnHardDropPerformed -= HandleHardDrop;
        TetrisInputHandler.OnHoldPerformed -= Hold;
    }
    private void HandleHardDrop()
    {
        board.Render(activePiece, false);

        // Vòng lặp: cứ đi xuống cho đến khi không đi được nữa
        while (board.IsValidMove(activePiece.cells, activePiece.position + Vector3Int.down))
        {
            activePiece.position += Vector3Int.down;
        }

        board.Render(activePiece, true);
        LockPiece(); // Chốt ngay lập tức
    }
    public void Hold()
    {
        if (!canHold) return;

        // 1. Xóa khối hiện tại khỏi bàn cờ chính
        board.Render(activePiece, false);

        Tetromino currentType = activePiece.type;

        if (tetrisHold.HeldTetromino == null)
        {
            // TH 1: Ô Hold trống -> Cất khối hiện tại, lấy khối mới từ Queue
            tetrisHold.SetHoldPiece(currentType);
            SpawnNewPiece();
        }
        else
        {
            // TH 2: Đã có gạch trong ô Hold -> Hoán đổi (Swap)
            Tetromino nextToPlay = tetrisHold.HeldTetromino.Value;

            // Cất khối hiện tại vào ô Hold
            tetrisHold.SetHoldPiece(currentType);

            // Lấy khối từ ô Hold ra bàn cờ
            int tileIndex = (int)nextToPlay;
            activePiece.Initialize(new Vector3Int(0, 8, 0), nextToPlay, tiles[tileIndex]);
            board.Render(activePiece, true);
        }

        canHold = false; // Khóa Hold cho đến khi khối này hạ cánh (Lock)
    }



    private void HandleMove(float dir)
    {
        Vector3Int translation = new Vector3Int(Mathf.RoundToInt(dir), 0, 0);
        MovePiece(translation);
    }

    private void HandleRotate()
    {
        board.Render(activePiece, false); // Xóa hình cũ

        // Logic xoay mảng tạm thời để kiểm tra
        Vector3Int[] originalCells = (Vector3Int[])activePiece.cells.Clone();
        for (int i = 0; i < activePiece.cells.Length; i++)
        {
            int x = activePiece.cells[i].x;
            int y = activePiece.cells[i].y;
            activePiece.cells[i].x = y;
            activePiece.cells[i].y = -x;
        }

        if (!board.IsValidMove(activePiece.cells, activePiece.position))
        {
            activePiece.cells = originalCells; // Kẹt thì trả về như cũ
        }

        board.Render(activePiece, true); // Vẽ lại
    }

    private void MovePiece(Vector3Int translation)
    {
        board.Render(activePiece, false); // Xóa hình cũ
        Vector3Int nextPos = activePiece.position + translation;

        if (board.IsValidMove(activePiece.cells, nextPos))
        {
            activePiece.position = nextPos;
            board.Render(activePiece, true); // Vẽ vị trí mới
        }
        else
        {
            board.Render(activePiece, true); // Vẽ lại vị trí cũ

            // Nếu hướng di chuyển là đi XUỐNG mà bị vướng -> Chốt gạch
            if (translation == Vector3Int.down)
            {
                LockPiece();
            }
        }
    }
    private void LockPiece()
    {
        // Vẽ lại lần cuối để chắc chắn gạch nằm trên Tilemap
        board.Render(activePiece, true);

        // KIỂM TRA XÓA HÀNG NGAY TẠI ĐÂY
        board.ClearFullLines();

        // Sinh gạch mới
        SpawnNewPiece();
    }
    public void SpawnNewPiece()
    {
        canHold = true;

        // 3. THAY ĐỔI QUAN TRỌNG: Thay vì Random.Range, hãy hỏi Queue
        int nextID = tetrisQueue.GetNextPiece();

        // 4. Khởi tạo khối gạch đang chơi với ID nhận được
        activePiece.Initialize(new Vector3Int(0, 8, 0), (Tetromino)nextID, tiles[nextID]);

        // Kiểm tra Game Over
        if (!board.IsValidMove(activePiece.cells, activePiece.position))
        {
            Debug.LogError("GAME OVER!");
            return;
        }

        // 5. Vẽ khối gạch ra bàn cờ chính
        board.Render(activePiece, true);
    }

}