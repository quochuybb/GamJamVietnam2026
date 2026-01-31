using UnityEngine.UI;

namespace Asset.Script.ControllerLayer
{
    using UnityEngine;
    using DG.Tweening; // Bắt buộc

    public class MainPanelController : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private RectTransform mainMenuPanel;
        [SerializeField] private RectTransform gameplayPanel;
        [SerializeField] private Button boookButton;

        [Header("Settings")]
        [SerializeField] private float tweenDuration = 0.5f;
        [SerializeField] private Ease transitionEase = Ease.InOutQuad;
        
        private float screenHeight;

        private void Start()
        {
            screenHeight = Screen.height;
            boookButton.gameObject.SetActive(false);
            mainMenuPanel.anchoredPosition = Vector2.zero;
            gameplayPanel.anchoredPosition = new Vector2(0, -screenHeight);
        }
        
        public void OnStartGame()
        {
            mainMenuPanel.DOAnchorPosY(screenHeight, tweenDuration)
                .SetEase(transitionEase);
            
            gameplayPanel.DOAnchorPosY(0, tweenDuration)
                .SetEase(transitionEase);
            boookButton.gameObject.SetActive(true);

        }


        public void OnBackToMainMenu()
        {
            boookButton.gameObject.SetActive(false);

            mainMenuPanel.DOAnchorPosY(0, tweenDuration)
                .SetEase(transitionEase);
            gameplayPanel.DOAnchorPosY(-screenHeight, tweenDuration)
                .SetEase(transitionEase);

        }
    }
}