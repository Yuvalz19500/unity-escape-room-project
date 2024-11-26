using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform teleportLocation;
    [SerializeField] private LayerMask layersToCheck;
    [SerializeField] private int teleportCountToPlayVoice = 3;
    
    private int _teleportCount = 0;
    private bool _diesToFireVoicePlayed = false;

    private void Update()
    {
        if (_teleportCount < teleportCountToPlayVoice || _diesToFireVoicePlayed) return;
        StatuePuzzle statuePuzzle = GetComponentInParent<StatuePuzzle>();
        StartCoroutine(statuePuzzle.PlayVoiceLine(statuePuzzle.diesToFireVoiceId));
        _diesToFireVoicePlayed = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out PlayerRaycast playerRaycast)) return;

        if (!playerRaycast.ShootRaycast(out RaycastHit hit, layersToCheck)) return;
        if (!hit.collider.CompareTag("StatueRaycastCollider"))
        {
            playerRaycast.gameObject.transform.position = teleportLocation.position;
            _teleportCount++;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerRaycast playerRaycast)) return;

        if (!playerRaycast.ShootRaycast(out RaycastHit hit, layersToCheck)) return;
        if (!hit.collider.CompareTag("StatueRaycastCollider"))
        {
            playerRaycast.gameObject.transform.position = teleportLocation.position;
            _teleportCount++;
        }
    }
}
