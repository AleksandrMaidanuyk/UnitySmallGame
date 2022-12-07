using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text _timerText;

    [SerializeField] private Text _points;

    [SerializeField] private Text _levelIndex;

    [Header("Panels")]
    [SerializeField] private GameObject _inGamePanel;
    [SerializeField] private GameObject _FinishPanel;

    private void Start()
    {
        showLevelIndex();
    }

    private void OnEnable()
    {
        PointsCounter.onPointSave += updatePoints;
        GameTimer.onTimerUpdate += updateTimer; 
        LevelManager.getInstance().onLevelFinished += showPanelNextLevel;
    }
    private void OnDisable()
    {
        PointsCounter.onPointSave -= updatePoints;
        GameTimer.onTimerUpdate -= updateTimer;
        LevelManager.getInstance().onLevelFinished -= showPanelNextLevel;
    }
    private void updatePoints(int value)
    {
        _points.text = value.ToString();
    }

    private void showLevelIndex()
    {
        _levelIndex.text = LevelManager.getInstance().getLevelIndex().ToString();
    }

    private void updateTimer(float value)
    {
        _timerText.text = Math.Round(value, 2).ToString();
    }

    private void showPanelNextLevel()
    {
        _FinishPanel.SetActive(true);
    }
}
