using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AlienBehavior : PinataBehavior
{
    [SerializeField] Transform anchorTransform;
    [SerializeField] Vector2 behaviorVector;
    [SerializeField] float theta;
    [SerializeField] float changeRotateTime;
    [SerializeField] float behaviorScaler;
    [SerializeField] float decay;
    [SerializeField] float switchRadius;
    [SerializeField] float anchorOffset;
    [SerializeField] float clampRadius;

    private bool canSwitchDirection;
    private Vector2 targetPos;

    protected override void Start()
    {
        base.Start();
        transform.parent.rotation = Quaternion.identity;
        behaviorVelocity = Vector2.right * behaviorScaler;
        behaviorVector = Vector2.right * behaviorScaler;

        canSwitchDirection = true;
        targetPos = anchorTransform.position;
    }

    public override void FixedUpdate()
    {
        rb.linearVelocity = behaviorVelocity;
        //knockbackVelocity = Vector2.zero;
        //        behaviorVelocity = new Vector2(behaviorVelocity.x * Mathf.Cos(theta) - behaviorVelocity.y * Mathf.Sin(theta),
        //Mathf.Sin(theta) * behaviorVelocity.x + Mathf.Cos(theta) * behaviorVelocity.y);
        //        theta += Random.Range(-1f, 1f);
        float distance = Vector2.Distance(anchorTransform.position, transform.position);
        behaviorVelocity += knockbackVelocity;
        if (distance > clampRadius)
        {
            //behaviorVelocity += knockbackVelocity * Mathf.Exp(-1 * (distance - clampRadius + 1));
            //Vector2 correction = anchorTransform.position - transform.position;
            //behaviorVelocity += correction.normalized * Mathf.Exp(distance - clampRadius + 1);
        } else
        {
            //behaviorVelocity += knockbackVelocity;
        }
        knockbackVelocity = Vector2.zero;
        behaviorVelocity = ExpDecay(behaviorVelocity, behaviorVector, decay, Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 1);

        //if (distance > clampRadius)
        //{
        //Vector2 correction = anchorTransform.position - transform.position;
        //behaviorVelocity += correction.normalized * Mathf.Exp(distance - clampRadius + 1);
        //    //behaviorVector = behaviorVector.normalized * Mathf.Exp(distance - clampRadius + 1);
        //    //canSwitchDirection = true;
        //    //continue;
        //}
    }

    //private void LateUpdate()
    //{
    //    float distance = Vector2.Distance(anchorTransform.position, transform.position);
    //    if (distance > clampRadius)
    //    {
    //        Vector2 center = anchorTransform.position;
    //        Vector2 offset = rb.position - center;
    //        Vector2 clampedOffset = Vector2.ClampMagnitude(offset, clampRadius);
    //        rb.MovePosition(center + clampedOffset);
    //    }
    //}

    public override void OnHit()
    {
        canSwitchDirection = true;
    }

    public override IEnumerator Behaviors()
    {
        canSwitchDirection = true;
        while (true)
        {
            yield return null;
            float distance = Vector2.Distance(anchorTransform.position, transform.position);

            if (distance > switchRadius)
            {
                if (!canSwitchDirection) continue;
                canSwitchDirection = false;

                behaviorVector = Vector2.zero;
                yield return new WaitForSeconds(changeRotateTime);
                float theta = Random.Range(0, 360);
                theta *= Mathf.Deg2Rad;
                targetPos = (Vector2)anchorTransform.position + RotateVector(Vector2.right, theta) * anchorOffset;
                //Vector2 targetPos = (Vector2)anchorTransform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                behaviorVector = targetPos - rb.position;
                behaviorVector = behaviorVector.normalized * behaviorScaler;
                //yield return new WaitForSeconds(changeRotateTime);
            }
            else
            {
                canSwitchDirection = true;
            }
            if(distance > clampRadius)
            {
                Vector2 correction = anchorTransform.position - transform.position;
                behaviorVector += correction.normalized * Mathf.Exp(distance - clampRadius + 1);
            }
        }
    }

    Vector2 RotateVector(Vector2 vectorToRotate, float theta)
    {
        Vector2 vector2Return = new Vector2(vectorToRotate.x * Mathf.Cos(theta) - vectorToRotate.y * Mathf.Sin(theta),
                Mathf.Sin(theta) * vectorToRotate.x + Mathf.Cos(theta) * vectorToRotate.y);
        return vector2Return;
    }

    void ChangeBehaviorVelocity()
    {
        behaviorVector = transform.up;
        //float theta = Random.Range(-20f, 20f);
        behaviorVector = new Vector2(behaviorVector.x * Mathf.Cos(theta) - behaviorVector.y * Mathf.Sin(theta),
        Mathf.Sin(theta) * behaviorVector.x + Mathf.Cos(theta) * behaviorVector.y);
        behaviorVector = behaviorVector.normalized * behaviorScaler;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + behaviorVelocity);
        Gizmos.DrawWireSphere(anchorTransform.position, switchRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(anchorTransform.position, anchorOffset);
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(targetPos, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(anchorTransform.position, clampRadius);
        Gizmos.DrawLine(anchorTransform.position, transform.position);
    }

    Vector3 ExpDecay(Vector3 a, Vector3 b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }
}
