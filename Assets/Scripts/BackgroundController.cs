using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Material _defoultSkybox;

    [SerializeField] private List<Material> _skyboxes;

    public static Action<List<Material>> onAssetsLoaded;

    private void Awake()
    {
        if (FindObjectsOfType<BackgroundController>().Length > 1)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SceneManager.sceneLoaded += randomSkybox;
        onAssetsLoaded += setSkyboxes;

        DontDestroyOnLoad(gameObject);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= randomSkybox;
        onAssetsLoaded -= setSkyboxes;
    }

    private void setSkyboxes(List<Material> skyboxes)
    {
        _skyboxes = skyboxes;
    }

    private void randomSkybox(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("Try Load");
        if (_skyboxes != null)
        {
            int index = UnityEngine.Random.Range(0, _skyboxes.Count);
            RenderSettings.skybox = _skyboxes[index];
        }
    }
}
