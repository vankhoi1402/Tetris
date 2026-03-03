using UnityEngine;
using UnityEngine.InputSystem;
using System;
using static UnityEngine.Rendering.DebugUI;

public class TetrisInputHandler : MonoBehaviour , InputSystem_Actions.IPlayerActions
{
    InputSystem_Actions inputActions;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
    }
    // Các sự kiện để các script khác đăng ký
    public static event Action<float> OnMovePerformed;
    public static event Action OnRotatePerformed;
    public static event Action OnHardDropPerformed;
    public static event Action OnHoldPerformed;

    // Hàm này nối với Player Input Component (Events)
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float dir = context.ReadValue<float>();
            // Chỉ gửi sự kiện nếu là di chuyển ngang (x)
            OnMovePerformed?.Invoke(dir);

           
        }
    }

    public void OnRotation(InputAction.CallbackContext context)
    {
        if (context.performed) OnRotatePerformed?.Invoke();
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (context.performed) OnHardDropPerformed?.Invoke();
    }
    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.performed) OnHoldPerformed?.Invoke();
    }
}
