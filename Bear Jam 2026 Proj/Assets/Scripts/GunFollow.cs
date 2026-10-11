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

    [Header("Plushie Soul")]
    public int maxWhiffs;
    private int numberOfWhiffs;

    [Header("Golden Ticket")]
    public float GoldenTicketSpawnChance;

    [Header("Prefabs")]
    public GameObject goldenTicketPrefab;
    public GameObject whiffPrefab;
    public GameObject hitParticles;
    public GameObject plushieSoulPrefab;

    private void OnEnable()
    {
        GameManager.onGameStart += ResetWhiffs;
    }

    private void OnDisable()
    {
        GameManager.onGameStart -= ResetWhiffs;
    }

    void ResetWhiffs()
    {
        numberOfWhiffs = 0;
        maxWhiffs = GameManager.instance.HasPlushieSoul ? 2 : 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //maxWhiffs = 1;
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
            if (hit.transform.TryGetComponent<GoldenTicket>(out GoldenTicket goldenTicket))
            {
                goldenTicket.TakeHit();
            }

            if (hit.transform.TryGetComponent<PinataHit>(out PinataHit pinata))
            {
                Instantiate(hitParticles, mousePos, Quaternion.identity);
                pinata.Takehit(mousePos);
                SpawnGoldenTicket(mousePos);
            }

        } else
        {
            Debug.Log("Whiffed!");
            numberOfWhiffs++;
            if(numberOfWhiffs >= maxWhiffs)
            {
                Instantiate(whiffPrefab, mousePos, Quaternion.identity);
                GameManager.instance.LoseGame();
            }
            else
            {
                Instantiate(plushieSoulPrefab, mousePos, Quaternion.identity);
            }

        }

        StopAllCoroutines();
        StartCoroutine(ShootAnim());
    }

    void SpawnGoldenTicket(Vector2 mousePos)
    {
        if (!GameManager.instance.HasGoldenTicket) return;
        float chance = Random.Range(0f, 1f);
        if(chance < GoldenTicketSpawnChance)
        {
            // We add the Vector3 here with -1 z so the raycast can always hit the golden ticket first and not a pinata behind it
            Instantiate(goldenTicketPrefab, new Vector3(mousePos.x, mousePos.y, -1f), Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
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
