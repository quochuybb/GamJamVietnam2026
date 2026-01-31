using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Cần thư viện này để lọc danh sách nhanh

public class MedicalFilterSystem : MonoBehaviour
{
    [Header("Database")]
    public List<IllnessSO> allIllnesses; 
    public List<SymptomSO> allPossibleSymptoms; 

    [Header("Runtime Selection")]
    public List<SymptomSO> selectedSymptoms = new List<SymptomSO>();
    
    /*[SerializeField] private IllnessUIItem[] illnessUIList; 

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
        FilterIllnesses();
    }

    private void FilterIllnesses()
    {
        foreach (var uiItem in illnessUIList)
        {
            IllnessSO illness = uiItem.illnessData;
            
            bool isMatch = true;
            
            if (selectedSymptoms.Count == 0)
            {
                isMatch = true; 
            }
            else
            {
                isMatch = !selectedSymptoms.Except(illness.requiredSymptoms).Any();
            }

            uiItem.SetInteractable(isMatch); 
        }
    }*/
}