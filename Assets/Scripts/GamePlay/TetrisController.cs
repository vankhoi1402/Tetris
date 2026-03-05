using UnityEngine;
using UnityEngine.Tilemaps;

public class TetrisController : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private Piece activePiece;
    [SerializeField] private LineManager lineManager;
    [SerializeField] private TetrisQueue tetrisQueue;
    [SerializeField] private TetrisHold tetrisHold;
    [SerializeField] private Tile[] tiles;

    private float dropTime = 0.5f;
    private float timer;
    private bool canHold = true;

    private void Start()
    {
        // 1. Kết nối LineManager với Board
        if (lineManager != null)
        {
            lineManager.Initialize(board);
        }

        // 2. Chuẩn bị Queue
        tetrisQueue.InitializeQueue(3);

        // 3. Bắt đầu game
        SpawnNewPiece();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= dropTime)
        {
            MovePiece(Vector3Int.down);
            timer = 0;
        }
    }

    #region Input Handling
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

    private void HandleMove(float dir)
    {
        Vector3Int translation = new Vector3Int(Mathf.RoundToInt(dir), 0, 0);
        MovePiece(translation);
    }

    private void HandleRotate()
    {
        // Sử dụng board.Render (hàm proxy đã viết trong Board.cs)
        board.Render(activePiece, false);

        Vector3Int[] originalCells = (Vector3Int[])activePiece.cells.Clone();

        // Logic xoay đơn giản
        for (int i = 0; i < activePiece.cells.Length; i++)
        {
            int x = activePiece.cells[i].x;
            int y = activePiece.cells[i].y;
            activePiece.cells[i].x = y;
            activePiece.cells[i].y = -x;
        }

        if (!board.IsValidMove(activePiece.cells, activePiece.position))
        {
            activePiece.cells = originalCells;
        }

        board.Render(activePiece, true);
    }

    private void HandleHardDrop()
    {
        board.Render(activePiece, false);

        while (board.IsValidMove(activePiece.cells, activePiece.position + Vector3Int.down))
        {
            activePiece.position += Vector3Int.down;
        }

        board.Render(activePiece, true);
        LockPiece();
    }
    #endregion

    private void MovePiece(Vector3Int translation)
    {
        board.Render(activePiece, false);
        Vector3Int nextPos = activePiece.position + translation;

        if (board.IsValidMove(activePiece.cells, nextPos))
        {
            activePiece.position = nextPos;
            board.Render(activePiece, true);
        }
        else
        {
            board.Render(activePiece, true);

            if (translation == Vector3Int.down)
            {
                LockPiece();
            }
        }
    }

    private void LockPiece()
    {
        // Chốt vị trí gạch lên Tilemap
        board.Render(activePiece, true);

        // Xóa hàng (LineManager sẽ dùng board.IsLineFull và Renderer để xử lý)
        lineManager.ClearFullLines();

        SpawnNewPiece();
    }

    public void Hold()
    {
        if (!canHold) return;

        board.Render(activePiece, false);
        Tetromino currentType = activePiece.type;

        if (tetrisHold.HeldTetromino == null)
        {
            tetrisHold.SetHoldPiece(currentType);
            SpawnNewPiece();
        }
        else
        {
            Tetromino nextToPlay = tetrisHold.HeldTetromino.Value;
            tetrisHold.SetHoldPiece(currentType);

            int tileIndex = (int)nextToPlay;
            // Spawn lại ở vị trí xuất phát (ví dụ x=0, y=8)
            activePiece.Initialize(new Vector3Int(0, 8, 0), nextToPlay, tiles[tileIndex]);

            // Kiểm tra nếu vừa đổi gạch ra mà bị kẹt ngay (Game Over)
            if (!board.IsValidMove(activePiece.cells, activePiece.position))
            {
                Debug.LogError("GAME OVER ON HOLD!");
            }

            board.Render(activePiece, true);
        }

        canHold = false;
    }

    public void SpawnNewPiece()
    {
        canHold = true;
        int nextID = tetrisQueue.GetNextPiece();

        // Vị trí xuất phát chuẩn (thường là giữa đỉnh bàn cờ)
        Vector3Int spawnPos = new Vector3Int(0, 8, 0);
        activePiece.Initialize(spawnPos, (Tetromino)nextID, tiles[nextID]);

        if (!board.IsValidMove(activePiece.cells, activePiece.position))
        {
            Debug.LogError("GAME OVER!");
            // Ở đây bạn có thể gọi màn hình Game Over UI
            this.enabled = false; // Dừng controller
            return;
        }

        board.Render(activePiece, true);
    }
}