using UnityEngine;

public class GunFollow : MonoBehaviour
{
    private Vector2 mousePos;
    public Vector2 offset;
    public AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UpdatePos();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void UpdatePos()
    {
        Vector2 finalPos = new Vector2(mousePos.x, 0.3f * Mathf.Cos(1.4f * mousePos.x));
        transform.position = finalPos + offset;
    }

    void Shoot()
    {
        source.Play();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit)
        {
            if(hit.transform.TryGetComponent<PinataHit>(out PinataHit pinata))
            {
                pinata.Takehit(mousePos);
            }
        }
    }
}
