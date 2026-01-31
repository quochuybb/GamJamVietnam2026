namespace Asset.Script.ControllerLayer
{
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 

public class SettingsToggle : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform settingsPanel;
    [SerializeField] private Button btn; 

    [Header("Positions")]
    [SerializeField] private Vector2 hiddenPos = new Vector2(0, -1000); 
    [SerializeField] private Vector2 visiblePos = Vector2.zero;         

    [Header("Animation")]
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Ease showEase = Ease.OutBack; 
    [SerializeField] private Ease hideEase = Ease.InBack; 

    private bool isOpen = false; 

    private void Start()
    {
        settingsPanel.anchoredPosition = hiddenPos;
        
        if (btn == null) btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleSettings);
    }

    public void ToggleSettings()
    {
        settingsPanel.DOKill();

        if (isOpen)
        {
            ClosePanel();
        }
        else
        {
            OpenPanel();
        }
    }

    void OpenPanel()
    {
        settingsPanel.DOAnchorPos(visiblePos, duration)
                     .SetEase(showEase)
                     .SetUpdate(true); 
        isOpen = true;
    }

    void ClosePanel()
    {
        settingsPanel.DOAnchorPos(hiddenPos, duration)
                     .SetEase(hideEase)
                     .SetUpdate(true);
        isOpen = false;
    }
}
}