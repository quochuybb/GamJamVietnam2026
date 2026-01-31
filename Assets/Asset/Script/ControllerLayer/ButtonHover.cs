using UnityEngine;
using TMPro; 
using UnityEngine.EventSystems; // Bắt buộc để bắt sự kiện chuột

public class ButtonTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    [SerializeField] private TextMeshProUGUI textComponent; // Kéo cái Text vào đây
    [SerializeField] private Color hoverColor = Color.white; // Màu khi chỉ chuột vào
    
    private Color normalColor; // Biến lưu màu gốc để trả lại khi chuột đi ra

    private void Awake()
    {
        // Nếu quên kéo text thì tự tìm trong object con
        if (textComponent == null)
            textComponent = GetComponentInChildren<TextMeshProUGUI>();

        // Lưu lại màu gốc ban đầu
        if (textComponent != null)
        {
            normalColor = textComponent.color;
        }
    }

    // Khi chuột bay vào (Hover)
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textComponent != null)
        {
            textComponent.color = hoverColor;
        }
    }

    // Khi chuột bay ra (Exit)
    public void OnPointerExit(PointerEventData eventData)
    {
        if (textComponent != null)
        {
            textComponent.color = normalColor;
        }
    }
}