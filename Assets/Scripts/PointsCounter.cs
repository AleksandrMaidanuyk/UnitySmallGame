using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PointsCounter : MonoBehaviour
{
    private int points = 0;

    public static Action<int> onPointAdd;
    public static Action<int> onPointSave;

    private void OnEnable()
    {
        onPointAdd += addPoints;
    }

    private void OnDisable()
    {
        onPointAdd -= addPoints;
    }

    private void addPoints(int value)
    {
        points += value;
        onPointSave.Invoke(points);
    }


}
