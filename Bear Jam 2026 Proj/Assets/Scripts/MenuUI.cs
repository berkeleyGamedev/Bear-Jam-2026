using System.Collections;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject playPanel;
    public GameObject shopPanel;
    public float edgePosition;

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
        Vector2 startPos = Vector2.up * start;
        Vector2 endPos = Vector2.up * end;
        StopAllCoroutines();
        StartCoroutine(PanelAnim(menuPanel, startPos, endPos, true));
    }

    public void ToggleShop(bool shopVisible)
    {
        if (shopVisible)
        {
            StopAllCoroutines();
            shopPanel.SetActive(true);
            StartCoroutine(PanelAnim(playPanel, Vector2.zero, Vector2.right * -edgePosition, false));
            StartCoroutine(PanelAnim(shopPanel, Vector2.right * edgePosition, Vector2.zero, true));
        }
        else
        {
            StopAllCoroutines();
            playPanel.SetActive(true);
            StartCoroutine(PanelAnim(shopPanel, Vector2.zero, Vector2.right * edgePosition, true));
            StartCoroutine(PanelAnim(playPanel, Vector2.right * -edgePosition, Vector2.zero, false));
        }
    }

    IEnumerator PanelAnim(GameObject panelToMove, Vector2 startPos, Vector2 endPos, bool visibilityWhenFinished)
    {
        if (panelToMove.TryGetComponent<RectTransform>(out var rect)) 
        {
            rect.anchoredPosition = startPos;
            while ((endPos-startPos).magnitude > 10f)
            {
                rect.anchoredPosition = ExpDecay(rect.anchoredPosition, endPos, 16, Time.deltaTime);
                yield return null;
            }
            rect.anchoredPosition = endPos;
        }
        panelToMove.SetActive(visibilityWhenFinished);

    }

    float ExpDecay(float a, float b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }

    Vector2 ExpDecay(Vector2 a, Vector2 b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }
}
