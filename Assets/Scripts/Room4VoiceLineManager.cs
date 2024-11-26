using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room4VoiceLineManager : MonoBehaviour
{
    [SerializeField] private int firstPuzzleSolvedVoiceId;
    [SerializeField] private int secondPuzzleSolvedVoiceId;
    [SerializeField] private int thirdPuzzleSolvedVoiceId;
    [SerializeField] private GameObject firstPuzzleCompletedObject;
    [SerializeField] private GameObject puzzelsCompletedObject;
    [SerializeField] private AchievementObserver achievementObserver;
    
    private VoiceOver_Manager _voiceOverManager;
    private int _puzzlesSolved = 0;
    
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

    public void PuzzleSolved()
    {
        _puzzlesSolved++;

        if (_puzzlesSolved == 1)
        {
            achievementObserver.EarnAchievement(AchievementId.Room4Puzzle1);
        }

        if (_puzzlesSolved == 2)
        {
            StartCoroutine(PlayVoiceLine(secondPuzzleSolvedVoiceId));
            achievementObserver.EarnAchievement(AchievementId.Room4Puzzle2);
        }

        if (_puzzlesSolved == 3)
        {
            StartCoroutine(PlayVoiceLine(thirdPuzzleSolvedVoiceId));
        }

        if (_puzzlesSolved == 4)
        {
            puzzelsCompletedObject.SetActive(true);
        }
    }

    public bool OnRiddleOneFinishVoice()
    {
        _puzzlesSolved++;
        StartCoroutine(PlayVoiceLine(firstPuzzleSolvedVoiceId));
        return true;
    }

    public bool CheckIfFirstPuzzleCompleted()
    {
        return firstPuzzleCompletedObject.activeInHierarchy;
    }
}