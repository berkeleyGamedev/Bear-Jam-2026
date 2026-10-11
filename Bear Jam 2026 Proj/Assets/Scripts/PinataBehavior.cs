using System.Collections;
using UnityEngine;

public abstract class PinataBehavior : MonoBehaviour
{
    protected Vector2 behaviorVelocity;
    protected Vector2 knockbackVelocity;

    protected Rigidbody2D rb;
    protected PinataHit hit;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hit = GetComponent<PinataHit>();
    }

    protected virtual void Start()
    {
        StartCoroutine(Behaviors());
    }

    public virtual void FixedUpdate()
    {
        rb.linearVelocity = rb.linearVelocity + behaviorVelocity + knockbackVelocity;
        knockbackVelocity = Vector2.zero;
        behaviorVelocity = Vector2.zero;
    }

    public void GetKnockback(Vector2 KB)
    {
        knockbackVelocity = KB;
    }
    public abstract IEnumerator Behaviors();

    public abstract void OnHit();
}
