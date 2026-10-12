using System.Collections;
using UnityEngine;

public class PlushieBehavior : PinataBehavior
{
    public float waitTime;

    private SpawnAnim spawnAnim;
    private PinataSpawner spawner;

    protected override void Awake()
    {
        base.Awake();
        spawnAnim = transform.parent.GetComponent<SpawnAnim>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PinataSpawner>();
    }

    public override void OnHit()
    {
        StopAllCoroutines();
        Debug.Log("Change Layer");
        gameObject.layer = 2;
    }

    public override IEnumerator Behaviors()
    {
        yield return new WaitForSeconds(waitTime);
        ScoreTracker.instance.IncreaseCombo(4);
        spawnAnim.DeathAnim(0.2f);
        spawner.SpawnNewPinata();


    }
}
