using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimersVoiceManager : MonoBehaviour
{
    [SerializeField] public int fourMinVoiceId;
    [SerializeField] public int oneMinVoiceId;
    [SerializeField] public int thirtySecVoiceId;
    [SerializeField] public int openedDoorVoiceId;
    [SerializeField] public int failedToOpenDoorVoiceId;

    private bool _fourMinVoicePlayed = false;
    private bool _oneMinVoicePlayed = false;
    private bool _thirtySecVoicePlayed = false;
    private bool _openedDoorVoicePlayed = false;
    private bool _failedToOpenDoorPlayed = false;
    
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

    public void PlayFourMinVoice()
    {
        if (_fourMinVoicePlayed) return;
        StartCoroutine(PlayVoiceLine(fourMinVoiceId));
        _fourMinVoicePlayed = true;
    }
    
    public void PlayOneMinVoice()
    {
        if (_oneMinVoicePlayed) return;
        StartCoroutine(PlayVoiceLine(oneMinVoiceId));
        _oneMinVoicePlayed = true;
    }
    
    public void PlayThirtySecVoice()
    {
        if (_thirtySecVoicePlayed) return;
        StartCoroutine(PlayVoiceLine(thirtySecVoiceId));
        _thirtySecVoicePlayed = true;
    }
    
    public void PlayOpenedDoorVoice()
    {
        if (_openedDoorVoicePlayed) return;
        StartCoroutine(PlayVoiceLine(openedDoorVoiceId));
        _openedDoorVoicePlayed = true;
    }
    
    public void PlayFailedToOpenDoorVoice()
    {
        if (_failedToOpenDoorPlayed) return;
        StartCoroutine(PlayVoiceLine(failedToOpenDoorVoiceId));
        _failedToOpenDoorPlayed = true;
        _openedDoorVoicePlayed = true;
    }
}


