using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer maskRenderer;
    [SerializeField] private Sprite[] maskSprites;
    
    public PatientProfileSO currentData;

    public void PrepareData(PatientProfileSO data)
    {
        currentData = data;
    }

    private void OnEnable()
    {
        if (currentData == null) return;

        bodyRenderer.sprite = currentData.body;
        maskRenderer.sprite = currentData.mask;
        
        Debug.Log($"Bệnh nhân {currentData.id} đã ngồi vào ghế.");
    }

    public void SetMask(Sprite sprite)
    {
        currentData.mask = sprite;
        maskRenderer.sprite = sprite;
    }

    public void setEmotion(Emotion emotion)
    {
        currentData.emotion = emotion;
    }

}
