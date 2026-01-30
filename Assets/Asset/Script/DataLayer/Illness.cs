using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Medical/Illness Definition")]
public class IllnessSO : ScriptableObject
{
    public string displayName; 
    [TextArea] public string description; 
    public List<SymptomSO> keySymptoms; 
}