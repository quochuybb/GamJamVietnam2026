using UnityEngine.UI;

namespace Asset.Script.ControllerLayer
{
    using UnityEngine;
    using DG.Tweening; // Bắt buộc

    public class MainPanelController : Singleton<MainPanelController> 
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
            AudioManager.Instance.PlayRandomMenuMusic();
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
            // 1. Reset logic các Manager
            PatientManager.Instance.ResetManager();
            DiagnosisManager.Instance.ResetDiagnosisUI();
            InterrogationManager.Instance.ResetInterrogation();
            AudioManager.Instance.PlayRandomMenuMusic();

            // 2. Reset UI
            if (boookButton != null) boookButton.gameObject.SetActive(false);

            // 3. Tween quay về menu
            mainMenuPanel.DOAnchorPosY(0, tweenDuration).SetEase(transitionEase);
            gameplayPanel.DOAnchorPosY(-screenHeight, tweenDuration).SetEase(transitionEase);
        }
            public void OnQuitGame()
            {
                Debug.Log("Đang thoát game...");

                // Thoát ứng dụng (Dành cho bản Build chính thức)
                Application.Quit();

                // Dòng này giúp nút thoát hoạt động ngay cả khi bạn đang chạy thử trong Unity Editor
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}