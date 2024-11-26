using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject timerCanvas;
    [SerializeField] private TMP_Text timeText;

    private bool _isBlinking = false;

    public void DisplayTime(float timeToDisplay){
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }
    
    public void StartBlink(float rate)
    {
        if (_isBlinking) return;;

        _isBlinking = true;
        InvokeRepeating(nameof(Blink), 0f, rate);
    }

    public void StopBlink()
    {
        CancelInvoke(nameof(Blink));
        timerCanvas.SetActive(true);
    }

    private void Blink()
    {
        timerCanvas.SetActive(!timerCanvas.activeInHierarchy);
    }
}
