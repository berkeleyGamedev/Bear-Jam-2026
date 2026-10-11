using System.Collections;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject menuPanel;

    private void OnEnable()
    {
        GameManager.onGameStart += TogglePanel;
        GameManager.onGameLose += TogglePanel;
    }

    private void OnDisable()
    {
        GameManager.onGameStart -= TogglePanel;
        GameManager.onGameLose -= TogglePanel;
    }

    void TogglePanel()
    {
        //menuPanel.SetActive(!GameManager.instance.GetIsGameActive());
        float start = (GameManager.instance.GetIsGameActive()) ? 0 : -1000;
        float end = (GameManager.instance.GetIsGameActive()) ? -1000 : 0;
        StopAllCoroutines();
        StartCoroutine(PanelAnim(start, end));
    }

    IEnumerator PanelAnim(float start, float end)
    {
        if (menuPanel.TryGetComponent<RectTransform>(out var rect)) 
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, start);
            while (Mathf.Abs(rect.anchoredPosition.y- end) > 0.1f)
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, ExpDecay(rect.anchoredPosition.y, end, 16, Time.deltaTime));
                yield return null;
            }
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, end);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float ExpDecay(float a, float b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }
}
