using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public bool inversed;

    private void OnEnable()
    {
        GameManager.onGameStart += DisableInversed;
        GameManager.onGameLose += DisableInversed;
    }

    private void OnDisable()
    {
        GameManager.onGameLose -= DisableInversed;
        GameManager.onGameStart -= DisableInversed;
    }

    void DisableInversed()
    {
        inversed = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = inversed ? mousePos * -1f: mousePos;
    }

    
}
