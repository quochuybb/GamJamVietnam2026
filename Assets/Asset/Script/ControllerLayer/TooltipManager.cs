using UnityEngine;
using TMPro;

public class TooltipManager : Singleton<TooltipManager>
{
    [Header("UI References")]
    [SerializeField] private GameObject tooltipObject;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private RectTransform tooltipRect;
    [SerializeField] private Canvas mainCanvas;

    [Header("Settings")]
    // X âm = sang trái, Y dương = đi lên. Bạn có thể chỉnh số này trong Inspector
    [SerializeField] private Vector2 tooltipOffset = new Vector2(50f, 100f); 

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

        // 1. Pivot Logic (Giữ nguyên)
        float pivotX = mousePos.x / Screen.width;
        float pivotY = mousePos.y / Screen.height;
        tooltipRect.pivot = new Vector2(pivotX, pivotY);

        // 2. Chuyển đổi tọa độ
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mainCanvas.transform as RectTransform,
            mousePos,
            mainCanvas.worldCamera,
            out localPoint
        );

        // 3. THÊM OFFSET TẠI ĐÂY (Sửa đổi mới)
        // Cộng thêm khoảng cách bạn muốn vào vị trí chuột đã tính toán
        localPoint += tooltipOffset;

        tooltipObject.transform.localPosition = localPoint;
    }

    public void ShowTooltip(string content)
    {
        descriptionText.text = content;
        tooltipObject.SetActive(true);
        SetTooltipPosition();
    }

    public void HideTooltip()
    {
        tooltipObject.SetActive(false);
    }
}