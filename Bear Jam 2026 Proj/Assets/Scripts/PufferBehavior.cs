using System.Collections;
using UnityEngine;

public class PufferBehavior : PinataBehavior
{
    private CircleCollider2D circleCol;
    private CapsuleCollider2D capsuleCol;
    protected override void Awake()
    {
        base.Awake();
        circleCol = GetComponent<CircleCollider2D>();
        capsuleCol = GetComponent<CapsuleCollider2D>();
        capsuleCol.enabled = true;
        circleCol.enabled = false;
    }
    public override IEnumerator Behaviors()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            switch (hit.timesHit)
            {
                case 0:
                    behaviorVelocity = Vector2.up * 15;
                    behaviorVelocity += Vector2.right * Random.Range(-2, 2);
                    yield return new WaitForSeconds(0.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public override void OnHit()
    {
        capsuleCol.enabled = false;
        circleCol.enabled = true;
    }

}
