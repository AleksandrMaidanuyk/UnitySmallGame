using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private BoxCollider _objCollider;

    [SerializeField] private MeshRenderer _MeshMaterial;

    [SerializeField] private int _multiplierPrice = 10;

    private float _speedMove = 1f;

    private float _multiplierScale = 1f;

    private float _deathHeight;

    private void Start() 
    {
        _deathHeight = -transform.position.y;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * getSpeed() * Time.deltaTime);
        if(transform.position.y < _deathHeight)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        PointsCounter.onPointAdd.Invoke(getPriceCharacter());
        Destroy(gameObject);
    }

    public void MultiplyScaleCharacter()
    {
        gameObject.transform.localScale *= _multiplierScale;
    }

    public void setMultiplier(float multiplier)
    {
        _multiplierScale = multiplier;
    }

    public void setDeathHeight(float height)
    {
        _deathHeight = height;
    }

    public float getMultiplier()
    {
        return _multiplierScale;
    }

    public void setTextureMaterial(Texture2D texture)
    {
        _MeshMaterial.material.mainTexture = texture;
    }

    public float getSizeX()
    {
        return gameObject.transform.localScale.x / _objCollider.size.x;
    }

    public float getSizeY()
    {
        return gameObject.transform.localScale.x / _objCollider.size.y;
    }

    private float getSpeed()
    {
        int levelIndex = LevelManager.getInstance().getLevelIndex();
        float levelMultiplier = ((float)levelIndex / 2);

        float speed = (_speedMove / _multiplierScale) * levelMultiplier;
        return speed;
    }

    private int getPriceCharacter()
    {
        float price = (1 / _multiplierScale) * _multiplierPrice;
        return  (int)price;
    }
}
