using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTime : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    private float _currentTime = 0f;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        DisplayTime(_currentTime);
    }

    public bool StartTimer()
    {
        gameObject.SetActive(true);
        return true;
    }

    private void DisplayTime(float timeToDisplay){
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
