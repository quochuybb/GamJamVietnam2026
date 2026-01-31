using System.Collections;
using System.Collections.Generic;
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
            // Bạn có thể hiện bảng tổng kết ngày ở đây
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
}
