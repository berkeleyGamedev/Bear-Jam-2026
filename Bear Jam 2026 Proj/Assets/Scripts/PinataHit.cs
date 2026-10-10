using UnityEngine;

public class PinataHit : MonoBehaviour
{
    private Rigidbody2D rb;
    private PinataBehavior behavior;
    public float force;

    public PinataStats stats;
    private float currentScale;
    private float currentKBMult;
    [SerializeField] private float currentHealth;

    public delegate void OnHit(Vector2 mousePos);
    public static event OnHit onHit;

    private float counter;


    private void Awake()
    {      
        rb = GetComponent<Rigidbody2D>();
        behavior = GetComponent<PinataBehavior>();
        currentScale = stats.startScale;
        currentKBMult = stats.startKBMult;
        currentHealth = stats.health;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Bandaid "spawn animation"
        transform.localScale = 0.1f * Vector3.one;

        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetScale = Vector3.one * currentScale;

        transform.localScale = ExpDecay(transform.localScale, targetScale, 16, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(counter < 2)
        {
            counter += Time.fixedDeltaTime;
        } else
        {
            counter = 0;
            //rb.linearVelocity += (Vector2)transform.up * 30;
        }

    }


    public void Takehit(Vector2 mousePos)
    {
        onHit.Invoke(mousePos);

        Vector2 hitVector = mousePos - rb.position;
        hitVector = hitVector.normalized;
        float theta = Random.Range(-20, 20);
        Vector2 rotatedVector = new Vector2(hitVector.x * Mathf.Cos(theta) - hitVector.y * Mathf.Sin(theta),
            hitVector.x * Mathf.Sin(theta) + hitVector.y * Mathf.Cos(theta));
        //rb.AddForce(-1 * hitVector.normalized * force, ForceMode2D.Impulse);
        //rb.linearVelocity += -1 * rotatedVector.normalized * (force * currentKBMult);
        behavior.GetKnockback(-1 * rotatedVector.normalized * (force * currentKBMult));
        currentScale += stats.scaleIncPerHit;
        currentScale = Mathf.Clamp(currentScale, 0, stats.scaleMax);

        currentKBMult += stats.KBMultIncPerHit;
        currentKBMult = Mathf.Clamp(currentKBMult, 0, stats.KBMax);

        currentHealth -= 1;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    Vector3 ExpDecay(Vector3 a, Vector3 b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }
}
