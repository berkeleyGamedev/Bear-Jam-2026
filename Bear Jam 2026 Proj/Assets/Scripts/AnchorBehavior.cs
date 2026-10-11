using System.Collections;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class AnchorBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int behavior;

    [Header("Behavior 1")]
    public float duration;
    public Vector2 startPoint;
    public Vector2 endPoint;
    public AnimationCurve lerpCurve;

    private Vector2 startPos;
    private Vector2 endPos;

    [Header("Behavior 2")]
    public Vector2 origin;
    public float xAxis;
    public float yAxis;
    public float rotateSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(LerpThroughPoints());
        startPos = (Vector2)transform.position + startPoint;
        endPos = (Vector2)transform.position + endPoint;


        rb.position = Vector2.Lerp(startPos, endPos, lerpCurve.Evaluate(0.5f / duration));
        behavior = Random.Range(0, 2);

        switch (behavior)
        {
            case 0:
                StartCoroutine(LerpThroughPoints());
                break;
            case 1:
                StartCoroutine(MoveInCircle());
                break;
        }
    }

    IEnumerator LerpThroughPoints()
    {
        float t = 0.5f;
        int signOfDeltaTime = 1;
        while (true)
        {
            if(t <= 0 || t >= duration)
            {
                signOfDeltaTime *= -1;
            }
            Vector2 targetPos = Vector2.Lerp(startPos, endPos, lerpCurve.Evaluate(t/duration));
            //Vector2 rbPos = ExpDecay(rb.position, targetPos, 16, Time.fixedDeltaTime);
            rb.MovePosition(targetPos);
            t += Time.deltaTime * signOfDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator MoveInCircle()
    {
        float theta = Mathf.PI / 2;
        Vector2 targetPos = origin + Vector2.up * yAxis;
        rb.position = targetPos;
        while (true)
        {
            targetPos = new Vector2(xAxis * Mathf.Cos(theta), yAxis * Mathf.Sin(theta)) + origin;
            rb.MovePosition(targetPos);
            theta += Time.fixedDeltaTime * rotateSpeed;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDrawGizmos()
    {
        switch (behavior)
        {


            case 0:
                Vector2 startPosGizmo;
                Vector2 endPosGizmo;
                if (!Application.isPlaying)
                {
                    startPosGizmo = (Vector2)transform.position + startPoint;
                    endPosGizmo = (Vector2)transform.position + endPoint;
                } else
                {
                    startPosGizmo = startPos;
                    endPosGizmo = endPos;
                }
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(startPosGizmo, 0.5f);
                Gizmos.DrawWireSphere(endPosGizmo, 0.5f);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(startPosGizmo, endPosGizmo);
                break;
            case 1:
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(origin, 0.2f);
                Gizmos.color = Color.red;
                //Gizmos.DrawWireSphere(origin, );
                Vector2 targetPoint = origin + Vector2.right * xAxis;
                for(int i = 0; i < 100; i++)
                {
                    float theta = 0;
                    theta = i * 2 * Mathf.PI/100;
                    Vector2 newtargetPoint = new Vector2(xAxis * Mathf.Cos(theta), yAxis * Mathf.Sin(theta)) + origin;
                    Gizmos.DrawLine(targetPoint, newtargetPoint);
                    targetPoint = newtargetPoint;
                }
                Gizmos.color = Color.greenYellow;
                Gizmos.DrawWireSphere(origin + yAxis * Vector2.up, 0.1f);
                break;
    }
    }

    Vector2 ExpDecay(Vector2 a, Vector2 b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }

}
