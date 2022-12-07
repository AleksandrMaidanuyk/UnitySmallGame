using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Character _character;

    [Space]

    [SerializeField] private TextureCreator _textureCreator;

    private bool isGamePlaying = true;
    
    private void Start()
    {
        StartCoroutine(spawnItems());
    }

    private void OnEnable()
    {
        LevelManager.getInstance().onLevelFinished += stopSpawn;
    }

    private void OnDisable()
    {
        LevelManager.getInstance().onLevelFinished -= stopSpawn;
    }

    private void setMultiplierScaleCharacter(Character character)
    {
        float minMultiplier = GameSetting.getInstance().getMinMultiplierScale();
        float maxMultiplier = GameSetting.getInstance().getMaxMultiplierScale();

        float multiplier = Random.Range(minMultiplier, maxMultiplier);
        character.setMultiplier(multiplier);
    }

    private void setTextureCharacter(Character character)
    {
        float multiplierRange = GameSetting.getInstance().getMaxMultiplierScale() - GameSetting.getInstance().getMinMultiplierScale();

        float stepRange = multiplierRange / _textureCreator.GetTextures2D().Count;

        float characterMultiplier = character.getMultiplier();

        int targetSize = (int)(characterMultiplier / stepRange);
        if (targetSize == _textureCreator.GetTextures2D().Count)
        {
            targetSize--;
        }

        character.setTextureMaterial(_textureCreator.GetTextures2D()[targetSize]);
    }

    private Vector3 findRandomPosition(Character character)
    {
        Vector3 maxPos = new Vector3(_mainCamera.pixelWidth, _mainCamera.pixelHeight, GameSetting.getInstance().getDistanceSpawn());
        Vector3 minPos = new Vector3(0, _mainCamera.pixelHeight, GameSetting.getInstance().getDistanceSpawn());

        maxPos = _mainCamera.ScreenToWorldPoint(maxPos);
        minPos = _mainCamera.ScreenToWorldPoint(minPos);

        float newPosX = Random.Range(minPos.x + character.getSizeX(), maxPos.x - character.getSizeX());
        float newPosY = maxPos.y + character.getSizeY();

        Vector3 newPoint = new Vector3(newPosX, newPosY, maxPos.z); // or minPos.z;

        return newPoint;
    }

    private void setDeathLineForCharacter(Character character)
    {
        Vector3 minPos = new Vector3(0, 0, GameSetting.getInstance().getDistanceSpawn());
        character.setDeathHeight(minPos.y);
    }

    private float calculateRandomDelay(float maxDelay)
    {
        int levelIndex = LevelManager.getInstance().getLevelIndex();
        return Random.Range(0, maxDelay / levelIndex);
    }

    private void stopSpawn()
    {
        isGamePlaying = false;
    }
    private IEnumerator spawnItems()
    {
        float maxDelay = GameSetting.getInstance().getMaxDeleySpawn();

        while (isGamePlaying)
        {
            yield return new WaitForSeconds(calculateRandomDelay(maxDelay));

            GameObject newItem = Instantiate<GameObject>(_character.gameObject, _character.gameObject.transform.position, _character.transform.rotation, gameObject.transform);
            Character newCharacter = newItem.GetComponent<Character>();

            setMultiplierScaleCharacter(newCharacter);

            newCharacter.MultiplyScaleCharacter();

            setTextureCharacter(newCharacter);

            newItem.transform.position = findRandomPosition(newCharacter);
            
        }
    }

}
