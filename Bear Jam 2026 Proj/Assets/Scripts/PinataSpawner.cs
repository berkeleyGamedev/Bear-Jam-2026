using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataSpawner : MonoBehaviour
{
    [SerializeField] private int pinataNumber;
    public List<PinataLevel> levels;
    private PinataLevel currentLevel;
    private int currentLevelIndex;
    private int currentLevelRoundsLeft;
    public float spawnDelay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnEnable()
    {
        GameManager.onGameStart += InitializeLevels;
    }

    private void OnDisable()
    {
        GameManager.onGameStart -= InitializeLevels;
    }

    void InitializeLevels()
    {
        pinataNumber = 0;
        currentLevelIndex = 0;
        currentLevel = levels[0];
        currentLevelRoundsLeft = currentLevel.numberOfRounds;
        SpawnNewPinata();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNewPinata()
    {
        StartCoroutine(SpawnPinata());
    }

    IEnumerator SpawnPinata()
    {
        yield return new WaitForSeconds(spawnDelay);
        if (currentLevelRoundsLeft <= 0)
        {
            Debug.Log("Out of Pinata Rounds!");
            if (currentLevelIndex < levels.Count - 1)
            {
                currentLevelIndex++;
            }
            currentLevel = levels[currentLevelIndex];
            currentLevelRoundsLeft = currentLevel.numberOfRounds;
        }
        currentLevelRoundsLeft--;
        pinataNumber++;
        int index = Random.Range(0, currentLevel.prefabPool.Count);
        GameObject selectedPrefab = currentLevel.prefabPool[index];
        Instantiate(selectedPrefab, new Vector2(0, 4.5f), Quaternion.Euler(0, 0, Random.Range(-50, 50)));
    }
}
