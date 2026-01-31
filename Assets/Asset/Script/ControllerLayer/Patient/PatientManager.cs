    using System.Collections;
    using System.Collections.Generic;
using Asset.Script.ControllerLayer;
using UnityEngine;

    public class PatientManager : Singleton<PatientManager>
    {
    [Header("Data")]
        public List<PatientProfileSO> dailyPatientList;
        public GameObject patientPrefab;
        public Transform spawnPoint;
        public Sprite[] patientSprites;
        [Header("UI Fader")]
        [SerializeField] private CanvasGroup faderCanvasGroup; // Kéo thả Canvas Group của Image đen vào đây
        [SerializeField] private float fadeDuration = 0.8f;
        [Header("Systems")]

        private GameObject patientInstance; 
        public PatientVisual patientVisual;
        private int currentIndex = 0;
        [Header("Win Cutscene")]
        [SerializeField] private GameObject winCutsceneVisual; // Kéo thả đối tượng "Image" (con của BlackPanel) vào đây
        [SerializeField] private float cutsceneTime = 3f;

        public void FirstPatientGoIn()
        {
            if (patientPrefab != null)
            {
                patientInstance = Instantiate(patientPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
                patientVisual = patientInstance.GetComponent<PatientVisual>();
                patientInstance.SetActive(false); 
            }
            NextPatient();
        }

        public void NextPatient()
        {
            StartCoroutine(SwitchPatientRoutine());
        }
        public void PlayWinCutscene()
        {
            StartCoroutine(WinSequenceRoutine());
        }

        private IEnumerator WinSequenceRoutine()
        {
            // 1. Màn hình tối dần (Fade OUT)
            yield return StartCoroutine(Fade(1f));

            // 2. Hiện hình ảnh Animation thắng cuộc
            if (winCutsceneVisual != null)
            {
                winCutsceneVisual.SetActive(true);
                // Vì Animator nằm trên Image này, nó sẽ tự động chạy clip mặc định khi được SetActive
        
                // 3. Trong lúc animation đang chạy, ta Reset dữ liệu bệnh nhân cũ ở nền sau
                DiagnosisManager.Instance.ResetDiagnosisUI();
                if (patientInstance != null) patientInstance.SetActive(false);

                // Đợi animation chạy xong
                yield return new WaitForSeconds(cutsceneTime);
        
                winCutsceneVisual.SetActive(false);
            }

            // 4. Chuẩn bị bệnh nhân mới
            if (currentIndex < dailyPatientList.Count)
            {
                PatientProfileSO nextData = dailyPatientList[currentIndex];
                patientVisual.PrepareData(nextData);
        
                yield return new WaitForSeconds(0.5f); // Nghỉ một chút giữa 2 bệnh nhân

                patientInstance.SetActive(true);
                InterrogationManager.Instance.StartSession(nextData);
                currentIndex++;

                // 5. Màn hình sáng lại (Fade IN)
                yield return StartCoroutine(Fade(0f));
            }
            else
            {
                Debug.Log("Hết ngày!");
            }
        }

        private IEnumerator SwitchPatientRoutine()
        {
            // 1. Fade OUT (Màn hình tối dần)
            yield return StartCoroutine(Fade(1f));

            // 2. Reset UI và Dữ liệu cũ trong lúc màn hình đang tối
            DiagnosisManager.Instance.ResetDiagnosisUI();

            if (patientInstance != null && patientInstance.activeSelf)
            {
                patientInstance.SetActive(false);
            }

            // 3. Chuẩn bị bệnh nhân mới
            if (currentIndex < dailyPatientList.Count)
            {
                PatientProfileSO nextData = dailyPatientList[currentIndex];
                if (nextData.themeMusic != null) {
                    AudioManager.Instance.PlayPatientTheme(nextData.themeMusic);
                }
                patientVisual.PrepareData(nextData);
                
                // Đợi một chút tạo cảm giác bệnh nhân mới bước vào
                yield return new WaitForSeconds(0.5f);

                patientInstance.SetActive(true);
                InterrogationManager.Instance.StartSession(nextData);
                
                currentIndex++;

                // 4. Fade IN (Màn hình sáng lại)
                yield return StartCoroutine(Fade(0f));
            }
            else
            {
                Debug.Log("Hết bệnh nhân. End Day!");
                MainPanelController.Instance.OnBackToMainMenu();
                
            }
        }

        private IEnumerator Fade(float targetAlpha)
        {
            if (faderCanvasGroup == null) yield break;

            float startAlpha = faderCanvasGroup.alpha;
            float time = 0;

            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                faderCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
                yield return null;
            }
            faderCanvasGroup.alpha = targetAlpha;
            
            // Chặn click khi màn hình đen, cho phép click khi màn hình trong suốt
            faderCanvasGroup.blocksRaycasts = (targetAlpha > 0);
        }

        public void SwapMask(string mask)
        {
            switch (mask)
            {
                case "anger":
                    patientVisual.setEmotion(Emotion.anger);
                    patientVisual.SetMask(patientSprites[0]);
                    break;
                case "disgust":
                    patientVisual.setEmotion(Emotion.disgust);
                    patientVisual.SetMask(patientSprites[1]);
                    break;
                case "sadness":
                    patientVisual.setEmotion(Emotion.sadness);
                    patientVisual.SetMask(patientSprites[2]);
                    break;
                case "happy":
                    patientVisual.setEmotion(Emotion.happy);
                    patientVisual.SetMask(patientSprites[3]);
                    break;
                case "suprised":
                    patientVisual.setEmotion(Emotion.suprised);
                    patientVisual.SetMask(patientSprites[4]);
                    break;
                case "fearful":
                    patientVisual.setEmotion(Emotion.fearful);
                    patientVisual.SetMask(patientSprites[5]);
                    break;
            }
        }
        public void ResetManager()
        {
            StopAllCoroutines(); // Dừng việc đang fade hoặc chuyển bệnh nhân
            currentIndex = 0;
            if (patientInstance != null) 
                patientInstance.SetActive(false);
    
            // Đưa màn hình fader về trong suốt
            if (faderCanvasGroup != null)
            {
                faderCanvasGroup.alpha = 0;
                faderCanvasGroup.blocksRaycasts = false;
            }
        }
    }
