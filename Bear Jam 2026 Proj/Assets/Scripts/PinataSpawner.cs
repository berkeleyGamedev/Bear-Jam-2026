using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PinataSpawner : MonoBehaviour
{
    [SerializeField] private int pinataNumber;
    public List<PinataLevel> levels;
    private PinataLevel currentLevel;
    private int currentLevelRoundsLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pinataNumber = 0;
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
        if(currentLevelRoundsLeft <= 0)
        {
            Debug.Log("Out of Pinata Rounds!");
            if (levels.Count > 1)
            {
                levels.RemoveAt(0);
            }
            currentLevel = levels[0];
            currentLevelRoundsLeft = currentLevel.numberOfRounds;
        }
        currentLevelRoundsLeft--;
        pinataNumber++;
        int index = Random.Range(0, currentLevel.prefabPool.Count);
        GameObject selectedPrefab = currentLevel.prefabPool[index];
        Instantiate(selectedPrefab, new Vector2(0, 4.5f), Quaternion.Euler(0, 0, Random.Range(-50, 50)));
    }
}
