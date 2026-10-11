using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class Lvl1Behavior : PinataBehavior
{
    public override void FixedUpdate()
    {
        //behaviorVelocity = rb.linearVelocity;
        base.FixedUpdate();
    }

    public override void OnHit()
    {

    }

    public override IEnumerator Behaviors()
    {
        //int behavior = Random.Range(0, 3);
        int behavior = 0;

        while (true)
        {
            yield return new WaitForFixedUpdate();
            switch (behavior)
            {
                case 0:
                    //behaviorVelocity = Vector2.up * 30;
                    //behaviorVelocity += (Vector2.right * Random.Range(-2, 2)).normalized;
                    yield return new WaitForSeconds(3f);
                    rb.linearVelocity = (Vector2)transform.up * 17;
                    yield return new WaitForFixedUpdate();
                    rb.linearVelocity = (Vector2)transform.up * 17;
                    break;
                default:
                    break;
            }
        }

        switch (behavior)
        {
            //case 0:
            //    break;
            //case 1:
            //    while (true)
            //    {
            //        yield return new WaitForSeconds(3f);
            //        behaviorVelocity += (Vector2)transform.right * -3f;
            //        yield return new WaitForSeconds(3f);
            //        behaviorVelocity += (Vector2)transform.right * 3f;
            //    }
            //case 2:
            //    while (true)
            //    {
            //        //rb.angularVelocity = 0;
            //        //behaviorVelocity = -rb.linearVelocity * Vector2.up;
            //        behaviorVelocity = Vector2.up * 20;
            //        behaviorVelocity += Vector2.right * Random.Range(-3, 3);
            //        yield return new WaitForSeconds(3);
            //    }
        }
    }
}
