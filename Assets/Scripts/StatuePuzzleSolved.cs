using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePuzzleSolved : MonoBehaviour
{
    [SerializeField] private GameObject[] puzzleTriggersToDisable;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject trigger in puzzleTriggersToDisable)
        {
            StatuePuzzle statuePuzzle = GetComponentInParent<StatuePuzzle>();
            trigger.SetActive(false);
            statuePuzzle.CompletePuzzle();
            statuePuzzle.EarnAchievement(AchievementId.DragonSolve);
        }
    }
}
