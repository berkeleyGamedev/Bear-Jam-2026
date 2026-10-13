using System.Collections;
using UnityEngine;

public class RocketBehavior : PinataBehavior
{
    public float rocketVelocity;
    public GameObject smokePrefab;
    public Transform target;
    private ParticleSystem smoke;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnHit()
    {
        
    }

    public override IEnumerator Behaviors()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if(hit.timesHit > 0)
            {
                for (int i = 0; i < 300; i++) 
                {
                    behaviorVelocity = transform.right * -1f * rocketVelocity;
                    if(smoke == null)
                    {
                        smoke = Instantiate(smokePrefab, Vector2.zero, Quaternion.identity).GetComponent<ParticleSystem>();
                    }
                    ParticleSystem.ShapeModule shapeModule = smoke.shape;
                    shapeModule.position = target.position;
                    shapeModule.rotation = new Vector3(transform.rotation.z - 60, 90, 0);
                    yield return new WaitForFixedUpdate();
                }
                //Destroy(smoke.gameObject);
                break;
            }
        }
    }
}
