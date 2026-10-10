using System.Collections;
using UnityEngine;

public class Lvl1Behavior : PinataBehavior
{
    public void Start()
    {
        StartCoroutine(Behaviors());
    }
    public override void FixedUpdate()
    {
        //behaviorVelocity = rb.linearVelocity;
        base.FixedUpdate();
    }
    public override IEnumerator Behaviors()
    {
        //int behavior = Random.Range(0, 3);
        int behavior = 2;

        switch (behavior)
        {
            case 0:
                while (true)
                {
                    behaviorVelocity = Vector2.right;
                    yield return new WaitForFixedUpdate();
                }
            case 1:
                while (true)
                {
                    behaviorVelocity = Vector2.right * -1;
                    yield return new WaitForFixedUpdate();
                }
            case 2:
                while (true)
                {
                    behaviorVelocity = -rb.linearVelocity * Vector2.up;
                    behaviorVelocity += Vector2.up * 6 * rb.gravityScale;
                    yield return new WaitForSeconds(2);
                    yield return new WaitForFixedUpdate();
                }
        }
    }
}
