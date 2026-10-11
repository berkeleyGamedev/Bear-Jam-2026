using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void OnGameStart();
    public static event OnGameStart onGameStart;

    public delegate void OnGameLose();
    public static event OnGameLose onGameLose;

    private bool isGameActive;

    private void Awake()
    {
        if(instance== null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public bool GetIsGameActive()
    {
        return isGameActive;
    }

    public void StartGame()
    {
        isGameActive = true;
        onGameStart.Invoke();
    }

    public void LoseGame()
    {
        isGameActive = false;
        StartCoroutine(LoseDelay());
    }

    IEnumerator LoseDelay()
    {
        yield return new WaitForSeconds(1);
        onGameLose.Invoke();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartGame();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
