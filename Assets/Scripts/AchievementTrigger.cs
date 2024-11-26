using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTrigger : MonoBehaviour
{
    [SerializeField] private AchievementManager achievementManager;
    [SerializeField] private AchievementId achievement;
    private bool _wasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (_wasTriggered) return;
        if(!other.CompareTag("Player")) return;

        achievementManager.Achieved(achievement);
        _wasTriggered = true;
    }
}