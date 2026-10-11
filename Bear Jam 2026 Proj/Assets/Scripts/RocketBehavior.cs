using System.Collections;
using UnityEngine;

public class RocketBehavior : PinataBehavior
{
    public float rocketVelocity;

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
                    yield return new WaitForFixedUpdate();
                }
                break;
            }
        }
    }
}
