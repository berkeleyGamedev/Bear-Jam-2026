using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PinataBulletSpawner : MonoBehaviour
{
    public GameObject holePrefab;
    public List<Sprite> holes;

    private void OnEnable()
    {
        PinataHit.onHit += SpawnHole;
    }

    private void OnDisable()
    {
        PinataHit.onHit -= SpawnHole;
    }

    void SpawnHole(Vector2 mousePos)
    {
        int index = Random.Range(0, holes.Count);
        GameObject bulletHole = Instantiate(holePrefab, mousePos, Quaternion.Euler(0,0,Random.Range(0, 360)), gameObject.transform);
        if(bulletHole.TryGetComponent<SpriteRenderer>(out var sr))
        {
            sr.sprite = holes[index];
        }
    }
}
