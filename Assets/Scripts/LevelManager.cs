using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    private int currentLevelIndex;
    public Action onNextLevelLoad;
    public Action onLevelFinished;
    private const string LEVEL_INDEX = "LEVEL_INDEX";

    private const int MULTIPLIER_PRICE = 120;

    private const int NUMBER_PAUSE_SCENE = 0;

    private static LevelManager instance;

    private void Awake()
    {
        currentLevelIndex = getLevelIndex();
        instance = this;
    }

    public static LevelManager getInstance()
    {
        if (instance == null)
            instance = new LevelManager();
        return instance;
    }

    private void OnEnable()
    {
        PointsCounter.onPointSave += checkPointsScore;
    }
    private void OnDisable()
    {
        PointsCounter.onPointSave -= checkPointsScore;
    }
    public void reloadlevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    public void loadNextLevel()
    {
        currentLevelIndex++;
        saveLevelIndex();

        onNextLevelLoad.Invoke();
        reloadlevel();
    }

    public void loadBaseGameScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }
    public void loadPauseScene()
    {
        SceneManager.LoadScene(NUMBER_PAUSE_SCENE); 
    }
    public void saveLevelIndex()
    {
        PlayerPrefs.SetInt(LEVEL_INDEX, currentLevelIndex);
    }
    public int getLevelIndex()
    {
        return PlayerPrefs.GetInt(LEVEL_INDEX, 1);
    }

    private int getPriceForLoadNextLevel()
    {
        int currentPrice = MULTIPLIER_PRICE * currentLevelIndex;
        return currentPrice;
    }

    private void checkPointsScore(int points)
    {
        if (points >= getPriceForLoadNextLevel())
        {
            onLevelFinished.Invoke();
        }
    }
}
