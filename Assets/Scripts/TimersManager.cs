using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimersManager : MonoBehaviour
{
    public UnityEvent onFailedToOpenDoor;

    [SerializeField] private float phase1Time = 30f;
    [SerializeField] private float phase2Timer = 300f;
    [SerializeField] private GameObject timersPuzzleSolved;
    [SerializeField] private float timeBetweenPhase = 3f;
    [SerializeField] private float timersBlinkRate = 1f;
    [SerializeField] private TimersVoiceManager timersVoiceManager;
    
    private Timer[] _timers;
    private float _timeRemaining = 0;
    private bool _timerIsRunning = false;
    private bool _isTransitioningPhase = false;
    private TimersPhase _currentPhase = TimersPhase.Phase1;

    private void Awake()
    {
        _timers = GetComponentsInChildren<Timer>();
    }
    
    private void Update()
    {
        if (!_timerIsRunning) return;
        if (_timeRemaining >= 0)
        {
            _timeRemaining -= Time.deltaTime;
            if (_currentPhase == TimersPhase.Phase2)
            {
                if (_timeRemaining <= 262)
                {
                    timersVoiceManager.PlayFourMinVoice();
                }
                
                if (_timeRemaining <= 63)
                {
                    timersVoiceManager.PlayOneMinVoice();
                }
                
                if (_timeRemaining <= 30)
                {
                    timersVoiceManager.PlayThirtySecVoice();
                }
            }

            foreach (Timer timer in _timers)
            {
                timer.DisplayTime(_timeRemaining);
            }
        }
        else
        {
            _timeRemaining = 0;
            _timerIsRunning = false;
            if (_currentPhase == TimersPhase.Phase1)
            {
                timersPuzzleSolved.SetActive(true);
                StartCoroutine(TransitionPhase(TimersPhase.Phase2));
            }
            else
            {
                timersVoiceManager.PlayFailedToOpenDoorVoice();
                onFailedToOpenDoor?.Invoke();
                BlinkTimers();
            }
        }
    }

    public bool OnTimersTriggered()
    {
        StartPhase(phase1Time);
        return true;
    }

    private void StartPhase(float phaseStartTime)
    {
        _timeRemaining = phaseStartTime;
        _timerIsRunning = true;
        _isTransitioningPhase = false;
        foreach (Timer timer in _timers)
        {
            timer.DisplayTime(_timeRemaining);
        }
    }

    private void BlinkTimers()
    {
        foreach (Timer timer in _timers)
        {
            timer.StartBlink(timersBlinkRate);
        }
    }
    
    private void StopBlinksTimers()
    {
        foreach (Timer timer in _timers)
        {
            timer.StopBlink();
        }
    }

    private IEnumerator TransitionPhase(TimersPhase nextPhase)
    {
        _isTransitioningPhase = true;
        BlinkTimers();
        yield return new WaitForSeconds(timeBetweenPhase);
        _currentPhase = nextPhase;
        StopBlinksTimers();
        StartPhase(phase2Timer);
    }

    public void StopTimers()
    {
        StartCoroutine(StopTimersAsync());
    }
    
    public IEnumerator StopTimersAsync()
    {
        while (_isTransitioningPhase)
        {
            yield return new WaitForSeconds(0);
        }
        
        _timerIsRunning = false;
        BlinkTimers();
    }
    
}

internal enum TimersPhase
{
    Phase1,
    Phase2
}
