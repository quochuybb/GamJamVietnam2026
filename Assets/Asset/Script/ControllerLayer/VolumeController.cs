namespace Asset.Script.ControllerLayer
{
    using UnityEngine;
    using UnityEngine.UI;

    public class VolumeController : MonoBehaviour
    {
        [Header("Settings")]
        public Image buttonImage1; // Kéo Image của nút vào đây
        public Image buttonImage2; // Kéo Image của nút vào đây
        public Color mutedColor = Color.red;
        public Color normalColor = Color.white;

        private bool isMuted = false;

        void Start()
        {
            // Kiểm tra trạng thái đã lưu trước đó (tùy chọn)
            isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
            ApplySoundState();
        }

        public void ToggleSound()
        {
            isMuted = !isMuted;
        
            // Lưu trạng thái để lần sau mở game vẫn giữ nguyên
            PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        
            ApplySoundState();

            // Chơi âm thanh click nếu muốn
            if (AudioManager.Instance != null) AudioManager.Instance.PlayButtonClick();
        }

        private void ApplySoundState()
        {
            // Tắt/Mở toàn bộ âm thanh trong hệ thống Unity
            AudioListener.pause = isMuted;

            // Thay đổi màu sắc của nút
            buttonImage1.color = isMuted ? mutedColor : normalColor;
            buttonImage2.color = isMuted ? mutedColor : normalColor;

        }   
    }
}