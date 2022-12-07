using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameTimer : MonoBehaviour
{
    private int _seconds = 0;
    private int _minutes = 0;

    private float _timeOnLevel = 0;

    private bool _playTimer = true;

    public static Action<float> onTimerUpdate;
    private void Start()
    {
        StartCoroutine(startTimer());
        LevelManager.getInstance().onLevelFinished += stopTimer;
    }

    private void OnDisable()
    {
        LevelManager.getInstance().onLevelFinished -= stopTimer; 
    }

    public float getTimeOnLevel()
    {
        return _timeOnLevel;
    }

    private void stopTimer()
    {
        _playTimer = false;
    }
    IEnumerator startTimer()
    {
        while (_playTimer)
        {
            yield return new WaitForSeconds(1f);
            _seconds++;

            if (_seconds >= 60)
            {
                _minutes++;
                _seconds = 0;
                _timeOnLevel = _minutes;
            }
            _timeOnLevel += 0.01f;
            onTimerUpdate.Invoke(_timeOnLevel);
        }
    }
}
