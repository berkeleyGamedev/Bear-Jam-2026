using System.Collections;
using UnityEngine;

public class SpawnAnim : MonoBehaviour
{
    public float startScale;
    public float endScale;
    public float duration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(Expand());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Expand()
    {
        float t = 0;
        transform.localScale *= startScale;
        Vector3 endTransform = Vector3.one * endScale;
        while((endTransform-transform.localScale).magnitude > 0.001f)
        {
            transform.localScale = ExpDecay(transform.localScale, endTransform, 16, Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endTransform;
    }

    public void DeathAnim()
    {
        StartCoroutine(Shrink());
    }

    IEnumerator Shrink()
    {
        float t = 0;
        Vector3 endTransform = Vector3.one * 0.01f;
        while ((endTransform - transform.localScale).magnitude > 0.001f)
        {
            transform.localScale = ExpDecay(transform.localScale, endTransform, 16, Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endTransform;
        Destroy(gameObject);
    }

    Vector3 ExpDecay(Vector3 a, Vector3 b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }
}
