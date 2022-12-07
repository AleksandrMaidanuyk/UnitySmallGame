using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCreator : MonoBehaviour
{
    private List<Texture2D> _textures2D;
    private List<int> _defoultSizesTexture = new List<int> { 32, 64, 128, 256 };
    private void Awake()
    {
        if (_textures2D == null)
        {
            _textures2D = new List<Texture2D>();
            FillListTextures();
        }
    }

    private void Start()
    {
        LevelManager.getInstance().onNextLevelLoad = refreshTextures;
    }
    public List<Texture2D> GetTextures2D()
    {
        return _textures2D;
    }

    private void FillListTextures()
    {
        if (GameSetting.getInstance().getSizesTextures() != null && GameSetting.getInstance().getSizesTextures().Count != 0)
        {
            List<int> sizesTexture = GameSetting.getInstance().getSizesTextures();

            StartCoroutine(generateTextures(sizesTexture));
        }
        else
        {
            StartCoroutine(generateTextures(_defoultSizesTexture));
        }
    }
    public void refreshTextures()
    {
        StartCoroutine(refreshFillTextures());
    }

    private IEnumerator refreshFillTextures()
    {
        for (int i = 0; i < _textures2D.Count; i++)
        {
            setColorTexture(_textures2D[i], getRandomColor());
        }
        yield return null;
    }
    private Texture2D generateTexture(int size, Color color)
    {
        Texture2D texture2D = new Texture2D(size, size);

        setColorTexture(texture2D, color);

        return texture2D;
    }
    private IEnumerator generateTextures(List<int> sizes)
    {
        for (int i = 0; i < sizes.Count; i++)
        {
            _textures2D.Add(generateTexture(sizes[i], getRandomColor()));
        }
        yield return null;
    }

    private Color getRandomColor()
    {
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        return color;
    }

    private void setColorTexture(Texture2D texture2D, Color color)
    {
        for (int y = 0; y < texture2D.height; y++)
        {
            for (int x = 0; x < texture2D.width; x++)
            {
                texture2D.SetPixel(x, y, color);
            }
        }
        texture2D.Apply();
    }


}
