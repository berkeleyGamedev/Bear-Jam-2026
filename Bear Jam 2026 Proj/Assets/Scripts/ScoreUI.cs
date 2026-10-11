using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public Image comboMeter;
    public TextMeshProUGUI comboText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        comboMeter.fillAmount = ExpDecay(comboMeter.fillAmount, ScoreTracker.instance.GetComboTimerRatio(), 24, Time.deltaTime);
        comboText.text = ScoreTracker.instance.GetCurrentCombo() + "x";
    }

    float ExpDecay(float a, float b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }
}
