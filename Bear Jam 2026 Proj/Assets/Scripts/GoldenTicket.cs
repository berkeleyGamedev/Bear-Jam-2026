using UnityEngine;

public class GoldenTicket : MonoBehaviour
{
    public int scoreValue;
    public float upwardVelocity;
    public float xVelocityRange;
    public float angularVelocityRange;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 launchVelocity = Vector2.up * upwardVelocity;
        launchVelocity += Vector2.right * Random.Range(-xVelocityRange, xVelocityRange);
        rb.linearVelocity = launchVelocity;
        rb.angularVelocity = Random.Range(-angularVelocityRange, angularVelocityRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.y < -20)
        {
            Destroy(gameObject);
        }
    }

    public void TakeHit()
    {
        ScoreTracker.instance.IncreaseScore(scoreValue);
        Destroy(gameObject);
    }
}
