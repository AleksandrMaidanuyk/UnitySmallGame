using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsBundleLoader : MonoBehaviour
{
    [SerializeField] private List<string> _namesSkyboxes;

    private List<Material> _materials = new List<Material>();
    private string _bundleURL;
    private int _version = 3;
    private void Awake()
    {
        if (FindObjectsOfType<AssetsBundleLoader>().Length > 1)
        {
            Destroy(gameObject);
        }

#if UNITY_STANDALONE
        _bundleURL = "https://www.dropbox.com/s/smlgorc4ocysl0p/skyboxasset?dl=1";
#endif

#if UNITY_IOS
     _bundleURL = "https://www.dropbox.com/s/j2rw34ak7lgnlni/IOS?dl=1";
#endif

#if UNITY_ANDROID
    _bundleURL = "https://www.dropbox.com/s/0tsy3tshl2swmhs/Android?dl=1";
#endif
    }
    private void Start()
    {
        DontDestroyOnLoad(this);

        StartCoroutine(loadAssetBundle());
    }

    private
    IEnumerator loadAssetBundle()
    {
        while (!Caching.ready)
        {
            yield return null;
        }

        WWW www = WWW.LoadFromCacheOrDownload(_bundleURL, _version);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            yield break;
        }

        AssetBundle assetBundle = www.assetBundle;

        foreach (string name in _namesSkyboxes)
        {
            var materialReques = assetBundle.LoadAssetAsync(name, typeof(Material));
            yield return materialReques;

            Material material = materialReques.asset as Material;
            yield return material;

            Debug.Log("Download!");
            _materials.Add(material);
        }
        BackgroundController.onAssetsLoaded.Invoke(_materials);
    }
}
