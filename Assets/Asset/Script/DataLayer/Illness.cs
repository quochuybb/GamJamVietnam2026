using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Medical/Illness Definition")]
public class IllnessSO : ScriptableObject
{
    public string displayName; 
    public List<SymptomSO> coreSymptoms ; 
    public List<SymptomSO> supportingSymptoms ;
    public List<SymptomSO> forbiddenSymptoms ;
}