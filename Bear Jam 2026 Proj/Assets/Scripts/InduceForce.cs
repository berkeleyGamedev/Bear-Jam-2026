using UnityEngine;

public class InduceForce : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 dir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            dir = dir.normalized;
            rb.AddForce(dir * force, ForceMode2D.Impulse);
        }

        rb.linearVelocity += (Vector2)transform.right * force;
    }
}
