using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiagnosisManager : Singleton<DiagnosisManager>
{
    [SerializeField] private Button symptomsPrefab;
    [SerializeField] private Button IllnessPrefab;
    [SerializeField] private Transform symptomsParent;
    [SerializeField] private Transform symptomsParent1;
    [SerializeField] private GameObject symptomsContainer;
    [SerializeField] private SymptomSO[] allPossibleSymptomsR; 
    [SerializeField] private SymptomSO[] allPossibleSymptomsL; 
    [SerializeField] private IllnessSO[] allIllnesses;
    [SerializeField] private GameObject Book;
    [SerializeField] private Sprite[] tickBoxImages;
    [SerializeField] private GameObject panelScreen;
    [SerializeField] private GameObject analyzeButton;
    [SerializeField] private Button confirmButton; 
    
    private bool isClosed = false;

    public List<SymptomSO> selectedSymptoms = new List<SymptomSO>();
    private List<IllnessSO> selectedIllnesses = new List<IllnessSO>();
    private List<IllnessSO> tempSelectedIllnesses = new List<IllnessSO>();

    private void Start()
    {
        if (analyzeButton != null) 
            analyzeButton.SetActive(false);     
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
            
            button.onClick.AddListener(() => ToggleSymptom(symptom,button));

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
            
            button.onClick.AddListener(() => ToggleSymptom(symptom,button));

        }
    
        symptomsParent.gameObject.SetActive(false);
        symptomsParent1.gameObject.SetActive(false);
        symptomsContainer.gameObject.SetActive(false);
        if (confirmButton != null) confirmButton.gameObject.SetActive(false);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
    }


    public void EnableAnalyzeButton()
    {
        if (analyzeButton != null)
            analyzeButton.gameObject.SetActive(true);
    }

    public void ToggleSymptom(SymptomSO symptom, Button button)
    {
        if (!selectedSymptoms.Contains(symptom))
        {
            button.image.sprite = tickBoxImages[0];
            selectedSymptoms.Add(symptom);
        }
        else
        {
            
            button.image.sprite = tickBoxImages[1];
            selectedSymptoms.Remove(symptom);
        }
        selectedIllnesses = AnalyzeSymptoms(selectedSymptoms);
        
    }

    private void DestroyIllnesses()
    {
        foreach (Transform child in panelScreen.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void ShowIllness()
    {
        if (panelScreen.activeInHierarchy)
        {
            panelScreen.SetActive(false);
        }
        else
        {
            Book.SetActive(false);
            panelScreen.SetActive(true);
            CloseNoteBook();
        }

        
        // Xóa các nút cũ trước khi tạo mới
        foreach (Transform child in panelScreen.transform)
        {
            Destroy(child.gameObject);
        }

        // Hiện nút Confirm khi bảng bệnh mở ra
        if (confirmButton != null) confirmButton.gameObject.SetActive(true);

        foreach (IllnessSO illness in selectedIllnesses)
        {
            Button button = Instantiate(IllnessPrefab, panelScreen.transform);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null) buttonText.text = illness.displayName;

            // Kiểm tra xem bệnh này đã được tick trước đó chưa để hiện đúng icon
            if (tempSelectedIllnesses.Contains(illness))
                button.image.sprite = tickBoxImages[0]; // Ảnh đã tick
            else
                button.image.sprite = tickBoxImages[1]; // Ảnh chưa tick

            button.onClick.AddListener(() => ToggleIllnessSelection(illness, button));
        }
    }

    public void ToggleIllnessSelection(IllnessSO illness, Button button)
    {
        if (!tempSelectedIllnesses.Contains(illness))
        {
            tempSelectedIllnesses.Add(illness);
            button.image.sprite = tickBoxImages[0]; // Đổi sang ảnh đã tick
            Debug.Log("Đã chọn: " + illness.displayName);
        }
        else
        {
            tempSelectedIllnesses.Remove(illness);
            button.image.sprite = tickBoxImages[1]; // Đổi sang ảnh chưa tick
            Debug.Log("Bỏ chọn: " + illness.displayName);
        }
    }
    public List<IllnessSO> AnalyzeSymptoms(List<SymptomSO> selectedSymptoms)
    {
        Dictionary<IllnessSO, int> potentialCandidates = new Dictionary<IllnessSO, int>();

        HashSet<SymptomSO> inputSet = new HashSet<SymptomSO>(selectedSymptoms);

        foreach (var illness in allIllnesses)
        {

            bool isForbidden = illness.forbiddenSymptoms.Any(s => inputSet.Contains(s));
            if (isForbidden) continue;
            
            bool hasAllCores = illness.coreSymptoms.All(s => inputSet.Contains(s));
            
            if (!hasAllCores) continue;
            
            int score = illness.supportingSymptoms.Count(s => inputSet.Contains(s));

            potentialCandidates.Add(illness, score);
        }

        return potentialCandidates.OrderByDescending(x => x.Value)
            .Select(x => x.Key)
            .ToList();
    }
    
    public void ConfirmDiagnosis()
    {
        List<IllnessSO> trueIllnesses = PatientManager.Instance.patientVisual.currentData.trueIllness;

        // 1. Kiểm tra số lượng có khớp không
        if (tempSelectedIllnesses.Count != trueIllnesses.Count)
        {
            GameOver("Chẩn đoán sai số lượng bệnh!");
            return;
        }

        // 2. Kiểm tra xem tất cả các bệnh người chơi chọn có nằm trong danh sách bệnh đúng không
        bool isAllCorrect = true;
        foreach (var selected in tempSelectedIllnesses)
        {
            if (!trueIllnesses.Contains(selected))
            {
                isAllCorrect = false;
                break;
            }
        }

        if (isAllCorrect)
        {
            GameWin();
        }
        else
        {
            GameOver("Chẩn đoán sai loại bệnh!");
        }
    }
    
    
    private void GameWin()
    {
        Debug.Log("🏆 BẠN ĐÃ THẮNG!");
        PatientManager.Instance.NextPatient();

    }
    private void GameOver(string reason)
    {
        Debug.Log("💀 GAME OVER: " + reason);
        // Hiển thị UI thua cuộc tại đây
    }
    
    public void TakeNoteBook()
    {
        DestroyIllnesses();
        panelScreen.SetActive(false);
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

    public void CloseNoteBook()
    {
        symptomsContainer.gameObject.SetActive(false);
        symptomsParent1.gameObject.SetActive(false);
        symptomsParent.gameObject.SetActive(false);
        isClosed = true;
        Book.SetActive(false); 
    }
    
    public void ResetDiagnosisUI()
    {
        // 1. Xóa danh sách đã chọn
        selectedSymptoms.Clear();
        tempSelectedIllnesses.Clear();
        selectedIllnesses.Clear();

        // 2. Ẩn các Panel và Nút
        if (panelScreen != null) panelScreen.SetActive(false);
        if (confirmButton != null) confirmButton.gameObject.SetActive(false);
        if (analyzeButton != null) analyzeButton.SetActive(false);
        if (symptomsContainer != null) symptomsContainer.SetActive(false);
    
        // 3. Đưa Notebook về trạng thái đóng
        isClosed = true;
        if (Book != null) Book.SetActive(false);

        // 4. Reset các Tickbox trên UI (Quan trọng)
        // Bạn cần reset thủ công các Image của Symptom buttons về tickBoxImages[1] (không tick)
        ResetSymptomButtonsUI();
    }

    private void ResetSymptomButtonsUI()
    {
        // Duyệt qua tất cả các button con trong Parent để đổi lại Sprite
        foreach (Transform child in symptomsParent)
        {
            var btn = child.GetComponent<Button>();
            if (btn != null) btn.image.sprite = tickBoxImages[1];
        }
        foreach (Transform child in symptomsParent1)
        {
            var btn = child.GetComponent<Button>();
            if (btn != null) btn.image.sprite = tickBoxImages[1];
        }
    }

}