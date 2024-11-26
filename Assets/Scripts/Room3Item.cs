using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3Item : MonoBehaviour
{
    [SerializeField] private Room3UI room3UI;
    [SerializeField] private int pillarVoiceId;
    [SerializeField] private int delayBeforeVoice = 3;
    
    private VoiceOver_Manager _voiceOverManager;

    private void Awake()
    {
        _voiceOverManager = GameObject.Find ("VoiceOver_Manager").GetComponent<VoiceOver_Manager>();
    }

    public void PickupItem()
    {
        room3UI.PillarCollected();
        StartCoroutine(OnPickupVoice());
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

    private IEnumerator OnPickupVoice()
    {
        yield return new WaitForSeconds(delayBeforeVoice);
        StartCoroutine(PlayVoiceLine(pillarVoiceId));
    }
}
