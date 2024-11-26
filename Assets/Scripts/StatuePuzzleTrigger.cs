using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePuzzleTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask layersToCheck;

    private bool _firstTimeFireTrigger = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerRaycast playerRaycast)) return;
        StatuePuzzle statuePuzzle = GetComponentInParent<StatuePuzzle>();

        if (!playerRaycast.ShootRaycast(out RaycastHit hit, layersToCheck)) return;
        if (!hit.collider.CompareTag("StatueRaycastCollider"))
        {
            statuePuzzle.ToggleFire(true);
            if (_firstTimeFireTrigger)
            {
                StartCoroutine(statuePuzzle.PlayVoiceLinesAfterFirstTrigger());
                statuePuzzle.EarnAchievement(AchievementId.DragonFire);
                _firstTimeFireTrigger = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out PlayerRaycast playerRaycast)) return;
        StatuePuzzle statuePuzzle = GetComponentInParent<StatuePuzzle>();
        
        statuePuzzle.ToggleFire(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out PlayerRaycast playerRaycast)) return;
        StatuePuzzle statuePuzzle = GetComponentInParent<StatuePuzzle>();
        
        if (!playerRaycast.ShootRaycast(out RaycastHit hit, layersToCheck)) return;
        statuePuzzle.ToggleFire(!hit.collider.CompareTag("StatueRaycastCollider"));
    }
}
