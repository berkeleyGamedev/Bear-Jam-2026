using System.Collections;
using UnityEngine;

public class WeedBehavior : PinataBehavior
{
    Crosshair crosshair;
    protected override void Start()
    {
        base.Start();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
        crosshair.inversed = true;
    }

    public override void OnHit()
    {
        
    }

    public override IEnumerator Behaviors()
    {
        yield return null;
    }

    private void OnDestroy()
    {
        crosshair.inversed = false;
    }
}
