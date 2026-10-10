using UnityEngine;

public class PinataHit : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;

    public PinataStats stats;
    private float currentScale;
    private float currentKBMult;


    private void Awake()
    {      
        rb = GetComponent<Rigidbody2D>();
        currentScale = stats.startScale;
        currentKBMult = stats.startKBMult;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetScale = Vector3.one * currentScale;

        transform.localScale = ExpDecay(transform.localScale, targetScale, 16, Time.deltaTime);
    }

    public void Takehit(Vector2 mousePos)
    {
        Vector2 hitVector = mousePos - rb.position;
        hitVector = hitVector.normalized;
        float theta = Random.Range(-20, 20);
        Vector2 rotatedVector = new Vector2(hitVector.x * Mathf.Cos(theta) - hitVector.y * Mathf.Sin(theta),
            hitVector.x * Mathf.Sin(theta) + hitVector.y * Mathf.Cos(theta));
        //rb.AddForce(-1 * hitVector.normalized * force, ForceMode2D.Impulse);
        rb.linearVelocity = -1 * rotatedVector.normalized * (force * currentKBMult);
        currentScale += stats.scaleIncPerHit;
        currentScale = Mathf.Clamp(currentScale, 0, stats.scaleMax);

        currentKBMult += stats.KBMultIncPerHit;
        currentKBMult = Mathf.Clamp(currentKBMult, 0, stats.KBMax);
    }

    Vector3 ExpDecay(Vector3 a, Vector3 b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }
}
