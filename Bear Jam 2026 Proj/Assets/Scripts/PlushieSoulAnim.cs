using System.Collections;
using UnityEngine;

public class PlushieSoulAnim : SpawnAnim
{
    public float lingerDuration;
    protected override IEnumerator Expand()
    {
        yield return base.Expand();
        yield return new WaitForSeconds(lingerDuration);
        StartCoroutine(Shrink());      
    }
}
