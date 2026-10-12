using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public int currentScore;
    public int totalCandy;
    [SerializeField] int currentCombo;
    [SerializeField] int maxCombo;
    [SerializeField] float comboTimer;
    [SerializeField] float currentComboTimer;

    public static ScoreTracker instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameManager.onGameStart += ResetScore;
        GameManager.onGameLose += ConvertScoreToCandy;

    }

    private void OnDisable()
    {
        GameManager.onGameStart -= ResetScore;
        GameManager.onGameLose -= ConvertScoreToCandy;
    }

    void ResetScore()
    {
        currentScore = 0;
        currentComboTimer = 0;
        currentCombo = 0;
        maxCombo = GameManager.instance.HasExtraCombo ? 15 : 7;
    }

    void ConvertScoreToCandy()
    {
        totalCandy += currentScore;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentComboTimer = 0;
        totalCandy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCombo > 0)
        {
            currentComboTimer -= Time.deltaTime;

            if(currentComboTimer <= 0)
            {
                currentCombo = 0;
                currentComboTimer = 0;
            }
        }
    }

    public void IncreaseCombo()
    {
        currentCombo++;
        currentComboTimer = comboTimer;
        currentCombo = Mathf.Clamp(currentCombo, 0, maxCombo);
    }

    public void IncreaseCombo(int combo)
    {
        currentCombo += combo;
        currentComboTimer = comboTimer;
        currentCombo = Mathf.Clamp(currentCombo, 0, maxCombo);
    }

    public void IncreaseScore(int score)
    {
        currentCombo++;
        currentComboTimer = comboTimer;
        currentCombo = Mathf.Clamp(currentCombo, 0, maxCombo);
        currentScore += score * currentCombo;
    }

    public float GetComboTimerRatio()
    {
        return currentComboTimer/comboTimer;
    }

    public int GetCurrentCombo()
    {
        return currentCombo;
    }
}
