using UnityEngine;
using TMPro;

public class TooltipManager : Singleton<TooltipManager>
{
    [Header("UI References")]
    [SerializeField] private GameObject tooltipObject;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private RectTransform tooltipRect;
    
    // Thêm tham chiếu đến Canvas chính
    [SerializeField] private Canvas mainCanvas; 

    private void Awake()
    {
        HideTooltip();
    }

    private void Update()
    {
        if (tooltipObject.activeSelf)
        {
            SetTooltipPosition();
        }
    }

    private void SetTooltipPosition()
    {
        Vector2 mousePos = Input.mousePosition;

        // 1. Tính toán Pivot thông minh (giữ nguyên logic của bạn để tooltip không tràn màn hình)
        float pivotX = mousePos.x / Screen.width;
        float pivotY = mousePos.y / Screen.height;
        tooltipRect.pivot = new Vector2(pivotX, pivotY);

        // 2. CHUYỂN ĐỔI TỌA ĐỘ CHUẨN (The Magic Fix)
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mainCanvas.transform as RectTransform, // Lấy khung hình chữ nhật của Canvas cha
            mousePos,                              // Vị trí chuột
            mainCanvas.worldCamera,                // Camera render UI (null nếu là Overlay)
            out localPoint                         // Kết quả trả về
        );

        // 3. Gán vị trí cục bộ thay vì vị trí thế giới
        tooltipObject.transform.localPosition = localPoint;
    }

    public void ShowTooltip(string content)
    {
        descriptionText.text = content;
        tooltipObject.SetActive(true);
        SetTooltipPosition(); // Cập nhật vị trí ngay lập tức để tránh nháy hình
    }

    public void HideTooltip()
    {
        tooltipObject.SetActive(false);
    }
}