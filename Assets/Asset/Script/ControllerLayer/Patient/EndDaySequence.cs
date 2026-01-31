using UnityEngine;
using DG.Tweening; // Uses the same tweening library as your MainPanelController
using UnityEngine.UI;
using TMPro;
using Asset.Script.ControllerLayer;

public class EndDaySequence : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private RectTransform textRectTransform; // Drag your TextMeshPro object here
    [SerializeField] private CanvasGroup panelCanvasGroup;    // Add a CanvasGroup component to the EndDayPanel
    
    
    [Header("Settings")]
    [SerializeField] private float scrollDuration = 20f;      // How long the text takes to scroll
    [SerializeField] private float endPosY = 1500f;           // How high the text goes (adjust based on text length)
    [SerializeField] private float startPosY = -800f;         // Starting position (below screen)

    public void PlayCredits()
    {
        gameObject.SetActive(true);
        
        // 1. Reset Position
        textRectTransform.anchoredPosition = new Vector2(0, startPosY);
        panelCanvasGroup.alpha = 0;

        // 2. Fade In the black background
        panelCanvasGroup.DOFade(1, 1f).OnComplete(() => {
            // 3. Start Scrolling Up
            textRectTransform.DOAnchorPosY(endPosY, scrollDuration)
                .SetEase(Ease.Linear) // Linear makes it scroll at constant speed like a movie
                .OnComplete(FinishSequence);
        });
    }

    // Allow player to skip by clicking (Optional)
    public void OnSkipClicked()
    {
        // Kill the current animation to prevent conflicts
        textRectTransform.DOKill(); 
        FinishSequence();
    }

    private void FinishSequence()
    {
        // Return to Main Menu using your existing controller
        MainPanelController.Instance.OnBackToMainMenu();
        
        // Hide this panel
        gameObject.SetActive(false);
    }
}