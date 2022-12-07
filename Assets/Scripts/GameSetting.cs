using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float _maxDelaySpawn = 3f;

    [Range(10, 20)]
    [SerializeField] private float _distanceSpawn = 10;

    [Header("Character")]

    [Range(1f, 5)]
    [SerializeField] private float _maxMultiplierScale = 2f;

    [Range(0.1f, 1f)]
    [SerializeField] private float _minMultiplierScale = 0.1f;

    [Range(0.1f, 10)]
    [SerializeField] private float _baseSpeedMove = 1f;

    [Header("Texture")]

    [SerializeField] private List<int> _sizesTextures;

    private static GameSetting instance;

    private void Awake()
    {
        instance = this;
    }
    public static GameSetting getInstance()
    {
        if (instance == null)
            instance = new GameSetting();
        return instance;
    }

    public float getMaxDeleySpawn()
    {
        return _maxDelaySpawn;
    }

    public float getDistanceSpawn()
    {
        return _distanceSpawn;
    }

    public float getMaxMultiplierScale()
    {
        return _maxMultiplierScale;
    }

    public float getMinMultiplierScale()
    {
        return _minMultiplierScale;
    }
    public float getBaseSpeedMove()
    {
        return _baseSpeedMove;
    }

    public List<int> getSizesTextures()
    {
        return _sizesTextures;
    }

}
