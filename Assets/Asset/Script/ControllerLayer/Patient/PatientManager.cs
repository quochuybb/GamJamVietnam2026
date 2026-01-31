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

    [Header("Systems")]

    private GameObject patientInstance; 
    private PatientVisual patientVisual;
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
        if (patientInstance.activeSelf)
        {
            patientInstance.SetActive(false);
            yield return new WaitForSeconds(0.5f); 
        }

        if (currentIndex < dailyPatientList.Count)
        {
            PatientProfileSO nextData = dailyPatientList[currentIndex];

            patientVisual.PrepareData(nextData);

            patientInstance.SetActive(true);
            InterrogationManager.Instance.StartSession(nextData);
            
            currentIndex++;
        }
        else
        {
            Debug.Log("Hết bệnh nhân. End Day!");
        }
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
