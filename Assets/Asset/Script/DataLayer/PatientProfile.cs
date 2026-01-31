using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Game/Character Profile")]
public class PatientProfileSO : ScriptableObject
{
    public string id;
    public TextAsset inkJSONAsset;
    public Sprite body;
    public Sprite mask;
    public Emotion emotion;
    public int maxOverload;
    public int difficulty;
    public IllnessSO[] trueIllness;
}