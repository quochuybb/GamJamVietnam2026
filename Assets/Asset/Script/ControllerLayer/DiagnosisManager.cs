using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiagnosisManager : Singleton<DiagnosisManager>
{
    [SerializeField] private Button symptomsPrefab;
    [SerializeField] private Transform symptomsParent;
    [SerializeField] private Transform symptomsParent1;
    [SerializeField] private GameObject symptomsContainer;
    [SerializeField] private SymptomSO[] allPossibleSymptomsR; 
    [SerializeField] private SymptomSO[] allPossibleSymptomsL; 
    [SerializeField] private IllnessSO[] allIllnesses;
    [SerializeField] private GameObject Book;
    private bool isClosed = false;

    public List<SymptomSO> selectedSymptoms = new List<SymptomSO>();

    private void Start()
    {
        foreach (SymptomSO symptom in allPossibleSymptomsR)
        {
            Button button = Instantiate(symptomsPrefab, symptomsParent);
            
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null) buttonText.text = symptom.displayName;

            SymptomHover hoverScript = button.GetComponent<SymptomHover>();
    
            if (hoverScript != null)
            {
                hoverScript.Setup(symptom.discription); 
            }
            
            button.onClick.AddListener(() => ToggleSymptom(symptom));

        }
        foreach (SymptomSO symptom in allPossibleSymptomsL)
        {
            Button button = Instantiate(symptomsPrefab, symptomsParent1);
            
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null) buttonText.text = symptom.displayName;

            SymptomHover hoverScript = button.GetComponent<SymptomHover>();
    
            if (hoverScript != null)
            {
                hoverScript.Setup(symptom.discription); 
            }
            
            button.onClick.AddListener(() => ToggleSymptom(symptom));

        }

        symptomsParent.gameObject.SetActive(false);
        symptomsParent1.gameObject.SetActive(false);
        symptomsContainer.gameObject.SetActive(false);
    }


    

    public void ToggleSymptom(SymptomSO symptom)
    {
        if (!selectedSymptoms.Contains(symptom))
        {
            selectedSymptoms.Add(symptom);
        }
        else
        {
            selectedSymptoms.Remove(symptom);
        }

    }

    public void TakeNoteBook()
    {
        if (!isClosed)
        {
            symptomsContainer.gameObject.SetActive(true);
            isClosed = true;
        }
        else
        {
            symptomsParent1.gameObject.SetActive(false);
            symptomsParent.gameObject.SetActive(false);
            Book.SetActive(true);
            symptomsContainer.gameObject.SetActive(false);
            isClosed = false;
        }
    }

    public void OpenNoteBook()
    {
        symptomsParent.gameObject.SetActive(true);
        symptomsParent1.gameObject.SetActive(true);
        Book.SetActive(false);
    }

}