using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkFovFinder : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private float _maxWidth;
    private float _maxHeight;
    private float _distanceSpawn;

    public float MaxWidth
    {
        get
        {
            return _mainCamera.pixelWidth;
        }
    }

    public float maxHeight
    {
        get
        {
            return _mainCamera.pixelHeight;
        }
    }

}
