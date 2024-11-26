using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroTrigger : MonoBehaviour
{
    [SerializeField] private int unfinishedRoomVoiceId;
    [SerializeField] private int outroVoiceId;
    [SerializeField] private CT_TheEnd endScreen;
    private VoiceOver_Manager _voiceOverManager;

    private void Awake()
    {
        _voiceOverManager = GameObject.Find ("VoiceOver_Manager").GetComponent<VoiceOver_Manager>();
    }
    
    private IEnumerator PlayVoiceLine(int id)
    {
        while (_voiceOverManager.audioPlaying)
        {
            yield return new WaitForSeconds(0);
        }
        
        TextProperties textProperties = gameObject.GetComponent<TextProperties>();
        if (textProperties)
        {
            if (_voiceOverManager)
            {
                _voiceOverManager.setupNewVoice(
                    textProperties.r_TextList(),
                    textProperties.t_Language,
                    id,
                    textProperties.r_TextList().voiceOverDescription(textProperties.t_Language, textProperties.managerID),
                    textProperties.r_TextList().r_audioPriority(textProperties.t_Language, textProperties.managerID),
                    false);
            }
        }
        yield return null;
    }

    private IEnumerator ShowOutroScreen()
    {
        yield return new WaitForSeconds(5);
        while (_voiceOverManager.audioPlaying)
        {
            yield return new WaitForSeconds(0);
        }
        
        endScreen.activateEndScreen();
        StartCoroutine(PlayVoiceLine(outroVoiceId));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayVoiceLine(unfinishedRoomVoiceId));
            StartCoroutine(ShowOutroScreen());
        }
    }
}
