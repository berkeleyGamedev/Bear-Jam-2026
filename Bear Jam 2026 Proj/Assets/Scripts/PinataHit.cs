using UnityEngine;

public class PinataHit : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;

    public float scalePerHit;
    public float scaleMax;
    private float currentScale;

    [Header("KnockBack")]
    public float knockbackMultStart;
    public float knockbackMultIncrease;
    public float knockbackMultMax;


    private void Awake()
    {      
        rb = GetComponent<Rigidbody2D>();
        currentScale = 1;
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
        //rb.AddForce(-1 * hitVector.normalized * force, ForceMode2D.Impulse);
        rb.linearVelocity = -1 * hitVector.normalized * (force * knockbackMultStart);
        currentScale += scalePerHit;
        currentScale = Mathf.Clamp(currentScale, 0, scaleMax);

        knockbackMultStart += knockbackMultIncrease;
        knockbackMultStart = Mathf.Clamp(knockbackMultStart, 0, knockbackMultMax);
    }

    Vector3 ExpDecay(Vector3 a, Vector3 b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }
}
