using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
public class InterrogationManager : Singleton<InterrogationManager>
{
    [SerializeField] private TextMeshProUGUI dialogueText; 
    [SerializeField] private TextMeshProUGUI indentityDialogueText; 
    [SerializeField] private Transform choiceContainer;    
    [SerializeField] private Button choiceButtonPrefab;   
    [SerializeField] private Slider overloadSlider;   

    [Header("External References")]
    [SerializeField] private PatientManager patientManager; 
    [SerializeField] private Button continueButton; 

    private Story _story;

    public void StartSession(PatientProfileSO profile)
    {
        _story = new Story(profile.inkJSONAsset.text);
        BindInkFunctions();
        
        dialogueText.text = "";
        ClearChoices();

        continueButton.gameObject.SetActive(true);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(AdvanceStory); 

        AdvanceStory();
    }
    public void AdvanceStory()
    {
        if (_story.canContinue)
        {
            string line = _story.Continue();
            if (string.IsNullOrWhiteSpace(line))
            {
                AdvanceStory();
                return;
            }

            if (line.Trim().StartsWith(">>>"))
            {
                HandleSpecialTags(line.Trim());
                return;
            }
            string[] lines = line.Split(": ");
            indentityDialogueText.text = lines[0];
            dialogueText.text = lines[1];
        }
        else if (_story.currentChoices.Count > 0)
        {
            continueButton.gameObject.SetActive(false);
            
            CreateChoiceButtons();
        }
        else
        {
            Debug.Log("End of Story");
            continueButton.gameObject.SetActive(false);
        }
    }

    private void OnClickChoice(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index);
        ClearChoices();
        UpdateOverloadMeter();
        
        continueButton.gameObject.SetActive(true);
        
        AdvanceStory();
    }
    private void BindInkFunctions()
    {
        _story.BindExternalFunction("SetSpriteState", (string stateName) => {
            if(patientManager != null) patientManager.SwapMask(stateName);
            Debug.Log($"Ink gọi đổi Sprite: {stateName}");
        });
        _story.BindExternalFunction("SetNotebookActive", (bool isActive) => {
            DiagnosisManager.Instance.TakeNoteBook();
            
        });
    }
    private void CreateChoiceButtons()
    {
        foreach (Choice choice in _story.currentChoices)
        {
            Button button = Instantiate(choiceButtonPrefab, choiceContainer);
            
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null) buttonText.text = choice.text;

            button.onClick.AddListener(() => OnClickChoice(choice));
        }
    }


    private void UpdateOverloadMeter()
    {
        if (_story.variablesState.Contains("overload_meter"))
        {
            float val = (float)_story.variablesState["overload_meter"];
            if(overloadSlider != null) overloadSlider.value = val;
            
            // if (val <= -50) GameOver();
        }
    }
    private void HandleSpecialTags(string tag)
    {
        if (tag.Contains("START_DIAGNOSIS"))
        {
            Debug.Log("Hội thoại kết thúc - Cho phép chẩn đoán!");
        
            // Gọi hàm hiển thị nút bên DiagnosisManager
            DiagnosisManager.Instance.EnableAnalyzeButton();
        
            // Có thể ẩn luôn nút Continue nếu muốn
            continueButton.gameObject.SetActive(false);
        }
    }
    private void ClearChoices()
    {
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }
    }
}