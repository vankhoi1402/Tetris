using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    public ButtonType type;
    public float hoverScale = 1.1f;
    public float pressedScale = 0.95f;

    private Vector3 originalScale;
    private Button button;
    private BaseButtonManager manager;
    private bool isPointerOver = false;

    void Awake()
    {
        button = GetComponent<Button>();
        originalScale = transform.localScale;
        manager = Object.FindFirstObjectByType<BaseButtonManager>();
    }

    void Start()
    {
        // Sự kiện Click chính thức (sau khi nhấn và thả)
        button.onClick.AddListener(() => {
            manager?.OnButtonClick(type);
        });
    }

    // --- XỬ LÝ HOVER ---
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        ButtonEvent.OnHover.Invoke();
        transform.localScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        transform.localScale = originalScale;
    }

    // --- XỬ LÝ CLICK/PRESS ---
    public void OnPointerDown(PointerEventData eventData)
    {
        // Khi nhấn xuống thì thu nhỏ lại tạo cảm giác nút bị lún xuống
        transform.localScale = originalScale * pressedScale;
        ButtonEvent.OnHover.Invoke();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Khi thả tay ra: 
        // Nếu vẫn đang ở trong nút thì trả về hoverScale, nếu đã kéo ra ngoài thì về original
        transform.localScale = isPointerOver ? originalScale * hoverScale : originalScale;
    }

    private void OnDisable()
    {
        // Reset về ban đầu nếu Button bị ẩn đi đột ngột để tránh lỗi kẹt Scale
        transform.localScale = originalScale;
        isPointerOver = false;
    }
}