using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private AP_Lamp[] lampsToSwitch;
    [SerializeField] private GameObject switchGameObject;
    [SerializeField] private float switchOnXRotation = -58f;
    [SerializeField] private float switchOffXRotation = -10f;
    [SerializeField] private bool pointless = false;
    [SerializeField] private AudioClip switchSound;
    [SerializeField] private LightSwitch otherLightSwitch;
    [SerializeField] private AchievementObserver achievementObserver;
    [SerializeField] private AchievementId achievementToUnlock = AchievementId.None;
    [SerializeField] private int lightsUpVoiceId;
    [SerializeField] private int preLightsVoiceId;

    private bool _isLightsOn = false;
    private AudioSource _audio;
    private VoiceOver_Manager _voiceOverManager;
    private bool _preLightsVoiceLinePlayed;
    private bool _afterLightsVoiceLinePlayed;
    private Task _voiceLineTask;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _voiceOverManager = GameObject.Find ("VoiceOver_Manager").GetComponent<VoiceOver_Manager>();
    }

    private void RotateSwitch()
    {
        Vector3 switchEulerAngles = transform.eulerAngles;
        switchGameObject.transform.eulerAngles = 
            new Vector3(_isLightsOn ? switchOffXRotation : switchOnXRotation, switchEulerAngles.y, switchEulerAngles.z);
        
        _audio.PlayOneShot(switchSound);
    }

    public bool SwitchLights()
    {
        if (!_preLightsVoiceLinePlayed && !otherLightSwitch.WasVoiceLinePlayed())
        {
            
            StartCoroutine(PlayVoiceLine(preLightsVoiceId));
            _preLightsVoiceLinePlayed = true;
        }
        if (!pointless && !_afterLightsVoiceLinePlayed)
        {
            StartCoroutine(PlayVoiceLine(lightsUpVoiceId));
            _afterLightsVoiceLinePlayed = true;
        }
        RotateSwitch();
        achievementObserver.EarnAchievement(achievementToUnlock);
        _isLightsOn = !_isLightsOn;
        if(pointless) return true;
        foreach (AP_Lamp apLamp in lampsToSwitch)
        {
            apLamp.B_AP_SwitchALight();
        }
        return true;
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

    private bool WasVoiceLinePlayed()
    {
        return _preLightsVoiceLinePlayed;
    }
}
