using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class GunFollow : MonoBehaviour
{
    private Vector2 mousePos;
    public Vector2 offset;
    public Transform crosshairTransform;
    public AudioSource source;

    public AnimationCurve shootCurve;
    public float shootDuration;
    public float rotateAmount;

    public GameObject whiffPrefab;
    public GameObject hitParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = crosshairTransform.position;
        UpdatePos();

        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.instance.GetIsGameActive()) return;
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
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(mousePos));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit)
        {
            if(hit.transform.TryGetComponent<PinataHit>(out PinataHit pinata))
            {
                Instantiate(hitParticles, mousePos, Quaternion.identity);
                pinata.Takehit(mousePos);
            }
        } else
        {
            Debug.Log("Whiffed!");
            Instantiate(whiffPrefab, mousePos, Quaternion.identity);
            GameManager.instance.LoseGame();
        }

        StopAllCoroutines();
        StartCoroutine(ShootAnim());
    }


    IEnumerator ShootAnim()
    {
        float t = 0;
        transform.rotation = Quaternion.identity;
        while(t < shootDuration)
        {
            Quaternion finalRot = Quaternion.Euler(0, 0, rotateAmount);
            transform.rotation = Quaternion.Lerp(Quaternion.identity, finalRot, shootCurve.Evaluate(t/shootDuration));
            t += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.identity;
    }
}
