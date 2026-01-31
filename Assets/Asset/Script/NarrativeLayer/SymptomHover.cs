using UnityEngine;
using UnityEngine.EventSystems;

public class SymptomHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string descriptionToShow;

    public void Setup(string desc)
    {
        this.descriptionToShow = desc;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(descriptionToShow))
        {
            TooltipManager.Instance.ShowTooltip(descriptionToShow);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideTooltip();
    }
}