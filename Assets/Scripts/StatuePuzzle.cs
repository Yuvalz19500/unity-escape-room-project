using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StatuePuzzle : MonoBehaviour
{
    [SerializeField] private VisualEffect dragonFlame;
    [SerializeField] private int timeBetweenClues = 30;
    [SerializeField] private AchievementObserver achievementObserver;
    public int firstTriggerVoiceId;
    public int diesToFireVoiceId;
    public int clueOneVoiceId;
    public int clueTwoVoiceId;
    public int clueThreeVoiceId;

    private AudioSource _audio;
    private VoiceOver_Manager _voiceOverManager;
    private bool _puzzleComplete = false;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _voiceOverManager = GameObject.Find ("VoiceOver_Manager").GetComponent<VoiceOver_Manager>();
    }

    private void Start()
    {
        dragonFlame.Stop();
    }

    public void ToggleFire(bool toggle)
    {
        if (toggle)
        {
            dragonFlame.Play();
            if (_audio.isPlaying) return;
            _audio.Play(0);
        }
        else
        {
            dragonFlame.Stop();
            if (!_audio.isPlaying) return;
            _audio.Stop();
        }
    }
    
    public IEnumerator PlayVoiceLine(int id)
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

    public IEnumerator PlayVoiceLinesAfterFirstTrigger()
    {
        yield return PlayVoiceLine(firstTriggerVoiceId);
        if (_puzzleComplete)
        {
            yield break;
        }
        yield return new WaitForSeconds(timeBetweenClues);
        if (_puzzleComplete)
        {
            yield break;
        }
        yield return PlayVoiceLine(clueOneVoiceId);
        if (_puzzleComplete)
        {
            yield break;
        }
        yield return new WaitForSeconds(timeBetweenClues);
        if (_puzzleComplete)
        {
            yield break;
        }
        yield return PlayVoiceLine(clueTwoVoiceId);
        if (_puzzleComplete)
        {
            yield break;
        }
        yield return new WaitForSeconds(timeBetweenClues);
        if (_puzzleComplete)
        {
            yield break;
        }
        yield return PlayVoiceLine(clueThreeVoiceId);
    }

    public void CompletePuzzle()
    {
        this._puzzleComplete = true;
    }

    public void EarnAchievement(AchievementId id)
    {
        achievementObserver.EarnAchievement(id);
    }
}
