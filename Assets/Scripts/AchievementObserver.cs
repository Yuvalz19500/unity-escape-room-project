using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementObserver", menuName = "ScriptableObject/AchievementObserver")]
public class AchievementObserver : ScriptableObject
{
    public event Action<AchievementId> OnAchievementEarned;

    public void EarnAchievement(AchievementId achievementId)
    {
        OnAchievementEarned?.Invoke(achievementId);
    }
}
