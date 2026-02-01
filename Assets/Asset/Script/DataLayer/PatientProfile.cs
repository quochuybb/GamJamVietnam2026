using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Game/Character Profile")]
public class PatientProfileSO : ScriptableObject
{
    public string id;
    public TextAsset inkJSONAsset;
    public string description;
    public Sprite body;
    public Sprite mask;
    public Emotion emotion;
    public int maxOverload;
    public int difficulty;
    public List<IllnessSO> trueIllness;
    public AudioClip themeMusic;
}