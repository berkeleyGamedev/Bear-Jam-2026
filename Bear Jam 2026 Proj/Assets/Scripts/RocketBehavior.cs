using System.Collections;
using UnityEngine;

public class RocketBehavior : PinataBehavior
{
    public float rocketVelocity;
    public float maxVelocity;
    public GameObject smokePrefab;
    public Transform target;
    private ParticleSystem smoke;

    protected override void Start()
    {
        base.Start();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Debug.Log(rb.linearVelocity.magnitude);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocity);
        if(smoke != null)
        {
            ParticleSystem.ShapeModule shapeModule = smoke.shape;
            shapeModule.position =  target ? target.position : shapeModule.position;
            shapeModule.rotation = new Vector3(-rb.rotation, 90, 0);
        }
    }

    public override void OnHit()
    {
        if (hit.timesHit > 1) return;
        smoke = Instantiate(smokePrefab, Vector2.zero, Quaternion.identity).GetComponent<ParticleSystem>();
    }

    public override void OnDeath()
    {
        if (smoke != null)
        {
            ParticleSystem.EmissionModule emissionModule = smoke.emission;
            emissionModule.enabled = false;
        }
    }

    public override IEnumerator Behaviors()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if(hit.timesHit > 0)
            {
                for (int i = 0; i < 180; i++) 
                {
                    behaviorVelocity = transform.right * -1f * rocketVelocity;

                    //ParticleSystem.ShapeModule shapeModule = smoke.shape;
                    //shapeModule.position = target.position;
                    //shapeModule.rotation = new Vector3(transform.rotation.z - 60, 90, 0);
                    yield return new WaitForFixedUpdate();
                }
                if(smoke != null)
                {
                    ParticleSystem.EmissionModule emissionModule = smoke.emission;
                    emissionModule.enabled  = false;
                }
                break;
            }
        }
    }
}
